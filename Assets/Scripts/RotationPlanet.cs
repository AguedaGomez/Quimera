using UnityEngine;
using System.Collections;
using System;

public class RotationPlanet : MonoBehaviour {

    //private Rigidbody planetRigidBody;
    private Transform planetTransform;

    private void Awake()
    {
        //planetRigidBody = GetComponent<Rigidbody>();
        planetTransform = GetComponent<Transform>();
    }

    private void FixedUpdate() {
        rotatePlanet();
    }

    private void rotatePlanet()
    {
        float rotationAxis = 0.9f * Time.deltaTime;
        //Quaternion rotationAngle = Quaternion.Euler(rotationAxis, 0f, rotationAxis);
        Vector3 rotationVector = new Vector3(rotationAxis, 0f, rotationAxis);

        planetTransform.Rotate(rotationVector);


        //planetRigidBody.MoveRotation(rotationAngle );
    }
}
