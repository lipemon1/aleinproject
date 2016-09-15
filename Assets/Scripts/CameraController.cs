
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float leftBorder;
    public float rightBorder;
    public float topBorder;
    public float bottomBorder;
    [Range(-3f, 3f)]
    public float playerOffsetX;
    [Range(-3f,3f)]
    public float playerOffsetY;

	void Start ()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
	
	}
		
	void Update ()
    {
        //FOLLOW_PLAYER
	    if(this.transform.position.x >= leftBorder && this.transform.position.x <= rightBorder &&
           this.transform.position.y >= bottomBorder && this.transform.position.y <= topBorder)
        {
            this.transform.position = new Vector3
            ((player.transform.position.x + playerOffsetX), (player.transform.position.y + playerOffsetY), this.transform.position.z);
        }
        //BOUNDARIES
        if(this.transform.position.x < leftBorder)
        {
            this.transform.position = new Vector3(leftBorder, this.transform.position.y, this.transform.position.z);
        }
        if(this.transform.position.x > rightBorder)
        {
            this.transform.position = new Vector3(rightBorder, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.y < bottomBorder)
        {
            this.transform.position = new Vector3(this.transform.position.x, bottomBorder, this.transform.position.z);
        }
        if (this.transform.position.y > topBorder)
        {
            this.transform.position = new Vector3(this.transform.position.x, topBorder, this.transform.position.z);
        }
    }

}