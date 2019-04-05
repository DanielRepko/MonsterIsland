using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory Instance;

    public int money;
    public CollectedPartsInfo collectedParts;

	// Use this for initialization
	void Start () {
		if(Instance == null) {
            Instance = this;
            collectedParts = new CollectedPartsInfo();
            collectedParts.collectedHeads = new List<string>();
            collectedParts.collectedTorsos = new List<string>();
            collectedParts.collectedLeftArms = new List<string>();
            collectedParts.collectedRightArms = new List<string>();
            collectedParts.collectedLegs = new List<string>();
            LoadInventory();
        } else if (Instance != this) {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddMoney(int amount) {
        money += amount;
        GameManager.instance.gameFile.player.inventory.monsterBucks = money;
    }

    public void AddMonsterPart(string monsterName, string partType) {
        switch(partType) {
            case Helper.PartType.Head:
                if(!collectedParts.collectedHeads.Contains(monsterName)) {
                    collectedParts.collectedHeads.Add(monsterName);
                    GameManager.instance.gameFile.player.inventory.collectedParts = collectedParts;
                } else {
                    AddMoney(10);
                }
                break;
            case Helper.PartType.Torso:
                if (!collectedParts.collectedTorsos.Contains(monsterName)) {
                    collectedParts.collectedTorsos.Add(monsterName);
                    GameManager.instance.gameFile.player.inventory.collectedParts = collectedParts;
                } else {
                    AddMoney(10);
                }
                break;
            case Helper.PartType.LeftArm:
                if (!collectedParts.collectedLeftArms.Contains(monsterName)) {
                    collectedParts.collectedLeftArms.Add(monsterName);
                    GameManager.instance.gameFile.player.inventory.collectedParts = collectedParts;
                } else {
                    AddMoney(10);
                }
                break;
            case Helper.PartType.RightArm:
                if (!collectedParts.collectedRightArms.Contains(monsterName)) {
                    collectedParts.collectedRightArms.Add(monsterName);
                    GameManager.instance.gameFile.player.inventory.collectedParts = collectedParts;
                } else {
                    AddMoney(10);
                }
                break;
            case Helper.PartType.Legs:
                if (!collectedParts.collectedLegs.Contains(monsterName)) {
                    collectedParts.collectedLegs.Add(monsterName);
                    GameManager.instance.gameFile.player.inventory.collectedParts = collectedParts;
                } else {
                    AddMoney(10);
                }
                break;
            default:
                AddMoney(10);
                break;
        }
    }

    private void LoadInventory() {
        money = GameManager.instance.gameFile.player.inventory.monsterBucks;
        collectedParts.collectedHeads = GameManager.instance.gameFile.player.inventory.collectedParts.collectedHeads;
        collectedParts.collectedTorsos = GameManager.instance.gameFile.player.inventory.collectedParts.collectedTorsos;
        collectedParts.collectedLeftArms = GameManager.instance.gameFile.player.inventory.collectedParts.collectedLeftArms;
        collectedParts.collectedRightArms = GameManager.instance.gameFile.player.inventory.collectedParts.collectedRightArms;
        collectedParts.collectedLegs = GameManager.instance.gameFile.player.inventory.collectedParts.collectedLegs;
    }
}
