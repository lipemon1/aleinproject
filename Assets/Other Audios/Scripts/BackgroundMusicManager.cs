using UnityEngine;
using System.Collections;

public class BackgroundMusicManager : MonoBehaviour {

    public GameObject[] bgm;
    public AudioSource[] bgmAudioSource;
    public PlayerBehaviour pb;
    public GameController gc;
    public SfxManager sfx;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PlayBGM(3, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (sfx == null)
        {
            sfx = GameObject.FindWithTag("SFX").GetComponent<SfxManager>();
        }

        
        if (pb == null)
        {
            pb = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
        }
        if (gc == null)
        {
            gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        }
    }

    public void PlayBGM(int indexVoice, bool Loop)
    {
        bgmAudioSource[indexVoice].loop = Loop;
        bgmAudioSource[indexVoice].Play();
    }

    public void MeteoroCaiu()
    {
        bgmAudioSource[3].Stop();
        PlayBGM(5, true);
        bgmAudioSource[5].volume = 0.6f;
    }

    public void SaiuDaCasaAposMeteoro()
    {
        StopAll();
        bgmAudioSource[3].volume = 0.2f;
        sfx.PlaySfx(7, true);
    }

    public void StopAll()
    {
        for(int i = 0; i < bgmAudioSource.Length; i++)
        {
            bgmAudioSource[i].Stop();
            sfx.StopAll();
        }
    }

    public void Quarentena()
    {
        sfx.PlaySfx(9, true);
        sfx.sfxAudioSource[9].volume = 0.15f;

        PlayBGM(5, true);
        bgmAudioSource[5].volume = 0.2f;
    }

    public void Fugindo()
    {
        StopAll();
        sfx.PlaySfx(9, true);
        sfx.sfxAudioSource[9].volume = 0.15f;

        PlayBGM(2, true);
        bgmAudioSource[2].volume = 0.4f;
    }

    public void VaiFugir()
    {
        StopAll();
        PlayBGM(4, true);
        bgmAudioSource[4].volume = 0.6f;
    }

    public void Casa()
    {
        StopAll();
        PlayBGM(0, true);
        bgmAudioSource[0].volume = 0.6f;
    }
}
