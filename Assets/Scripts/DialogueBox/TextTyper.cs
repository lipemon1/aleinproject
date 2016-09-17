using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextTyper : MonoBehaviour
{

    public float letterPause = 0.1f;

    public Text dialogText;
    private DialogueBoxManager dialogManager;

    public bool onLoop = false;
    public bool clicouDnv = false;


    // Use this for initialization
    void Start()
    {
        dialogManager = gameObject.GetComponent<DialogueBoxManager>();

    }


    /// <summary>
    /// Função que dirá para o script começar a escrever o texto na caixa de dialogo
    /// </summary>
    public void StartToSpeak()
    {
        if (clicouDnv == false && onLoop == true)
        {
            clicouDnv = true;
            StopCoroutine(TypeText());

        }

        dialogText.text = "";
        StartCoroutine(TypeText());
        dialogManager.SetInTalk(true);

        //Debug.LogWarning("asdasds");
    }

    IEnumerator TypeText()
    {
        //dialogManager.startTalk = false;

        foreach (char letter in dialogManager.actualText.ToCharArray())
        {
            if (onLoop == false)
            {
                onLoop = true;
            }

            dialogText.text += letter;
            yield return 0;

            yield return new WaitForSeconds(letterPause);

            // quebra a thread caso ja esteja rodando e precise falar outra coisa

            if (clicouDnv == true)
            {
                clicouDnv = false;
                //StopCoroutine(TypeText());

                onLoop = false;
                yield break;
                break;
            }
        }

        dialogManager.finishTalk = true;
        onLoop = false;
    }
}
