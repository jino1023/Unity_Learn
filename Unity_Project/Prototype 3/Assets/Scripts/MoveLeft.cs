using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 15;
    private float dashSpeed = 30;
    private float leftBound = -15;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.isGameOver)
        {
            if (playerControllerScript.isDash)
            {
                transform.Translate(Vector3.left * Time.deltaTime * dashSpeed);
            }
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (gameObject.CompareTag("Obstacle") && transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
