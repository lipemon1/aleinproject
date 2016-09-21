using UnityEngine;
using System.Collections;

public enum Acoes { OVNICAINDO, SEAPROXIMOUDOOVNI }

public class TriggerAcao : MonoBehaviour
{

    public bool geraAcao;
    public Acoes acao;
    public bool jaFoiAtivado;
    GameController gm;
    public SfxManager sfx;
    public BackgroundMusicManager bgm;

    // Use this for initialization
    void Start()
    {
        jaFoiAtivado = false;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (sfx == null)
        {
            sfx = GameObject.FindWithTag("SFX").GetComponent<SfxManager>();
        }

        if (bgm == null)
        {
            bgm = GameObject.FindWithTag("BGM").GetComponent<BackgroundMusicManager>();
        }
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
                        sfx.PlaySfx(10, false);
                        bgm.MeteoroCaiu();
                    }
                }

            }
        }
        else if (acao == Acoes.SEAPROXIMOUDOOVNI)
        {
            if (jaFoiAtivado == false)
            {
                jaFoiAtivado = true;
                bgm.StopAll();
                gm.AproximouDoOvni();
                Debug.LogWarning("AÇÃO DE QUANDO CHEGA PERTO DO OVNI CAI (CLIQUE DUAS VEZES NESSE AVISO PARA SABER ONDE ESTOU)");
            }
        }

    }
}
