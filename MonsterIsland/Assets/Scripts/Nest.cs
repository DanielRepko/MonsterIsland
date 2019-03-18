using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelName {
    Hub = 0, Plains, Desert, Underwater, Jungle, Skyland, Castle
}

public enum LevelPosition {
    Start = 0, Shop, Boss
}

public class Nest : MonoBehaviour {

    public LevelName levelName;
    public LevelPosition levelPosition;
    public bool isActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activate() {
        if(!isActive) {
            isActive = true;
            NestManager.instance.ActivateNest((int)levelName, (int)levelPosition);
        }
    }
}
