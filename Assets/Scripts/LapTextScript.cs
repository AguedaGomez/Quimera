using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LapTextScript : MonoBehaviour {

    public Text lapText;


    void Awake()
    {
        lapText = GetComponent<Text>();
    }

    public void upgradeLap(int lap)
    {
        lapText.text = "LAP " + lap + "/3" ;

    }
}
