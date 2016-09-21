using UnityEngine;
using System.Collections;

public class VoicesManager : MonoBehaviour {

    public DialogueBoxManager dialog;

    public GameObject[] voices;
    public AudioSource[] voicesAudioSource;
    public BackgroundMusicManager bgm;
    public SfxManager sfx;

	// Use this for initialization
	void Start () {
        if(dialog == null)
        {
            dialog = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogueBoxManager>();
        }


        if (bgm == null)
        {
            bgm = GameObject.FindWithTag("BGM").GetComponent<BackgroundMusicManager>();
        }
        if (sfx == null)
        {
            sfx = GameObject.FindWithTag("SFX").GetComponent<SfxManager>();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayVoice(int indexVoice)
    {

        for(int i = 0; i < voicesAudioSource.Length; i++)
        {
            voicesAudioSource[i].Stop();
        }

        voicesAudioSource[indexVoice].Play();

        if(indexVoice == 6)
        {
            Invoke("FalarAtrasado7", 4.0f);
            Invoke("FalarAtrasado8", 5.0f);
        }
    }

    public void FalaSelecionada(string fala)
    {
        switch (fala)
        {
            case "Aqui está querida.":
                PlayVoice(10);
                break;

            case "Obrigado amor. Agora leve a Emily para o quarto por favor.":
                PlayVoice(11);
                break;

            case "Vamos para a cama sua bagunceira.":
                PlayVoice(12);
                break;

            case "Como foi hoje na escola filha?":
                PlayVoice(14);
                break;

            case "Foi legal, hoje aprendemos um pouco sobre flores.":
                PlayVoice(15);
                break;

            case "O dia só começou a ficar esquisito quando estava voltando pra casa.":
                PlayVoice(16);
                break;

            case "Vi várias luzes no céu, elas estavam girando muito rápido, depois de um tempo sumiram.":
                PlayVoice(17);
                break;

            case "Fiquei com medo e vim correndo para casa. Contei à mamãe mas ela não acreditou em mim...":
                PlayVoice(18);
                break;

            case "Não se preocupe querida, deviam ser apenas aviões ou algo parecido.":
                PlayVoice(19);
                Invoke("ApertarOk", 3.5f);
                break;

            case "Agora deite-se e durma bem. Amanhã precisamos acordar cedo para buscar algumas ferramentas na casa do seu tio":
                
                break;

            case "Papai, estou com medo de ficar sozinha. E se as luzes voltarem a aparecer?":
                PlayVoice(20);
                break;

            case "E se elas me pegarem? Estou com um mau pressentimento!":
                PlayVoice(21);
                break;

            case "Não se preocupe Emily.":
                PlayVoice(22);
                Invoke("ApertarOk", 1.0f);
                break;

            case "Papai vai deixar a porta aberta para que se qualquer coisa acontecer você avisar a mim e sua mãe.":
                break;

            case "PAPAAAAAAAI!!":
                PlayVoice(23);
                break;

            case "O que foi isso??":
                PlayVoice(40);
                break;

            case "Fiquem aqui dentro, vou lá fora ver o que houve":
                PlayVoice(24);
                Invoke("ApertarOk", 2.0f);
                break;

            case "Se acontecer alguma coisa gritem que eu volto correndo!":
                break;

            default:
                Debug.LogError("Fala não encontrada");
                break;

            case "HEY! ALGUÉM ESTÁ AI??":
                PlayVoice(26);
                break;

            case "Finalmente. Depois de 2 meses no coma você acordou.":
                PlayVoice(27);
                break;

            case "Dois meses? Do que está falando? Quem é você? Onde estou e como vim parar aqui? O que aconteceu com...":
                PlayVoice(28);
                break;

            case "Acalme-se meu caro. Sou o doutor responsável por controlar a sua mutação. ":
                PlayVoice(29);
                Invoke("ApertarOk", 5.0f);
                break;

            case "Você foi atingido quase que fatalmente por um pedaço daquela máquina que caiu em seu terreno.":
                Invoke("ApertarOk", 5.0f);
                break;

            case "Além disso, inalou um poderoso e desconhecido gás proveniente dela.":
                Invoke("ApertarOk", 4.5f);
                break;

            case "É incrível você ainda estar vivo.":
                Invoke("ApertarOk", 3.0f);
                break;

            case "Grande parte de sua cura foi ocasionada pelo estranho gás.":
                Invoke("ApertarOk", 3.0f);
                break;

            case "Vamos descobrir muito sobre ele com você.":
                
                break;

            case "O que fizeram com minha esposa e filha? Onde elas estão?":
                PlayVoice(30);
                break;

            case "Elas não sofreram nada com o acidente, estão são e salvas em sua casa.":
                PlayVoice(31);
                Invoke("ApertarOk", 4.0f);
                break;

            case "Mas precisamos de você aqui. Temos muito para investigar ainda.":
                
                break;

            case "Não, eu quero ver a minha família. Quero sair daqui agora!":
                PlayVoice(32);
                break;

            case "Nós não podemos deixar você sair antes de encerrar o tratamento.":
                PlayVoice(33);
                break;

            case "NÃO! Eu vou sair daqui agora!":
                PlayVoice(34);
                bgm.VaiFugir();
                break;


                
        }
    }

    void ApertarOk()
    {
        dialog.OkButtonPressed();
    }

    void FalarAtrasado7()
    {
        PlayVoice(7);
    }

    void FalarAtrasado8()
    {
        PlayVoice(8);
    }


    IEnumerator FalarAtrasado(int indexFala, float time)
    {
        yield return new WaitForSeconds(time);
        PlayVoice(indexFala);
    }
}
