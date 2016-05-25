using UnityEngine;
using System.Collections;

public class FuelScript : MonoBehaviour {

    private PlayerMovement pm;

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(new Vector3(1,0,1));
	}

    void OnTriggerEnter(Collider player) {
        if (player.gameObject.GetComponent<PlayerMovement>() != null)
        {
            pm = player.gameObject.GetComponent<PlayerMovement>();

            if (pm.fuel <= 80)
            {
                pm.fuel += 20;
            }
            else
            {
                pm.fuel = 100;
            }
        }
 
    }

}
