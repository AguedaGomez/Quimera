using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class positionTextscript : MonoBehaviour {

    public Text positionText;
    private GameOverManager gom;

    void Start()
    {
        positionText = this.GetComponent<Text>();
        gom = GameObject.Find("GameOverText").GetComponent<GameOverManager>();
    }

    // Update is called once per frame
    void Update()
    {

        int[] shipsPositions = GameObject.FindObjectOfType<GameManager>().shipsPositionsInRace;

        for (int i = 0; i < shipsPositions.Length; i++)
        {
            if (shipsPositions[i] == 5) {
                switch (i)
                {
                    case 5:
                        positionText.text = "1ST";
                        break;
                    case 4:
                        positionText.text = "2ND";
                        break;
                    case 3:
                        positionText.text = "3RD";
                        break;
                    case 2:
                        positionText.text = "4TH";
                        break;
                    case 1:
                        positionText.text = "5TH";
                        break;
                    case 0:
                        positionText.text = "6TH";
                        break;
                    default:
                        break;
                }
            }
        }
        gom.finalPosition = positionText.text;
    }
}
