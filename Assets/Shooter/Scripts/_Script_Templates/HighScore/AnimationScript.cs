using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationScript : MonoBehaviour {
    /// To manage the numbers for the score changing
    public Text Score;
    public Text MissedScore;
    public Text TotalHits;
    public Text TotalAccuracy;

    float count;
    /// manage the rendering
    Image buttonVisibility;
    Button buttonNext;
    // Ui Controller
    //UIController ScorePoints;

	void Start () {
        //ScorePoints = GameObject.FindObjectOfType<UIController>();
        //StartCoroutine(AnimateText());
        buttonVisibility = gameObject.transform.Find("Next Button").GetComponent<Image>();
        buttonVisibility.enabled = false;

        buttonNext = gameObject.transform.Find("Next Button").GetComponent<Button>();
        buttonNext.enabled = false;

    }

    IEnumerator AnimateText()
    {
        while(true)
        {
            int a = Random.Range(0, 20);
            int b = Random.Range(0, 20);
            int c = Random.Range(0, 20);
            Score.text = a.ToString() + b.ToString() + c.ToString();
            MissedScore.text = b.ToString() + a.ToString() + c.ToString();
            TotalHits.text = b.ToString() + c.ToString() + a.ToString();
            TotalAccuracy.text = c.ToString() + b.ToString() + a.ToString() + " %";
            yield return new WaitForSeconds(0.05f);
            count += 0.1f;
            if(count >= 5)
            {
                //Score.text = UIController.PlayerScore.ToString();
                //MissedScore.text = UIController.PlayerMissed.ToString();
                //TotalHits.text = UIController.PlayerTotalHits.ToString();
                //TotalAccuracy.text = UIController.GetAccuracy().ToString();


                buttonNext.enabled = true;
                buttonVisibility.enabled = true;
                yield break;
            }            
        }  
    }
}
