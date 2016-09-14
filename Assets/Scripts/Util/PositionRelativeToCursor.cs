using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PositionRelativeToCursor : MonoBehaviour {

    public float x_offset;
    public float y_offset;
    private Vector2 mousePosition;
    RectTransform myPosition;
    public Canvas myCanvas;
    private Vector2 mousePositionAux;

    // Use this for initialization
    void Start () {
        myPosition = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos;
        mousePositionAux = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, mousePositionAux, myCanvas.worldCamera, out pos);
        Vector2 PosAux = new Vector2(pos.x + (x_offset * 10), pos.y + (y_offset * 10));
        transform.position = myCanvas.transform.TransformPoint(PosAux);
    }
}