using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject IAShipPrefab;
    public GameObject playerShipPrefab;
    public ShipManager[] spaceships;


    // Use this for initialization
    void Start () {
        SpawnAllShips();
    }

    private void SpawnAllShips()
    {
        // For all IAShips...
        for (int i = 0; i < spaceships.Length-1; i++) // el menos uno es para erservar el ultimo espacio para el jugador
        {
            // ... create them, set their player number and references needed for control.
            spaceships[i].m_Instance =
                Instantiate(IAShipPrefab, spaceships[i].SpawnPoint.position, spaceships[i].SpawnPoint.rotation) as GameObject;
            spaceships[i].ShipNumber = i + 1;
            spaceships[i].Setup();
        }
        spaceships[spaceships.Length - 1].m_Instance =
    Instantiate(playerShipPrefab, spaceships[spaceships.Length - 1].SpawnPoint.position, spaceships[spaceships.Length - 1].SpawnPoint.rotation) as GameObject;
        spaceships[spaceships.Length - 1].ShipNumber = spaceships.Length;
        spaceships[spaceships.Length-1].Setup(); // para el jugador, que es el ultimo del array.
    }

    // This function is used to turn all the tanks back on and reset their positions and properties.
    private void ResetAllShips()
    {
        for (int i = 0; i < spaceships.Length; i++)
        {
            spaceships[i].Reset();
        }
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < spaceships.Length - 1; i++) // el menos uno es para erservar el ultimo espacio para el jugador
        {
            spaceships[i].UpdateCrashOrOut();
        }
    }
}
