using UnityEngine;
using System.Collections;

public class ShipPositionManager : MonoBehaviour {

    public int lap;
    public int cp;
    public int distanceNextCP = 0;

    // Use this for initialization
    void Start () {
	
	}

    private void UpdateDistanceNextCP()
    {
        int nextcp;
        if (cp == 9)
        {
            nextcp = 0;
        }
        else
        {  nextcp = cp + 1; }
        distanceNextCP = (int)Vector3.Distance(this.transform.position, GameObject.Find("CP0" + nextcp ).transform.position);
    }

    public void updateCP (int cpnumber){
        cp = cpnumber;
    }


    // Update is called once per frame
    void Update () {
        UpdateDistanceNextCP();
	}
}
