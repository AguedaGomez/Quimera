using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour {

    private ShipPositionManager spm;
    private int CPnumber;
    private LapTextScript laptext;

    private void Start() {
        char[] CPName = this.name.ToCharArray();
        CPnumber = (int)char.GetNumericValue(CPName[3]);
        laptext = GameObject.Find("LapText").GetComponent<LapTextScript>();
    }

    void OnTriggerEnter(Collider ship)
    {
        //Debug.Log("Algo ha entrado en el" + this.gameObject.name); Funciona
        spm = ship.gameObject.GetComponent<ShipPositionManager>();
        if (spm.cp == 9 && CPnumber == 0) {
            spm.lap++;
            if (spm.gameObject.tag == "Player") {
                laptext.upgradeLap(spm.lap);
            }
        }
        spm.updateCP(CPnumber);
    }



}
