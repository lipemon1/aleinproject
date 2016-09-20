using UnityEngine;
using System.Collections;

public class PortaBehaviour : MonoBehaviour
{

    public bool podeSerQuebrada;
    public float vida;
    public float vidaInicial = 100f;
    public float _dano = 10f;
    public GameObject parteQueIraSumir;
    public GameObject bordaPorta;

    public GameObject objetoComTagInteragivel;

    public bool portaMorreu = false;

    // Use this for initialization
    void Start()
    {
        vida = vidaInicial;
        objetoComTagInteragivel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (vida <= 0)
        {
            bordaPorta.GetComponent<OutlineWhenCloseToPlayer>().enabled = true;
            portaMorreu = true;
            Debug.LogWarning("Porta Morreu");
            vida = 0;
            parteQueIraSumir.gameObject.SetActive(false);
            objetoComTagInteragivel.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
        }

        if (portaMorreu == true)
        {
            if(objetoComTagInteragivel.active == false)
            objetoComTagInteragivel.SetActive(true);
        }
    }

    public void Hit()
    {
        AplicarDano(_dano);
    }

    public void AplicarDano(float dano)
    {
        if (vida > 0)
        {
            vida -= dano;
        }
    }
}
