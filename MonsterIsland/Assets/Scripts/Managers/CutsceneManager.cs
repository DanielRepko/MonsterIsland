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
        var bosses = GameManager.instance.gameFile.gameProgression.defeatedBosses;
        bool hasBeenDefeated = false;
        bool desert = false;
        bool water = false;
        bool jungle = false;

        //Check if i've already been defeated. If I have, destroy myself.
        switch (SceneManager.GetActiveScene().name) {
            case "Hub":
                if(GameManager.instance.gameFile.gameProgression.defeatedBosses.desertBossDefeated) {
                    desert = true;
                    GameObject.Find("DesertGem").GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                if (GameManager.instance.gameFile.gameProgression.defeatedBosses.underwaterBossDefeated) {
                    water = true;
                    GameObject.Find("UnderwaterGem").GetComponent<SpriteRenderer>().color = Color.blue;
                }
                if (GameManager.instance.gameFile.gameProgression.defeatedBosses.jungleBossDefeated) {
                    jungle = true;
                    GameObject.Find("JungleGem").GetComponent<SpriteRenderer>().color = Color.green;
                }
                if(desert && water && jungle) {
                    GameObject.Find("TransitionObjects").transform.Find("HubToSkylandTransition").gameObject.SetActive(true);
                }
                if(GameManager.instance.gameFile.gameProgression.defeatedBosses.skylandBossDefeated) {
                    GameObject.Find("TransitionObjects").transform.Find("HubToCastleTransition").gameObject.SetActive(true);
                    GameObject.Find("CastleBarGroup").SetActive(false);
                }
                break;
            case "Plains":
                if (bosses.plainsBossDefeated) {
                    hasBeenDefeated = true;
                }
                break;
            case "Desert":
                if (bosses.desertBossDefeated) {
                    hasBeenDefeated = true;
                }
                break;
            case "Underwater":
                if (bosses.underwaterBossDefeated) {
                    hasBeenDefeated = true;
                }
                break;
            case "Jungle":
                if (bosses.jungleBossDefeated) {
                    hasBeenDefeated = true;
                }
                break;
            case "Skyland":
                if (bosses.skylandBossDefeated) {
                    hasBeenDefeated = true;
                }
                break;
        }

        if (hasBeenDefeated) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator EndCutscene() {
        yield return new WaitForSeconds((float) director.duration);
        
        PlayerController.Instance.enabled = true;
        PlayerController.Instance.gameObject.transform.Find("GameplayCanvas").gameObject.SetActive(true);
        PlayerController.Instance.gameObject.transform.Find("Main Camera").gameObject.SetActive(true);
        if(!AudioManager.Instance.musicAudioSource.isPlaying) {
            AudioManager.Instance.LoadLevelMusic(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }
    }

    IEnumerator StartBossFight() {
        yield return new WaitForSeconds((float) director.duration);
        
        PlayerController.Instance.enabled = true;
        PlayerController.Instance.gameObject.transform.Find("GameplayCanvas").gameObject.SetActive(true);
        FindObjectOfType<Boss>().target = PlayerController.Instance.gameObject;
        AudioManager.Instance.PlayMusic(AudioManager.Instance.bossMusic, true);
    }

    IEnumerator StartSkylandBossFight() {
        yield return new WaitForSeconds((float) director.duration);

        PlayerController.Instance.enabled = true;
        PlayerController.Instance.gameObject.transform.Find("GameplayCanvas").gameObject.SetActive(true);
        AudioManager.Instance.PlayMusic(AudioManager.Instance.bossMusic, true);
        GameObject.Find("BossFightDirector").GetComponent<PlayableDirector>().Play();
    }

    IEnumerator StartFinalBossFight() {
        yield return new WaitForSeconds((float) director.duration);
        PlayerController.Instance.enabled = true;
        PlayerController.Instance.gameObject.transform.Find("GameplayCanvas").gameObject.SetActive(true);
        GameObject.Find("BossFightDirector").GetComponent<PlayableDirector>().Play();
        AudioManager.Instance.PlayMusic(AudioManager.Instance.finalBossMusic, true);
    }

    public void StopBossFight() {
        var boss = FindObjectOfType<Boss>();
        if(boss != null) {
            FindObjectOfType<Boss>().target = null;
        }
        AudioManager.Instance.musicAudioSource.Stop();
        PlayerController.Instance.enabled = false;
        PlayerController.Instance.gameObject.transform.Find("GameplayCanvas").gameObject.SetActive(false);
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

    public void PlaySkylandBossStart() {
        SetupCutscene(true);

        director.Play();

        StartCoroutine("StartSkylandBossFight");
    }

    public void PlayFinalBossStart() {
        SetupCutscene(true);

        director.Play();

        StartCoroutine("StartFinalBossFight");
    }

    public void PlayBossEnd() {
        StopBossFight();

        director.Play();

        StartCoroutine("EndCutscene");
    }

    public void PlaySkylandBossEnd() {
        GameObject.Find("BossFightDirector").GetComponent<PlayableDirector>().Stop();
        AudioManager.Instance.musicAudioSource.Stop();
        PlayerController.Instance.enabled = false;
        PlayerController.Instance.gameObject.transform.Find("GameplayCanvas").gameObject.SetActive(false);

        director.Play();

        StartCoroutine("EndCutscene");
    }

    public void PlayFinalBossEnd() {
        GameObject.Find("BossFightDirector").GetComponent<PlayableDirector>().Stop();
        AudioManager.Instance.musicAudioSource.Stop();
        PlayerController.Instance.enabled = false;
        PlayerController.Instance.gameObject.transform.Find("GameplayCanvas").gameObject.SetActive(false);

        director.Play();

        StartCoroutine("LoadCredits");
    }

    IEnumerator LoadCredits() {
        yield return new WaitForSeconds((float) director.duration);
        GameManager.instance.gameFile = null;
        GameManager.instance.fileNumber = -1;
        foreach (Transform manager in ManagerManager.instance.transform) {
            Destroy(manager.gameObject);
        }
        Destroy(ManagerManager.instance.gameObject);
        Destroy(PlayerController.Instance.gameObject);
        SceneManager.LoadScene("Credits");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && gameObject.name != "EndOfFightTrigger") {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            switch (SceneManager.GetActiveScene().name) {
                case "Plains":
                case "Desert":
                case "Underwater":
                case "Jungle":
                    PlayBossStart();
                    break;
                case "Skyland":
                    PlaySkylandBossStart();
                    break;
                case "Castle":
                    PlayFinalBossStart();
                    break;
            }
        } else if (collision.tag == "Player" && gameObject.name == "EndOfFightTrigger") {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            switch (SceneManager.GetActiveScene().name) {
                case "Plains":
                case "Desert":
                case "Underwater":
                case "Jungle":
                    PlayBossEnd();
                    break;
                case "Skyland":
                    PlaySkylandBossEnd();
                    break;
                case "Castle":
                    PlayFinalBossEnd();
                    break;
            }
        }

        if(collision.tag == "Player" && SceneManager.GetActiveScene().name == "Hub") {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            var progress = GameManager.instance.gameFile.gameProgression;

            //If the desert has been cleared AND the desert gem cutscene has not been viewed
            if(progress.defeatedBosses.desertBossDefeated && !progress.viewedCutscenes.desertGem) {
                //Mark the desert gem as played
                GameManager.instance.gameFile.gameProgression.viewedCutscenes.desertGem = true;

                //If the underwater and jungle bosses have already been defeated
                if(progress.viewedCutscenes.underwaterGem && progress.viewedCutscenes.jungleGem) {
                    //Play final desert gem
                    PlayFinalDesertGem();
                } else {
                    //Otherwise, play normal desert gem
                    PlayActivateDesertGem();
                }
                //Otherwise, if underwater was cleared AND the underwater cutscene has not been viewed
            } else if (progress.defeatedBosses.underwaterBossDefeated && !progress.viewedCutscenes.underwaterGem) {
                //Mark the underwater cutscene as viewed
                GameManager.instance.gameFile.gameProgression.viewedCutscenes.underwaterGem = true;

                //If desert and jungle have been cleared, play final gem version. Otherwise, play normal.
                if(progress.viewedCutscenes.desertGem && progress.viewedCutscenes.jungleGem) {
                    PlayFinalUnderwaterGem();
                } else {
                    PlayActivateUnderwaterGem();
                }
                //Otherwise, just do all that again but with jungle
            } else if (progress.defeatedBosses.jungleBossDefeated && !progress.viewedCutscenes.jungleGem) {
                //Mark the jungle cutscene as viewed
                GameManager.instance.gameFile.gameProgression.viewedCutscenes.jungleGem = true;

                //If desert and underwater have been cleared, play final gem version. Otherwise, play normal.
                if (progress.viewedCutscenes.desertGem && progress.viewedCutscenes.underwaterGem)
                {
                    PlayFinalJungleGem();
                }
                else
                {
                    PlayActivateJungleGem();
                }
                //IF IT'S NONE OF THOSE, check if skyland was cleared. If it was, castle cutscene.
            } else if(progress.defeatedBosses.skylandBossDefeated && !progress.viewedCutscenes.castleGate) {
                GameManager.instance.gameFile.gameProgression.viewedCutscenes.castleGate = true;
                PlayOpenGate();
            }
        }
    }
}
