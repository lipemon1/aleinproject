using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueBoxInfo : MonoBehaviour {

	public string characterName;
	public Sprite characterImg;
	private DialogueBoxManager dialogManager;


	// Use this for initialization
	void Start () {
		dialogManager = GameObject.FindGameObjectWithTag ("DialogManager").GetComponent<DialogueBoxManager> ();	
	}	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() // Teste para chamar o dialogo quando clicar nesse personagem
	{
		Debug.Log("Clicou em personagem");

        //dialogManager.CharacterSpeak(this);
        
        GetComponent<TextsArrays>().ResetLine();
	}

    public void FazerComentario()
    {

    }
}
