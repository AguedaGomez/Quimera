using UnityEngine;
using System.Collections;

public class IAMovement : MonoBehaviour {

    GameObject waypoint;
    NavMeshAgent nav;

	// Use this for initialization
	void Awake () {

        waypoint = GameObject.Find("WayPoint00");
        nav = GetComponent<NavMeshAgent>();

       /* string cadena = "WayPoint2";
        cadena = nextWP(cadena);
        Debug.Log(cadena);
        */ // Esto era para comprobar que la funcion de modificar el nombre funcionaba.
    }
	
	// Update is called once per frame
	void Update () {

        nav.SetDestination(waypoint.transform.position);
        

	}

    public void nextWaypoint(string WP) {
        if (WP == waypoint.name)
        {
            if (waypoint.name == "WayPoint16")
            {
                waypoint = GameObject.Find("WayPoint00");
            }
            else
            {
                waypoint = GameObject.Find(nextWP(waypoint.name));
                Debug.Log("Yendo a " + waypoint.name);
            }
        }
    }

    private string nextWP(string WPname) {
        char[] letters = WPname.ToCharArray();
        int decenas = (int)char.GetNumericValue( letters[8]);
        int unidades = (int)char.GetNumericValue(letters[9]);
        unidades++;
        if (unidades == 10) {
            decenas++;
            unidades = 0; }
        letters[8] = char.Parse(decenas.ToString());
        letters[9] = char.Parse(unidades.ToString());
        string devolucion = new string(letters);
        return new string(letters);
    }
}
