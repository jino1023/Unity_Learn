using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panSetting;
    private bool setFlag = true;

    #region ΩÃ±€≈Ê
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

    private void Awake()
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

    #region º≥¡§ √¢
    public void OnSetButtonClicked()
    {
        if (setFlag)
        {
            panSetting.SetActive(false);
            setFlag = false;
        }
        else
        {
            panSetting.SetActive(true);
            setFlag = true;
        }
    }
    #endregion
}
