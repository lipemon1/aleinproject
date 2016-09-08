using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class ItensPegaveis
    {
        public string Name;
        public int id;
        public int quantidade;
        public Sprite objectSprite;
        public Image slotPosition;

        public ItensPegaveis(int _id, string _nome)
        {
            id = _id;
            Name = _nome;
            quantidade = 0;
            objectSprite = null;
        }
    }

    int index = 0;
    public ItensPegaveis[] listaItens;
    public Image[] slotsInventario;

    // Use this for initialization
    void Start()
    {
        listaItens = new ItensPegaveis[5];
        CriarItens();

    }

    void CriarItens()
    {
        PopularItens(InventoryItens.item1Id, InventoryItens.item1Nome);
        PopularItens(InventoryItens.item1Id, InventoryItens.item1Nome);
        PopularItens(InventoryItens.item1Id, InventoryItens.item1Nome);
        PopularItens(InventoryItens.item1Id, InventoryItens.item1Nome);
        PopularItens(InventoryItens.item1Id, InventoryItens.item1Nome);
        
        for (int i = 0; i < listaItens.Length; i++)
        {
            listaItens[i].slotPosition = slotsInventario[i];
            Debug.Log("" + i);
        }
    }

    // Cria os slots dos ingredientes na lista de ingredientes do player
    void PopularItens(int id, string nome)
    {
        listaItens[index] = new ItensPegaveis(id, nome);
        index++;
    }

    public void AddToInventory(GameObject objeto)
    {
        for (int i = 0; i < listaItens.Length; i++)
        {
            if (objeto.GetComponent<ClickableObject>().GetId() == listaItens[i].id)
            {
                listaItens[i].quantidade++;
                Debug.LogWarning("" + objeto.name + " Adicionado ao Inventário!");
                listaItens[i].objectSprite = objeto.GetComponent<SpriteRenderer>().sprite;

                if (listaItens[i].quantidade > 0)
                {
                    slotsInventario[i].sprite = listaItens[i].objectSprite;
                }

                return;

            }
        }
    }

}
