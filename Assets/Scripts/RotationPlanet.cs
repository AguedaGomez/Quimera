using UnityEngine;
using System.Collections;
using System;

public class RotationPlanet : MonoBehaviour {

    private Rigidbody planetRigidBody;

    private void Awake()
    {
        planetRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        rotatePlanet();
    }

    private void rotatePlanet()
    {
        float rotationAxis = 0.15f * Time.frameCount;
        Quaternion rotationAngle = Quaternion.Euler(rotationAxis, 0f, rotationAxis);

        planetRigidBody.MoveRotation(rotationAngle );
    }
}
