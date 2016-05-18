using UnityEngine;
using System.Collections;

public class IAMovement : MonoBehaviour {

    private GameObject waypoint;
    //private NavMeshAgent nav;
    private Vector3 shipPosition;
    public Vector3 speed;

	// Use this for initialization
	void Awake () {

        waypoint = GameObject.Find(nextWP("WayPoint000"));
        //nav = GetComponent<NavMeshAgent>();
        speed = Vector3.one;
    }
	
	// Update is called once per frame
	void Update () {

        shipPosition = this.transform.position;
        this.transform.position = Vector3.SmoothDamp(shipPosition, waypoint.transform.position, ref speed , 0.1f, 150f);

        //nav.SetDestination(waypoint.transform.position); Esto va muuy mal
        

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

    private string previousWP(string WPname)
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
        //Debug.Log("TEST: PREVIOUS Random WP = " + devolucion);  Funciona
        return devolucion;
    }
}
