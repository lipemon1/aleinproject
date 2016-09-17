using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickableObject : MonoBehaviour {

    public string descricao;
    private bool clicado;
    public bool ativado = false;
    public bool pertoPlayer;
    public bool objetoInventario = false;

    public GameObject descricaoPanel;
    public Text DescricaoText;

    [Tooltip("Id do Objeto (Deve ser o mesmo id presente no objeto correspondente no script InventoryItens.cs)")]
    public int id =1;

    private Transform playerPosition;

    [Tooltip("Distancia atual do player")]
    private float distanciaPlayer;
    [Tooltip("Distancia minima do personagem para que mostre ou esconda a descrição")]
    public float distanciaMinima = 3.0f;

	// Use this for initialization
	void Start () {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (gameObject.tag != "clickable")
            gameObject.tag = "clickable";

        DescricaoText.text = descricao;
        if(descricaoPanel.gameObject.active == true)
        {
            descricaoPanel.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        CloseToPlayer();

        if(ativado == true)
        {
            ShowDescription();       
        }
        if(ativado == true && pertoPlayer == false)
        {
            HideDescription();
        }

    }

    void CloseToPlayer()
    {
        distanciaPlayer = Vector2.Distance(playerPosition.position, transform.position);
        if (distanciaPlayer < distanciaMinima)
        {
            pertoPlayer = true;
            //GetComponent<SpriteRenderer>().color = Color.red;
            GetComponent<SpriteOutline>().enabled = true;
        }
        else
        {
            pertoPlayer = false;
            //GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<SpriteOutline>().enabled = false;
        }
    }

    public void ShowDescription()
    {        
        descricaoPanel.SetActive(true);
        ativado = true;
    }
    public void HideDescription()
    {
        descricaoPanel.SetActive(false);
        ativado = false;

        if(clicado == true && objetoInventario == true)
        {
            
            Destroy(gameObject);
        }
    }
    
    public int GetId()
    {
        return id;
    }

    void OnMouseDown()
    {
        Debug.Log("Sprite Clicked");

        if (ativado == true) // se a descrição do objeto ja esta sendo mostrada e houve o clique no objeto novamente, a descrição é escondida
        {
            HideDescription();
        }

        else if (ativado == false) { // se a descricao esta escondida, ativa a variavel para mostrar a descricao
            ativado = true;            
        }
        
        clicado = true; // seta true em clicado para caso seja um objeto de inventario, ele va para la se ja foi clicado
    }
}
