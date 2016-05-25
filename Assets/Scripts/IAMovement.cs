using UnityEngine;
using System.Collections;

public class IAMovement : MonoBehaviour {

    private GameObject waypoint;
    private GameObject player;
    private Vector3 shipPosition;
    private Vector3 vectorRef;
    public float speed;
    public int myPosInRace;
    public int playerPosInRace;

	// Use this for initialization
	void Awake () {

        waypoint = GameObject.Find(nextWP("WayPoint000"));
        vectorRef = Vector3.one;
        speed = 120f;
    }
	
	// Update is called once per frame
	void Update () {
        speed = 120f;
        Move();
	}

    private void Move() {
        float myPos = myPosInRace;
        float playerPos = playerPosInRace;
        player = GameObject.FindGameObjectWithTag("Player");
        speed += player.GetComponent<PlayerMovement>().m_Speed / 3; // añadirle un poco de la velocidad del player para que no se queden muy atras si voy muy rápido.

        if (myPosInRace > playerPosInRace) // si voy por delante del player
        {
            speed = Mathf.Abs( speed * (1.0f - ((myPos - playerPos)/10))); // voy ligeramente mas despacio
        }
        else { // si voy por detras
            speed = speed * (1.0f + ((playerPos - myPos)/10)); // voy ligeramente mas rapido
        }
        shipPosition = this.transform.position;
        this.transform.position = Vector3.SmoothDamp(shipPosition, waypoint.transform.position, ref vectorRef, 0.1f, speed);

    }

    public void nextWaypoint(string WP) {
        if (waypoint.name == WP)
        {
            waypoint = GameObject.Find(nextWP(WP));
        }
                //Debug.Log("Yendo a " + waypoint.name);
    }

    public void CrashOrOut() {
        string prevWP = previousWP(waypoint.name);
        GameObject prevWayPoint = GameObject.Find(prevWP);
        this.transform.position = prevWayPoint.transform.position;
        waypoint = GameObject.Find(secureWP(waypoint.name));
    }

    private string secureWP(string badWP) {
        char[] letters = badWP.ToCharArray();
        int unidades = (int)char.GetNumericValue(letters[10]);
        unidades = Random.Range(0, 3); // se reduce el rango a 0-2 , porque los WPXX3 están algunos fuera de la pista.
        letters[10] = char.Parse(unidades.ToString());
        string devolucion = new string(letters);
        //Debug.Log("TEST: PREVIOUS Random WP = " + devolucion);  Funciona
        return devolucion;
    }

    private string nextWP(string WPname)
    {
        char[] letters = WPname.ToCharArray();
        int decenas = (int)char.GetNumericValue(letters[9]);
        int unidades = (int)char.GetNumericValue(letters[10]);
        int centenas = (int)char.GetNumericValue(letters[8]);
        decenas++;
        if (decenas == 10) {
            centenas++;
            decenas = 0;
        }
        if (centenas == 1 && decenas == 7) {
            centenas = 0;
            decenas = 0;
        }
        unidades = Random.Range(0, 4);
        letters[8] = char.Parse(centenas.ToString());
        letters[9] = char.Parse(decenas.ToString());
        letters[10] = char.Parse(unidades.ToString());
        string devolucion = new string(letters);
        //Debug.Log("TEST: next Random WP = " + devolucion);
        return devolucion;
    }

    public static string previousWP(string WPname)
    {
        char[] letters = WPname.ToCharArray();
        int decenas = (int)char.GetNumericValue(letters[9]);
        int unidades = (int)char.GetNumericValue(letters[10]);
        int centenas = (int)char.GetNumericValue(letters[8]);
        decenas--;
        if (centenas == 0 && decenas == -1) //caso: le damos el 001 deberia darnos un 16X
        {
            centenas++;
            decenas = 6;
        }
        if (centenas == 1 && decenas == -1) // caso: le damos 101, deberia devolver 09X 
        {
            centenas = 0;
            decenas = 9;
        }
        unidades = Random.Range(0, 3); // se reduce el rango a 0-2 , porque los WPXX3 están algunos fuera de la pista.
        letters[8] = char.Parse(centenas.ToString());
        letters[9] = char.Parse(decenas.ToString());
        letters[10] = char.Parse(unidades.ToString());
        string devolucion = new string(letters);
        return devolucion;
    }
}
