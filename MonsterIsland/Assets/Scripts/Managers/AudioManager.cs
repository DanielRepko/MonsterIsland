using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;

    public AudioSource musicAudioSource;
    public AudioSource soundAudioSource;

	// Use this for initialization
	void Start () {
		if(Instance == null) {
            Instance = this;
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
}
