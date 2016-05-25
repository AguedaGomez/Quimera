using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    private GameObject player;
    private int playerLaps;
    private TimeManager tm;
    private positionTextscript posTextScript;
    private Text gameOver;
    public string finalPosition;
    private int positionPlayer;
    public Button restartGameButton;
    public Button goToMenuButton;

	// Use this for initialization
	void Awake () {
        tm = GameObject.Find("timeText").GetComponent<TimeManager>();
        gameOver = this.GetComponent<Text>();
        restartGameButton.enabled = false;
        restartGameButton.image.color = new Color(0, 0, 0, 0);
        restartGameButton.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0);
        goToMenuButton.enabled = false;
        goToMenuButton.image.color = new Color(0, 0, 0, 0);
        goToMenuButton.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLaps = player.GetComponent<ShipPositionManager>().lap;

        if (playerLaps == 3)
        {
            endGame();
        }
	
	}

    private void endGame()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().RaceFinished();
        gameOver.text = "GAME OVER\n\n" + tm.timeText.text + "\n\n" + finalPosition;
        tm.enabled = false;
        restartGameButton.enabled = true;
        restartGameButton.GetComponentInChildren<Text>().color = new Color32(136, 250, 255, 255);
        goToMenuButton.enabled = true;
        goToMenuButton.GetComponentInChildren<Text>().color = new Color32(136, 250, 255, 255);
    }

    public void GoToMenu() {
        Application.LoadLevel("MainMenu");
    }

    public void RestartGame() {
        Application.LoadLevel("QuimeraRacingFinal");
    }

}
