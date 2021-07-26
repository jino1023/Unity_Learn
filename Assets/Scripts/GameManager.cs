using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string[] user_names = new string[3];
    public string[] scores = new string[3];
    public string player_name;
    private int user_displayed = 3;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [Serializable]
    class SaveData
    {
        public string[] usernames = new string[3];
        public string[] scores = new string[3];
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.usernames = user_names;
        data.scores = scores;

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            user_names = data.usernames;
            scores = data.scores;
        }
    }

    public string BestScore()
    {
        if (user_names[0] == null || user_names[0] == "")
        {
            return "No Scores";
        }
        return user_names[0] + ": " + scores[0];
    }

    public string ScoreBoard()
    {
        if (user_names[0] == null || user_names[0] == "")
        {
            return "No Scores";
        }

        string scoreBoard = "";

        for (int i = 0; i < user_displayed; i++)
        {
            if (user_names[i] != null && user_names[i] != "")
                scoreBoard += user_names[i] + ": " + scores[i] + "\n";
        }
        return scoreBoard;
    }

    public void CompareScore(int score)
    {
        for (int i = 0; i < user_displayed; i++)
        {
            int boardScore;
            if (scores[i] == null || scores[i] == "")
            {
                boardScore = 0;
            } 
            else
            {
                boardScore = int.Parse(scores[i]);
            }

            if (boardScore < score)
            {
                string tampN = user_names[i];
                string tampS = scores[i];
                user_names[i] = player_name;
                scores[i] = score.ToString();
                i++;

                while (i < user_displayed)
                {
                    string temp = user_names[i];
                    user_names[i] = tampN;
                    tampN = temp;
                    temp = scores[i];
                    scores[i] = tampS;
                    tampS = temp;
                    i++;
                }
                return;
            }
        }
    }
}
