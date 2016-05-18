using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class ShipManager  {

    public Transform SpawnPoint;
    public Color ShipColor;                             // This is the color this tank will be tinted.

    [HideInInspector]
    public int ShipNumber;            // This specifies which player this the manager for.
    [HideInInspector]
    public string m_ColoredPlayerText;    // A string that represents the player with their number colored to match their tank.
    [HideInInspector]
    public GameObject m_Instance;         // A reference to the instance of the tank when it is created.

    private RaycastHit trackHit;
    private Transform shipTransform;

    void Awake()
    {
        shipTransform = m_Instance.transform;
    }


    public void Setup() // Basicamente sólo lo colorea del color que toca
    {
        // Create a string using the correct color that says 'PLAYER 1' etc based on the tank's color and the player's number.
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(ShipColor) + ">PLAYER " + ShipNumber + "</color>";

        // Get all of the renderers of the tank.
        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

        // Go through all the renderers...
        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this tank.
            renderers[i].material.color = ShipColor;
        }

        //Debug.Log("TEST: Se ejecuta el SETUP del shipManager");

    }
    
    // Update is called once per frame
    public void UpdateCrashOrOut()
    {
        shipTransform = m_Instance.transform;
        //Raycast hacia el suelo para ver si te sales de la pista.
        bool RayHitsGround = Physics.Raycast(shipTransform.position, shipTransform.transform.TransformDirection(Vector3.down), out trackHit);
        Debug.DrawRay(shipTransform.position, shipTransform.transform.TransformDirection(Vector3.down), Color.yellow);
        if (!RayHitsGround)
        {
            Debug.Log("La nave" + ShipNumber + "se ha salido");
            //Aqui hay que hacer que vuelva hacia atrás en la pista.
            m_Instance.GetComponent<IAMovement>().CrashOrOut();
        }

    }

    public void Reset()
    {
        shipTransform.position = SpawnPoint.position;
        shipTransform.rotation = SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }


}
