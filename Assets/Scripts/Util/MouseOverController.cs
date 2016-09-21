using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseOverController : MonoBehaviour
{

    public Text textDescription;


    // Use this for initialization
    void Start()
    {
        if (textDescription != null)
        {
            textDescription.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (textDescription == null && GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ShowDescriptionByMouse == true)
        {
            Debug.LogWarning("falta objeto Canvas Follow Mouse na cena");
            textDescription = GameObject.FindGameObjectWithTag("Mouse Over Text").GetComponent<Text>();
            textDescription.text = "";
        }
    }

    public void UpdateText(string newText)
    {
        textDescription.text = newText;
    }
}
