using UnityEngine;
using System.Collections;

public class VoicesManager : MonoBehaviour {

    public GameObject[] voices;
    public AudioSource[] voicesAudioSource;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayVoice(int indexVoice)
    {
        voicesAudioSource[indexVoice].Play();
    }
}
