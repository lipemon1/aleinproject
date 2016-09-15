using UnityEngine;
using System.Collections;
using System;

public class TextsArrays : MonoBehaviour
{
    [System.Serializable]
    public class BlocoFalas
    {
        public string[] linhas;// linhas de dialogo do bloco de falas
        public bool temOpcoesResposta; // se esse bloco de falas apresenta opcoes de resposta
        public string[] Respostas;  // oções de resposta
        //public int linhaAtual;        // linha atual das falas
        public int respostaEscolhida; // qual foi a resposta escolhida pelo jogador 
        public bool justListen; // bloco onde o jogador só ouve, não fala nada
        public BlocoFalas()
        {
            justListen = true;
            temOpcoesResposta = false;
        }

    }

    public string[] actualDialog;
    public int actualBlock = 0; // o sistema de dialogos contera 'blocos' de textos para cada personagem, sendo alterados dependendo do momento do dialogo no jogo
    public int linhaAtual = 0;

    public BlocoFalas[] Blocos = new BlocoFalas[10];

    int idPersonagem; // id do personagem a qual as falas pertencem

    string[] texts;

    // Use this for initialization
    void Start()
    {
        UpdateCharacter();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Seta as falas corretas para o personagem a qual esse script esta atribuido, atravez da tag
    /// </summary>
    void UpdateCharacter()
    {
        if (gameObject.tag == "Player")
        {
            idPersonagem = Characters.player.id;
        }
        else if (gameObject.tag == "Eric")
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

    #region Init Dialogos Diferentes Personagens
    void DialogosPlayer()
    {
        Blocos[0].linhas = new string[]
        {
            "O que estou fazendo aqui?",
            "Me tira desse jogo por favor"
        };
        Blocos[0].justListen = true;

        Blocos[1].linhas = new string[]
        {
            "Finalmente em casa."
        };
        Blocos[1].justListen = false;
        
        Blocos[2].linhas = new string[]
        {
            "Olá Meninas!"
        };

        Blocos[3].linhas = new string[]
        {
            "Não preciso fazer nada la em cima."
        };

        AtualizarBlocoAtual();
    }

    void DialogosEric()
    {
        Blocos[0].linhas = new string[]
        {
            "Lindo Dia não é?",
            "Como estás?",
            "Poderia me fazer um favor?",
            "Vem aqui pra eu esmagar sua cabeça!"
        };
        Blocos[0].justListen = true;

        Blocos[1].linhas = new string[]
        {
            "Tira esse canudinho do buraco da gaveta e vem aqui"
        };
        Blocos[1].justListen = false;

        AtualizarBlocoAtual();
    }
    void DialogosFilha()
    {

    }
    void DialogosMulher()
    {
        Blocos[0].linhas = new string[]
         {
            "CHEIRA MEU CABELO"

         };
        Blocos[0].justListen = true;
        AtualizarBlocoAtual();
    }

    #endregion

    #region Getters and Setters

    void AtualizarBlocoAtual()
    {

        switch (actualBlock)
        {
            case 0:
                setActualText(Blocos[0].linhas);
                break;
            case 1:
                setActualText(Blocos[1].linhas);
                break;
        }
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
        AtualizarBlocoAtual();
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
        if (linhaAtual == actualDialog.Length - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
