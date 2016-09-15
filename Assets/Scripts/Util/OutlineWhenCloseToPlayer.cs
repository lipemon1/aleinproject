using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class OutlineWhenCloseToPlayer : MonoBehaviour {

    public float distanciaMinima = 3f;
    float distanciaPlayer;
    Transform playerTransform;

    public Color color = Color.white;

    [Range(0, 16)]
    public int outlineSize = 1;

    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        //UpdateOutline(true);

        distanciaPlayer = Vector2.Distance(playerTransform.position, transform.position);
	    if(distanciaPlayer < distanciaMinima)
        {
            
            UpdateOutline(true);
        }
        else
        {
            UpdateOutline(false);
        }
	}
    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateOutline(true);
    }

    void OnDisable()
    {
        UpdateOutline(false);
    }

    void UpdateOutline(bool outline)
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", color);
        mpb.SetFloat("_OutlineSize", outlineSize);
        spriteRenderer.SetPropertyBlock(mpb);
    }
}
