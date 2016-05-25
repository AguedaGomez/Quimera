using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject IAShipPrefab;
    public GameObject playerShipPrefab;
    public ShipManager[] spaceships;

    private ShipPositionManager[] spm;
    public bool raceHasStarted = false;

    public int[,] shipsPositionsInRaceData;
    public int[] shipsPositionsInRace;


    // Use this for initialization
    void Start()
    {
        SpawnAllShips();
        shipsPositionsInRaceData = new int[spaceships.Length, 4];
        shipsPositionsInRace = new int[spaceships.Length]; // en la posicion cero ira el identificador de la nave que vaya primero, en la uno el segundo... (el jugador es el identificador 5)
        for (int i = 0; i < spaceships.Length; i++)
        {
            spaceships[i].FreezeShip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (raceHasStarted)
        {
            for (int i = 0; i < spaceships.Length; i++)
            {
                spaceships[i].UpdateCrashOrOut();
            }
            RacePosition();
        }

        if (Input.GetButton("Cancel"))
        {
            Application.LoadLevel("MainMenu");
        }

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
            //spm[i] = spaceships[i].m_Instance.GetComponent<ShipPositionManager>();
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

    private void RacePosition()//Recordar para este algoritmo que el jugador es el spaceships[5] (el ultimo del array).
    {
        int aux;
        for (int i = 0; i < spaceships.Length; i++) //relleno la matriz con los datos que hacen falta para calcular la posicion de todas las naves
        {
            shipsPositionsInRaceData[i, 0] = spaceships[i].m_Instance.GetComponent<ShipPositionManager>().lap; // 0 = vuelta
            shipsPositionsInRaceData[i, 1] = spaceships[i].m_Instance.GetComponent<ShipPositionManager>().cp;  // 1 = ultimo CP cruzado
            shipsPositionsInRaceData[i, 2] = spaceships[i].m_Instance.GetComponent<ShipPositionManager>().distanceNextCP; //  2 = distancia hasta el siguiente CP
            for (int posicion = 0; posicion < shipsPositionsInRace.Length; posicion++)
            {
                if (shipsPositionsInRace[posicion] == i)
                {
                    shipsPositionsInRaceData[i, 3] = posicion;  // 3 = posicion actual en la carrera
                }
            }
        }
        for (int i = 0; i < spaceships.Length; i++)
        {
            for (int j = 0; j < spaceships.Length; j++)
            {
                if (j != i)
                {// para evitar comparacion consigo mismo
                    if (shipsPositionsInRaceData[i, 3] < shipsPositionsInRaceData[j, 3]) // si mi posicion es inferior
                    {
                        if (shipsPositionsInRaceData[i, 0] > shipsPositionsInRaceData[j, 0])// si he dado una vuelta más que tu, intercambiamos la posicion.
                        {
                            aux = shipsPositionsInRaceData[i, 3];
                            shipsPositionsInRaceData[i, 3] = shipsPositionsInRaceData[j, 3];
                            shipsPositionsInRaceData[j, 3] = aux;
                            //Debug.Log("La nave nº " + i + " intercambia la pos con la nave nº " + j);
                        }
                        else if (shipsPositionsInRaceData[i, 0] == shipsPositionsInRaceData[j, 0])
                        {
                            if (shipsPositionsInRaceData[i, 1] > shipsPositionsInRaceData[j, 1] || shipsPositionsInRaceData[i, 1] == 0 && shipsPositionsInRaceData[j, 1] == 9) // si hemos dado las mismas vueltas pero yo he cruzado un CP que tú aún no, intercambiamos la posicion. + Caso particular 0 !> 9 asi que hay que darle tratamiendo especifico.
                            {
                                aux = shipsPositionsInRaceData[i, 3];
                                shipsPositionsInRaceData[i, 3] = shipsPositionsInRaceData[j, 3];
                                shipsPositionsInRaceData[j, 3] = aux;
                                //Debug.Log("La nave nº " + i + " intercambia la pos con la nave nº " + j);

                            }
                            else if (shipsPositionsInRaceData[i, 1] == shipsPositionsInRaceData[j, 1] && shipsPositionsInRaceData[i, 2] < shipsPositionsInRaceData[j, 2])
                            { // si hemos cruzado el mismo CP pero mi distancia hasta el siguiente es menor, intercambiamos la posicion.
                                aux = shipsPositionsInRaceData[i, 3];
                                shipsPositionsInRaceData[i, 3] = shipsPositionsInRaceData[j, 3];
                                shipsPositionsInRaceData[j, 3] = aux;
                            }
                        }                       
                    }                 
                }
            }
        }
        for (int i = 0; i < shipsPositionsInRace.Length; i++)
        {
            shipsPositionsInRace[shipsPositionsInRaceData[i, 3]] = i;
        }

        for (int i = 0; i < spaceships.Length - 1; i++)
        {
            spaceships[i].m_Instance.GetComponent<IAMovement>().myPosInRace = shipsPositionsInRaceData[i, 3];
            spaceships[i].m_Instance.GetComponent<IAMovement>().playerPosInRace = shipsPositionsInRaceData[5, 3];
        }
    }

    public void StartRace() {
        for (int i = 0; i < spaceships.Length ; i++)
        {
            spaceships[i].UnFreezeShip();
        }
        raceHasStarted = true;
    }

    public void RaceFinished() {
        for (int i = 0; i < spaceships.Length; i++)
        {
            spaceships[i].FreezeShip();
        }
    }

}
