using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour {

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

    // Use this for initialization
    void Start () {
        currentMusicVolume = 10;
        currentSoundVolume = 10;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RaiseMusicVolume() {
        SetMusic(currentMusicVolume + 1);
    }

    public void RaiseSoundVolume() {
        SetSound(currentSoundVolume + 1);
    }

    public void LowerMusicVolume() {
        SetMusic(currentMusicVolume - 1);
    }

    public void LowerSoundVolume() {
        SetSound(currentSoundVolume - 1);
    }

    public void SetMusic(int volumeLevel) {
        if(volumeLevel > 10) {
            volumeLevel = 10;
        } else if (volumeLevel < 0) {
            volumeLevel = 0;
        }

        currentMusicVolume = volumeLevel;

        foreach(GameObject bar in musicActiveBars) {
            if(volumeLevel > 0) {
                bar.SetActive(true);
            } else {
                bar.SetActive(false);
            }
            volumeLevel--;
        }
    }

    public void SetSound(int volumeLevel) {
        if (volumeLevel > 10) {
            volumeLevel = 10;
        } else if (volumeLevel < 0) {
            volumeLevel = 0;
        }

        currentSoundVolume = volumeLevel;

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
