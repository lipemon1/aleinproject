using UnityEngine;
using System.Collections;

public class SfxManager : MonoBehaviour {

    public GameObject[] sfx;
    public AudioSource[] sfxAudioSource;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopAll()
    {
        for (int i = 0; i < sfxAudioSource.Length; i++)
        {
            sfxAudioSource[i].Stop();
        }
    }

    public void PlaySfx(int indexVoice, bool loop)
    {
        sfxAudioSource[indexVoice].loop = loop;
        sfxAudioSource[indexVoice].Play();
    }
}
