using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Camera mainCam;
    public Camera subCam;
    private Rigidbody playerRb;

    // [SerializeField] private float speed = 20.0f;
    [SerializeField] private float horsePower = 20.0f;
    [SerializeField] private float turnSpeed = 45.0f;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float rpm;
    [SerializeField] float speed;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;

    public float horizontalInput;
    public float verticalInput;
    public KeyCode switchkey;
    public float inputId;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
        mainCam = Camera.main;
        mainCam.enabled = true;
        subCam.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsOnGround())
        {
            if (inputId == 1)
            {
                horizontalInput = Input.GetAxis("Horizontal2");
                verticalInput = Input.GetAxis("Vertical2");

                if (Input.GetKeyDown(switchkey))
                {
                    /*
                    if (mainCam.enabled)
                    {
                        subCam.enabled = true;
                        mainCam.enabled = false;
                    }
                    else if (!mainCam.enabled)
                    {
                        subCam.enabled = false;
                        mainCam.enabled = true;
                    }
                    */
                    mainCam.enabled = !mainCam.enabled;
                    subCam.enabled = !subCam.enabled;
                }

                // Moves the vehicle foward based on vertical Input
                // transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
                playerRb.AddRelativeForce(Vector3.forward * verticalInput * horsePower);
                // Roates the vehicle based on horizontal Input
                transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

                speed = Mathf.Round(playerRb.velocity.magnitude * 2.237f);
                speedometerText.SetText("Speed : " + speed + "mph");
                rpm = Mathf.Round((speed % 30) * 40);
                rpmText.SetText("RPM : " + rpm);
            }

            else if (inputId == 2)
            {
                // get player Input
                horizontalInput = Input.GetAxis("Horizontal1");
                verticalInput = Input.GetAxis("Vertical1");

                if (Input.GetKeyDown(switchkey))
                {
                    mainCam.enabled = !mainCam.enabled;
                    subCam.enabled = !subCam.enabled;
                }

                // Moves the vehicle foward based on vertical Input
                // transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
                playerRb.AddRelativeForce(Vector3.forward * verticalInput * horsePower);
                // Roates the vehicle based on horizontal Input
                transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

                speed = Mathf.Round(playerRb.velocity.magnitude * 2.237f);
                speedometerText.SetText("Speed : " + speed + "mph");
                rpm = Mathf.Round((speed % 30) * 40);
                rpmText.SetText("RPM : " + rpm);
            }
        }
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }
        if (wheelsOnGround == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
