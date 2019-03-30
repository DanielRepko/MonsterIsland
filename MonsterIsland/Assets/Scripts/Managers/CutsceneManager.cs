﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour {

    public static CutsceneManager Instance;
    public PlayableDirector director;
    public GameObject playerCamera;
    public PlayerController playerController;
    public GameObject gameplayCanvas;
    public TimelineAsset activateDesertGem;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
