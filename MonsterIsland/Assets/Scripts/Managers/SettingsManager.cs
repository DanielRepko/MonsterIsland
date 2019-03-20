using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour {

    public static SettingsManager Instance;

    [Header("Music Objects", order = 0)]
    public List<GameObject> musicActiveBars;
    private int currentMusicVolume;

    [Header("Sound Objects", order = 1)]
    public List<GameObject> soundActiveBars;
    private int currentSoundVolume;

    [Header("Control Objects", order = 2)]
    public GameObject primaryButtonText;
    public GameObject secondaryButtonText;
    public GameObject leftButtonText;
    public GameObject rightButtonText;
    public GameObject jumpButtonText;
    public GameObject interactButtonText;
    public GameObject torsoButtonText;
    public GameObject headButtonText;

    [Header("Other Objects", order = 3)]
    public AudioMixer audioMixer;

    // Use this for initialization
    void Start () {
        if (Instance == null) {
            Instance = this;
            SetMusic(8);
            SetSound(8);
        } else if (Instance != this) {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Raises the music volume 1 level
    public void RaiseMusicVolume() {
        SetMusic(currentMusicVolume + 1);
    }

    //Raises the sound volume 1 level
    public void RaiseSoundVolume() {
        SetSound(currentSoundVolume + 1);
    }

    //Lowers the music volume 1 level
    public void LowerMusicVolume() {
        SetMusic(currentMusicVolume - 1);
    }

    //Lowers the sound volume 1 level
    public void LowerSoundVolume() {
        SetSound(currentSoundVolume - 1);
    }

    //Sets the current volume level for the music, both in here, in the audio mixer, and in the ui
    public void SetMusic(int volumeLevel) {
        if(volumeLevel > 10) {
            volumeLevel = 10;
        } else if (volumeLevel < 0) {
            volumeLevel = 0;
        }

        currentMusicVolume = volumeLevel;
        audioMixer.SetFloat("MusicVolume", (currentMusicVolume * 10 - 80));

        foreach(GameObject bar in musicActiveBars) {
            if(volumeLevel > 0) {
                bar.SetActive(true);
            } else {
                bar.SetActive(false);
            }
            volumeLevel--;
        }
    }

    //Sets the current volume level for the sound effects, both in here, in the audio mixer, and in the ui
    public void SetSound(int volumeLevel) {
        if (volumeLevel > 10) {
            volumeLevel = 10;
        } else if (volumeLevel < 0) {
            volumeLevel = 0;
        }

        currentSoundVolume = volumeLevel;
        audioMixer.SetFloat("SoundVolume", (currentSoundVolume * 10 - 80));

        foreach (GameObject bar in soundActiveBars) {
            if (volumeLevel > 0) {
                bar.SetActive(true);
            } else {
                bar.SetActive(false);
            }
            volumeLevel--;
        }
    }
}
