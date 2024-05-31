using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour {
    public  GameObject entryContainer;
    GameObject entryTemplate;
    List<GameObject> scoreEntryFromList;
    float templateHeight = 35f;
    private void Awake()
    {
        entryContainer = GameObject.Find("EntryContainer");
        entryTemplate = GameObject.Find("HighScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        if (!PlayerPrefs.HasKey("highScoreTables"))
        {
            AddFirstTimeScore();
            UpdateScoreTable();
        }
        else
        {
            UpdateScoreTable();
        }
    }

    private void AddFirstTimeScore()
    {
        List<ScoreEntry> scoreEntry;
        HighScores high = new HighScores();
        #region
        scoreEntry = new List<ScoreEntry>()
        {
            new ScoreEntry{score = 100, name = "AAA"},
            new ScoreEntry{score = 985, name = "fgh"},
            new ScoreEntry{score = 546, name = "hgh"},
            new ScoreEntry{score = 727, name = "ghh"},
            new ScoreEntry{score = 724, name = "hhh"},
            new ScoreEntry{score = 452, name = "ghg"},
            new ScoreEntry{score = 451, name = "ghg"},
            new ScoreEntry{score = 124, name = "ghh"},
            new ScoreEntry{score = 459, name = "ghh"},
            new ScoreEntry{score = 352, name = "fgh"}
        };
        high.HighScoreEntry = scoreEntry;
        string json = JsonUtility.ToJson(high);
        PlayerPrefs.SetString("highScoreTables", json);
        PlayerPrefs.Save();
        #endregion
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddHighScoreToTable(182794, "Rao");
        }
    }

    public void AddHighScore()
    {
        AddHighScoreToTable(10000, "Default");
    }

    private void UpdateScoreTable()
    { 
        string jsonString = PlayerPrefs.GetString("highScoreTables");                                           
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);
        for (int i = 0; i < highscores.HighScoreEntry.Count; i++)
        {
            for (int j = 0; j < highscores.HighScoreEntry.Count; j++)
            {
                if (highscores.HighScoreEntry[j].score < highscores.HighScoreEntry[i].score)
                {
                    ScoreEntry holder = highscores.HighScoreEntry[i];
                    highscores.HighScoreEntry[i] = highscores.HighScoreEntry[j];
                    highscores.HighScoreEntry[j] = holder;
                }
            }
        }
        if (highscores.HighScoreEntry.Count > 10)
        {
            highscores.HighScoreEntry.RemoveAt(highscores.HighScoreEntry.Count - 1); /// removing last index
        }
        if (entryContainer.transform.childCount > 0)
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in entryContainer.transform)
            {
                children.Add(child.gameObject);
            }
            children.ForEach(child => GameObject.Destroy(child));
        }
        scoreEntryFromList = new List<GameObject>();
        foreach (ScoreEntry score in highscores.HighScoreEntry)
        {
            CreateNewScoreEntry(score, entryContainer, scoreEntryFromList);
        }
        // Save Update HighScores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highScoreTables", json);
        PlayerPrefs.Save();
    }

    private void CreateNewScoreEntry(ScoreEntry scoreEntry, GameObject container, List<GameObject> gameObjectsList)
    {
        ///Debug.Log("Inside For Loop");

        GameObject entryTransform = Instantiate(entryTemplate, container.transform);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * gameObjectsList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = gameObjectsList.Count + 1;
        string rankString;
        switch (rank)
        {
            default: rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        entryTransform.transform.Find("PosRank").GetComponent<Text>().text = rankString;

        int score = scoreEntry.score;
        entryTransform.transform.Find("PosScore").GetComponent<Text>().text = score.ToString();
        string name = scoreEntry.name;
        entryTransform.transform.Find("PosName").GetComponent<Text>().text = name;

        gameObjectsList.Add(entryTransform);
    }

    [System.Serializable]
    private class ScoreEntry
    {
        public int score;
        public string name;
    }

    private class HighScores
    {
        public List<ScoreEntry> HighScoreEntry;
    }

    public void AddHighScoreToTable(int score, string name)     /// here where is required to check the list if
                                                                ///the new high score is higher than any element in the list and remove the last element
    {
        // Create new score entry
        ScoreEntry scoreEntry = new ScoreEntry { score = score, name = name };
        // Load saved HighScores 
        string jsonString = PlayerPrefs.GetString("highScoreTables");   // Load List from Prefs
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);
        // Add new Entry to high scores
        highscores.HighScoreEntry.Add(scoreEntry);
        // Save Update HighScores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highScoreTables", json);
        PlayerPrefs.Save();
        UpdateScoreTable();
    }

    public bool IsScoreHigherThanElementsInTables(int score)   // need to compare the score to the ones in the tablet
    {
        string jsonString = PlayerPrefs.GetString("highScoreTables");   // Load List from Prefs
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);
        if (score > highscores.HighScoreEntry[highscores.HighScoreEntry.Count -1].score)
        {
            return true;
        }
        else
        {
            return false;
        }  
    }    
}


//AddFirstTimeScore();

#region
//HighScores highScores = new HighScores { HighScoreEntry = scoreEntry };
//string json = JsonUtility.ToJson(highScores);
//PlayerPrefs.SetString("highScoreTables", json);
//PlayerPrefs.Save();
//Debug.Log(PlayerPrefs.GetString("highScoreTables"));
#endregion

#region
//scoreEntry = new List<ScoreEntry>()
//{
//    new ScoreEntry{score = 10000, name = "AAA"},
//    new ScoreEntry{score = 98562, name = "fgh"},
//    new ScoreEntry{score = 54655, name = "hgh"},
//    new ScoreEntry{score = 72727, name = "ghh"},
//    new ScoreEntry{score = 72422, name = "hhh"},
//    new ScoreEntry{score = 45245, name = "ghg"},
//    new ScoreEntry{score = 45224, name = "ghg"},
//    new ScoreEntry{score = 12452, name = "ghh"},
//    new ScoreEntry{score = 45225, name = "ghh"},
//    new ScoreEntry{score = 45252, name = "fgh"}
//};
#endregion