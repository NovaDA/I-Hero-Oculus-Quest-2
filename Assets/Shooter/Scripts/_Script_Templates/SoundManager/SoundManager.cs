using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {


    public AudioClip[] MusicClip;
    public AudioClip[] soundClip;

    static AudioClip[] Music;
    static AudioClip[] SoundVFX;
    //static AudioSource prefabAudioSource;

    // Use this for initialization
    private void Awake()
    {
        Music = MusicClip;
        SoundVFX = soundClip;
        //prefabAudioSource = GameObject.FindGameObjectWithTag("SoundSFX").GetComponent<AudioSource>(); 
    }

    public static void PlayMusic()
    {
        AudioSource MusicSource = GameObject.Find("AudioSFX").GetComponent<AudioSource>();
        if (GameLevelManager.GetSceneName() == "Main_Menu")
        {
            AudioManager.instance.PlayMusic(Music[0], 2);

        }
        else if (GameLevelManager.GetSceneName() == "World1_1")
        {
            
        }
        else if (GameLevelManager.GetSceneName() == "HighScoreManagement")
        {

        }else if(GameLevelManager.GetSceneName() == "boss")
        {

        }
        else if(GameLevelManager.GetSceneName() == "GameOver")
        {

        }
       
    }

    //public static void PlaySFX(string TypeOfSound, Transform position)
    //{
    //    AudioSource MusicSource;
    //    MusicSource = Instantiate(prefabAudioSource, position.position, Quaternion.identity);

    //    if (TypeOfSound == "Spawn")
    //    {
    //        MusicSource.clip = SoundVFX[0];
    //        MusicSource.Play();
    //    }
    //    if(TypeOfSound == "Shoot")
    //    {
    //        MusicSource.clip = SoundVFX[1];
    //        MusicSource.volume = 0.1f;
    //        MusicSource.Play();
    //    }
    //    if (TypeOfSound == "Die")
    //    {
    //        MusicSource.clip = SoundVFX[2];
    //        MusicSource.Play();
    //    }
    //    if (TypeOfSound == "SpawnCry")
    //    {
    //        MusicSource.clip = SoundVFX[3];
    //        MusicSource.Play();
    //    }
    //    if (TypeOfSound == "Step")
    //    {
    //        MusicSource.clip = SoundVFX[4];
    //        MusicSource.Play();
    //    }
    //    if (TypeOfSound == "Despawn")
    //    {
    //        MusicSource.clip = SoundVFX[5];
    //        MusicSource.Play();
    //    }
    //    if(TypeOfSound == "Item")
    //    {
    //        MusicSource.clip = SoundVFX[6];
    //        MusicSource.Play();
    //    }
    //}


    #region OLD CODE
            //MusicSource.clip = Music[0];
            //MusicSource.Play();
            //MusicSource.loop = true;
    #endregion

}
