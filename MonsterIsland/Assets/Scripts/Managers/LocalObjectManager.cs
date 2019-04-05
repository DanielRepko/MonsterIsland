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

    public void LoadNests() {
        GlobalObjectManager.instance.LoadNests(startNest.GetComponent<Nest>().levelName.ToString());
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

}
