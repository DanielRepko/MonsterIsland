using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalNestManager : MonoBehaviour {

    public static GlobalNestManager instance;
    public bool[,] gameNests = {
        { false, false, false },
        { false, false, false },
        { false, false, false },
        { false, false, false },
        { false, false, false },
        { false, false, false },
        { false, false, false }
    };

    private GameObject localNestManager;

	// Use this for initialization
	void Start () {
        if(instance == null) {
            instance = this;
            InitializeNests();
            SceneManager.sceneLoaded += SceneLoaded;
            SceneLoaded(gameObject.scene, LoadSceneMode.Single);
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InitializeNests() {
        gameNests[0, 0] = GameManager.instance.gameFile.gameProgression.nestInfo.hubNest;
        gameNests[1, 0] = GameManager.instance.gameFile.gameProgression.nestInfo.plainsNest1;
        gameNests[1, 1] = GameManager.instance.gameFile.gameProgression.nestInfo.plainsNest2;
        gameNests[1, 2] = GameManager.instance.gameFile.gameProgression.nestInfo.plainsNest3;
        gameNests[2, 0] = GameManager.instance.gameFile.gameProgression.nestInfo.desertNest1;
        gameNests[2, 1] = GameManager.instance.gameFile.gameProgression.nestInfo.desertNest2;
        gameNests[2, 2] = GameManager.instance.gameFile.gameProgression.nestInfo.desertNest3;
        gameNests[3, 0] = GameManager.instance.gameFile.gameProgression.nestInfo.underwaterNest1;
        gameNests[3, 1] = GameManager.instance.gameFile.gameProgression.nestInfo.underwaterNest2;
        gameNests[3, 2] = GameManager.instance.gameFile.gameProgression.nestInfo.underwaterNest3;
        gameNests[4, 0] = GameManager.instance.gameFile.gameProgression.nestInfo.jungleNest1;
        gameNests[4, 1] = GameManager.instance.gameFile.gameProgression.nestInfo.jungleNest2;
        gameNests[4, 2] = GameManager.instance.gameFile.gameProgression.nestInfo.jungleNest3;
        gameNests[5, 0] = GameManager.instance.gameFile.gameProgression.nestInfo.skylandNest1;
        gameNests[5, 1] = GameManager.instance.gameFile.gameProgression.nestInfo.skylandNest2;
        gameNests[5, 2] = GameManager.instance.gameFile.gameProgression.nestInfo.skylandNest3;
        gameNests[6, 0] = GameManager.instance.gameFile.gameProgression.nestInfo.castleNest1;
        gameNests[6, 1] = GameManager.instance.gameFile.gameProgression.nestInfo.castleNest2;
        gameNests[6, 2] = GameManager.instance.gameFile.gameProgression.nestInfo.castleNest3;
        SceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }
    
    public void LoadNests(string scene) {
        if(localNestManager != null) {
            switch(scene) {
                case "Hub":
                    localNestManager.GetComponent<LocalNestManager>().LoadLocalNests(gameNests[0, 0], false, false);
                    break;
                case "Plains":
                    localNestManager.GetComponent<LocalNestManager>().LoadLocalNests(gameNests[1, 0], gameNests[1, 1], gameNests[1, 2]);
                    break;
                case "Desert":
                    localNestManager.GetComponent<LocalNestManager>().LoadLocalNests(gameNests[2, 0], gameNests[2, 1], gameNests[2, 2]);
                    break;
                case "Underwater":
                    localNestManager.GetComponent<LocalNestManager>().LoadLocalNests(gameNests[3, 0], gameNests[3, 1], gameNests[3, 2]);
                    break;
                case "Jungle":
                    localNestManager.GetComponent<LocalNestManager>().LoadLocalNests(gameNests[4, 0], gameNests[4, 1], gameNests[4, 2]);
                    break;
                case "Skyland":
                    localNestManager.GetComponent<LocalNestManager>().LoadLocalNests(gameNests[5, 0], gameNests[5, 1], gameNests[5, 2]);
                    break;
                case "Castle":
                    localNestManager.GetComponent<LocalNestManager>().LoadLocalNests(gameNests[6, 0], gameNests[6, 1], gameNests[6, 2]);
                    break;
            }
        }
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode) {
        localNestManager = GameObject.Find("LocalNestManager");

        if(localNestManager != null) {
            if (localNestManager.scene.name == "Hub") {
                UIManager.Instance.DisableQuickTravelMenu();
            } else {
                UIManager.Instance.EnableQuickTravelMenu();
            }

            localNestManager.GetComponent<LocalNestManager>().LoadNests();
        }
    }

    public void ActivateNest(int levelNameID, int levelPositionID) {
        gameNests[levelNameID, levelPositionID] = true;
        UpdateNestList();
    }

    private void UpdateNestList() {
        var nestInfo = new NestInfo();
        nestInfo.hubNest = gameNests[0, 0];
        nestInfo.plainsNest1 = gameNests[1, 0];
        nestInfo.plainsNest2 = gameNests[1, 1];
        nestInfo.plainsNest3 = gameNests[1, 2];
        nestInfo.desertNest1 = gameNests[2, 0];
        nestInfo.desertNest2 = gameNests[2, 1];
        nestInfo.desertNest3 = gameNests[2, 2];
        nestInfo.underwaterNest1 = gameNests[3, 0];
        nestInfo.underwaterNest2 = gameNests[3, 1];
        nestInfo.underwaterNest3 = gameNests[3, 2];
        nestInfo.jungleNest1 = gameNests[4, 0];
        nestInfo.jungleNest2 = gameNests[4, 1];
        nestInfo.jungleNest3 = gameNests[4, 2];
        nestInfo.skylandNest1 = gameNests[5, 0];
        nestInfo.skylandNest2 = gameNests[5, 1];
        nestInfo.skylandNest3 = gameNests[5, 2];
        nestInfo.castleNest1 = gameNests[6, 0];
        nestInfo.castleNest2 = gameNests[6, 1];
        nestInfo.castleNest3 = gameNests[6, 2];
        GameManager.instance.gameFile.gameProgression.nestInfo = nestInfo;
    }

    public void RestPressed() {
        PlayerController.Instance.health = PlayerController.Instance.maxHealth;
        UIManager.Instance.UpdateHeartCount();
        GameManager.instance.FinalizeSave();
        UIManager.Instance.HideNestCanvas();
    }
}
