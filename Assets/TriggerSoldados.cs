using UnityEngine;
using System.Collections;

public class TriggerSoldados : MonoBehaviour {

    public GameObject[] soldier;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActivateSoldier();
            Debug.Log("Ativar Soldados");
        }
    }

    void ActivateSoldier()
    {
        for(int i = 0; i < soldier.Length; i++)
        {
            soldier[i].SetActive(true);
        }
    }
}
