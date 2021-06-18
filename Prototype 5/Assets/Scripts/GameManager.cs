using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI lifeText;
    public Button retryButton;
    public GameObject titleScreen;
    public GameObject setttingScreen;

    private float spawnRate = 1.5f;
    private int score;
    private bool paused;
    public int life=5;
    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        titleScreen.SetActive(true);
    }

    // set info before start game
    public void StartGame(int difficult)
    {
        string difficulty;
        switch (difficult)
        {
            case 1:
                difficulty = "Easy";
                break;
            case 2:
                difficulty = "Medium";
                break;
            case 3:
                difficulty = "Hard";
                break;
            default:
                difficulty = "Easy";
                break;
        }

        difficultyText.text = "Level : " + difficulty;
        difficultyText.gameObject.SetActive(true);
        spawnRate /= difficult;

        UpdateScore(0);
        scoreText.text = "Score" +"<br>"+ score;
        scoreText.gameObject.SetActive(true);

        lifeText.text = "Life" + "<br>" + life;
        lifeText.gameObject.SetActive(true);

        isGameActive = true;
        titleScreen.SetActive(false);
        StartCoroutine(SpawnTarget());
    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            setttingScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            setttingScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score" + "<br>" + score;
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

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePaused();
        }
    }
}
