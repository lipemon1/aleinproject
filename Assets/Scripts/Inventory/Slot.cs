using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    private GameController gameController;
    
    public Stack<Item> items;
    public Sprite slotEmpty;
    public Sprite slotHighlight;
    //public MouseOverController mouseOverController;
    //public InventoryTextController inventoryTextController;
    public bool isEmpty;
    public bool IsEmpty
    {
        get { return Items.Count == 0; }
    }

    public bool IsAvailable
    {
        get {return CurrentItem.maxSize > Items.Count; }
    }

    public Item CurrentItem
    {
        get { return Items.Peek(); }
    }

    public Stack<Item> Items
    {
        get
        {
            return items;
        }

        set
        {
            items = value;
        }
    }

    // Use this for initialization
    void Start () {
        Items = new Stack<Item>();

        RectTransform slotRect = GetComponent<RectTransform>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //if (mouseOverController == null)
        //{
        //    mouseOverController = GameObject.FindGameObjectWithTag("Text Controller").GetComponent<MouseOverController>();
        //}
        //if (inventoryTextController == null)
        //{
        //    inventoryTextController = GameObject.FindGameObjectWithTag("Inventory Text Controller").GetComponent<InventoryTextController>();
        //}
    }

    // Update is called once per frame
    void Update () {
        // Debug.Log("Mouse Over Controller: " + mouseOverController.name);
        //if (mouseOverController == null)
        //{
        //    mouseOverController = GameObject.FindGameObjectWithTag("Text Controller").GetComponent<MouseOverController>();
        //}
        //if (inventoryTextController == null)
        //{
        //    inventoryTextController = GameObject.FindGameObjectWithTag("Inventory Text Controller").GetComponent<InventoryTextController>();
        //}
        if(IsEmpty == true)
        {
            isEmpty = true;
        }
        else
        {
            isEmpty = false;
        }
    }

    public void MouseEnter()
    {
        gameController.SetMouseOverUI(true);
        // Debug.Log("Inventory Text Controller: " + inventoryTextController.name);
        if(!IsEmpty)
        {
            // inventoryTextController.UpdateText(Items.Peek().type.ToString());

            //inventoryTextController.UpdateText(Items.Peek().item.ToString());
            //Debug.LogWarning("Hover Enter em item: " + Items.Peek().item.ToString());
        }
    }

    public void MouseExit()
    {
        gameController.SetMouseOverUI(false);
        //Debug.Log("Inventory Text Controller: " );

    }

    public void AddItem(Item item)
    {
        Items.Push(item);

        ChangeSprite(item.spriteNeutral, item.spriteHighlighted);
    }

    public void AddItems(Stack<Item> items)
    {
        this.Items = new Stack<Item>(items);

        ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);
    }

    private void ChangeSprite(Sprite neutral, Sprite highlight)
    {
        GetComponent<Image>().sprite = neutral;

        SpriteState st = new SpriteState();

        st.highlightedSprite = highlight;

        st.pressedSprite = neutral;

        GetComponent<Button>().spriteState = st;
    }

    private void UseItem()
    {
        if (!IsEmpty)
        {
            // Pop() remove item da stack
            Items.Peek().Use();

            if (IsEmpty)
            {
             
                ChangeSprite(slotEmpty, slotHighlight);
                Inventory.EmptySlot++;
            }
        }
    }

    private void SelectItem()
    {
        //Items.Peek().Activate();

        if (!IsEmpty)
        {
            // Pop() remove item da stack
            Items.Peek().Activate();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //UseItem();
            SelectItem();
        }
    }

    public void ClearSlot()
    {
        items.Clear();
        Debug.LogWarning("Clear Slot");
        ChangeSprite(slotEmpty, slotHighlight);
    }
}
