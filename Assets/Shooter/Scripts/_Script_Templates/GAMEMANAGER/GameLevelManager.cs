using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelManager : MonoBehaviour {    // Change name to something more appropriate

    // Use this for initialization
    public string[] levelScenes;
    static string[] levels;
    static string SceneName;
    static int LevelIndex = 0;

    public static GameLevelManager instance;
    private void Awake()
    {
        if (instance != null){
            Destroy(gameObject);
        }
        else{
            instance = this;
            DontDestroyOnLoad(gameObject);
            levels = levelScenes;
        }
    }
    
    public void LoadNextLevel()
    {
       LevelIndex++;
       SceneManager.LoadScene(levels[LevelIndex]);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene(levels[0]);
    }

    public void HighScore()
    {
        SceneManager.LoadScene(levels[1]);
    }

    public void Continue()
    {
        SceneManager.LoadScene(levels[LevelIndex]);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(levels[2]);
    }

    public void Quit()
    {
        Application.Quit();
    }

    static public string GetSceneName()
    {
        SceneName = SceneManager.GetActiveScene().name;
        return SceneName;
    }


    #region manage Music on Loading new Levels

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        #region
        // Debug.Log(" Level Delegate " + " Level Loaded ");
        // Debug.Log(scene.name);
        //Debug.Log(mode);
        #endregion

        
        //SoundManager.PlayMusic();
        if(GetSceneName() == "Main_Menu")
        {
            //AudioManager.instance.PlayMusic()
            GameManager.State = (int)GameManager.GameState.MainMenu;
            LevelIndex = 0;
        }
        else if(GetSceneName() == "boss")
        {
            GameManager.State = (int)GameManager.GameState.Boss;
        }
        else
        {
            GameManager.State = (int)GameManager.GameState.Playing;
        }
        Debug.Log(GameManager.State);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            HighScore();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            LoadGameOver();
        }
    }

    #endregion

}
