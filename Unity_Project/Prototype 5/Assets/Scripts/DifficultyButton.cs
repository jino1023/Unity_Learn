using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void SetDifficulty()
    {
        switch (gameObject.name)
        {
            case "Easy Button":
                difficulty = 1;
                break;
            case "Medium Button":
                difficulty = 2;
                break;
            case "Hard Button":
                difficulty = 3;
                break;
            default:
                difficulty = 1;
                break;
        }
        gameManager.StartGame(difficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
