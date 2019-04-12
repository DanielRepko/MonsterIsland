using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour {

    public static CutsceneManager Instance;
    public PlayableDirector director;
    public GameObject playerCamera;
    public GameObject gameplayCanvas;
    public TimelineAsset activateDesertGem;
    public TimelineAsset activateUnderwaterGem;
    public TimelineAsset activateJungleGem;
    public TimelineAsset finalDesertGem;
    public TimelineAsset finalUnderwaterGem;
    public TimelineAsset finalJungleGem;
    public TimelineAsset openGate;
    public TimelineAsset plainsBossStart;

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

        director.playableAsset = null;
        PlayerController.Instance.enabled = true;
        gameplayCanvas.SetActive(true);
        playerCamera.SetActive(true);
    }

    IEnumerator StartBossFight() {
        yield return new WaitForSeconds((float) director.duration);

        director.playableAsset = null;
        PlayerController.Instance.enabled = true;
        gameplayCanvas.SetActive(true);
        FindObjectOfType<Boss>().target = PlayerController.Instance.gameObject;
        AudioManager.Instance.PlayMusic(AudioManager.Instance.bossMusic, true);
    }

    private void SetupCutscene(bool stopMusic) {
        if(stopMusic) {
            AudioManager.Instance.musicAudioSource.Stop();
        }
        PlayerController.Instance.enabled = false;
        gameplayCanvas.SetActive(false);
        playerCamera.SetActive(false);
    }

    public void PlayActivateDesertGem() {
        SetupCutscene(false);

        director.Play(activateDesertGem);

        StartCoroutine("EndCutscene");
    }

    public void PlayActivateUnderwaterGem() {
        SetupCutscene(false);

        director.Play(activateUnderwaterGem);

        StartCoroutine("EndCutscene");
    }

    public void PlayActivateJungleGem() {
        SetupCutscene(false);

        director.Play(activateUnderwaterGem);

        StartCoroutine("EndCutscene");
    }

    public void PlayFinalDesertGem() {
        SetupCutscene(false);

        director.Play(finalDesertGem);

        StartCoroutine("EndCutscene");
    }

    public void PlayFinalUnderwaterGem() {
        SetupCutscene(false);

        director.Play(finalUnderwaterGem);

        StartCoroutine("EndCutscene");
    }

    public void PlayFinalJungleGem() {
        SetupCutscene(false);

        director.Play(finalJungleGem);

        StartCoroutine("EndCutscene");
    }

    public void PlayOpenGate() {
        SetupCutscene(false);

        director.Play(openGate);

        StartCoroutine("EndCutscene");
    }

    public void PlayPlainsBossStart() {
        SetupCutscene(true);

        director.Play(plainsBossStart);

        StartCoroutine("StartBossFight");
    }
}
