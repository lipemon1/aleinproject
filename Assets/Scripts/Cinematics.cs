using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum Cenas { EncontrouCristal, Escapando}

public class Cinematics : MonoBehaviour {

    public string NomeCena;
        public float tempoAnimacao;
    private float tempoCorrent;
    public Animator cinematicAnimator;
    public Cenas cena;

	// Use this for initialization
	void Start () {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        Cursor.visible = false;
        tempoCorrent = 0f;

        if(cena == Cenas.EncontrouCristal)
        {
            cinematicAnimator.SetTrigger("EncontraCristal");
        }
        else if(cena == Cenas.Escapando)
        {
            cinematicAnimator.SetTrigger("Escapando");
        }
    }
	
	// Update is called once per frame
	void Update () {
        tempoCorrent += Time.deltaTime;
        if(tempoCorrent >= tempoAnimacao)
        {
            IrParaCena();
        }
	}
    public void IrParaCena()
    {
        SceneManager.LoadScene(NomeCena);
    }
}
