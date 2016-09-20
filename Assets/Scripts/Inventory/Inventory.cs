using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour {

    private RectTransform inventoryRect;

    private float inventoryWidth, inventoryHeight;
    public int slots, rows;
    public float slotPaddingLeft, slotPaddingTop, slotSize;
    public GameObject slotPrefab;
    private List<GameObject> allSlots;
    public static int emptySlot;
    public int quantidade;
    public static Slot from, to;
    public GameObject iconPrefab;
    public Canvas canvas;

    public EventSystem eventSystem;

    private float hoverYOffset;
    private float hoverXOffset;

    public static GameObject hoverObject;

    private GameController gameController;

    public static int EmptySlot
    {
        get
        {
            return emptySlot;
        }

        set
        {
            emptySlot = value;
        }
    }

    // Use this for initialization
    void Start () {
        CreateLayout();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
    }
	
	// Update is called once per frame
	void Update () {
        quantidade = emptySlot;
        if (eventSystem  == null)
        {
            eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(!eventSystem.IsPointerOverGameObject(-1) && from != null)
            {
                if(from != null)
                { 
                    from.GetComponent<Image>().color = Color.white;
                    //from.ClearSlot();
                    Destroy(GameObject.Find("Hover"));
                    to = null;
                    from = null;
                    hoverObject = null;
                }
            }
            //se clicou na cena
            //else
            //{
            //    if (from != null)
            //    {
            //        ClearInventorySlot();
            //    }
            //}

        }

	    if(hoverObject != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);
            position.Set(position.x + hoverXOffset, position.y + hoverYOffset);
            hoverObject.transform.position = canvas.transform.TransformPoint(position);
        }
	}

    private void CreateLayout()
    {
        allSlots = new List<GameObject>();

        hoverXOffset = slotSize * 0.01f;
        hoverYOffset = slotSize * 0.01f;

        EmptySlot = slots;

        inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;

        inventoryHeight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;

        inventoryRect = GetComponent<RectTransform>();

        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);

        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHeight);

        int columns = slots / rows;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);

                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                newSlot.name = "Slot";

                newSlot.transform.SetParent(this.transform.parent);

                //Debug.Log(newSlot.transform.parent.name);

                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), -slotPaddingTop * (y + 1) - (slotSize * y));

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

                allSlots.Add(newSlot);
            }
        }
    }

    public bool AddItem(Item item)
    {
        // se item nao for stackable
        if (item.maxSize == 1)
        {
            PlaceEmpty(item);
            return true;
        }
        // se item for stackable
        else
        {
            // se houver espaco no slot atual
            foreach (GameObject slot in allSlots)
            {
                Slot temp = GetComponent<Slot>();

                if(!temp.IsEmpty)
                {
                    if(temp.CurrentItem.Type == item.Type && temp.IsAvailable)
                    {
                        temp.AddItem(item);
                        return true;
                    }
                }
            }
            // se nao houver espaco no slot atual e houver slot livre
            if(EmptySlot > 0)
            {
                PlaceEmpty(item);
            }
        }

        return false;
    }

    private bool PlaceEmpty(Item item)
    {
        if(EmptySlot > 0)
        {
            foreach(GameObject slot in allSlots)
            {
                Slot temp = slot.GetComponent<Slot>();

                if(temp.IsEmpty)
                {
                    temp.AddItem(item);
                    EmptySlot--;
                    return true;
                }
            }
        }

        return false;
    }

    public void MoveItem(GameObject clicked)
    {
        if(from == null)
        {
            if(!clicked.GetComponent<Slot>().IsEmpty)
            {
                from = clicked.GetComponent<Slot>();
                //from.GetComponent<Image>().color = Color.gray;

                hoverObject = (GameObject)Instantiate(iconPrefab);
                hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
                hoverObject.name = "Hover";

                RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
                RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

                hoverObject.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas/Inventario").transform, true);
                hoverObject.transform.localScale = from.gameObject.transform.transform.localScale;  
            }
        }
        else if (to == null)
        {
            to = clicked.GetComponent<Slot>();
            Destroy(GameObject.Find("Hover"));

            if (to != null && from != null)
            {
                Stack<Item> tempTo = new Stack<Item>(to.Items);
                to.AddItems(from.Items);

                if(tempTo.Count == 0)
                {
                    from.ClearSlot();
                }
                else
                {
                    from.AddItems(tempTo);
                }

                from.GetComponent<Image>().color = Color.white;
                to = null;
                from = null;
                hoverObject = null;
            }
        }
    }

    public void ClearInventorySlot()
    {
        Debug.Log("Entrei no ClearInventorySlot");

        from.GetComponent<Image>().color = Color.white;
        from.ClearSlot();
        Destroy(GameObject.Find("Hover"));
        to = null;
        from = null;
        hoverObject = null;
        gameController.SetActiveItem("");
        EmptySlot++;
    }
}
