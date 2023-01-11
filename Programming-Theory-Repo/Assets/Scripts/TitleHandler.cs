using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TitleHandler : MonoBehaviour
{
    public TextMeshProUGUI nameField;

    private void GoToMainScene()
    {
        if (GameManager.Instance != null)
        {
            SetName(nameField.text);
        }
        SceneManager.LoadScene(1);
    }

    private void SetName(string username)
    {
        GameManager.Instance.player_name = username;
    }

    private void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
