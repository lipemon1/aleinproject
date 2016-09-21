using UnityEngine;
using System.Collections;

public class InteractionsController : MonoBehaviour
{
    private GameController gameController;
    private int defaultTimer = 120;

    #region Objetos
    public GameObject copoCheioPrefab;
    public GameObject filhaPrefab;
    public GameObject filhaNaCama;
    public GameObject filhaDpsQueOvnicaiu;
    public GameObject esposaDepoisDoOvni;
    public GameObject dutoFinal;
    
    #endregion

    public Inventory inventory;

    private string nextScene;
    public DialogueBoxManager dialogManager;
    PensamentosManager pensamentoM;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        pensamentoM = gameController.gameObject.GetComponent<PensamentosManager>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        dialogManager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogueBoxManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ModeloParaCase()
    {
        string name = "";

        switch (name)
        {
            case "":

                break;
        }
    }

    private void ModelosDeInteracao()
    {
        // Debug.Log("Cheguei no " + object_name + " e interagi com esse caralho!");
        // UpdateInteractionText("Cheguei no " + object_name + " e interagi com esse caralho!", defaultTimer);
    }


    public void ExecuteInteraction(GameObject clickedObject, string object_name)
    {
        Item itemScript = clickedObject.GetComponent<Item>();

        // Debug.Log("Entrou em ExecuteInteraction");
        switch (object_name)
        {

            #region Fora de Casa
            case "Porta Entrada":

                gameController.ChangeToScene("Cena2-DentroDeCasa");
                break;
            #endregion

            #region Dentro da Casa
            #region Cozinha
            case "Copo":
                // se o arame ja caiu e se ja vasculhou o lixo
                //if (gameController.GetWireFallen() && gameController.GetSearchedTrash())
                //{
                inventory.AddItem(clickedObject.GetComponent<Item>());
                Destroy(clickedObject.gameObject);
                gameController.TemCopo = true;

                //UpdateInteractionText("Peguei um pedaço de arame!", defaultTimer);
                Debug.LogWarning("PEGOU O " + object_name.ToString().ToUpper());

                //}
                break;
            case "Pia":
                if (gameController.GetSceneName() == "Cena2-DentroDeCasa")
                {
                    if (gameController.GetActiveItem() == "Copo")
                    {
                        dialogManager.SetQuantidadeFalas(1);
                        dialogManager.AdicionarFala(dialogManager.Viktor.name, "Alguma hora preciso arrumar essa torneira, está horrivel.");
                        dialogManager.RealizarConversa();

                        inventory.ClearInventorySlot();
                        inventory.AddItem(copoCheioPrefab.GetComponent<Item>());
                        gameController.TemCopo = false;
                        pensamentoM.MostrarPensamento("Entregar água para Rose");
                        Debug.LogWarning("Encher Copo!!!!!!!!!!!");
                    }
                    else if (gameController.MulherPediuAgua == true)
                    {
                        gameController.FazerComentario(clickedObject.GetComponent<MyText>().GetOneComentary());
                    }
                    else
                    {
                        gameController.FazerComentario("Pia está limpa");
                    }
                }
                else if (gameController.GetSceneName() == "Cena2.1-DentroDeCasa")
                {
                    gameController.FazerComentario(clickedObject.GetComponent<MyText>().GetOneComentary());
                }
                    break;
            #endregion
            #region Sala
            case "Escada":
                Debug.LogWarning("ESCADASSSS");
                if (gameController.GetSceneName() == "Cena2-DentroDeCasa")
                {
                    if (gameController.DevePegarFilha == true && gameController.EstaComFilha == true)
                    {
                        Debug.LogWarning("Escadas");
                        gameController.ChangeToScene("Cena3-Upstairs");
                        gameController.SubiuEscadas = true;
                    }
                    else
                    {
                        gameController.FazerComentario("Não preciso fazer nada lá em cima agora...");
                    }
                }
                else if (gameController.GetSceneName() == "Cena3.1-Upstairs")
                {
                    Debug.LogWarning("Cliquei na escada dps do ovni");
                    gameController.ChangeToScene("Cena2.1-DentroDeCasa");
                }
                break;
            case "Lanterna":
                if (gameController.GetSceneName() == "Cena2.1-DentroDeCasa")
                {
                    if (gameController.PegouLanterna == false)
                    {
                        inventory.AddItem(clickedObject.GetComponent<Item>());
                        Destroy(clickedObject);

                        pensamentoM.MostrarPensamento("Sair para investigar");
                        gameController.PegouLanterna = true;
                    }
                }
                else
                {
                    gameController.FazerComentario(clickedObject.GetComponent<MyText>().GetOneComentary());
                }
                break;
            case "Porta Saida":
                if (gameController.GetSceneName() == "Cena2.1-DentroDeCasa")
                {
                    if (gameController.PegouLanterna == true)
                    {
                        gameController.ChangeToScene("Cena1.1-ChegadaEmCasa");
                    }
                    else
                    {
                        gameController.FazerComentario("La fora está muito escuro. Preciso de uma lanterna...");
                    }
                }
                else if(gameController.GetSceneName() == "Cena2-DentroDeCasa")
                {
                    gameController.FazerComentario(clickedObject.GetComponent<MyText>().GetOneComentary());
                }
                break;
            #endregion
            #region Personagens
            case "Esposa":
                //   Fazer a mulher falar
                if (gameController.MulherPediuAgua == true && gameController.JaEntregouAgua == false &&
                    gameController.GetActiveItem() == "Copo Cheio")
                {
                    dialogManager.SetQuantidadeFalas(3);
                    dialogManager.AdicionarFala(dialogManager.Viktor.name, "Aqui está querida.");
                    dialogManager.AdicionarFala(dialogManager.Esposa.name, "Obrigado amor. Agora leve a Emily para o quarto por favor.");
                    dialogManager.AdicionarFala(dialogManager.Viktor.name, "Vamos para a cama sua bagunceira.");

                    dialogManager.RealizarConversa();

                    gameController.JaEntregouAgua = true;
                    inventory.ClearInventorySlot();
                }

                else if (gameController.MulherPediuAgua == true && gameController.JaEntregouAgua == false)
                {
                    gameController.FazerComentario(clickedObject.GetComponent<MyText>().GetOneComentary());

                }
                else if (gameController.MulherPediuAgua == false && gameController.JaEntregouAgua == false)
                {
                    gameController.FazerComentario("O amor da minha vida");
                }

                break;

            case "Filha":
                if (gameController.DevePegarFilha == true)
                {
                    if (gameController.EstaComFilha == false)
                    {

                        gameController.EstaComFilha = true;
                        inventory.AddItem(clickedObject.GetComponent<Item>());
                        Destroy(clickedObject);
                    }
                }
                else
                {
                    gameController.FazerComentario(clickedObject.GetComponent<MyText>().GetOneComentary());
                }


                break;
            #endregion
            #endregion

            #region UpStairs
            #region Corredor
            case "Porta Quarto Filha":
                //if (gameController.EstaComFilha == true && gameController.ColocouFilhaNaCama == false)
                //{
                //    gameController.ChangeToScene("Cena4-QuartoFilha");

                //}
                //if (gameController.ColocouFilhaNaCama == true && gameController.OvniCaiu == true)
                //{
                //    gameController.ChangeToScene("Cena3.1-Upstairs");
                //}
                if(gameController.GetSceneName() == "Cena4-QuartoFilha")
                {
                    if(gameController.ColocouFilhaNaCama == true && gameController.OvniCaiu == true)
                    {
                        gameController.ChangeToScene("Cena3.1-Upstairs");
                    }
                }
                else if(gameController.GetSceneName() == "Cena3-Upstairs") { 
                    gameController.ChangeToScene("Cena4-QuartoFilha");
                }



                break;
            #endregion
            #region Quarto Filha
            case "Cama Filha":
                Debug.LogWarning("Cliquei na cama da filha");
                if (gameController.EstaComFilha == true && gameController.ColocouFilhaNaCama == false)
                {
                    if(gameController.activeItem == "Filha")
                    {
                        inventory.ClearInventorySlot();
                        pensamentoM.EsconderPensamentos();
                        filhaNaCama.SetActive(true);
                        gameController.EstaComFilha = false;
                        dialogManager.SetQuantidadeFalas(11);
                        dialogManager.AdicionarFala(dialogManager.Viktor.name, "Como foi hoje na escola filha?");

                        dialogManager.AdicionarFala(dialogManager.Filha.name, "Foi legal, hoje aprendemos um pouco sobre flores.");
                        dialogManager.AdicionarFala(dialogManager.Filha.name, "O dia só começou a ficar esquisito quando estava voltando pra casa.");
                        dialogManager.AdicionarFala(dialogManager.Filha.name, "Vi várias luzes no céu, elas estavam girando muito rápido, depois de um tempo sumiram.");
                        dialogManager.AdicionarFala(dialogManager.Filha.name, "Fiquei com medo e vim correndo para casa. Contei à mamãe mas ela não acreditou em mim...");
                        
                        dialogManager.AdicionarFala(dialogManager.Viktor.name, "Não se preocupe querida, deviam ser apenas aviões ou algo parecido.");
                        dialogManager.AdicionarFala(dialogManager.Viktor.name, "Agora deite-se e durma bem. Amanhã precisamos acordar cedo para buscar algumas ferramentas na casa do seu tio");


                        dialogManager.AdicionarFala(dialogManager.Filha.name, "Papai, estou com medo de ficar sozinha. E se as luzes voltarem a aparecer?");
                        dialogManager.AdicionarFala(dialogManager.Filha.name, "E se elas me pegarem? Estou com um mau pressentimento!");

                        dialogManager.AdicionarFala(dialogManager.Viktor.name, "Não se preocupe Emily.");
                        dialogManager.AdicionarFala(dialogManager.Viktor.name, "Papai vai deixar a porta aberta para que se qualquer coisa acontecer você avisar a mim e sua mãe.");
                        dialogManager.RealizarConversa();
                    }
                }
                break;
            #endregion
            #endregion

            #region Cena Quarentena:
            case "Porta Quarentena" :
                gameController.ChangeToScene("Cena6-Facility");
                break;
            #endregion
            #region cenaFacility
            case "Porta Escapada":
                Debug.LogWarning("Porta escapada clicada");
                gameController.ChangeToScene("Cena7-SalaDuto");
                break;
            #endregion
            #region Sala Duto:
            case "Pedaço de Pau":
                inventory.AddItem(clickedObject.GetComponent<Item>());
                Destroy(clickedObject.gameObject);
                break;

            case "Duto Escapada":
                Debug.LogWarning("Duto Escapada");
                if (gameController.JaQuebrouDutoFinal == true)
                {
                    gameController.ChangeToScene("EscapandoFacility");
                }
                //if (gameController.activeItem == "Pedaço de Pau")
                //{
                //    Debug.LogWarning("Meti o Pau");
                //    inventory.ClearInventorySlot();
                //    dutoFinal.GetComponent<PortaBehaviour>().AplicarDano(100);
                //    gameController.JaQuebrouDutoFinal = true;
                    
                //}

                
                Debug.LogWarning("duto escapada");
                
                break;
            #endregion

            default:
                UpdateInteractionText("Item não definido (olhar se está com tag Interactive)", defaultTimer);
                break;
        }

    }

    public void OvniAtingiuChao()
    {
        filhaNaCama.SetActive(false);
        filhaDpsQueOvnicaiu.SetActive(true);
        esposaDepoisDoOvni.SetActive(true);
    }

    public void AdicionarFilhaAoInventario()
    {
        inventory.AddItem(filhaPrefab.GetComponent<Item>());
    }

    public void OkPressionado()
    {
        if (gameController.GetSceneName() == "Cena4-QuartoFilha")
        {
            if (gameController.EstaComFilha == false && gameController.ColocouFilhaNaCama == false && gameController.OvniCaiu == false)
            {
                gameController.ColocouFilhaNaCama = true;
            }
        }
    }

    public void UpdateInteractionText(string text, int timer)
    {
        //textController.UpdateText(text, timer);
    }
}
