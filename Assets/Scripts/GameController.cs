using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{

    public enum Cenas
    {
        ChegaEmCasa,
        ChegaEmCasaAposOvni,
        SalaDeCasa,
        SalaDeCasaPosOvniCaiu,
        Cozinha,
        UpStairs,
        UpStairsPosOvniCaiu,
        QuartoDaFilha,
        Cratera, // tela onde o ovni caiu
        Quarentena,
        Facility,
        SalaDuto
    }
    [System.Serializable]
    public class ScreenShake
    {        
        public float shakeQuantidade = 0.3f;
        public float shakeTempo = 0.5f;
        [HideInInspector]
        public Hashtable ht;
    }

    [Tooltip("Indica se esta rodando no Android ")]
    public bool onMobile;
    [Tooltip("Indica se o jogador está em meio à um dialogo")]
    public bool onDialogue;
    [Tooltip("Se ativado, apresenta o texto com a descrição do objeto proximo ao mouse ao passa-lo sob o objeto(objeto precisa ter a classe myText)\nÉ preciso ter o objeto Canvas Follow Mouse na cena")]
    public bool ShowDescriptionByMouse = true;

    public Cenas cenaAtual;    

    public GameObject mobileHud;

    public float timeToFade;
    public GameObject FadePanel;
    public bool fadeDone;
    private Camera camera;

    PlayerBehaviour playerBehav;

    AudioSource audioSource;

    public string activeItem = "";
    public bool mouseOverUI;

    [Header("Screen Shakes")]
    public ScreenShake OvniCaindoShake;
    public ScreenShake OvniAtingiuChaoShake;
    public ScreenShake porradaTentaculoShake;
    public ScreenShake porradaViktorShake;

    public bool ViktorJaehMonstro;

    [Header("Objetos Interação")]
    #region CONTROLE OBJETOS INTERAÇÃO
    [Header("Casa/Terreno")]
    public bool dialogoInicialDentroCasa;
    
    public bool DialogoInicialDentroCasa
    {
        get
        {
            return dialogoInicialDentroCasa;
        }

        set
        {
            dialogoInicialDentroCasa = value;
        }
    }
    public bool temCopo;
    public bool TemCopo
    {
        get
        {
            return temCopo;
        }

        set
        {
            temCopo = value;
        }
    }
    public bool mulherPediuAgua;
    public bool MulherPediuAgua
    {
        get
        {
            return mulherPediuAgua;
        }

        set
        {
            mulherPediuAgua = value;
        }
    }
    public bool jaEntregouAgua;
    public bool JaEntregouAgua
    {
        get
        {
            return jaEntregouAgua;
        }

        set
        {
            jaEntregouAgua = value;
        }
    }

    public bool DevePegarFilha
    {
        get
        {
            return devePegarFilha;
        }

        set
        {
            devePegarFilha = value;
        }
    }

    public bool EstaComFilha
    {
        get
        {
            return estaComFilha;
        }

        set
        {
            estaComFilha = value;
        }
    }

    public bool SubiuEscadas
    {
        get
        {
            return subiuEscadas;
        }

        set
        {
            subiuEscadas = value;
        }
    }

    public bool ColocouFilhaNaCama
    {
        get
        {
            return colocouFilhaNaCama;
        }

        set
        {
            colocouFilhaNaCama = value;
        }
    }

    public bool OvniCaiu
    {
        get
        {
            return ovniCaiu;
        }

        set
        {
            ovniCaiu = value;
        }
    }

    public bool PegouLanterna
    {
        get
        {
            return pegouLanterna;
        }

        set
        {
            pegouLanterna = value;
        }
    }

    public bool DialogoInicialQuarentena
    {
        get
        {
            return dialogoInicialQuarentena;
        }

        set
        {
            dialogoInicialQuarentena = value;
        }
    }

    public bool JaQuebrouDutoFinal
    {
        get
        {
            return jaQuebrouDutoFinal;
        }

        set
        {
            jaQuebrouDutoFinal = value;
        }
    }

    public bool devePegarFilha;
    public bool estaComFilha;
    public bool subiuEscadas;
    public bool colocouFilhaNaCama;
    public bool ovniCaiu;
    public bool pegouLanterna;
    public bool jaQuebrouDutoFinal = false;

    [Header("Quarentena")]
    public bool dialogoInicialQuarentena;


    #endregion

    public DialogueBoxManager dialogManager;
    PensamentosManager pensamentoM;
    InteractionsController interacController;
    AudioManager audioManager;

    void Awake()
    {

        if (gameObject.tag != "GameController")
            gameObject.tag = "GameController";
        if (FadePanel == null)
        {
            FadePanel = GameObject.FindGameObjectWithTag("FadePanel");
        }

        playerBehav = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();

        if (!onMobile && ShowDescriptionByMouse)
        {
            if (Camera.main.GetComponent<MouseOverController>() == false)
            {
                Camera.main.gameObject.AddComponent<MouseOverController>();
            }
        }
        else if (onMobile)
        {
            ShowDescriptionByMouse = false;
        }
    }

    // Use this for initialization
    void Start()
    {
        Cursor.visible = true;
        audioSource = GetComponent<AudioSource>();
        dialogManager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogueBoxManager>();
        pensamentoM = gameObject.GetComponent<PensamentosManager>();
        interacController = gameObject.GetComponent<InteractionsController>();

        StartCoroutine(FadeIn(0.0f, timeToFade));
        if (mobileHud == null)
        {
            mobileHud = GameObject.FindGameObjectWithTag("MobileControll");
        }

        if (onMobile)
        {
            ShowMobileHud();
        }
        else
        {
            HideMobileHud();

        }
        SetCenaAtual();
        Invoke("UpdateInfoScenes",0.1f); // chamando depois de um tempo pq no start é muito cedo pra chamar uma função que esta em outra classe que altera coisa visual
        //UpdateInfoScenes();

        UpdateInfoControll();

        camera = Camera.main;



        #region Atribuições Shake Screen
        OvniCaindoShake.ht = new Hashtable();
        porradaTentaculoShake.ht = new Hashtable();
        porradaViktorShake.ht = new Hashtable();
        OvniAtingiuChaoShake.ht = new Hashtable();


        //Ovni caiu
        OvniCaindoShake.ht.Add("x", OvniCaindoShake.shakeQuantidade);
        OvniCaindoShake.ht.Add("y", OvniCaindoShake.shakeQuantidade);
        OvniCaindoShake.ht.Add("time", OvniCaindoShake.shakeTempo);
        //Porradas Tentaculo
        porradaTentaculoShake.ht.Add("x", porradaTentaculoShake.shakeQuantidade);
        porradaTentaculoShake.ht.Add("y", porradaTentaculoShake.shakeQuantidade);
        porradaTentaculoShake.ht.Add("time", porradaTentaculoShake.shakeTempo);
        // Porradas Viktor
        porradaViktorShake.ht.Add("x", porradaViktorShake.shakeQuantidade);
        porradaViktorShake.ht.Add("y", porradaViktorShake.shakeQuantidade);
        porradaViktorShake.ht.Add("time", porradaViktorShake.shakeTempo);
        // Ovni atingiu o chao
        OvniAtingiuChaoShake.ht.Add("x", OvniAtingiuChaoShake.shakeQuantidade);
        OvniAtingiuChaoShake.ht.Add("y", OvniAtingiuChaoShake.shakeQuantidade);
        OvniAtingiuChaoShake.ht.Add("time", OvniAtingiuChaoShake.shakeTempo);        
        #endregion


    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateInfoControll()
    {
        //if (PlayerPrefs.GetInt(PlayerPrefsKeys.ovniCaiu) == 1)
        //{
        //    ovniCaiu = true;
        //}

        //#region Cena Upstairs
        //if (cenaAtual == Cenas.UpStairs)
        //{
        //    if (ovniCaiu)
        //    {

        //        playerBehav.gameObject.transform.position = new Vector3(-12.74f, -1.79f, playerBehav.gameObject.transform.position.z);
        //    }
        //    else
        //    {
        //        playerBehav.Flip();
        //        playerBehav.gameObject.transform.position = new Vector3(6.36f, -1.79f, playerBehav.gameObject.transform.position.z);
        //    }
        //}
        //#endregion
        //#region Cena Sala Casa
        //else if (cenaAtual == Cenas.SalaDeCasa)
        //{
        //    if (ovniCaiu)
        //    {
                
        //        playerBehav.gameObject.transform.position = new Vector3(-1.82f, -1.08f, playerBehav.gameObject.transform.position.z);
        //    }
        //    else
        //    {
        //        playerBehav.gameObject.transform.position = new Vector3(-6.62f, -1.08f, playerBehav.gameObject.transform.position.z);
        //    }
        //}
        //#endregion

        //#region Cena Chega em Casa
        //else if (cenaAtual == Cenas.ChegaEmCasa)
        //{
        //    if (ovniCaiu)
        //    {
        //        playerBehav.gameObject.transform.position = new Vector3(4.42f, -1.95f, playerBehav.gameObject.transform.position.z);
        //        playerBehav.Flip();   
        //    }
        //    else
        //    {

        //        playerBehav.gameObject.transform.position = new Vector3(1.47f, -1.95f, playerBehav.gameObject.transform.position.z);
        //        playerBehav.Flip();
        //    }
            
        //}
        //#endregion
        //if (cenaAtual != Cenas.UpStairs && cenaAtual != Cenas.QuartoDaFilha && cenaAtual != Cenas.SalaDeCasa)
        //{
        //    PlayerPrefs.SetInt(PlayerPrefsKeys.ovniCaiu, 0);
        //}
    }

    // Update is called once per frame
    void Update()
    {
    

        if (onMobile)
        {
            if (onDialogue)
                HideMobileHud();
            else
                ShowMobileHud();
        }
        //UpdateInfoScenes();

        ManageScenes();
    }

    public void FazerComentario(string texto, bool precisaConfirmacao, float tempoProximaFala)
    {
        dialogManager.MakeComentary(texto,precisaConfirmacao, tempoProximaFala);
    }
    public void FazerComentario(string texto)
    {
        dialogManager.MakeComentary(texto, true, 0f);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void SetCenaAtual()
    {
        if (SceneManager.GetActiveScene().name == "Cena1-ChegadaEmCasa")
        {
            cenaAtual = Cenas.ChegaEmCasa;
            //playerBehav.HideLifeBar();
            playerBehav.DeveEsconderLifeBar1 = true;
        }
        else if (SceneManager.GetActiveScene().name == "Cena1.1-ChegadaEmCasa")
        {
            cenaAtual = Cenas.ChegaEmCasaAposOvni;
            //playerBehav.HideLifeBar();
            playerBehav.DeveEsconderLifeBar1 = true;
        }        

        else if (SceneManager.GetActiveScene().name == "Cena2-DentroDeCasa")
        {
            cenaAtual = Cenas.SalaDeCasa;
            //playerBehav.HideLifeBar();
            playerBehav.DeveEsconderLifeBar1 = true;
        }
        else if (SceneManager.GetActiveScene().name == "Cena2.1-DentroDeCasa")
        {
            cenaAtual = Cenas.SalaDeCasaPosOvniCaiu;
            //playerBehav.HideLifeBar();
            playerBehav.DeveEsconderLifeBar1 = true;
        }
        

        else if (SceneManager.GetActiveScene().name == "Cena3-Upstairs")
        {
            cenaAtual = Cenas.UpStairs;
            //playerBehav.HideLifeBar();
            playerBehav.DeveEsconderLifeBar1 = true;
        }
        else if (SceneManager.GetActiveScene().name == "Cena3.1-Upstairs")
        {
            cenaAtual = Cenas.UpStairsPosOvniCaiu;
            //playerBehav.HideLifeBar();
            playerBehav.DeveEsconderLifeBar1 = true;
        }
        else if (SceneManager.GetActiveScene().name == "Cena4-QuartoFilha")
        {
            cenaAtual = Cenas.QuartoDaFilha;
            //playerBehav.HideLifeBar();
            playerBehav.DeveEsconderLifeBar1 = true;
        }
        else if (SceneManager.GetActiveScene().name == "Cena5-Quarentena")
        {
            cenaAtual = Cenas.Quarentena;
            //playerBehav.HideLifeBar();
            playerBehav.estaNaInstalacao = true;
            playerBehav.DeveEsconderLifeBar1 = true;
        }
        else if (SceneManager.GetActiveScene().name == "Cena6-Facility")
        {
            playerBehav.estaNaInstalacao = true;
            cenaAtual = Cenas.Facility;
            //playerBehav.ShowLifeBar();
        }
        else if(SceneManager.GetActiveScene().name == "Cena7-SalaDuto")
        {
            playerBehav.estaNaInstalacao = true;
            cenaAtual = Cenas.SalaDuto;
        }
    }

    void UpdateInfoScenes()
    {
        if (cenaAtual == Cenas.SalaDeCasa ||
           cenaAtual == Cenas.Cozinha ||
           cenaAtual == Cenas.UpStairs ||
           cenaAtual == Cenas.QuartoDaFilha)
        {
            playerBehav.SetCanRun(false);
            playerBehav.SetCanSneak(false);
            playerBehav.SetCanAttack(false);
        }
        if (cenaAtual == Cenas.ChegaEmCasa)
        {
            if (OvniCaiu == false)
                playerBehav.SetCanRun(false);
            else
            {
                playerBehav.SetCanRun(true);
            }
        }
        else if(cenaAtual == Cenas.QuartoDaFilha)
        {
            pensamentoM.MostrarPensamento("Coloca-la na cama");
            interacController.AdicionarFilhaAoInventario();
        }
        else if (cenaAtual == Cenas.UpStairs)
        {
            pensamentoM.MostrarPensamento("Levar Filha ao quarto");
            interacController.AdicionarFilhaAoInventario();
        }

        else if (cenaAtual == Cenas.UpStairsPosOvniCaiu)
        {
            pensamentoM.MostrarPensamento("Descer para investigar");
        }

        else if (cenaAtual == Cenas.SalaDeCasaPosOvniCaiu)
        {
            pensamentoM.MostrarPensamento("Pegar Lanterna");
        }
        else if(cenaAtual == Cenas.ChegaEmCasaAposOvni)
        {
            pensamentoM.MostrarPensamento("Ir investigar");
        }
        else if(cenaAtual == Cenas.Quarentena)
        {
            if(dialogoInicialQuarentena == false)
                playerBehav.FicarParado();
            pensamentoM.EsconderPensamentos();
            
        }
    }

    void ManageScenes()
    {
        switch (cenaAtual)
        {
            case Cenas.ChegaEmCasa:

                break;
            case Cenas.SalaDeCasa:
                if(DialogoInicialDentroCasa == false)
                {
                    playerBehav.FicarParado();
                }
                if (fadeDone == true && DialogoInicialDentroCasa == false)
                {
                    DialogoInicialDentroCasa = true;
                    dialogManager.SetQuantidadeFalas(8);
                    //dialogManager.AdicionarFala(dialogManager.Filha.name, "PAPAI!");
                    dialogManager.AdicionarFalaSemConfirmacao(dialogManager.Filha.name, "PAPAI!",0.7f);
                    dialogManager.AdicionarFalaSemConfirmacao(dialogManager.Viktor.name, "Hahaha, boa noite filha, não deveria estar na sua cama?",0.7f);
                    dialogManager.AdicionarFalaSemConfirmacao(dialogManager.Filha.name, "Estávamos esperando você papai.", 0.6f);
                    dialogManager.AdicionarFalaSemConfirmacao(dialogManager.Esposa.name, "Oi querido. Foi tudo bem no trabalho?.", 0.6f);
                    dialogManager.AdicionarFalaSemConfirmacao(dialogManager.Viktor.name, "Olá, foi sim, mas, não precisavam me esperar até tão tarde.",0.8f);
                    dialogManager.AdicionarFalaSemConfirmacao(dialogManager.Esposa.name, "Tudo bem, a Emily queria te ver antes de dormir.", 0.7f);
                    dialogManager.AdicionarFalaSemConfirmacao(dialogManager.Esposa.name, "Leve ela pra cama mas antes traga um copo d’água pra mim amor. Estou com sede.",0.8f);
                    dialogManager.AdicionarFalaSemConfirmacao(dialogManager.Viktor.name, "Claro querida, só um minuto.",0.6f);

                    dialogManager.RealizarConversa();
                    DialogoInicialDentroCasa = true;
                }
                break;
            case Cenas.Cozinha:
                break;
            case Cenas.UpStairs:
                break;
            case Cenas.QuartoDaFilha:
                break;

            case Cenas.Quarentena:
                playerBehav.estaNaInstalacao = true;
                if (fadeDone == true && DialogoInicialQuarentena == false)
                {
                    playerBehav.estaNaInstalacao = true;
                    DialogoInicialQuarentena = true;
                    dialogManager.SetQuantidadeFalas(15);
                    dialogManager.AdicionarFala(dialogManager.Viktor.name, "HEY! ALGUÉM ESTÁ AI??");
                    dialogManager.AdicionarFala(dialogManager.Eric.name, "Finalmente. Depois de 2 meses no coma você acordou.");
                    dialogManager.AdicionarFala(dialogManager.Viktor.name, "Dois meses? Do que está falando? Quem é você? Onde estou e como vim parar aqui? O que aconteceu com...");

                    dialogManager.AdicionarFala(dialogManager.Eric.name, "Acalme-se meu caro. Sou o doutor responsável por controlar a sua mutação. ");
                    dialogManager.AdicionarFala(dialogManager.Eric.name, "Você foi atingido quase que fatalmente por um pedaço daquela máquina que caiu em seu terreno.");
                    dialogManager.AdicionarFala(dialogManager.Eric.name, "Além disso, inalou um poderoso e desconhecido gás proveniente dela.");
                    dialogManager.AdicionarFala(dialogManager.Eric.name, "É incrível você ainda estar vivo.");
                    dialogManager.AdicionarFala(dialogManager.Eric.name, "Grande parte de sua cura foi ocasionada pelo estranho gás.");
                    dialogManager.AdicionarFala(dialogManager.Eric.name, "Vamos descobrir muito sobre ele com você.");

                    dialogManager.AdicionarFala(dialogManager.Viktor.name, "O que fizeram com minha esposa e filha? Onde elas estão?");

                    dialogManager.AdicionarFala(dialogManager.Eric.name, "Elas não sofreram nada com o acidente, estão são e salvas em sua casa.");
                    dialogManager.AdicionarFala(dialogManager.Eric.name, "Mas precisamos de você aqui. Temos muito para investigar ainda.");

                    dialogManager.AdicionarFala(dialogManager.Viktor.name, "Não, eu quero ver a minha família. Quero sair daqui agora!");
                    dialogManager.AdicionarFala(dialogManager.Eric.name, "Nós não podemos deixar você sair antes de encerrar o tratamento.");
                    dialogManager.AdicionarFala(dialogManager.Viktor.name, "NÃO! Eu vou sair daqui agora!");

                    dialogManager.RealizarConversa();
                    DialogoInicialDentroCasa = true;
                }
                break;
            case Cenas.Facility:
                
                break;
            case Cenas.SalaDuto:
              
                break;
        }
    }



    public void BotaoOk()
    {

        if (dialogoInicialDentroCasa == true && mulherPediuAgua == false)
        {
            mulherPediuAgua = true;
            pensamentoM.MostrarPensamento("Pegar água na cozinha");
        }
        else if (JaEntregouAgua == true && DevePegarFilha == false)
        {
            pensamentoM.MostrarPensamento("Levar filha para o quarto");
            DevePegarFilha = true;
        }
        if(DialogoInicialDentroCasa == true)
        {
            playerBehav.CanMove = true;
        }
    }

    /// <summary>
    /// Deve ser chamada toda vez que o jogador entra em outra cena para que os valores das variaveis sejam alterados
    /// </summary>
    

    public void DerrubarOvni()
    {
        playerBehav.FicarParado();
        iTween.ShakePosition(camera.gameObject, OvniCaindoShake.ht);
        float tempo = 3f;

        Invoke("TremerOvni", tempo - 0.5f);
        Invoke("OvniAtingiuOChao", tempo);
    }
    public void TremerOvni()
    {
        iTween.ShakePosition(camera.gameObject, OvniAtingiuChaoShake.ht);
    }

    public void OvniAtingiuOChao()
    {
        OvniCaiu = true;
        interacController.OvniAtingiuChao();
        dialogManager.SetQuantidadeFalas(4);
        dialogManager.AdicionarFala(dialogManager.Filha.name, "PAPAAAAAAAI!!");
        dialogManager.AdicionarFala(dialogManager.Esposa.name, "O que foi isso??");
        dialogManager.AdicionarFala(dialogManager.Viktor.name, "Fiquem aqui dentro, vou lá fora ver o que houve");
        dialogManager.AdicionarFala(dialogManager.Viktor.name, "Se acontecer alguma coisa gritem que eu volto correndo!");
        dialogManager.RealizarConversa();
        PlayerPrefs.SetInt(PlayerPrefsKeys.ovniCaiu, 1);
    }

    public void AproximouDoOvni()
    {
        playerBehav.FicarParado();

        Invoke("AproximouDeVddDoOvni",0.5f);
    }
    public void AproximouDeVddDoOvni()
    {
        //dialogManager.SetQuantidadeFalas(1);
        //dialogManager.AdicionarFalaSemConfirmacao(dialogManager.Viktor.name, "Mas que Me...", 0.2f);
        //dialogManager.RealizarConversa();

        ChangeToScene("EncontraCristal");
    }

    public void ChangeToScene(string sceneName)
    {
        Debug.LogError("ENTROU AQUI");
        fadeDone = false;
        playerBehav.FicarParado();
        FadePanel.GetComponent<CanvasGroup>().blocksRaycasts = false; //this prevents the UI element to receive input events
        //FadePanel.GetComponent<CanvasGroup>().alpha = 0;

        StartCoroutine(FadeOut(1.0f, timeToFade));
        Debug.LogError("LA");
        StartCoroutine(GotToScene(sceneName));
        Debug.LogError("TROCA A CENA");

    }
    IEnumerator GotToScene(string name)
    {
        yield return new WaitForSeconds(timeToFade);

        SceneManager.LoadScene(name);
    }

    IEnumerator FadeOut(float aValue, float aTime)
    {

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(0, aValue, t));
            FadePanel.GetComponent<Image>().color = newColor;
            yield return null;
        }
        fadeDone = true;
    }
    IEnumerator FadeIn(float aValue, float aTime)
    {
        for (float t = 0.0f; t < 1f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(1, aValue, t));
            FadePanel.GetComponent<Image>().color = newColor;
            yield return null;
        }
        fadeDone = true;
    }
    public void SetOnDialogue(bool value)
    {
        onDialogue = value;
    }

    public bool GetOnMobile()
    {
        return onMobile;
    }

    void HideMobileHud()
    {
        mobileHud.GetComponent<CanvasGroup>().alpha = 0f; //this makes everything transparent
        mobileHud.GetComponent<CanvasGroup>().blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    void ShowMobileHud()
    {
        mobileHud.GetComponent<CanvasGroup>().alpha = 1f; //this makes everything transparent
        mobileHud.GetComponent<CanvasGroup>().blocksRaycasts = true; //this prevents the UI element to receive input events
    }

    public void SetActiveItem(string item)
    {
        activeItem = item;
        //   Debug.Log(activeItem.ToString());
    }

    public string GetActiveItem()
    {
        return (activeItem.ToString());
    }

    public void SetMouseOverUI(bool state)
    {
        // Debug.Log("SetMouseOverUI: " + mouseOverUI);
        mouseOverUI = state;
    }
    public bool GetMouseOverUI()
    {
        // Debug.Log("SetMouseOverUI: " + mouseOverUI);
        return (mouseOverUI);
    }

    // pede ao gerenciador de interaçoes pra executar a interação do objeto clicado
    public void Interaction(GameObject clickedObject, string clicked_object_name)
    {
        Debug.Log("Entrou em GameController.Interaction");
        //Debug.LogWarning("Nome do ClickedObject: " + clickedObject.name);

        GameObject.FindGameObjectWithTag("GameController").GetComponent<InteractionsController>().
            ExecuteInteraction(clickedObject, clicked_object_name);
        //InteractionsController.ExecuteInteraction(clickedObject, clicked_object_name);
    }
}
