using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject PanOption;
    public GameObject PanBlack;
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

        // 시작시 검은화면에서 점점 밝게하는 효과
        StartCoroutine("FadeBlack");
    }
    #endregion

    IEnumerator FadeBlack()
    {
        // 
        for (float ft = 1f; ft >= 0; ft -= 0.002f)
        {
            Color c = PanBlack.GetComponent<Image>().color;
            c.a = ft;
            PanBlack.GetComponent<Image>().color = c;
            yield return null;
        }
        PanBlack.SetActive(false);
    }

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
