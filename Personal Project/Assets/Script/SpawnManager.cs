using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject Enemy;
    public GameObject Player;

    public int bulletCount;
    public int spawnCount = 1;
    public float spawnRangeX = 40;

    // Start is called before the first frame update
    void Start()
    {
        // Vector3 spawnRotate = (Enemy.transform.position - Player.transform.position); //Quaternion.Euler(spawnRotate)
        SpawnBullet(spawnCount);
    }

    // Update is called once per frame
    void Update()
    {
        bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length;
        if (bulletCount == 0)
        {
            SpawnBullet(spawnCount);
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        return new Vector3(xPos, 1.5f, 30.0f);
    }

    void SpawnBullet(int countToSpawn)
    {
        for (int i = 0; i < countToSpawn; i++)
        {
            Instantiate(bulletPrefab, GenerateSpawnPosition(), bulletPrefab.transform.rotation);
        }

        spawnCount++;
    }

}
