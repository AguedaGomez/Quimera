using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    public Transform target; // el jugador
    float distance = 10.0f; // distancia entre la camara y el jugador
    float height = 3.0f; // altura de la camara
    float damping = 5.0f; // Suavizado del movimiento de camara en movimientos rectos
    bool smoothRotation = true;
    float rotationDamping = 10.0f; // Suavizado del movimiento de camara en movimientos curvos

    void FixedUpdate()
    {

        target = GameObject.FindGameObjectWithTag("Player").transform;


        Vector3 wantedPosition = target.TransformPoint(0, height, distance);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

        //if (smoothRotation)
        //{
            Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
        //}

        //else transform.LookAt(target, target.up);
    }
}
