using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;

    public AudioSource musicAudioSource;
    public AudioSource soundAudioSource;
    public AudioMixer audioMixer;

    public AudioClip bossMusic;
    public AudioClip castleMusic;
    public AudioClip desertMusic;
    public AudioClip finalBossMusic;
    public AudioClip hubMusic;
    public AudioClip jungleMusic;
    public AudioClip mainMenuMusic;
    public AudioClip plainsMusic;
    public AudioClip skylandMusic;
    public AudioClip underwaterMusic;

    // Use this for initialization
    void Start () {
		if(Instance == null) {
            Instance = this;
            SceneManager.sceneLoaded += LoadLevelMusic;
            LoadLevelMusic(SceneManager.GetActiveScene(), LoadSceneMode.Single);
            Resources.FindObjectsOfTypeAll<SettingsManager>()[0].SetMusic(PlayerPrefs.GetInt("MusicVolume", 10));
            Resources.FindObjectsOfTypeAll<SettingsManager>()[0].SetSound(PlayerPrefs.GetInt("SoundVolume", 10));
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}
	
    public void PlayMusic(AudioClip song, bool loop) {
        if(musicAudioSource.clip == song) {
            return;
        }

        if(musicAudioSource.isPlaying) {
            musicAudioSource.Stop();
        }
        musicAudioSource.loop = loop;
        musicAudioSource.clip = song;
        musicAudioSource.Play();
    }

    public void PlaySound(AudioClip sound) {
        soundAudioSource.PlayOneShot(sound);
    }

	// Update is called once per frame
	void Update () {
		
	}

    //Starts playing music depending on which level scene was entered
    private void LoadLevelMusic(Scene scene, LoadSceneMode mode) {
        switch (scene.name) {
            case "Plains":
                    PlayMusic(plainsMusic, true);
                break;
            case "Hub":
                if (musicAudioSource.clip != hubMusic) {
                    PlayMusic(hubMusic, true);
                }
                break;
            case "Desert":
                PlayMusic(desertMusic, true);
                break;
            case "Underwater":
                PlayMusic(underwaterMusic, true);
                break;
            case "Jungle":
                PlayMusic(jungleMusic, true);
                break;
            case "Skyland":
                PlayMusic(skylandMusic, true);
                break;
            case "Castle":
                PlayMusic(castleMusic, true);
                break;
            case "MonsterMaker":
                if (musicAudioSource.clip != hubMusic) {
                    PlayMusic(hubMusic, true);
                }
                break;
        }
    }
}
