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
        //if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ShowDescriptionByMouse == false && TemComentarioPlayer == false)
        //{
        //    Destroy(this);
        //}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        Debug.Log("Clicou");
        if(TemComentarioPlayer == true)
        {
            if(Comentarios.Length == 0 )
            {
                Debug.LogWarning("Falta incluir comentário do objeto");
            }
            else if(Comentarios.Length == 1) { 
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().FazerComentario(Comentarios[0]);
            }
            else if( Comentarios.Length > 1)
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().
                    FazerComentario(Comentarios[Random.Range(0,Comentarios.Length)]);
            }
        }
    }

    void OnMouseEnter()
    {
        if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ShowDescriptionByMouse == true)
        textController.UpdateText(myDescription);
    }

    void OnMouseExit()
    {
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ShowDescriptionByMouse == true)
            textController.UpdateText("");
    }
}
