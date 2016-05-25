using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDownScript : MonoBehaviour {

    public Text countDownText;
    private double startTime = 0.5;
    private int seconds = 0;
    private double time = 0;
    private TimeManager tm;

    // Use this for initialization
    void Start () {
        countDownText = this.GetComponent<Text>();
        startTime = Time.time;
        tm = GameObject.Find("timeText").GetComponent<TimeManager>();
        
	}
	
	// Update is called once per frame
	void Update () {

        if (!tm.raceStarted || countDownText.text == "GO")
        {
            CountDown();
        }
    }


    private void CountDown() {
        time = Time.time - startTime;

        seconds = (int)time % 60;

        switch (seconds)
        {
            case 0:
                countDownText.text = "" + 3;
                break;
            case 1:
                countDownText.text = "" + 2;
                break;
            case 2:
                countDownText.text = "" + 1;
                break;
            case 3:
                countDownText.text = "GO" ;
                tm.raceStarted = true;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().StartRace();
                break;
            case 4:
                countDownText.text = "";
                this.enabled = false; // ya no se va a usar más este objeto/script.
                break;
            default:
                break;
        }

    }

}
