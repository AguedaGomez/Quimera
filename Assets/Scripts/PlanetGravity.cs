using UnityEngine;
using System.Collections;
using System;

public class PlanetGravity : MonoBehaviour {

    private Rigidbody[] shipsBodies;
    private Rigidbody planetRigidBody;


    private void Awake() {

        planetRigidBody = GetComponent<Rigidbody>();

        shipsBodies = new Rigidbody[10]; 
    }

    private void FixedUpdate() {
        shipsBodies[0] = GameObject.FindGameObjectWithTag("SpaceShip").GetComponent<Rigidbody>();

        float distancePlanetShip = CalculateDistance(planetRigidBody.position, shipsBodies[0].position);

        if (distancePlanetShip < 300)
        {
            PlanetGravitation(shipsBodies[0]);
        }
        Debug.Log("TEST: distancia = " + distancePlanetShip);


    }

    private void PlanetGravitation(Rigidbody ship)
    {
        //shipsBodies[0].MovePosition(shipsBodies[0].position -((planetRigidBody.position - shipsBodies[0].position) /800)); //Funciona pero la gravedad va al revés
        //shipsBodies[0].MovePosition(Vector3.MoveTowards(shipsBodies[0].position, planetRigidBody.position, 0.0005f)); // demasiado rápido e incontrolable

        //ship.MovePosition()

    }

    private float CalculateDistance(Vector3 planet, Vector3 ship) {

        return Vector3.Distance(planet, ship);

    }


}
