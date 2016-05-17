using UnityEngine;
using System.Collections;

public class WaypointScrpit : MonoBehaviour {

    IAMovement IAShip;

    void OnTriggerEnter(Collider ship) {

        Debug.Log("Algo ha entrado en el" + this.gameObject.name);

        if (ship.gameObject.GetComponent<IAMovement>() != null) //para evitar el null reference error
        {
            IAShip = ship.gameObject.GetComponent<IAMovement>();
            IAShip.nextWaypoint();
        }

    }

}
