using UnityEngine;
using System.Collections;

public class ClickableObjectsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //RaycastHit2D hit = new RaycastHit2D();

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("Click esquerdo");
        //    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        //    if (hit.collider != null)
        //    {
        //        Debug.LogWarning("acertei");
        //    }
        //}

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "clickable")
            {
                Debug.LogWarning("Deucerto");
            }
        }

        //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        //Debug.Log("mouse pos " + mousePosition.x + " y " + mousePosition.y + " ");

        //if (hit != null && hit.collider != null)
        //{
        //    Debug.Log("I'm hitting " + hit.collider.name);

        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        GameObject objetoSelecionado = hit.collider.gameObject;
        //        if (objetoSelecionado.GetComponent<ClickableObject>().tag == "clickable")
        //        {
        //            Debug.Log("Bunda");
        //        }
        //    }

        //}

    }
    void OnMouseDown()
    {
        Debug.Log("Sprite Clicked");
    }
}
