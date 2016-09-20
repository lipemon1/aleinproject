using UnityEngine;
using System.Collections;

public class bulletBehaviour : MonoBehaviour {

    public float speed;
    public GameObject player;
    public bool canGo = false;
    public float damage = 5.0f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");

        if(player != null)
        {
            canGo = true;
        }

        DestroyItSelf();
	}
	
	// Update is called once per frame
	void Update () {
        if (canGo)
        {
            if(transform.position.x < player.transform.position.x)
            {
                transform.Translate(new Vector3(1 * speed * Time.deltaTime, 0f, 0f));
            }

            if (transform.position.x > player.transform.position.x)
            {
                transform.Translate(new Vector3(-1 * speed * Time.deltaTime, 0f, 0f));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")){ 
            player.GetComponent<PlayerBehaviour>().AplicarDano(damage);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyItSelf()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
