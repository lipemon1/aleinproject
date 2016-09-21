using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public bool doctor;
    public bool soldier;

    public DoctorAI doctorAi;
    public SoldierAI soldierAi;

    // Use this for initialization
    void Start () {
        if (doctor && doctorAi == null)
        {
            doctorAi = GetComponent<DoctorAI>();
        }

        if (soldier && soldierAi == null)
        {
            soldierAi = GetComponent<SoldierAI>();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void DesableAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }

    public void KillEnemy()
    {
        Debug.LogWarning("Kill enemy");

        
        GetComponent<Collider2D>().enabled = false;

        if (doctor && doctorAi != null)
        {
            Invoke("DesableAnimator", 1.8f);
            //doctorAi.isAlive = false;
            doctorAi.KillDoctor();
            doctorAi.enabled = false;
            Destroy(doctorAi.gameObject.GetComponent("DoctorAI"));
        }

        if (soldier && soldierAi != null)
        {
            Invoke("DesableAnimator", 1.0f);
            soldierAi.isAlive = false;
            soldierAi.KillSoldier();
            soldierAi.enabled = false;
            Destroy(soldierAi.gameObject.GetComponent("SoldierAI"));
        }
    }
}
