using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueBoxManager : MonoBehaviour
{

    /*
     * 
     * 
     * COLOCAR ISSO COMO  COMPONENTE DO PAINEL DE DIALOGO
     * 
     * 
     * */
    [System.Serializable]
    public class DialogPersonas
    {
        public string name;
        public Sprite characterIcon;
        public TextsArrays textsArrays;
    }

    [System.Serializable]
    // RECEBE OS COMPONENTES DO PAINEL DE DIALOGO
    public class DialogObjects
    {
        public GameObject OkButton;
        public Text DialogText;
        public Text CharacterNameText;
        public Image characterSprite;
    }

    [System.Serializable]
    // NOME E icone de todos os personagens que falam
    public class CharacterInfo
    {
        public string name;
        public Sprite icon;
    }

    [System.Serializable]
    //Array desse tipo recebera o nome e a fala do personagem
    public class Fala
    {
        public string texto;
        public string nome;
    }

    public bool inTalk;
    public bool finishTalk;
    public bool startTalk;
    public bool justListen; // para dialogos onde o jogador nao precisará responder nada, só "ouvir" o que o outro personagem tem a dizer
    public bool isComentary = false;

    public string actualText;

    private GameController gameController;

    private CanvasGroup canvasGroup;

    private GameObject Player;

    public DialogObjects dialogObjects;
    public DialogPersonas playerDialog;
    //public DialogPersonas otherCharacterDialog;

    public CharacterInfo Viktor;
    public CharacterInfo Esposa;
    public CharacterInfo Filha;
    public CharacterInfo Eric;

    //public int actualSpeaker = 1; // 'index' de quem esta falando no momento, 0 para o jogador, 1 para o outro personagem
    // public int quantidadeFalasBloco = 1;

    public int indexBlocoFala = 0;
    public int indexBlocoConversas = 0;
    private TextTyper textTyper;

    public Fala[] falas;

    public bool isConversa;

    // Use this for initialization
    void Start()
    {
        if (gameObject.active == false)
        {
            gameObject.SetActive(true);
        }
        if (gameObject.tag != "DialogManager") // seta a tag se ainda nao possuir
        {
            gameObject.tag = "DialogManager";
        }
        canvasGroup = GetComponent<CanvasGroup>();

        Hide(); // esconde o painel de dialogo

        textTyper = gameObject.GetComponent<TextTyper>();
        Player = GameObject.FindGameObjectWithTag("Player");
        // Passa as informações do player para o dialogo
        if (Player != null)
        {
            playerDialog.name = Player.GetComponent<DialogueBoxInfo>().characterName;
            playerDialog.characterIcon = Player.GetComponent<DialogueBoxInfo>().characterImg;
            playerDialog.textsArrays = Player.GetComponent<TextsArrays>();
        }

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        //falas = new Fala[3];

        //for (int i = 0; i < falas.Length; i++)
        //{
        //    falas[i] = new Fala();
        //}


        //customComentary(falas);
        //customComentary("Fala Vagabunda", Viktor.name);
        //customComentary("Falei seu cuzudo", Esposa.name);

    }

    #region Gerar Falas
    /// QUANDO FOR CHAMAR ESSAS 3 FUNCOES PARA FALAR, CHAMAR NESSA ORDEM
    /// <summary>
    /// Seta a quantidade de falas que vai ter no dialogo
    /// </summary>
    /// <param name="quantidadeFalas"></param>
    public void SetQuantidadeFalas(int quantidadeFalas)
    {
        falas = new Fala[quantidadeFalas];
        indexBlocoConversas = 0;
        for (int i = 0; i < falas.Length; i++)
        {
            falas[i] = new Fala();
        }
    }

    /// <summary>
    /// Adiciona a fala passando o nome do personagem como referencia
    /// </summary>
    /// <param name="nome"></param>
    /// <param name="texto"></param>
    public void AdicionarFala(string nome, string texto)
    {
        falas[indexBlocoConversas].nome = nome;
        falas[indexBlocoConversas].texto = texto;
        indexBlocoConversas++;
    }
    /// <summary>
    /// Chama a funcao Custom Comentary que vai realizar a atualizao dos componentes da ui e chamar a funcao para falar realmente
    /// </summary>
    public void RealizarConversa()
    {
        Player.GetComponent<PlayerBehaviour>().CanMove = false;
        Player.GetComponent<PlayerBehaviour>().FicarParado();
        indexBlocoConversas = 0;
        customComentary(falas);
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        ManagerTalkVariables();
        //HandleSpeak();
    }
    void HandleSpeak()
    {
        //if (startTalk)
        //{
        //    startTalk = false;

        //    TextsArrays texts;
        //    if (isComentary)
        //    {

        //    }
        //    else
        //    {
        //        Debug.LogWarning("Entrei no else");
        //        switch (actualSpeaker)
        //        {

        //            case (int)Speakers.Player: // player
        //                texts = Player.GetComponent<TextsArrays>();

        //                actualText = texts.GetActualPhrase();
        //                textTyper.StartToSpeak();
        //                break;

        //            case (int)Speakers.Other: // outro personagem

        //                texts = otherCharacterDialog.textsArrays;

        //                if (texts.GetEndTalking() == true && finishTalk) // se o personagem ja esta na ultima frase e ja acabou de falar
        //                {
        //                    Debug.Log("Não há mais frases");
        //                }
        //                else
        //                {
        //                    actualText = texts.GetActualPhrase();
        //                    textTyper.StartToSpeak();
        //                }
        //                break;
        //        }
        //    }
        //}
    }


    public void customComentary(Fala[] falas)
    {
        actualText = "";
        //isComentary = true;
        finishTalk = false;
        dialogObjects.DialogText.text = "";
        textTyper.dialogText.text = "";
        startTalk = true;
        isConversa = true;
        inTalk = true; // fala para o dialog manager que está em um dialogo

        // Atualiza as informações da ui pelo nome do personagem
        if (falas[indexBlocoConversas].nome == Viktor.name)
        {
            dialogObjects.characterSprite.sprite = Viktor.icon;
            dialogObjects.CharacterNameText.text = Viktor.name;
        }
        else if (falas[indexBlocoConversas].nome == Esposa.name)
        {
            dialogObjects.characterSprite.sprite = Esposa.icon;
            dialogObjects.CharacterNameText.text = Esposa.name;
        }
        else if (falas[indexBlocoConversas].nome == Filha.name)
        {
            dialogObjects.characterSprite.sprite = Filha.icon;
            dialogObjects.CharacterNameText.text = Filha.name;
        }
        else if (falas[indexBlocoConversas].nome == Eric.name)
        {
            dialogObjects.characterSprite.sprite = Eric.icon;
            dialogObjects.CharacterNameText.text = Eric.name;
        }

        actualText = falas[indexBlocoConversas].texto; // o texto atual para ser falado recebe o texto da array
        // o index dessa array é atualizado la pra baixo dentro da funcao OKPressed(), que é chamada quando o jogador clica no botao ok da UI

        textTyper.StartToSpeak();
        Debug.LogWarning("MakeCUSTOMComentary()");
    }


    public void MakeComentary(string texto)
    {
        dialogObjects.characterSprite.sprite = playerDialog.characterIcon;
        dialogObjects.CharacterNameText.text = playerDialog.name;
        //dialogObjects.characterSprite.sprite = iconPersonagem;
        //dialogObjects.CharacterNameText.text = namePersonagem;

        actualText = "";
        isComentary = true;
        finishTalk = false;
        dialogObjects.DialogText.text = "";
        textTyper.dialogText.text = "";
        startTalk = true;
        inTalk = true; // fala para o dialog manager que está em um dialogo
        actualText = texto;
        textTyper.StartToSpeak();
        Debug.LogWarning("MakeComentary()");
    }

    public void CharacterSpeak(DialogueBoxInfo infoCharacter)
    {
        dialogObjects.DialogText.text = "";
        actualText = "";
        startTalk = true;
        isComentary = false;
        SetInTalk(true); // fala para o dialog manager que está em um dialogo
        //SetDialogueInfo(infoCharacter);
    }

    void ManagerTalkVariables()
    {
        //if (startTalk == true)
        //{
        //    finishTalk = false;
        //}
        if (finishTalk == false)
        {
            dialogObjects.OkButton.SetActive(false);
        }
        else
        {
            dialogObjects.OkButton.SetActive(true);
            startTalk = false;
            inTalk = false;
        }

        if (inTalk)
        {
            gameController.SetOnDialogue(true);
            Show();
        }
    }

    public void OkButtonPressed()
    {
        if (isComentary)
        {
            isComentary = false;
            gameController.SetOnDialogue(false);
            finishTalk = false;
            Hide();
        }
        else if (isConversa == true)
        {
            if (indexBlocoConversas != falas.Length - 1)
            {
                indexBlocoConversas++;
                startTalk = true;
                //textTyper.StartToSpeak();
                customComentary(falas);

            }
            else
            {
                isComentary = false;
                gameController.SetOnDialogue(false);
                finishTalk = false;
                Player.GetComponent<PlayerBehaviour>().CanMove = true;

                gameController.BotaoOk();
                GameObject.FindGameObjectWithTag("GameController").GetComponent<InteractionsController>().OkPressionado();
                Hide();
            }
        }
        //else
        //{
        //    if (actualSpeaker == (int)Speakers.Player)
        //    {
        //        if (playerDialog.textsArrays.GetEndTalking() == false)
        //        {
        //            playerDialog.textsArrays.NextLine();
        //            finishTalk = false;
        //            startTalk = true;
        //        }
        //        else
        //        {
        //            //Hide();
        //            //gameController.SetOnDialogue(false);
        //        }
        //    }
        //    else if (actualSpeaker == (int)Speakers.Other)
        //    {
        //        if (otherCharacterDialog.textsArrays.GetEndTalking() == false)
        //        {
        //            otherCharacterDialog.textsArrays.NextLine();
        //            finishTalk = false;
        //            startTalk = true;
        //        }
        //        else
        //        {
        //            Hide();
        //            gameController.SetOnDialogue(false);
        //        }
        //    }
        //}
    }

    void Hide()
    {
        canvasGroup.alpha = 0f; //this makes everything transparent
        canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    void Show()
    {
        canvasGroup.alpha = 1f; //this makes everything transparent
        canvasGroup.blocksRaycasts = true; //this prevents the UI element to receive input events
    }

    public void SetInTalk(bool value)
    {
        inTalk = value;
        if (value == true)
            finishTalk = false;
    }

    public void SetJustListen(bool value)
    {
        justListen = value;
    }


    /// <summary>
    /// Dois personagens estarão em dialogo (Player e mais um personagem), essa função seta as informações(nome, imagem) do outro personagem para que seja mostrada na caixa de dialogo
    /// </summary>
    //public void SetDialogueInfo(DialogueBoxInfo infoCharacterScript)
    //{
    //    otherCharacterDialog.textsArrays = infoCharacterScript.gameObject.GetComponent<TextsArrays>();

    //    otherCharacterDialog.name = infoCharacterScript.characterName;
    //    otherCharacterDialog.characterIcon = infoCharacterScript.characterImg;

    //    dialogObjects.CharacterNameText.text = otherCharacterDialog.name;
    //    dialogObjects.characterSprite.sprite = otherCharacterDialog.characterIcon;

    //}

}
