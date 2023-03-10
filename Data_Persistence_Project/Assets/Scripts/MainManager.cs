using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text BestScore;
    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                // brick=inst
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        // load socre from json file and set best score
        GameManager.Instance.LoadScore();
        SetBestScore();
    }

    private void Update()
    {
        if (!m_Started)
        {   
            // if spacekey down start game
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
                
            }
        }
        else if (m_GameOver)
        {
            // if space key down when gameover then restart game scene
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                SetBestScore();
            }
            // if r key down when gameover then go to menu scene
            else if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    // set best score
    void SetBestScore()
    {
        BestScore.text = "Best Score : "+GameManager.Instance.BestScore();
    }

    // add point
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }


    // run when game over
    public void GameOver()
    {
        GameManager.Instance.CompareScore(m_Points);
        GameManager.Instance.SaveScore();
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

}
