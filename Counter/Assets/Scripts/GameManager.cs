using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI timeText;
    public Button retryButton;
    public GameObject titleScreen;

    private float spawnRate = 1.8f;
    private int score;
    private float remainTime = 60;
    public int life = 5;
    public bool isGameActive;
    private float timeInterval = 1.0f;
    private float repeatTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        titleScreen.SetActive(true);
    }

    // set info before start game
    public void StartGame(int difficult)
    {
        Debug.Log("Start Game");
        string difficulty = difficult switch
        {
            1 => "Easy",
            4 => "Medium",
            9 => "Hard",
            _ => "Easy",
        };

        difficultyText.text = "Level : "+ "<br>" + difficulty;
        difficultyText.gameObject.SetActive(true);
        spawnRate /= difficult;

        UpdateScore(0);
        scoreText.text = "Count" + "<br>" + score;
        scoreText.gameObject.SetActive(true);

        lifeText.text = "Life" + "<br>" + life;
        lifeText.gameObject.SetActive(true);

        timeText.text = "Time " + "<br>" + remainTime;
        timeText.gameObject.SetActive(true);

        isGameActive = true;
        titleScreen.SetActive(false);
        StartCoroutine(SpawnTarget());
        InvokeRepeating("UpdateRemainTime", timeInterval, repeatTime);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Count" + "<br>" + score;
    }

    public void ReduceLife(int lifeToAdd)
    {
        life -= lifeToAdd;
        lifeText.text = "Life" + "<br>" + life;
    }

    public void GameOver()
    {
        gameoverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    void UpdateRemainTime()
    {
        if (isGameActive)
        {
            remainTime -= 1;
            timeText.text = "Time " + "<br>" + remainTime;
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
}
