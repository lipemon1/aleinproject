using UnityEngine;
using System.Collections;

public class MyText : MonoBehaviour {

    public string myDescription;
    private MouseOverController textController;

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

    void OnMouseEnter()
    {
        textController.UpdateText(myDescription);
    }

    void OnMouseExit()
    {
        textController.UpdateText("");
    }
}
