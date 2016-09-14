using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextTyper : MonoBehaviour
{

    public float letterPause = 0.1f;

    public Text dialogText;
    private DialogueBoxManager dialogManager;

    // Use this for initialization
    void Start()
    {
        dialogManager = gameObject.GetComponent<DialogueBoxManager>();

    }

    // Update is called once per frame
    void Update()
    {


    }
    /// <summary>
    /// Função que dirá para o script começar a escrever o texto na caixa de dialogo
    /// </summary>
    public void StartToSpeak()
    {
        dialogText.text = "";
        StartCoroutine(TypeText());

    }

    IEnumerator TypeText()
    {
        //dialogManager.startTalk = false;
        foreach (char letter in dialogManager.actualText.ToCharArray())
        {
            dialogText.text += letter;

            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
        dialogManager.finishTalk = true;
    }
}
