using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float spawnDelay = 1.0f;
    private bool spawnOK = true;

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (spawnOK)
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                changeSpawnOk();
                Invoke("changeSpawnOk", spawnDelay);
            }
        }             
    }
    
    void changeSpawnOk()
    {
        spawnOK = !spawnOK;
    }

}
