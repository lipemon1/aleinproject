using UnityEngine;
using System.Collections;

public class DoorBehaviour : MonoBehaviour {

    public float distanciaMinimaAtivar = 3.0f;
    public float distancePlayer;
    public bool playerPerto;
    GameObject Player;

    public string IrParaCena;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        PlayerClose();
	    
	}
    void PlayerClose()
    {
        float distancePlayer = Vector2.Distance(Player.transform.position, transform.position);
        if (distancePlayer < distanciaMinimaAtivar)
        {
            playerPerto = true;

        }
        else
        {
            playerPerto = false;
        }
    }

    void OnMouseDown()
    {
        Debug.LogWarning("MouseDownNaPorta");
        if(playerPerto)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ChangeToScene(IrParaCena);
        }
    }
}
