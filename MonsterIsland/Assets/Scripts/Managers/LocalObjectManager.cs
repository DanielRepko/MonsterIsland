using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalObjectManager : MonoBehaviour {

    public static LocalObjectManager Instance;

    public GameObject startNest;
    public GameObject shopNest;
    public GameObject bossNest;
    public GameObject chestA;
    public GameObject chestB;

	// Use this for initialization
	void Start () {
        //Since each LocalObjectManager is unique to their scene, the instance should update depending on the scene
        Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLocalNests(bool startStatus, bool shopStatus, bool bossStatus) {
        startNest.GetComponent<Nest>().isActive = startStatus;
        UIManager.Instance.SetStartWarp(startStatus);

        if (shopNest != null) {
            shopNest.GetComponent<Nest>().isActive = shopStatus;
            UIManager.Instance.SetShopWarp(shopStatus);
        }

        if (bossNest != null) {
            bossNest.GetComponent<Nest>().isActive = bossStatus;
            UIManager.Instance.SetBossWarp(bossStatus);
        }
    }

    public void LoadLocalChests(bool chestAOpen, bool chestBOpen) {
        if (chestA != null) {
            chestA.GetComponent<Chest>().isOpen = chestAOpen;
            if(chestAOpen) {
                chestA.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/In-Game Sprites/Chest_Open");
                chestA.transform.position += new Vector3(0, 0.4f, 0);
            }
        }

        if (chestB != null) {
            chestB.GetComponent<Chest>().isOpen = chestBOpen;
            if(chestBOpen) {
                chestB.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/In-game Sprites/Chest_Open");
                chestB.transform.position += new Vector3(0, 0.4f, 0);
            }
        }
    }

    public void LoadObjects() {
        GlobalObjectManager.instance.LoadObjects(startNest.GetComponent<Nest>().levelName.ToString());
    }

    public void ActivateLocalNest(LevelName levelName, LevelPosition levelPosition) {
        GlobalObjectManager.instance.ActivateNest((int) levelName, (int) levelPosition);
        if(levelPosition == LevelPosition.Start) {
            UIManager.Instance.SetStartWarp(true);
        } else if (levelPosition == LevelPosition.Shop) {
            UIManager.Instance.SetShopWarp(true);
        } else if (levelPosition == LevelPosition.Boss) {
            UIManager.Instance.SetBossWarp(true);
        }
    }

    public void ActivateLocalChest(LevelName levelName, int chestID) {
        GlobalObjectManager.instance.OpenChest((int)levelName, chestID);
        if(chestID == chestA.GetComponent<Chest>().chestID) {
            chestA.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/In-Game Sprites/Chest_Open");
            chestA.transform.position += new Vector3(0, 0.4f, 0);
        } else if (chestID == chestB.GetComponent<Chest>().chestID) {
            chestB.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/In-game Sprites/Chest_Open");
            chestB.transform.position += new Vector3(0, 0.4f, 0);
        }
    }

}
