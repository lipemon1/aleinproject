using UnityEngine;
using System.Collections;

// script para todos os items do jogo

    
public enum ItemType {COPO, PIA, ESPOSA, FILHA, ESCADA};

public class Item : MonoBehaviour {

    private ItemType type;

    public string item;

    public Sprite spriteNeutral;

    public Sprite spriteHighlighted;

    public int maxSize;

    private GameController gameController;

    private Inventory inventory;

    public ItemType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    void Start()
<<<<<<< HEAD
    {        
=======
    {
>>>>>>> master
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    
    public void Use()
    {
 
    }

    public void Activate()
    {
        Debug.Log("Entrou em SetActiveItem de GameController");
        gameController.SetActiveItem(item.ToString());
    }
}