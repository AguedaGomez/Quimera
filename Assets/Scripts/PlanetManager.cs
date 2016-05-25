using UnityEngine;
using System.Collections;

public class PlanetManager : MonoBehaviour {

    private Transform planetTransform;
    private GameObject[] alienShips;
    private GameObject player;
    private Vector3 gravityStrength;

    private void Awake()
    {
        planetTransform = GetComponent<Transform>();
        gravityStrength = Vector3.one; 
    }

    private void FixedUpdate()
    {
        rotatePlanet();
        planetGravity();
    }

    private void rotatePlanet()
    {
        float rotationAxis = 6f * Time.deltaTime;
        Vector3 rotationVector = new Vector3(rotationAxis, 0f, rotationAxis);
        planetTransform.Rotate(rotationVector);
    }

    private void planetGravity() {
        player = GameObject.FindGameObjectWithTag("Player");
        alienShips = GameObject.FindGameObjectsWithTag("SpaceShip");

        if ( Vector3.Distance( player.transform.position , planetTransform.position ) < 300)
        {
            player.transform.position = Vector3.SmoothDamp(player.transform.position, planetTransform.position,ref  gravityStrength , 0.1f, 50.0f); //el ultimo parametro es el que controla la fuerza de la gravedad y no gravityStrength.
            if (Vector3.Distance(player.transform.position, planetTransform.position) < 100)
            {
                player.GetComponent<PlayerMovement>().IncreaseSpeed();
            }
            }
    }

    void OnTriggerEnter(Collider ship)
    {
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().raceHasStarted)
        {
            if (ship.gameObject.GetComponent<IAMovement>() != null) //para evitar el null reference error
            {
                ship.gameObject.GetComponent<IAMovement>().CrashOrOut();
            }
            else
            {
                player.gameObject.GetComponent<PlayerMovement>().CrashOrOut();
            }
        }
        

    }
}
