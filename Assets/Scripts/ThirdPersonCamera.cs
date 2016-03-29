using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    public Transform target;
    float distance = 10.0f;
    float height = 3.0f;
    float damping = 5.0f;
    bool smoothRotation = true;
    float rotationDamping = 10.0f;

    void FixedUpdate()
    {
        Vector3 wantedPosition = target.TransformPoint(0, height, distance);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

        if (smoothRotation)
        {
            Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
        }

        else transform.LookAt(target, target.up);
    }
}
