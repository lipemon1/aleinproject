using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    [Tooltip("Indica se esta rodando no Android ")]
    public bool onMobile;
    [Tooltip("Indica se o jogador está em meio à um dialogo")]
    public bool onDialogue;
    [Tooltip("Se ativado, apresenta o texto com a descrição do objeto proximo ao mouse ao passa-lo sob o objeto(objeto precisa ter a classe myText)\nÉ preciso ter o objeto Canvas Follow Mouse na cena")]
    public bool ShowDescriptionByMouse = true;

    public GameObject mobileHud;

    void Awake()
    {

//#if UNITY_ANDROID
//        onMobile = true;
//        ShowDescriptionByMouse = false;
//#endif

        if (gameObject.tag != "GameController")
            gameObject.tag = "GameController";

        if(!onMobile && ShowDescriptionByMouse)
        {
            if (Camera.main.GetComponent<MouseOverController>() == false)
            {
                Camera.main.gameObject.AddComponent<MouseOverController>();
            }
        }
        else if(onMobile)
        {
            ShowDescriptionByMouse = false;
        }
    }

	// Use this for initialization
	void Start () {
        if (onMobile)
        {
            ShowMobileHud();
        }
        else { 
            HideMobileHud();
            
        }


    }
	
	// Update is called once per frame
	void Update () {
        if (onMobile) {
            if (onDialogue)
                HideMobileHud();
            else
                ShowMobileHud();
        }
    }

    public void SetOnDialogue(bool value)
    {
        onDialogue = value;
    }

    public bool GetOnMobile()
    {
        return onMobile;
    }

    void HideMobileHud()
    {
        mobileHud.GetComponent<CanvasGroup>().alpha = 0f; //this makes everything transparent
        mobileHud.GetComponent<CanvasGroup>().blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    void ShowMobileHud()
    {
        mobileHud.GetComponent<CanvasGroup>().alpha = 1f; //this makes everything transparent
        mobileHud.GetComponent<CanvasGroup>().blocksRaycasts = true; //this prevents the UI element to receive input events
    }

}
