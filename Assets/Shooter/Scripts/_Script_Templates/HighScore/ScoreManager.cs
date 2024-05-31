using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    int PlayerScore;     // Variable for player
    bool scoreIsHigher;
    HighScoreManager HighScoreManager;

	// Use this for initialization
	void Awake () {

        GameObject[] gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();   ///   Use this algorith to assire to Obatain the desired Object Even if disabled
        foreach (GameObject go in gameObjects)
        {
            if (go.GetComponent<UIController>())
            {
                PlayerScore = UIController.PlayerScore;
            }

            if(go.GetComponent<HighScoreManager>())
            {
                HighScoreManager = go.GetComponent<HighScoreManager>();

            }
        }
        CompareWithTables();
    }

    private void CompareWithTables()
    {
        scoreIsHigher = HighScoreManager.IsScoreHigherThanElementsInTables(PlayerScore);
        if(scoreIsHigher)
        {
            GameObject[] gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject go in gameObjects)
            {
                if (go.gameObject.name == "UIKeyBoard")
                {
                    if (!go.gameObject.activeSelf)
                    {
                        go.gameObject.SetActive(true);
                    }
                }
            }
            GameObject.Find("SCORE VALUE").GetComponent<Text>().text = PlayerScore.ToString();
        }  
    }

    public bool ScoreIsHighInTable()
    {
        return scoreIsHigher;
    }

    public void SubmitScoreToTable(string name)
    {
        HighScoreManager.AddHighScoreToTable(PlayerScore, name);
    }
}



