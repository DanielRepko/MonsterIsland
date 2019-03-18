using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelName {
    Hub, Plains, Desert, Underwater, Jungle, Skyland, Castle
}

public enum LevelPosition {
    Start, Shop, Boss
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
}
