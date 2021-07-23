using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        GameManager.Instance.LoadScore();
        scoreText.text = "Best Score" + "<br>" + $"{GameManager.Instance.best_name} : {GameManager.Instance.best_score}";
    }

    public void SetName(string username)
    {
        GameManager.Instance.user_name = username;
    }

    public void GoToMain()
    {
        if (GameManager.Instance != null)
        {
            SetName(nameField.text);
        }
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
