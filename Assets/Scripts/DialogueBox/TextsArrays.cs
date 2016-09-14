using UnityEngine;
using System.Collections;

public class TextsArrays : MonoBehaviour
{

    public string[] actualDialog;
    public int actualBlock = 0; // o sistema de dialogos contera 'blocos' de textos para cada personagem, sendo alterados dependendo do momento do dialogo no jogo
    public int linhaAtual = 0;

    int idPersonagem; // id do personagem a qual as falas pertencem

    string[] texts;

    // Use this for initialization
    void Start()
    {
        if(gameObject.tag == "Player")
        {
            idPersonagem = Characters.player.id;            
        }
        else if(gameObject.tag == "Eric")
        {
            idPersonagem = Characters.eric.id;
        }
        else if (gameObject.tag == "Mulher")
        {
            idPersonagem = Characters.mulher.id;
        }
        else if (gameObject.tag == "Filha")
        {
            idPersonagem = Characters.filha.id;
        }


        UpdateCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Pega a frase atual do bloco de dialogos para ser mostrada na tela
    /// </summary>
    public string GetActualPhrase()
    {
        return actualDialog[linhaAtual];
    }

    public void setDialogBlock(int value)
    {
        actualBlock = value;
    }

    void setActualText(string[] newStrings)
    {
        actualDialog = new string[newStrings.Length];
        actualDialog = newStrings;
    }

    public void NextLine()
    {
        linhaAtual++;
    }

    public void ResetLine()
    {
        linhaAtual = 0;
    }

    public bool GetEndTalking()
    {
        if(linhaAtual == actualDialog.Length - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void UpdateCharacter()
    {        
        switch (idPersonagem)
        {
            case (int)Characters.Ids.Player:
                DialogosPlayer();
                break;
            case (int)Characters.Ids.Eric:
                DialogosEric();
                break;
            case (int)Characters.Ids.Mulher:
                DialogosMulher();
                break;
            case (int)Characters.Ids.Filha:
                DialogosFilha();
                break;
        }
        
    }
    void DialogosPlayer()
    {
       
        
        switch (actualBlock)
        {
            
            case 0:
                texts = new string[]
                {
                    "O que eu faço aqui?",
                    "Me tira desse jogo por favor."
                };
                setActualText(texts);
                break;

            case 1:
                texts = new string[]
                {
                    "Não olha pra mim não, da um jeito nisso."
                };
                setActualText(texts);
                break;
            case 2:
                texts = new string[]
                {
                    "Ia Ia Ôoooooo..."
                };
                setActualText(texts);
                break;

            default:
                texts = new string[]
                {
                "Hey, Não olher pra mim, dê um jeito de resolver sozinho"
                };
                setActualText(texts);
                break;
        }
    }
    void DialogosEric()
    {
        string[] texts;
        switch (actualBlock)
        {
            case 0:
                texts = new string[]
                {
                    "Lindo Dia não é?",
                    "Como estás?",                    
                    "Poderia me fazer um favor?",
                    "Vem aqui pra eu esmagar sua cabeça!"
                };
                setActualText(texts);
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;


            default:
                break;
        }
    }
    void DialogosFilha()
    {

    }
    void DialogosMulher()
    {
        switch (actualBlock)
        {

            case 0:
                texts = new string[]
                {
                    "Amor, pega um copo de água na cozinha pra mim, por favor?"
                };
                setActualText(texts);
                break;

            case 1:
                texts = new string[]
                {
                    "Não olha pra mim não, da um jeito nisso."
                };
                setActualText(texts);
                break;

        }
    }
}
