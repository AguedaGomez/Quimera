using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public float m_Speed = 60f;                 // How fast the tank moves forward and back.
    public float m_TurnSpeed = 150f;            // How fast the tank turns in degrees per second.
    public Slider fuelSlider;
    public Slider velocitySlider;

    public float fuel = 100f;
    private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
    private string m_TurnAxisName;              // The name of the input axis for turning.
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    private float m_MovementInputValue;         // The current value of the movement input.
    private float m_TurnInputValue;             // The current value of the turn input.
    private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        fuelSlider = GameObject.Find("fuelSlider").GetComponent<Slider>();
        velocitySlider = GameObject.Find("velocitySlider").GetComponent<Slider>();
        velocitySlider.value = m_Speed/5;
    }


    private void OnEnable()
    {
        // When the ship is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;

        // Also reset the input values.
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable()
    {
        // When the ship is turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        // The axes names are based on player number.
        m_MovementAxisName = "Vertical";
        m_TurnAxisName = "Horizontal";
    }


    private void Update()
    {
        // Store the value of both input axes.
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        // If space is held down and ship has fuel 
        if (Input.GetButton("Fire1") && fuel > 0)
        {
            m_Speed += 2;
            fuel -= 0.5f;
            velocitySlider.value += 0.2f;
        }
        fuelSlider.value = fuel;

    }


    private void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();
        Turn();
    }

    private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * m_Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position - movement);
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }

    public void CrashOrOut()
    {
        int prevCP = this.GetComponent<ShipPositionManager>().cp;
        this.transform.rotation = GameObject.Find("CP0" + prevCP).transform.rotation;
        this.transform.position = GameObject.Find("CP0" + prevCP).transform.position;
        m_Speed = m_Speed / 1.5f;
        velocitySlider.value = velocitySlider.value / 1.5f;     
    }

    public void IncreaseSpeed() {
        m_Speed += (( m_Speed * Time.fixedDeltaTime)/3);
        velocitySlider.value +=  ((velocitySlider.value * Time.fixedDeltaTime)/3);
    }

}
