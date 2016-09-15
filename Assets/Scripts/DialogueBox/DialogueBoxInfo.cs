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
		Debug.Log("Clicado");

        dialogManager.dialogObjects.DialogText.text = "";
        dialogManager.actualText = "";
        dialogManager.startTalk = true;
		dialogManager.SetInTalk (true); // fala para o dialog manager que está em um dialogo
        dialogManager.SetDialogueInfo(this); // passa a referencia desse script
        GetComponent<TextsArrays>().ResetLine();
	}
}
