using UnityEngine;
using System.Collections;

public enum Direcao { DIREITA, ESQUERDA}

public class ChangeCursorImage : MonoBehaviour {
    public Direcao direcaoCursor;
    public Texture2D cursorDireita;
    public Texture2D cursorEsquerda;
    private Texture2D cursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    
    void Start()
    {
        if (direcaoCursor == Direcao.DIREITA)
        {
            cursor = cursorDireita;
        }
        else if (direcaoCursor == Direcao.ESQUERDA)
            cursor = cursorEsquerda;
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, hotSpot, cursorMode);
    }
    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}