using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueBoxManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogPersonas
    {
        public string name;
        public Sprite characterIcon;
        public TextsArrays textsArrays;        
    }

    [System.Serializable]
    public class DialogObjects
    {
        public GameObject OkButton;
        public Text DialogText;
        public Text CharacterNameText;
        public Image characterSprite;
    }

    public enum Speakers{
        Player,
        Other
    }

    public bool inTalk;
    public bool finishTalk;
    public bool startTalk;
    public bool justListen; // para dialogos onde o jogador nao precisará responder nada, só "ouvir" o que o outro personagem tem a dizer

    public string actualText;

    private GameController gameController;

    private CanvasGroup canvasGroup;

    private GameObject Player;

    public DialogObjects dialogObjects;
    public DialogPersonas playerDialog;
    public DialogPersonas otherCharacterDialog;    

    public int actualSpeaker = 1; // 'index' de quem esta falando no momento, 0 para o jogador, 1 para o outro personagem

    private TextTyper textTyper;    

    // Use this for initialization
    void Start()
    {
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

    }

    // Update is called once per frame
    void Update()
    {
        ManagerTalkVariables();
        if (startTalk)
        {
            startTalk = false;
            Debug.LogWarning("Start to talk asshole");
            TextsArrays texts;
            switch (actualSpeaker)
            {

                case (int)Speakers.Player: // player
                    texts = Player.GetComponent<TextsArrays>();
                    texts.setDialogBlock(Random.Range(0, 2));
                    actualText = texts.GetActualPhrase();
                    textTyper.StartToSpeak();
                    break;
                case (int)Speakers.Other: // outro personagem

                    texts = otherCharacterDialog.textsArrays;

                    if (texts.GetEndTalking() == true && finishTalk) // se o personagem ja esta na ultima frase e ja acabou de falar
                    {
                        Debug.Log("Não há mais frases");
                    }
                    else
                    {
                        actualText = texts.GetActualPhrase();
                        textTyper.StartToSpeak();
                    }
                    break;

            }
        }
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
        Debug.LogWarning("OK PRESSED");
        startTalk = true;
        if (actualSpeaker == (int)Speakers.Player)
        {
            if(playerDialog.textsArrays.GetEndTalking() == false)
            {
                playerDialog.textsArrays.NextLine();
                finishTalk = false;
            }
        }
        else if(actualSpeaker == (int)Speakers.Other)
        {
            if (otherCharacterDialog.textsArrays.GetEndTalking() == false)
            {
                otherCharacterDialog.textsArrays.NextLine();
                finishTalk = false;
            }
            else
            {
                Hide();
                gameController.SetOnDialogue(false);
            }
        }
        
        
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
    public void SetDialogueInfo(DialogueBoxInfo infoCharacterScript)
    {
        otherCharacterDialog.textsArrays = infoCharacterScript.gameObject.GetComponent<TextsArrays>();

        otherCharacterDialog.name = infoCharacterScript.characterName;
        otherCharacterDialog.characterIcon = infoCharacterScript.characterImg;

        dialogObjects.CharacterNameText.text = otherCharacterDialog.name;
        dialogObjects.characterSprite.sprite = otherCharacterDialog.characterIcon;

    }

}
