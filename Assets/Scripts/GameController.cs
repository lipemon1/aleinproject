using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public bool onMobile;

    public GameObject mobileHud;

    void Awake()
    {
        if (gameObject.tag != "GameController")
            gameObject.tag = "GameController";
    }

	// Use this for initialization
	void Start () {
        if (onMobile)
            mobileHud.SetActive(true);
        else
            mobileHud.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool GetOnMobile()
    {
        return onMobile;
    }

}
