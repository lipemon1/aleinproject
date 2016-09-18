using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public enum Cenas
    {
        ChegaEmCasa,
        SalaDeCasa,
        Cozinha,
        UpStairs,
        QuartoDaFilha,
        Cratera, // tela onde o ovni caiu
        Quarentena
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


    PlayerBehaviour playerBehav;
    public string activeItem = "";
    public bool mouseOverUI;

    [Header("Objetos Interação")]
    #region CONTROLE OBJETOS INTERAÇÃO
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

    public bool devePegarFilha;
    public bool estaComFilha;
    public bool subiuEscadas;
    public bool colocouFilhaNaCama;
    public bool ovniCaiu;


    #endregion

    public DialogueBoxManager dialogManager;

    void Awake()
    {

        //#if UNITY_ANDROID
        //        onMobile = true;
        //        ShowDescriptionByMouse = false;
        //#endif

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

        dialogManager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogueBoxManager>();

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
        UpdateInfoScenes();

        if (cenaAtual == Cenas.UpStairs)
        {
            if (PlayerPrefs.GetInt(PlayerPrefsKeys.ovniCaiu) == 1)
            {
                ovniCaiu = true;
                playerBehav.gameObject.transform.position = new Vector3(-12.74f, -1.79f, playerBehav.gameObject.transform.position.z);
            }
            else
            {
                playerBehav.Flip();
                playerBehav.gameObject.transform.position = new Vector3(6.36f, -1.79f, playerBehav.gameObject.transform.position.z);
            }
        }
        if (cenaAtual != Cenas.UpStairs && cenaAtual != Cenas.QuartoDaFilha)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.ovniCaiu, 0);
        }
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
        UpdateInfoScenes();

        ManageScenes();

        //if(Input.GetKeyDown(KeyCode.O))
        //{
        //    dialogManager.SetQuantidadeFalas(2);
        //    dialogManager.AdicionarFala(dialogManager.Viktor.name, "Ola Meninas");
        //    dialogManager.AdicionarFala(dialogManager.Filha.name, "PAPAIII");
        //    dialogManager.RealizarConversa();
        //}

        //Debug.LogWarning("Active Item: " + GetActiveItem());

    }

    public void FazerComentario(string texto)
    {
        dialogManager.MakeComentary(texto);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    void ManageScenes()
    {
        switch (cenaAtual)
        {
            case Cenas.ChegaEmCasa:

                break;
            case Cenas.SalaDeCasa:

                if (fadeDone == true && DialogoInicialDentroCasa == false)
                {
                    DialogoInicialDentroCasa = true;
                    dialogManager.SetQuantidadeFalas(8);
                    dialogManager.AdicionarFala(dialogManager.Filha.name, "PAPAI!");
                    dialogManager.AdicionarFala(dialogManager.Viktor.name, "Hahaha, boa noite filha, não deveria estar na sua cama?");
                    dialogManager.AdicionarFala(dialogManager.Filha.name, "Estávamos esperando você papai.");
                    dialogManager.AdicionarFala(dialogManager.Esposa.name, "Oi querido. Foi tudo bem no trabalho?.");
                    dialogManager.AdicionarFala(dialogManager.Viktor.name, "Olá, foi sim, mas, não precisavam me esperar até tão tarde.");
                    dialogManager.AdicionarFala(dialogManager.Esposa.name, "Tudo bem, a Emily queria te ver antes de dormir.");
                    dialogManager.AdicionarFala(dialogManager.Esposa.name, "Leve ela pra cama mas antes traga um copo d’água pra mim amor. Estou com sede.");
                    dialogManager.AdicionarFala(dialogManager.Viktor.name, "Claro querida, só um minuto.");

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
        }
    }

    public void BotaoOk()
    {
        if (dialogoInicialDentroCasa == true && mulherPediuAgua == false)
        {
            mulherPediuAgua = true;
        }
        else if (JaEntregouAgua == true && DevePegarFilha == false)
        {
            DevePegarFilha = true;
        }
    }

    /// <summary>
    /// Deve ser chamada toda vez que o jogador entra em outra cena para que os valores das variaveis sejam alterados
    /// </summary>
    void UpdateInfoScenes()
    {
        if (SceneManager.GetActiveScene().name == "Cena1 - ChegadaEmCasa")
        {
            cenaAtual = Cenas.ChegaEmCasa;
        }
        else if (SceneManager.GetActiveScene().name == "Cena2-DentroDeCasa")
        {
            cenaAtual = Cenas.SalaDeCasa;
        }
        else if (SceneManager.GetActiveScene().name == "Cena3-Upstairs")
        {
            cenaAtual = Cenas.UpStairs;
        }

        if (cenaAtual == Cenas.ChegaEmCasa ||
           cenaAtual == Cenas.SalaDeCasa ||
           cenaAtual == Cenas.Cozinha ||
           cenaAtual == Cenas.UpStairs ||
           cenaAtual == Cenas.QuartoDaFilha)
        {
            playerBehav.SetCanRun(false);
            playerBehav.SetCanSneak(false);
            playerBehav.SetCanAttack(false);
        }
    }

    public void DerrubarOvni()
    {
        playerBehav.FicarParado();

        Invoke("OvniAtingiuOChao", 3f);
    }

    public void OvniAtingiuOChao()
    {
        OvniCaiu = true;
        dialogManager.SetQuantidadeFalas(3);
        dialogManager.AdicionarFala(dialogManager.Filha.name, "PAPAAAAAAAI!!");
        dialogManager.AdicionarFala(dialogManager.Viktor.name, "Fiquem aqui dentro, vou lá fora ver o que houve");
        dialogManager.AdicionarFala(dialogManager.Viktor.name, "Se acontecer alguma coisa gritem que eu volto correndo!");
        dialogManager.RealizarConversa();
        PlayerPrefs.SetInt(PlayerPrefsKeys.ovniCaiu, 1);
    }

    public void ChangeToScene(string sceneName)
    {
        fadeDone = false;
        FadePanel.GetComponent<CanvasGroup>().blocksRaycasts = false; //this prevents the UI element to receive input events
        //FadePanel.GetComponent<CanvasGroup>().alpha = 0;

        StartCoroutine(FadeOut(1.0f, timeToFade));

        StartCoroutine(GotToScene(sceneName));

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
