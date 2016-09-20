using UnityEngine;
using System.Collections;

public class ComputadorAbrirPorta : MonoBehaviour
{

    public bool foiAtivado = false;
    private bool foiAtivadoAlgumaVez = false;
    public Animator PortaEntrada;
    public Animator PortaSaida;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (foiAtivadoAlgumaVez)
        {
            if (foiAtivado)
            {
                AbrirPortas();
            }
            else
            {
                FecharPortas();
            }
        }
    }

    void OnMouseDown()
    {
        Debug.LogWarning("Ativar Computador");

        //foiAtivado = true;
        foiAtivado = !foiAtivado;
        if (!foiAtivadoAlgumaVez)
        {
            foiAtivadoAlgumaVez = true;
        }

    }

    void AbrirPortas()
    {
        PortaEntrada.SetTrigger("Ativar");
        PortaSaida.SetTrigger("Ativar");
    }
    void FecharPortas()
    {
        PortaEntrada.SetTrigger("Desativar");
        PortaSaida.SetTrigger("Desativar");
    }
}
