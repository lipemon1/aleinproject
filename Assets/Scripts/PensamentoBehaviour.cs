using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class PensamentoBehaviour : MonoBehaviour
{

    public int test = 0;
    public bool estaSendoClicado;
    

    public bool EstaSendoClicado
    {
        get
        {
            return estaSendoClicado;
        }

        set
        {
            estaSendoClicado = value;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void _estaSendoClicado()
    {
        Debug.LogWarning("Esta Sendo Clicado");
        EstaSendoClicado = true;
    }
    public void _SaiuDoClick()
    {
        EstaSendoClicado = false;
        Debug.LogWarning("Saiu do Click");
    }

}
