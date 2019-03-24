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
            SceneManager.sceneLoaded += SceneLoaded;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void LoadNests(string scene) {
        Debug.Log("LoadNests Started");
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

        if(scene.name == "Hub") {
            UIManager.Instance.DisableQuickTravelMenu();
        } else {
            UIManager.Instance.EnableQuickTravelMenu();
        }

        localNestManager = GameObject.Find("LocalNestManager");
        if(localNestManager != null) {
            localNestManager.GetComponent<LocalNestManager>().LoadNests();
        }
    }

    public void ActivateNest(int levelNameID, int levelPositionID) {
        gameNests[levelNameID, levelPositionID] = true;
    }
}
