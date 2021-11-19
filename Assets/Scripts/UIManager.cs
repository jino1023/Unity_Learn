using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject PanOption;
    #region Singleton
    private static UIManager _instance = null;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (UIManager)FindObjectOfType(typeof(UIManager));
                if (_instance == null)
                {
                    Debug.Log("There's no active ManagerClass object");
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    #region Button Event
    public void OnOptionBtnClicked()
    {
        if (PanOption.activeSelf == false)
        {
            PanOption.SetActive(true);
        }
    }

    public void OnStartBtnClicked()
    {
        SceneManager.LoadScene(1); // 0=main, 1=stage
    }

    public void OnExitBtnClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    #endregion
}
