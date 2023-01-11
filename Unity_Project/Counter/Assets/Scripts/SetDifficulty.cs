using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDifficulty : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        Debug.Log("get button component"); 
        button.onClick.AddListener(Difficulty);
        Debug.Log("add listener to button");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log("get gamemanager component");
    }

    void Difficulty()
    {
        Debug.Log("Button clicked");

        difficulty = gameObject.name switch
            {
            "EasyButton" => 1,
            "MediumButton" => 4,
            "HardButton" => 9,
            _ => 1, // defalut
        };

        gameManager.StartGame(difficulty);
    }
}
