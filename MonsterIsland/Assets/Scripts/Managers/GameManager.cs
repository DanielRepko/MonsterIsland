using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [Range(0, 3)]
    public int fileNumber;
    public GameFile gameFile;

    public float lastTimeUpdate;

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
    public void CreateSave() {
        //Store the top level Game File info
        GameFile newFile = new GameFile();
        newFile.fileID = fileNumber;
        newFile.totalPlayTime = 0;
        DateTime date = DateTime.Now;
        newFile.saveDate = date.ToShortDateString();
        newFile.saveArea = LevelName.Plains.ToString();
        newFile.saveNest = LevelPosition.Start.ToString();
        newFile.player = new PlayerInfo();
        newFile.gameProgression = new GameProgression();

        //Store infomration about the player themself
        newFile.player.name = "Mitch";
        newFile.player.totalHearts = 3;
        newFile.player.headPart = new HeadPartInfo();
        newFile.player.torsoPart = new TorsoPartInfo();
        newFile.player.leftArmPart = new ArmPartInfo();
        newFile.player.rightArmPart = new ArmPartInfo();
        newFile.player.legsPart = new LegPartInfo();
        newFile.player.inventory = new InventoryInfo();

        //Initial Player Inventory
        newFile.player.inventory.monsterBucks = 0;
        newFile.player.inventory.collectedParts = new CollectedPartsInfo();
        newFile.player.inventory.collectedParts.collectedHeads = new List<string>();
        newFile.player.inventory.collectedParts.collectedHeads.Add(Helper.MonsterName.Mitch);
        newFile.player.inventory.collectedParts.collectedTorsos = new List<string>();
        newFile.player.inventory.collectedParts.collectedTorsos.Add(Helper.MonsterName.Mitch);
        newFile.player.inventory.collectedParts.collectedLeftArms = new List<string>();
        newFile.player.inventory.collectedParts.collectedLeftArms.Add(Helper.MonsterName.Mitch);
        newFile.player.inventory.collectedParts.collectedRightArms = new List<string>();
        newFile.player.inventory.collectedParts.collectedRightArms.Add(Helper.MonsterName.Mitch);
        newFile.player.inventory.collectedParts.collectedLegs = new List<string>();
        newFile.player.inventory.collectedParts.collectedLegs.Add(Helper.MonsterName.Mitch);
        newFile.player.inventory.collectedWeapons = new List<string>();

        //Legendary Parts Collected
        newFile.gameProgression.collectedLegendaryParts = new CollectedLegendaryParts();
        newFile.gameProgression.collectedLegendaryParts.headCollected = false;
        newFile.gameProgression.collectedLegendaryParts.torsoCollected = false;
        newFile.gameProgression.collectedLegendaryParts.leftArmCollected = false;
        newFile.gameProgression.collectedLegendaryParts.rightArmCollected = false;
        newFile.gameProgression.collectedLegendaryParts.legsCollected = false;

        //Bosses Defeated
        newFile.gameProgression.defeatedBosses = new DefeatedBosses();
        newFile.gameProgression.defeatedBosses.plainsBossDefeated = false;
        newFile.gameProgression.defeatedBosses.desertBossDefeated = false;
        newFile.gameProgression.defeatedBosses.underwaterBossDefeated = false;
        newFile.gameProgression.defeatedBosses.jungleBossDefeated = false;
        newFile.gameProgression.defeatedBosses.skylandBossDefeated = false;
        newFile.gameProgression.defeatedBosses.castleBossDefeated = false;

        //Chests Opened
        newFile.gameProgression.openedChests = new OpenedChests();
        newFile.gameProgression.openedChests.plainsChest1 = false;
        newFile.gameProgression.openedChests.plainsChest2 = false;
        newFile.gameProgression.openedChests.hubChest = false;
        newFile.gameProgression.openedChests.desertChest1 = false;
        newFile.gameProgression.openedChests.desertChest2 = false;
        newFile.gameProgression.openedChests.underwaterChest1 = false;
        newFile.gameProgression.openedChests.underwaterChest2 = false;
        newFile.gameProgression.openedChests.jungleChest1 = false;
        newFile.gameProgression.openedChests.jungleChest2 = false;
        newFile.gameProgression.openedChests.skylandChest1 = false;
        newFile.gameProgression.openedChests.skylandChest2 = false;

        //Nests Activated
        newFile.gameProgression.nestInfo = new NestInfo();
        newFile.gameProgression.nestInfo.hubNest = true;
        newFile.gameProgression.nestInfo.plainsNest1 = true;
        newFile.gameProgression.nestInfo.plainsNest2 = false;
        newFile.gameProgression.nestInfo.plainsNest3 = false;
        newFile.gameProgression.nestInfo.desertNest1 = false;
        newFile.gameProgression.nestInfo.desertNest2 = false;
        newFile.gameProgression.nestInfo.desertNest3 = false;
        newFile.gameProgression.nestInfo.underwaterNest1 = false;
        newFile.gameProgression.nestInfo.underwaterNest2 = false;
        newFile.gameProgression.nestInfo.underwaterNest3 = false;
        newFile.gameProgression.nestInfo.jungleNest1 = false;
        newFile.gameProgression.nestInfo.jungleNest2 = false;
        newFile.gameProgression.nestInfo.jungleNest3 = false;
        newFile.gameProgression.nestInfo.skylandNest1 = false;
        newFile.gameProgression.nestInfo.skylandNest2 = false;
        newFile.gameProgression.nestInfo.skylandNest3 = false;
        newFile.gameProgression.nestInfo.castleNest1 = false;
        newFile.gameProgression.nestInfo.castleNest2 = false;
        newFile.gameProgression.nestInfo.castleNest3 = false;

        //Store the save file as the active gameFile, and save to a json file
        gameFile = newFile;
        var fileToJson = JsonUtility.ToJson(newFile);
        var savePath = System.IO.Path.Combine(Application.persistentDataPath, "file" + fileNumber + ".json");
        System.IO.File.WriteAllText(savePath, fileToJson);
        Debug.Log("Saved File" + fileNumber + " to " + savePath);
    }

    //Updates an existing save file
    public void FinalizeSave() {
        var time = Time.timeSinceLevelLoad;
        gameFile.totalPlayTime += (Time.timeSinceLevelLoad - lastTimeUpdate);
        lastTimeUpdate = time;
        gameFile.saveDate = DateTime.Now.ToShortDateString();
        var fileToJson = JsonUtility.ToJson(gameFile);
        var savePath = System.IO.Path.Combine(Application.persistentDataPath, "file" + fileNumber + ".json");
        System.IO.File.WriteAllText(savePath, fileToJson);
        Debug.Log("Saved File" + fileNumber + " to " + savePath);
    }

    public void DeleteSave() {
        var savePath = System.IO.Path.Combine(Application.persistentDataPath, "file" + fileNumber + ".json");
        System.IO.File.Delete(savePath);
        Debug.Log("File " + fileNumber + " deleted");
    }

    private void OnGUI() {
        if (GUILayout.Button("Force Save")) {
            FinalizeSave();
        }
    }
}
