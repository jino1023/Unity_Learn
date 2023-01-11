using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody targetRb;
    public ParticleSystem explosionPaticle;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    public int scoreVaule = 5;
    public int lifeVaule = 1;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            // if gameobject tag is "Bad"
            if (gameObject.CompareTag("Bad"))
            {
                Destroy(gameObject);
                Instantiate(explosionPaticle, transform.position, explosionPaticle.transform.rotation);
                gameManager.UpdateScore(scoreVaule);
                gameManager.ReduceLife(lifeVaule);
                if (gameManager.life < 1)
                {
                    gameManager.GameOver();
                }
            }
            // if not
            else
            {
                Destroy(gameObject);
                Instantiate(explosionPaticle, transform.position, explosionPaticle.transform.rotation);
                gameManager.UpdateScore(scoreVaule);
            }
        }
    }

    private void OnMouseDown()
    {
        DestroyTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);        
    }
}
