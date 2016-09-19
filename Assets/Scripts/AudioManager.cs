using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    [Header("Arrays de Game Objects")]
    public GameObject[] backgroundMusic;
    public GameObject[] sfx;
    public GameObject[] viktorVoices;
    public GameObject[] thomasVoices;
    public GameObject[] roseVoices;
    public GameObject[] emilyVoices;

    [Header("Arrays de Audio Source")]
    public AudioSource[] backgroundMusicAudioSource;
    public AudioSource[] sfxAudioSource;
    public AudioSource[] viktorVoicesAudioSource;
    public AudioSource[] thomasVoicesAudioSource;
    public AudioSource[] roseVoicesAudioSource;
    public AudioSource[] emilyVoicesAudioSource;

    [Header("Arrays de Lenghts")]
    public float[] backgroundMusicAudioSourceLenght;
    public float[] sfxAudioSourceLenght;
    public float[] viktorVoicesAudioSourceLenght;
    public float[] thomasVoicesAudioSourceLenght;
    public float[] roseVoicesAudioSourceLenght;
    public float[] emilyVoicesAudioSourceLenght;

    private bool isLoaded;


    // Use this for initialization
    void Start () {
        GetAudioSources();
        GetAudioSourcesLenght();

        isLoaded = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Função pra carregar todos os audiosources do jogo
    /// </summary>
    void GetAudioSources()
    {
        //Background Music
        if(backgroundMusic.Length != backgroundMusicAudioSource.Length)
        {
            LogError("GameObjects", "Background", "Audio Source");
        }
        for(int i = 0; i < backgroundMusic.Length; i++)
        {
            backgroundMusicAudioSource[i] = backgroundMusic[i].GetComponent<AudioSource>();
        }

        //Sound Effects
        if (sfx.Length != sfxAudioSource.Length)
        {
            LogError("GameObjects", "Sounde Effects", "Audio Source");
        }
        for (int i = 0; i < sfx.Length; i++)
        {
            sfxAudioSource[i] = sfxAudioSource[i].GetComponent<AudioSource>();
        }

        //Viktor Voices
        if (viktorVoices.Length != viktorVoicesAudioSource.Length)
        {
            LogError("GameObjects", "Viktor Voices", "Audio Source");
        }
        for (int i = 0; i < viktorVoices.Length; i++)
        {
            viktorVoicesAudioSource[i] = viktorVoicesAudioSource[i].GetComponent<AudioSource>();
        }

        //Thomas Voices
        if (thomasVoices.Length != thomasVoicesAudioSource.Length)
        {
            LogError("GameObjects", "Thomas Voices", "Audio Source");
        }
        for (int i = 0; i < thomasVoices.Length; i++)
        {
            thomasVoicesAudioSource[i] = thomasVoicesAudioSource[i].GetComponent<AudioSource>();
        }

        //Rose Voices
        if (roseVoices.Length != roseVoicesAudioSource.Length)
        {
            LogError("GameObjects", "Rose Voices", "Audio Source");
        }
        for (int i = 0; i < roseVoices.Length; i++)
        {
            roseVoicesAudioSource[i] = roseVoicesAudioSource[i].GetComponent<AudioSource>();
        }

        //Emily Voices
        if (emilyVoices.Length != emilyVoicesAudioSource.Length)
        {
            LogError("GameObjects", "Emily Voices", "Audio Source");
        }
        for (int i = 0; i < emilyVoices.Length; i++)
        {
            emilyVoicesAudioSource[i] = emilyVoicesAudioSource[i].GetComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Função pra carregar todos os comprimentos dos audiosources do jogo
    /// </summary>
    void GetAudioSourcesLenght()
    {
        //Background Music
        if (backgroundMusicAudioSourceLenght.Length != backgroundMusicAudioSource.Length)
        {
            LogError("Audio Source", "Background Music", "Audio Lengths");
        }
        for (int i = 0; i < backgroundMusicAudioSource.Length; i++)
        {
            backgroundMusicAudioSourceLenght[i] = backgroundMusicAudioSource[i].clip.length;
        }

        //Sound Effects
        if (sfxAudioSourceLenght.Length != sfxAudioSource.Length)
        {
            LogError("Audio Source", "Sound Effects", "Audio Lengths");
        }
        for (int i = 0; i < sfx.Length; i++)
        {
            sfxAudioSourceLenght[i] = sfxAudioSource[i].clip.length;
        }

        //Viktor Voices
        if (viktorVoicesAudioSourceLenght.Length != viktorVoicesAudioSource.Length)
        {
            LogError("Audio Source", "Viktor Voices", "Audio Lengths");
        }
        for (int i = 0; i < viktorVoices.Length; i++)
        {
            viktorVoicesAudioSourceLenght[i] = viktorVoicesAudioSource[i].clip.length;
        }

        //Thomas Voices
        if (thomasVoicesAudioSourceLenght.Length != thomasVoicesAudioSource.Length)
        {
            LogError("Audio Source", "Thomas Voices", "Audio Lengths");
        }
        for (int i = 0; i < thomasVoices.Length; i++)
        {
            thomasVoicesAudioSource[i] = thomasVoicesAudioSource[i].GetComponent<AudioSource>();
        }

        //Rose Voices
        if (roseVoicesAudioSourceLenght.Length != roseVoicesAudioSource.Length)
        {
            LogError("Audio Source", "Rose Voices", "Audio Lengths");
        }
        for (int i = 0; i < roseVoices.Length; i++)
        {
            roseVoicesAudioSourceLenght[i] = roseVoicesAudioSource[i].clip.length;
        }

        //Emily Voices
        if (emilyVoicesAudioSourceLenght.Length != emilyVoicesAudioSource.Length)
        {
            LogError("Audio Source", "Emily Voices", "Audio Lengths");
        }
        for (int i = 0; i < emilyVoices.Length; i++)
        {
            emilyVoicesAudioSourceLenght[i] = emilyVoicesAudioSource[i].clip.length;
        }
    }

    /// <summary>
    /// Função para printar erros durante o carregamento
    /// </summary>
    /// <param name="firstArray"></param>
    /// <param name="nameArray"></param>
    /// <param name="secondArray"></param>
    void LogError(string firstArray, string nameArray, string secondArray)
    {
        Debug.LogError("O tamanho do array de "+firstArray+" de "+ nameArray +" é diferente do tamanho de arrays de "+secondArray+" deles");
    }

    /// <summary>
    /// Reproduz um efeito sonoro específico e o desativa depois de estar parado
    /// </summary>
    /// <param name="indexSoundEffect"></param>
    public void PlaySoundEffect(int indexSoundEffect)
    {
        //ativar gameobject
        sfx[indexSoundEffect].SetActive(true);
        //tocar audiosource dele desde o inicio
        sfxAudioSource[indexSoundEffect].Play();
        //depois que terminar desativar gameobject
        DesactivateSfx(sfxAudioSourceLenght[indexSoundEffect], indexSoundEffect);
    }

    /// <summary>
    /// Desativa o GameObject do array SFX
    /// </summary>
    /// <param name="timeToWait"></param> Tempo para esperar antes de desativar
    /// <param name="indexSoundEffect"></param> Index de quem você vai desativar dentro de SFX
    /// <returns></returns>
    IEnumerator DesactivateSfx(float timeToWait, int indexSoundEffect)
    {
        yield return new WaitForSeconds(timeToWait + 0.25f);
        sfxAudioSource[indexSoundEffect].Stop();
        sfx[indexSoundEffect].SetActive(false);
    }

    /// <summary>
    /// Reproduz uma trilha de fundo específica
    /// </summary>
    /// <param name="stopCurrents"></param> Se deseja parar as trilhas que estão tocando no momento
    /// <param name="indexBGM"></param>
    public void PlayBGM(bool stopCurrents, int indexBGM, bool playLoopping)
    {
        //ativa o gameobject dele
        backgroundMusic[indexBGM].SetActive(true);

        //se deseja parar as trilhas que estão tocando
        if (stopCurrents)
        {
            // Para todos os AudioSources do array BackgroundMusicAudioSource
            for (int i = 0; i < backgroundMusicAudioSource.Length; i++)
            {
                //para todos menos para o gameobject do index que estou passando
                if (indexBGM != i)
                {
                    //dou stop na música
                    backgroundMusicAudioSource[i].Stop();
                    //desativo o gameobject
                    backgroundMusic[i].SetActive(false);
                }
            }
        }

        //se deseja tocar em looping
        if (playLoopping)
        {
            //seta o bgm para tocar em looping
            backgroundMusicAudioSource[indexBGM].loop = true;
        } else
        {
            //seta o bgm para nao tocar em looping
            backgroundMusicAudioSource[indexBGM].loop = false;
            StopBGMAfterEnd(backgroundMusicAudioSourceLenght[indexBGM], indexBGM);
        }
    }

    /// <summary>
    /// Desativa o Gameobject do array BackgroundMusic
    /// </summary>
    /// <param name="time"></param> Tempo para esperar antes de desativar
    /// <param name="indexBGM"></param> Index de quem você vai desativar dentro de BackgroundMusic
    /// <returns></returns>
    IEnumerator StopBGMAfterEnd(float time, int indexBGM)
    {
        yield return new WaitForSeconds(time + 0.25f);
        backgroundMusicAudioSource[indexBGM].Stop();
        backgroundMusic[indexBGM].SetActive(false);
    }

    public void PlayDialog(string CharacterName, int indexDialog)
    {
        switch (CharacterName)
        {
            //Para todos eles:
            // Ativa o GameObject
            //Toca o Audio Source
            //Chama o Desativador

            case "Viktor":
                viktorVoices[indexDialog].SetActive(true);
                viktorVoicesAudioSource[indexDialog].Play();
                break;
            case "Thomas":
                thomasVoices[indexDialog].SetActive(true);
                thomasVoicesAudioSource[indexDialog].Play();
                break;
            case "Rose":
                roseVoices[indexDialog].SetActive(true);
                roseVoicesAudioSource[indexDialog].Play();
                break;
            case "Emily":
                emilyVoices[indexDialog].SetActive(true);
                emilyVoicesAudioSource[indexDialog].Play();
                break;
        }
    }

    IEnumerator DesactivateDialog(float time, int indexDialog, int CharacterIndex)
    {
        yield return new WaitForSeconds(time);
        switch (CharacterIndex)
        {
            //viktor
            case 1:
                viktorVoicesAudioSource[indexDialog].Stop();
                viktorVoices[indexDialog].SetActive(false);
                break;
            //thomas
            case 2:
                thomasVoicesAudioSource[indexDialog].Stop();
                thomasVoices[indexDialog].SetActive(false);
                break;
            //Rose
            case 3:
                roseVoicesAudioSource[indexDialog].Stop();
                roseVoices[indexDialog].SetActive(false);
                break;
            //Emily
            case 4:
                emilyVoicesAudioSource[indexDialog].Stop();
                emilyVoices[indexDialog].SetActive(false);
                break;
        }
    }
    
}
