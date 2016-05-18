using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public static Text timeText;
    private double startTime = 0.5;
    private int miliseconds = 0;
    private int seconds = 0;
    private int minutes = 0;
    private double time = 0;

	void Awake () {

        timeText = GetComponent<Text>();
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        time = Time.time - startTime;
        miliseconds = (int)(time * 10) % 10;
        minutes = (int)time / 60;
        seconds = (int)time % 60;
        timeText.text = minutes.ToString() + ":" + seconds.ToString() + ":" + miliseconds.ToString();
	
	}
}
