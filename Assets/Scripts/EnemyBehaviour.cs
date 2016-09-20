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

    public void KillEnemy()
    {
        if(doctor && doctorAi != null)
        {
            doctorAi.KillDoctor();
        }

        if (soldier && soldierAi != null)
        {
            soldierAi.KillSoldier();
        }
    }
}
