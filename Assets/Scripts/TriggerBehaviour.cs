using UnityEngine;
using System.Collections;

public enum Personagem { VIKTOR, ESPOSA, FILHA, ERIC }


public class TriggerBehaviour : MonoBehaviour
{

    public bool jaFoiAtivado = false;

    public bool isComentario;
    public string comentario;
    public bool geraDialogo;
    public bool precisaConfirmacao = true;
    public float tempoProximaFala;    

    [System.Serializable]
    public class Fala
    {
        public Personagem personagem;
        public string Texto;
    }

    public Fala[] falas;


    // Use this for initialization
    void Start()
    {
        
    }

    public void AtivarDialogo()
    {
        if (geraDialogo)
        {
            DialogueBoxManager dialogManager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogueBoxManager>();

            if (falas.Length > 0)
            {
                dialogManager.SetQuantidadeFalas(falas.Length);
                for (int i = 0; i < falas.Length; i++)
                {
                    if (falas[i].personagem == Personagem.VIKTOR)
                        dialogManager.AdicionarFala(dialogManager.Viktor.name, falas[i].Texto);
                    else if (falas[i].personagem == Personagem.ESPOSA)
                        dialogManager.AdicionarFala(dialogManager.Esposa.name, falas[i].Texto);
                    else if (falas[i].personagem == Personagem.FILHA)
                        dialogManager.AdicionarFala(dialogManager.Filha.name, falas[i].Texto);
                    else if (falas[i].personagem == Personagem.ERIC)
                        dialogManager.AdicionarFala(dialogManager.Eric.name, falas[i].Texto);
                }
                dialogManager.RealizarConversa();
            }
            else
            {
                Debug.LogWarning("Trigger Setado como Dialogo mas nao possui nenhuma frase");
            }
        }
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
