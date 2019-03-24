using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateNest(int levelNameID, int levelPositionID) {
        gameNests[levelNameID, levelPositionID] = true;
    }
}
