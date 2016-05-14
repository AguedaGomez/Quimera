using UnityEngine;
using System.Collections;

public class SphereBoxPosition : MonoBehaviour
{

    public Transform player;

    private void Awake()
    {

    }

 
    private void FixedUpdate() {
        this.transform.position = player.position;
    }

}