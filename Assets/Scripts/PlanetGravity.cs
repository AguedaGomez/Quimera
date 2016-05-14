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
            PlanetGravitation(shipsBodies[0], distancePlanetShip);
        }
        //Debug.Log("TEST: distancia = " + distancePlanetShip);


    }

    private void PlanetGravitation(Rigidbody ship, float distance)
    {
        //shipsBodies[0].MovePosition(shipsBodies[0].position -((planetRigidBody.position - shipsBodies[0].position) /800)); //Funciona pero la gravedad va al revés
        //shipsBodies[0].MovePosition(Vector3.MoveTowards(shipsBodies[0].position, planetRigidBody.position, 0.0005f)); // demasiado rápido e incontrolable

        //ship.MovePosition(ship.position - ((ship.position - planetRigidBody.position ) / (1/ distance ) )); // igual que el primero

        Vector3 direction = planetRigidBody.position - ship.position;
        //ship.AddForce(direction.normalized * 300/distance);

        //Vector3 orbitation = Vector3.Cross(direction.normalized, Vector3.up);
        //ship.AddForce(orbitation * 100/distance);

        //Debug.Log("FuerzaG: " + (direction.normalized * 300 / distance) + "FuerzaRotacion: " + (orbitation * 100 / distance));

        float Gm1m2 = 100 * (UnityEngine.Random.value + 0.1f);
        float Fg = Gm1m2 / (distance * distance);

        Vector3 directionFg = direction * Fg;
        ship.velocity += directionFg;

    }

    private float CalculateDistance(Vector3 planet, Vector3 ship) {

        return Vector3.Distance(planet, ship);

    }


}
