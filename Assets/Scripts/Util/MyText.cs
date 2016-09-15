using UnityEngine;
using System.Collections;

public class MyText : MonoBehaviour {

    public string myDescription;
    private MouseOverController textController;

    public bool TemComentarioPlayer = false;

    public string[] Comentarios = new string[]
    {
        "Não preciso fazer nada la em cima"
    };

	// Use this for initialization
	void Start () {
        //textController = GameObject.FindGameObjectWithTag("Text Controller").GetComponent<MouseOverController>();
        textController = Camera.main.GetComponent<MouseOverController>();
        if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ShowDescriptionByMouse == false)
        {
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if(TemComentarioPlayer == true)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().FazerComentario(Comentarios[0]);
        }
    }

    void OnMouseEnter()
    {
        textController.UpdateText(myDescription);
    }

    void OnMouseExit()
    {
        textController.UpdateText("");
    }
}
