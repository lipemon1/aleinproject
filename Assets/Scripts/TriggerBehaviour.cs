using UnityEngine;
using System.Collections;

public class TriggerBehaviour : MonoBehaviour {

    public bool jaFoiAtivado = false;
    public string comentario;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void SetFoiAtivado(bool value)
    {
        jaFoiAtivado = value;
    }
    public bool GetFoiAtivado()
    {
        return jaFoiAtivado;
    }
}
