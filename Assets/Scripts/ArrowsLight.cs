using UnityEngine;
using System.Collections;

public class ArrowsLight : MonoBehaviour {

    private GameObject[] arrows;
    private GameObject player;
    private Renderer rend;

	// Use this for initialization
	void Start () {
        arrows = GameObject.FindGameObjectsWithTag("Arrows");
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {

        player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < arrows.Length; i++)
        {
            rend = arrows[i].GetComponent<Renderer>();
    
            if (Vector3.Distance(player.transform.position, arrows[i].transform.position) < 400f)
            {
                rend.material.SetColor("_Color", new Color(0f, 1.0f, 0.8f, 1.0f) );
            }
            else {
                rend.material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0f));
            }
        }
    }
}
