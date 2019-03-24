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

	// Use this for initialization
	void Start () {
        if(instance == null) {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if(LocalNestManager.Instance != null) {
            switch(scene.name) {
                case "Hub":
                    LocalNestManager.Instance.LoadLocalNests(gameNests[0, 0], false, false);
                    break;
                case "Plains":
                    LocalNestManager.Instance.LoadLocalNests(gameNests[1, 0], gameNests[1, 1], gameNests[1, 2]);
                    break;
                case "Desert":
                    LocalNestManager.Instance.LoadLocalNests(gameNests[2, 0], gameNests[2, 1], gameNests[2, 2]);
                    break;
                case "Underwater":
                    LocalNestManager.Instance.LoadLocalNests(gameNests[3, 0], gameNests[3, 1], gameNests[3, 2]);
                    break;
                case "Jungle":
                    LocalNestManager.Instance.LoadLocalNests(gameNests[4, 0], gameNests[4, 1], gameNests[4, 2]);
                    break;
                case "Skyland":
                    LocalNestManager.Instance.LoadLocalNests(gameNests[5, 0], gameNests[5, 1], gameNests[5, 2]);
                    break;
                case "Castle":
                    LocalNestManager.Instance.LoadLocalNests(gameNests[6, 0], gameNests[6, 1], gameNests[6, 2]);
                    break;
            }
        }
    }

    public void ActivateNest(int levelNameID, int levelPositionID) {
        gameNests[levelNameID, levelPositionID] = true;
    }
}
