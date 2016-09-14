using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public bool onMobile;
    public bool onDialogue;

    public GameObject mobileHud;

    void Awake()
    {
        if (gameObject.tag != "GameController")
            gameObject.tag = "GameController";
    }

	// Use this for initialization
	void Start () {
        if (onMobile)
        {
            ShowMobileHud();
        }
        else
            HideMobileHud();
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
