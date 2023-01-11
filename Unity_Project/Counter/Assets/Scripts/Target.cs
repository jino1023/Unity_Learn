using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager gameManager;
    public ParticleSystem explosionPaticle;

    private float zRange = 25;
    private float ySpawnPos = 55;
    private int lifeVaule = 1;
    [SerializeField] private int scoreVaule = 5;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.position = RandomSpawnPos();
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(0, ySpawnPos, Random.Range(-zRange, zRange));
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
