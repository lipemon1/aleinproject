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

    public GameObject mobileHud;

    public float timeToFade;
    public GameObject FadePanel;

    public int cenaAtual;
    PlayerBehaviour playerBehav;    

    void Awake()
    {

        //#if UNITY_ANDROID
        //        onMobile = true;
        //        ShowDescriptionByMouse = false;
        //#endif

        if (gameObject.tag != "GameController")
            gameObject.tag = "GameController";
        if(FadePanel == null)
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

    }

    public void FazerComentario(string texto)
    {
        DialogueBoxManager dialogManager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogueBoxManager>();

        dialogManager.MakeComentary(texto);
    }

    void ManageScenes()
    {
        switch (cenaAtual)
        {
            case (int)Cenas.ChegaEmCasa:

                break;
            case (int)Cenas.SalaDeCasa:
                break;
            case (int)Cenas.Cozinha:
                break;
            case (int)Cenas.UpStairs:
                break;
            case (int)Cenas.QuartoDaFilha:
                break;
        }
    }

    /// <summary>
    /// Deve ser chamada toda vez que o jogador entra em outra cena para que os valores das variaveis sejam alterados
    /// </summary>
    void UpdateInfoScenes()
    {
        if (SceneManager.GetActiveScene().name == "Cena1 - ChegadaEmCasa")
        {
            cenaAtual = (int)Cenas.ChegaEmCasa;
        }
        else if (SceneManager.GetActiveScene().name == "Cena2-DentroDeCasa")
        {
            cenaAtual = (int)Cenas.SalaDeCasa;
        }

        if (cenaAtual == (int)Cenas.ChegaEmCasa ||
           cenaAtual == (int)Cenas.SalaDeCasa ||
           cenaAtual == (int)Cenas.Cozinha ||
           cenaAtual == (int)Cenas.UpStairs ||
           cenaAtual == (int)Cenas.QuartoDaFilha)
        {
            playerBehav.SetCanRun(false);
            playerBehav.SetCanSneak(false);
            playerBehav.SetCanAttack(false);
        }


    }

    public void ChangeToScene(string sceneName)
    {
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
    }
    IEnumerator FadeIn(float aValue, float aTime)
    {
        for (float t = 0.0f; t < 1f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(1, aValue, t));
            FadePanel.GetComponent<Image>().color = newColor;
            yield return null;
        }
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

}
