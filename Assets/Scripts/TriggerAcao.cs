﻿using UnityEngine;
using System.Collections;

public enum Acoes { OVNICAINDO }

public class TriggerAcao : MonoBehaviour
{

    public bool geraAcao;
    public Acoes acao;
    public bool jaFoiAtivado;

    // Use this for initialization
    void Start()
    {
        jaFoiAtivado = false;

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
                        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().DerrubarOvni();
                    }
                }

            }
        }

    }
}
