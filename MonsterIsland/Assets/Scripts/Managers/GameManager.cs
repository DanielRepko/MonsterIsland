using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [Range(0, 3)]
    public int FileNumber;
    private GameFile gameFile;

    public PlayerController player;
    public GameObject coinPrefab;
    public GameObject headDropPrefab;
    public GameObject leftArmDropPrefab;
    public GameObject rightArmDropPrefab;
    public GameObject torsoDropPrefab;
    public GameObject legsDropPrefab;

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Creates a new save file
    public void CreateSave(int fileNumber, string monsterName) {
        //Store the top level Game File info
        GameFile newFile = new GameFile();
        newFile.fileID = fileNumber;
        newFile.totalPlayTime = 0;
        DateTime date = DateTime.Now;
        newFile.saveDate = date.ToShortDateString();
        newFile.saveArea = LevelName.Hub.ToString();
        newFile.saveNest = LevelPosition.Start.ToString();
        newFile.player = new PlayerInfo();
        newFile.gameProgression = new GameProgression();

        //Store infomration about the player themself
        newFile.player.name = monsterName;
        newFile.player.totalHearts = 3;
    }

    //Updates an existing save file
    public void SaveGame() {
        GameFile fileToUpdate = gameFile;
    }
}
