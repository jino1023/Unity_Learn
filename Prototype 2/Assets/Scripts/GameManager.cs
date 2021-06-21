using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int life = 3;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLife(int value)
    {
        life += value;

        if (life <= -100)
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
            life = 0;
        }
        Debug.Log("Life : " + life);
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score : " + score);
    }
}
