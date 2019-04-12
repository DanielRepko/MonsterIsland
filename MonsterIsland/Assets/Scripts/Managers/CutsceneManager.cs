using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {
    
    private PlayableDirector director;

    private void Start() {
        director = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator EndCutscene() {
        yield return new WaitForSeconds((float) director.duration);

        GetComponent<PlayableDirector>().playableAsset = null;
        PlayerController.Instance.enabled = true;
        PlayerController.Instance.gameObject.transform.Find("GameplayCanvas").gameObject.SetActive(true);
        PlayerController.Instance.gameObject.transform.Find("Main Camera").gameObject.SetActive(true);
    }

    IEnumerator StartBossFight() {
        yield return new WaitForSeconds((float) director.duration);

        GetComponent<PlayableDirector>().playableAsset = null;
        PlayerController.Instance.enabled = true;
        PlayerController.Instance.gameObject.transform.Find("GameplayCanvas").gameObject.SetActive(true);
        FindObjectOfType<Boss>().target = PlayerController.Instance.gameObject;
        AudioManager.Instance.PlayMusic(AudioManager.Instance.bossMusic, true);
    }

    private void SetupCutscene(bool stopMusic) {
        if(stopMusic) {
            AudioManager.Instance.musicAudioSource.Stop();
        }
        PlayerController.Instance.enabled = false;
        PlayerController.Instance.gameObject.transform.Find("GameplayCanvas").gameObject.SetActive(false);
        PlayerController.Instance.gameObject.transform.Find("Main Camera").gameObject.SetActive(false);
    }

    public void PlayActivateDesertGem() {
        SetupCutscene(false);

        director.Play(Resources.Load<TimelineAsset>("ActivateDesertGem"));

        StartCoroutine("EndCutscene");
    }

    public void PlayActivateUnderwaterGem() {
        SetupCutscene(false);

        director.Play(Resources.Load<TimelineAsset>("ActivateUnderwaterGem"));

        StartCoroutine("EndCutscene");
    }

    public void PlayActivateJungleGem() {
        SetupCutscene(false);

        director.Play(Resources.Load<TimelineAsset>("ActivateJungleGem"));

        StartCoroutine("EndCutscene");
    }

    public void PlayFinalDesertGem() {
        SetupCutscene(false);

        director.Play(Resources.Load<TimelineAsset>("FinalDesertGem"));

        StartCoroutine("EndCutscene");
    }

    public void PlayFinalUnderwaterGem() {
        SetupCutscene(false);

        director.Play(Resources.Load<TimelineAsset>("FinalUnderwaterGem"));

        StartCoroutine("EndCutscene");
    }

    public void PlayFinalJungleGem() {
        SetupCutscene(false);

        director.Play(Resources.Load<TimelineAsset>("FinalJungleGem"));

        StartCoroutine("EndCutscene");
    }

    public void PlayOpenGate() {
        SetupCutscene(false);

        director.Play(Resources.Load<TimelineAsset>("OpenGate"));

        StartCoroutine("EndCutscene");
    }

    public void PlayBossStart() {
        SetupCutscene(true);

        director.Play();

        StartCoroutine("StartBossFight");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            switch (SceneManager.GetActiveScene().name) {
                case "Plains":
                case "Desert":
                case "Underwater":
                case "Jungle":
                    PlayBossStart();
                    break;
            }
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
