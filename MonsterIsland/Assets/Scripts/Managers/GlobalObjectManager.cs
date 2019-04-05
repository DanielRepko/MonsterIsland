using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalObjectManager : MonoBehaviour {

    public static GlobalObjectManager instance;
    public bool[,] gameNests = {
        { false, false, false },
        { false, false, false },
        { false, false, false },
        { false, false, false },
        { false, false, false },
        { false, false, false },
        { false, false, false }
    };
    public bool[,] gameChests = {
        { false, false },
        { false, false },
        { false, false },
        { false, false },
        { false, false },
        { false, false }
    };

    private GameObject localObjectManager;

	// Use this for initialization
	void Start () {
        if(instance == null) {
            instance = this;
            InitializeObjects();
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

    private void InitializeObjects() {
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

        gameChests[0, 0] = GameManager.instance.gameFile.gameProgression.openedChests.hubChest;
        gameChests[1, 0] = GameManager.instance.gameFile.gameProgression.openedChests.plainsChest1;
        gameChests[2, 0] = GameManager.instance.gameFile.gameProgression.openedChests.desertChest1;
        gameChests[3, 0] = GameManager.instance.gameFile.gameProgression.openedChests.underwaterChest1;
        gameChests[4, 0] = GameManager.instance.gameFile.gameProgression.openedChests.jungleChest1;
        gameChests[5, 0] = GameManager.instance.gameFile.gameProgression.openedChests.skylandChest1;

        SceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    public void LoadObjects(string scene) {
        if(localObjectManager != null) {
            switch(scene) {
                case "Hub":
                    localObjectManager.GetComponent<LocalObjectManager>().LoadLocalNests(gameNests[0, 0], false, false);
                    localObjectManager.GetComponent<LocalObjectManager>().LoadLocalChests(gameChests[0, 0], true);
                    break;
                case "Plains":
                    localObjectManager.GetComponent<LocalObjectManager>().LoadLocalNests(gameNests[1, 0], gameNests[1, 1], gameNests[1, 2]);
                    localObjectManager.GetComponent<LocalObjectManager>().LoadLocalChests(gameChests[1, 0], gameChests[1, 1]);
                    break;
                case "Desert":
                    localObjectManager.GetComponent<LocalObjectManager>().LoadLocalNests(gameNests[2, 0], gameNests[2, 1], gameNests[2, 2]);
                    localObjectManager.GetComponent<LocalObjectManager>().LoadLocalChests(gameChests[2, 0], gameChests[2, 1]);
                    break;
                case "Underwater":
                    localObjectManager.GetComponent<LocalObjectManager>().LoadLocalNests(gameNests[3, 0], gameNests[3, 1], gameNests[3, 2]);
                    localObjectManager.GetComponent<LocalObjectManager>().LoadLocalChests(gameChests[3, 0], gameChests[3, 1]);
                    break;
                case "Jungle":
                    localObjectManager.GetComponent<LocalObjectManager>().LoadLocalNests(gameNests[4, 0], gameNests[4, 1], gameNests[4, 2]);
                    localObjectManager.GetComponent<LocalObjectManager>().LoadLocalChests(gameChests[4, 0], gameChests[4, 1]);
                    break;
                case "Skyland":
                    localObjectManager.GetComponent<LocalObjectManager>().LoadLocalNests(gameNests[5, 0], gameNests[5, 1], gameNests[5, 2]);
                    localObjectManager.GetComponent<LocalObjectManager>().LoadLocalChests(gameChests[5, 0], gameChests[5, 1]);
                    break;
                case "Castle":
                    localObjectManager.GetComponent<LocalObjectManager>().LoadLocalNests(gameNests[6, 0], gameNests[6, 1], gameNests[6, 2]);
                    localObjectManager.GetComponent<LocalObjectManager>().LoadLocalChests(true, true);
                    break;
            }
        }
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode) {
        localObjectManager = GameObject.Find("LocalObjectManager");

        if(localObjectManager != null) {
            if (localObjectManager.scene.name == "Hub") {
                UIManager.Instance.DisableQuickTravelMenu();
            } else {
                UIManager.Instance.EnableQuickTravelMenu();
            }

            localObjectManager.GetComponent<LocalObjectManager>().LoadObjects();
        }
    }

    public void ActivateNest(int levelNameID, int levelPositionID) {
        gameNests[levelNameID, levelPositionID] = true;
        UpdateNestList();
    }

    public void OpenChest(int levelNameID, int chestID) {
        gameChests[levelNameID, chestID] = true;
        UpdateChestList();
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

    private void UpdateChestList() {
        var chestInfo = new OpenedChests();
        chestInfo.hubChest = gameChests[0, 0];
        chestInfo.plainsChest1 = gameChests[1, 0];
        chestInfo.plainsChest1 = gameChests[1, 1];
        chestInfo.desertChest1 = gameChests[2, 0];
        chestInfo.desertChest1 = gameChests[2, 1];
        chestInfo.underwaterChest1 = gameChests[3, 0];
        chestInfo.underwaterChest1 = gameChests[3, 1];
        chestInfo.jungleChest1 = gameChests[4, 0];
        chestInfo.jungleChest1 = gameChests[4, 1];
        chestInfo.skylandChest1 = gameChests[5, 0];
        chestInfo.skylandChest1 = gameChests[5, 1];
        GameManager.instance.gameFile.gameProgression.openedChests = chestInfo;
    }

    public void RestPressed() {
        PlayerController.Instance.health = PlayerController.Instance.maxHealth;
        UIManager.Instance.UpdateHeartCount();
        GameManager.instance.FinalizeSave();
        UIManager.Instance.HideNestCanvas();
    }
}
