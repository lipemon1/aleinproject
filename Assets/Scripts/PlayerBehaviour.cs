using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerBehaviour : MonoBehaviour {

    public bool onMobile;

    public float walkingSpeed;
    public float runningSpeed;
    public float sneakingSpeed;

    public float actualSpeed;

    public bool running;
    public bool sneaking;
    public bool walking;

    private float directionX;
    

	// Use this for initialization
	void Start () {
        walking = true;
        sneaking = false;
        running = false;

	}
	
	// Update is called once per frame
	void Update () {
        if (!onMobile) { 
            directionX = Input.GetAxis("Horizontal");
        }
        
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleRun();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleSneaking();
        }

        actualSpeed = walkingSpeed;
        walking = true;

        changeSpeed();

        transform.Translate(new Vector3(directionX * actualSpeed * Time.deltaTime, 0f, 0f));
	}

    void changeSpeed()
    {
        if(running)
        {
            actualSpeed = runningSpeed;
        }
        else if (sneaking)
        {
            actualSpeed = sneakingSpeed;
        }
        else if(walking)
        {
            actualSpeed = walkingSpeed;
        }
        

    }

    public void SetDirectionX(float direction)
    {
        directionX = direction;
    }

    public void ToggleRun()
    {
        running = !running;
        walking = false;
        sneaking = false;

    }
    public void ToggleWalking()
    {
        walking = !walking;
        running = false;
        sneaking = false;
    }
    public void ToggleSneaking()
    {
        sneaking = !sneaking;
        running = false;
        walking = false;
    }
}
