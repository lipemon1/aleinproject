using UnityEngine;
using System.Collections;

public class bulletBehaviour : MonoBehaviour
{

    public float speed;
    public GameObject player;
    public bool canGo = false;
    public float damage = 5.0f;
    public int direcao; // 1 direita 0 esquerda

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //if(player != null)
        //{
        //    canGo = true;
        //}
        canGo = true;

        //DestroyItSelf();
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (canGo)
        {
            //if(transform.position.x < player.transform.position.x)
            //{
            //    transform.Translate(new Vector3(1 * speed * Time.deltaTime, 0f, 0f));
            //}

            //if (transform.position.x > player.transform.position.x)
            //{
            //    transform.Translate(new Vector3(-1 * speed * Time.deltaTime, 0f, 0f));
            //}
            if(direcao == 1)
                transform.Translate(new Vector3(1 * speed * Time.deltaTime, 0f, 0f));
            else if(direcao == 0)
                transform.Translate(new Vector3(-1 * speed * Time.deltaTime, 0f, 0f));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Eu bala, atingi ALGUMA COISA");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Eu bala, atingi player");
            player.GetComponent<PlayerBehaviour>().AplicarDano(damage);
            Destroy(gameObject);
        }
    }

    public void SetDirecao(int valor)
    {
        direcao = valor;
    }

    IEnumerator DestroyItSelf()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
