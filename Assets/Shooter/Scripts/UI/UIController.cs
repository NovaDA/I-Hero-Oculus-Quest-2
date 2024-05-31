using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    static GameObject ScoreDisplay;                // the text value showing in the screen
    static GameObject HitHelper;
    static int scoreValue;                 // static varible that can be easily passed in the game from scene to scene without changin 
    static int SucessfulHits;
    static int MissedHits;
    static float Accuracy;

    // Use this for initialization
    void Start ()
    {
        ScoreDisplay = GameObject.FindGameObjectWithTag("UI");
        UpdateScoreText();
        if(GameObject.Find("Hit Object") != null)
        {
            HitHelper = GameObject.Find("Hit Object");
            HitHelper.SetActive(false);
        }
        
    }

    #region
    public static int PlayerScore
    {
        get { return scoreValue; }
        set { scoreValue = value; }
    }

    public static int PlayerMissed
    {
        get { return MissedHits; }
        set { MissedHits = value; }
    }

    public static int PlayerTotalHits
    {
        get { return SucessfulHits + MissedHits; }
        set { SucessfulHits = value; }
    }
    #endregion

    public static void IncreaseScore(int value)
    {
        scoreValue += value;
        UpdateScoreText();
    }

    public static void IncreaseScoreBonus(int value)
    {
        scoreValue += 1000;
        UpdateScoreText();
    }

    public static void IncreaseHits()
    {
        SucessfulHits++;
    }

    public static void IncreaseMissedHits()
    {
        MissedHits++;
    }

    public static float GetAccuracy()
    {
        float totalHit = SucessfulHits + MissedHits;
        Accuracy = (SucessfulHits / totalHit) * 100;
        return Accuracy;
    }

    private static void UpdateScoreText()
    {
        if (GameLevelManager.GetSceneName() == "HighScoreManagement")
        {
            return;
        }
        else
        {
            ScoreDisplay.GetComponent<TextMesh>().text = scoreValue.ToString();
        }
   
    }

    public static void ResetScore()
    {
        scoreValue = 0;
    }

    public static void SetHitPoint(Vector3 position)
    {
        HitHelper.SetActive(true);
        HitHelper.transform.position = new Vector3(position.x, position.y + 0.5f, position.z -0.5f);
    }

    public static void HidHelper()
    {
        HitHelper.SetActive(false);
    }
        
}
