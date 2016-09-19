using UnityEngine;
using System.Collections;

public enum Acoes { OVNICAINDO, SEAPROXIMOUDOOVNI }

public class TriggerAcao : MonoBehaviour
{

    public bool geraAcao;
    public Acoes acao;
    public bool jaFoiAtivado;
    GameController gm;

    // Use this for initialization
    void Start()
    {
        jaFoiAtivado = false;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogWarning("Trigger enter 2d");
        if (acao == Acoes.OVNICAINDO)
        {
            GameController gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            if (gm.ColocouFilhaNaCama == true)
            {
                if (gm.ovniCaiu == false)
                {
                    if (jaFoiAtivado == false)
                    {
                        jaFoiAtivado = true;
                        gm.DerrubarOvni();
                    }
                }

            }
        }
        else if (acao == Acoes.SEAPROXIMOUDOOVNI)
        {
            if (jaFoiAtivado == false)
            {
                jaFoiAtivado = true;
                
                gm.AproximouDoOvni();
                Debug.LogWarning("AÇÃO DE QUANDO CHEGA PERTO DO OVNI CAI (CLIQUE DUAS VEZES NESSE AVISO PARA SABER ONDE ESTOU)");
            }
        }

    }
}
