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
    private LocalNestManager localNestManager;

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
        switch(scene.name) {
            case "Hub":
            case "Plains":
            case "Desert":
            case "Jungle":
            case "Underwater":
            case "Skyland":
            case "Castle":
                localNestManager = GameObject.Find("LocalNestManager").GetComponent<LocalNestManager>();
                return;
            default:
                localNestManager = null;
                break;
        }
    }

    public void ActivateNest(int levelNameID, int levelPositionID) {
        gameNests[levelNameID, levelPositionID] = true;
    }
}
