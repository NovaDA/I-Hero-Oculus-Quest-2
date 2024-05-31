using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameUI : MonoBehaviour
{
    public Image fadePlane;
    public GameObject gameOverUI;

    public RectTransform newWaveBanner;
    public TextMeshProUGUI newWaveTitle;
    public TextMeshProUGUI newWaveEnemyCount;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI gameOverScoreUI;
    public RectTransform healthBar;

    Spawner spawner;
    Player player;

    private void Awake(){
        spawner = FindObjectOfType<Spawner>();
        spawner.OnNewWave += OnNewWave;
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.OnDeath += OnGameOver;
    }

    private void Update()
    {
        scoreUI.text = ScoreKeeper.score.ToString("D6");
        float healthPercent = 0;
        if(player != null){
            healthPercent = player.health / player.startingHealth; 
        }
        healthBar.localScale = new Vector3(healthPercent, 1, 1);
    }

    void OnNewWave(int waveNumber){
        newWaveTitle.text = "- Wave " + waveNumber + " -";
        newWaveEnemyCount.text = "Enemies: " + spawner.waves[waveNumber - 1].enemyCount;

        StopCoroutine("AnimateWaveBanner");
        StartCoroutine("AnimateWaveBanner");
    }

    IEnumerator AnimateWaveBanner()
    {
        float delayTime = 2f;
        float speed = 3f;
        float animationPercent = 0;
        int dir = 1;

        float endDelayTime = Time.time + 1 / speed + delayTime;
        while (animationPercent >= 0){
            animationPercent += Time.deltaTime * speed * dir;
            if(animationPercent >= 1){
                animationPercent = 1;
                if(Time.time > endDelayTime)
                {
                    dir = -1;
                }
            }

            newWaveBanner.anchoredPosition = Vector2.up * Mathf.Lerp(-100, 100, animationPercent);
            yield return null;
        }

    }
    // Start is called before the first frame update
    

    void OnGameOver(){
        Cursor.visible = true;
        StartCoroutine(Fade(Color.clear, new Color(0,0,0,0.95f), 1));
        gameOverScoreUI.text = scoreUI.text;
        scoreUI.gameObject.SetActive(false);
        healthBar.transform.parent.gameObject.SetActive(false);
        gameOverUI.SetActive(true);
    }

    IEnumerator Fade(Color from, Color to, float time){
        float speed = 1f / time;
        float percent = 0f;

        while ( percent < 1)
        {
            percent += Time.deltaTime * speed;
            fadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
        }
    }

    // UI Input
    public void StartNewGame(){
        SceneManager.LoadScene("Game_Scene");    
    }

    public void ReturnToMainMenu(){

    }
}
