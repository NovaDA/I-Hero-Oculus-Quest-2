using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip mainTheme;
    public AudioClip menuTheme;
    string sceneName;

    private void Start(){
        OnLevelWasLoaded(0);
    }
    // Update is called once per frame
    private void OnLevelWasLoaded(int sceneIndex){
        string newSceneName = SceneManager.GetActiveScene().name;
        if(newSceneName != sceneName){
            sceneName = newSceneName;
            Invoke("PlayMusic", 0.2f);
        }
    }

    void PlayMusic(){
        AudioClip clipToPlay = null;

        if(sceneName == "Main_Menu")
        {
            clipToPlay = menuTheme;
        }else if(sceneName == "Game_Scene")
        {
            clipToPlay = mainTheme;
        }

        if(clipToPlay != null)
        {
            AudioManager.instance.PlayMusic(clipToPlay, 2);
            Invoke("PlayMusic", clipToPlay.length);
        }
    }
}
