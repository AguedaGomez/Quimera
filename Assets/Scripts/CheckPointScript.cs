using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour {

    private ShipManager sm;
    private int CPnumber;

    private void Start() {
        char[] CPName = this.name.ToCharArray();
        CPnumber = (int)char.GetNumericValue(CPName[3]);
    }

    void OnTriggerEnter(Collider ship)
    {
        //Debug.Log("Algo ha entrado en el" + this.gameObject.name); Funciona
        sm = ship.gameObject.GetComponent<ShipManager>();
        if (sm.cp == 9 && CPnumber == 0) {
            sm.lap++;
        }
        sm.cp = CPnumber;
    }



}
