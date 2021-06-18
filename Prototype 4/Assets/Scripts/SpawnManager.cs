using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;

    private float spawnRange = 9;
    public int enemyCount;
    public int waveNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], RandomSpawnPos(), powerupPrefabs[randomPowerup].transform.rotation);
        spawnWave(waveNum);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNum++;
            spawnWave(waveNum);
            int randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], RandomSpawnPos(), powerupPrefabs[randomPowerup].transform.rotation);
        }
    }

    // wave spawn
    void spawnWave(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomSpawn = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomSpawn], RandomSpawnPos(), enemyPrefabs[randomSpawn].transform.rotation);
        }
    }

    // return random x, z
    private Vector3 RandomSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomSpawnPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomSpawnPos;
    }
}
