using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum AudioChannel { Master, Sfx, Music};

    public float masterVolumePercent { get; private set; }
    public float sfxVolumePercent { get; private set; }
    public float musicVolumePercent { get; private set; }

    AudioSource sfxSource2D;
    AudioSource[] musicSources;
    int activeMusicSourceIndex;

    public static AudioManager instance;

    Transform audioListener;
    Transform player;

    SoundLibrary library;
    private void Awake(){

        if(instance != null){
            Destroy(gameObject);
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);

            library = GetComponent<SoundLibrary>();

            musicSources = new AudioSource[2];
            for (int i = 0; i < 2; i++){
                GameObject newMusicSource = new GameObject("Music source " + (i + 1));
                musicSources[i] = newMusicSource.AddComponent<AudioSource>();
                newMusicSource.transform.parent = transform;
            }

            GameObject newSfx2DSource = new GameObject("2D sfx source");
            sfxSource2D = newSfx2DSource.AddComponent<AudioSource>();
            newSfx2DSource.transform.parent = transform;

            audioListener = FindObjectOfType<AudioListener>().transform;
            if(FindObjectOfType<Player>() != null) { 
                player = FindObjectOfType<Player>().transform; }
                

            masterVolumePercent = PlayerPrefs.GetFloat("Master Vol", 1);
            sfxVolumePercent = PlayerPrefs.GetFloat("Sfx Vol", 1);
            musicVolumePercent = PlayerPrefs.GetFloat("Music Vol", 1);
        }

        
    }

    private void Update()
    {
        if(player != null){
            audioListener.position = player.position;
        }
    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
                Debug.Log(masterVolumePercent);
                break;
            case AudioChannel.Sfx:
                sfxVolumePercent = volumePercent;
                Debug.Log(sfxVolumePercent);
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                Debug.Log(musicVolumePercent);
                break;
        }

        

        musicSources[0].volume = musicVolumePercent * masterVolumePercent;
        musicSources[1].volume = musicVolumePercent * masterVolumePercent;

        PlayerPrefs.SetFloat("Master Vol", masterVolumePercent);
        PlayerPrefs.SetFloat("Sfx Vol", sfxVolumePercent);
        PlayerPrefs.SetFloat("Music Vol", musicVolumePercent);
        PlayerPrefs.Save();
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1){
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();

        StartCoroutine(AnimateMusicCrossFade(fadeDuration));
    }

    public void PlaySound(AudioClip clip, Vector3 pos){
        if(clip != null){
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
        }
        
    }

    public void PlaySound(string soundName, Vector3 pos){
        PlaySound(library.GetClipFromName(soundName), pos);
    }

    public void PlaySound2D(string soundName){
        sfxSource2D.PlayOneShot(library.GetClipFromName(soundName), sfxVolumePercent * masterVolumePercent);
    }
    IEnumerator AnimateMusicCrossFade(float duration){
        float percent = 0;

        while(percent < 1){
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp( musicVolumePercent * masterVolumePercent,0, percent);
            yield return null;
        } 
    }
}
