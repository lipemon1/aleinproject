using UnityEngine;
using System.Collections;

public class TriggersComentarios : MonoBehaviour {

    GameController gameController;

    public class BlocoTrigger
    {
        public string tagName;

        public BlocoTrigger(string _tagName)
        {
            tagName = _tagName;
        }
    }

    // Adicionar novos blocos com nome da tag aqui
    public BlocoTrigger T_entrouEmCasa = new BlocoTrigger("Triggers/Comentarios/EntrouEmCasa");
    public BlocoTrigger T_ChegandoEmCasa = new BlocoTrigger("Triggers/Comentarios/ChegandoEmCasa");
    public BlocoTrigger T_FicandoPesada = new BlocoTrigger("Triggers/Comentarios/ElaEstaFicandoPesada");

    // Use this for initialization
    void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {        
        // Adicionar outras chamadas da função aqui com o nome da variavel criada
        TriggerComentario_UsoUnico(T_entrouEmCasa, other);
        TriggerComentario_UsoUnico(T_ChegandoEmCasa, other);
        TriggerComentario_UsoUnico(T_FicandoPesada, other);
        
    }
    /// <summary>
    /// Quando entra em contato com a trigger, muda seu estado para que ja foi ativado e faz o comentário do personagem
    /// </summary>
    /// <param name="infoTrigger">Class BlocoTrigger que recebe as informações do trigger, deve ser instanciado com nome correto da tag</param>
    /// <param name="other">Objeto em que colidiu passado pelo TriggerEnter2D</param>
    void TriggerComentario_UsoUnico(BlocoTrigger infoTrigger, Collider2D other)
    {
        TriggerBehaviour triggerBehav = other.gameObject.GetComponent<TriggerBehaviour>();
        if (other.gameObject.tag == infoTrigger.tagName && triggerBehav.GetFoiAtivado() == false)
        {            
            if(triggerBehav.isComentario == true) { 
                Debug.LogWarning(triggerBehav.comentario);
                gameController.FazerComentario(triggerBehav.comentario, triggerBehav.precisaConfirmacao, triggerBehav.tempoProximaFala);
                triggerBehav.SetFoiAtivado(true);
            }
            else if(triggerBehav.geraDialogo == true)
            {
                triggerBehav.AtivarDialogo();
                triggerBehav.SetFoiAtivado(true);
            }
        }
    }
}
