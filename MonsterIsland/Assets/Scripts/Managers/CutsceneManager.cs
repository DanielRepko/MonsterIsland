using System.Collections;
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
    public TimelineAsset activateUnderwaterGem;
    public TimelineAsset activateJungleGem;
    public TimelineAsset finalDesertGem;
    public TimelineAsset finalUnderwaterGem;
    public TimelineAsset finalJungleGem;
    public TimelineAsset openGate;

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

    IEnumerator EndCutscene() {
        yield return new WaitForSeconds((float) director.duration);

        playerController.enabled = true;
        gameplayCanvas.SetActive(true);
        playerCamera.SetActive(true);
    }

    private void SetupCutscene() {
        playerController.enabled = false;
        gameplayCanvas.SetActive(false);
        playerCamera.SetActive(false);
    }

    public void PlayActivateDesertGem() {
        SetupCutscene();

        director.Play(activateDesertGem);

        StartCoroutine("EndCutscene");
    }

    public void PlayActivateUnderwaterGem() {
        SetupCutscene();

        director.Play(activateUnderwaterGem);

        StartCoroutine("EndCutscene");
    }

    public void PlayActivateJungleGem() {
        SetupCutscene();

        director.Play(activateUnderwaterGem);

        StartCoroutine("EndCutscene");
    }

    public void PlayFinalDesertGem() {
        SetupCutscene();

        director.Play(finalDesertGem);

        StartCoroutine("EndCutscene");
    }

    public void PlayFinalUnderwaterGem() {
        SetupCutscene();

        director.Play(finalUnderwaterGem);

        StartCoroutine("EndCutscene");
    }

    public void PlayFinalJungleGem() {
        SetupCutscene();

        director.Play(finalJungleGem);

        StartCoroutine("EndCutscene");
    }

    public void PlayOpenGate() {
        SetupCutscene();

        director.Play(openGate);

        StartCoroutine("EndCutscene");
    }
}
