using UnityEngine;
using System.Collections;

public class InteractionsController : MonoBehaviour
{

    private GameController gameController;


    private int defaultTimer = 120;

    #region Objetos
    public GameObject copoCheioPrefab;
    //public GameObject cabineDireitaObject;
    //public GameObject scissorObject;
    //public GameObject deskObject;
    //public GameObject faixaObject;
    //public GameObject deskPrefabObject;
    //public GameObject faixaPrefabObject;

    //public Transform wire;
    //public Transform scissor;
    //public Transform desk;

    //public GameObject deskPrefab;
    //public GameObject faixaPrefab;
    #endregion

    public Inventory inventory;

    private string nextScene;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        //switch(gameController.GetCurrentScene())
        //   {
        //       case "Banheiro":
        //           #region Persistencia do Banheiro
        //           #region Cabine Direita
        //           if (cabineDireitaObject == null)
        //           {
        //               cabineDireitaObject = GameObject.Find("Cabine Direita");
        //           }

        //           // se ja abriu a porta
        //           if (gameController.GetOpenedBox())
        //           {
        //               if (cabineDireitaObject != null)
        //               {
        //                   Destroy(cabineDireitaObject);
        //               }
        //           }
        //           #endregion

        //           #region Arame
        //           // se ainda nao pegou o arame
        //           if (!gameController.GetWire())
        //           {
        //               if (wireObject == null)
        //               {
        //                   wireObject = GameObject.Find("Arame");
        //                   wire = wireObject.GetComponent<Transform>();
        //               }
        //               else
        //               {   
        //                   // se o arame ja caiu
        //                   if(gameController.GetWireFallen())
        //                   {
        //                       if (!gameController.GetSearchedTrash())
        //                       {
        //                           wire.transform.position = new Vector3(4.582485f, 0.14f, 7);
        //                       }
        //                       else
        //                       {
        //                           wire.transform.position = new Vector3(4.582485f, -0.6f, 5);
        //                       }
        //                   }
        //               }
        //           }
        //           // se ja pegou o arame
        //           else
        //           {
        //               wireObject = GameObject.Find("Arame");
        //               if (wireObject != null)
        //               {
        //                   Destroy(wireObject);
        //               }
        //           }
        //           #endregion

        //           #region Tesoura
        //           // se ainda nao pegou a tesoura
        //           if (!gameController.GetScissor())
        //           {
        //               if(scissorObject == null)
        //               { 
        //                   scissorObject = GameObject.Find("Tesoura");
        //               }
        //               else
        //               {
        //                   if(gameController.GetUsedWireOnFlush())
        //                   {
        //                       scissorObject.transform.position = new Vector3(scissorObject.transform.position.x, -0.6f, 3);
        //                   }
        //               }
        //           }
        //           // se ja pegou a tesoura
        //           else
        //           {
        //               if (scissorObject == null)
        //               {
        //                   scissorObject = GameObject.Find("Tesoura");
        //               }
        //                   Destroy(scissorObject);
        //           }
        //           #endregion
        //           #endregion
        //           break;

        //       case "Externa":
        //           #region Persistencia da Externa
        //           #region Carteira escolar
        //           if(deskObject == null)
        //           {
        //               deskObject = GameObject.Find("Carteira");
        //               // desk = deskObject.GetComponent<Transform>();
        //           }

        //           // se ja clicou nas carteiras
        //           if(gameController.GetClickedOnDesks())
        //           {
        //               // se nao pegou a carteira
        //               if(!gameController.GetDesk())
        //               {
        //                   deskObject.transform.position = new Vector3(deskObject.transform.localPosition.x, -0.45f, 4);
        //                   deskObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        //               }
        //               // se ja pegou a carteira
        //               else
        //               {
        //                   Destroy(deskObject);
        //               }
        //           }
        //           #endregion
        //           #endregion
        //           break;

        //       case "Quadra":
        //           #region Persistencia da Quadra
        //           if(faixaObject == null)
        //           {
        //               faixaObject = GameObject.Find("Faixa");
        //           }

        //           // se ja colocou a faixa no chao
        //           if(gameController.IsFaixaOnFloor)
        //           {
        //               Destroy(faixaObject);

        //               // se nao comecou a festa
        //               if (!gameController.PartyHasStarted)
        //               {
        //                   if(faixaPrefabObject == null)
        //                   {
        //                       faixaPrefabObject = GameObject.Find("FaixaEmCena(Clone)");

        //                       if(faixaPrefabObject == null)
        //                       { 
        //                           Instantiate(faixaPrefab, new Vector3(-13.0f, -0.33f, 3f), Quaternion.identity);
        //                           faixaPrefabObject = GameObject.Find("FaixaEmCena(Clone)");
        //                       }
        //                   }

        //                   faixaPrefabObject.transform.position = new Vector3(-13.0f, -0.33f, 3f);
        //               }
        //               else
        //               {
        //                   if (faixaPrefabObject != null)
        //                   {
        //                       Destroy(faixaPrefabObject);
        //                   }
        //                   else
        //                   {
        //                       faixaPrefabObject = GameObject.Find("FaixaEmCena(Clone)");
        //                       Destroy(faixaPrefabObject);
        //                   }
        //               }
        //           }
        //           else
        //           {
        //               if(gameController.HasFaixa)
        //               {
        //                   Destroy(faixaObject);
        //               }
        //           }

        //           if(gameController.IsDeskOnFloor)
        //           {
        //               if(!gameController.PartyHasStarted)
        //               {
        //                   if (deskPrefabObject == null)
        //                   {
        //                       deskPrefabObject = GameObject.Find("CarteiraEmCena(Clone)");
        //                       if(deskPrefabObject == null)
        //                       { 
        //                           Instantiate(deskPrefab, new Vector3(-9.12f, -0.33f, 3f), Quaternion.identity);
        //                           deskPrefabObject = GameObject.Find("CarteiraEmCena(Clone)");
        //                       }
        //                   }

        //                   faixaPrefabObject.transform.position = new Vector3(-9.12f, -0.33f, 3f);
        //               }
        //               else
        //               {
        //                   if (deskPrefabObject != null)
        //                   {
        //                       Destroy(deskPrefabObject);
        //                   }
        //                   else
        //                   {
        //                       deskPrefabObject = GameObject.Find("CarteiraEmCena(Clone)");
        //                       Destroy(deskPrefabObject);
        //                   }
        //               }
        //           }
        //           #endregion
        //           break;
        //   }
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
        // Debug.Log("Entrou em ExecuteInteraction");
        switch (object_name)
        {

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
                if (gameController.GetActiveItem() == "Copo")
                {
                    inventory.ClearInventorySlot();
                    inventory.AddItem(copoCheioPrefab.GetComponent<Item>());
                    gameController.TemCopo = false;
                    gameController.FazerComentario("Cheinho...");
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
                break;
            #endregion

            case "Escada":
                //    Fazer Subir Escada
                break;

            #region Personagens
            case "Esposa":
                //   Fazer a mulher falar
                if (gameController.MulherPediuAgua == true && gameController.JaEntregouAgua == false &&
                    gameController.GetActiveItem() == "Copo Cheio")
                {
                    gameController.FazerComentario("Agua Entregue");
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
            #endregion
            #endregion

            default:
                UpdateInteractionText("Item não definido (olhar se está com tag Interactive)", defaultTimer);
                break;
        }

    }

    public void UpdateInteractionText(string text, int timer)
    {
        //textController.UpdateText(text, timer);
    }
}
