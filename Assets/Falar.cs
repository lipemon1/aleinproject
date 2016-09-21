using UnityEngine;
using System.Collections;

public class Falar : MonoBehaviour {

    public GameController gameController;
    public VoicesManager vm;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnMouseDown()
    {
        if (gameController.activeItem == "Copo" && gameController.temCopo)
        {
            vm.PlayVoice(9);
        }
    }
}
