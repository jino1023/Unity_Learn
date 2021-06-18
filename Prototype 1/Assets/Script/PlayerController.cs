using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20.0f;
    public float turnSpeed = 45.0f;
    public float horizontalInput;
    public float verticalInput;
    public KeyCode switchkey;
    Camera mainCam;
    public Camera subCam;
    public float inputId;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        mainCam.enabled = true;
        subCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
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
            transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
            // Roates the vehicle based on horizontal Input
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
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
            transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
            // Roates the vehicle based on horizontal Input
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        }
        
    }
}
