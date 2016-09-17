using UnityEngine;
using System.Collections;

// script para todos os items do jogo

public enum ItemType {COPO, PIA};

public class Item : MonoBehaviour {

    public ItemType type;

    public string item;

    public Sprite spriteNeutral;

    public Sprite spriteHighlighted;

    public int maxSize;

    private GameController gameController;

    private Inventory inventory;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    // aqui está o que acontece quando clica pra usar cada item do inventário
    public void Use()
    {
        Debug.Log("Usei " + item);

        switch (type)
        {
            //case ItemType.ARAME:
            //    inventory.ClearInventorySlot();
            //break;

        }
    }

    public void Activate()
    {
        Debug.Log("Entrou em SetActiveItem de GameController");
        gameController.SetActiveItem(item.ToString());
    }
}