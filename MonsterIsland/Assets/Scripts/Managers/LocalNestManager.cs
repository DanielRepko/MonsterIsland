using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalNestManager : MonoBehaviour {

    public static LocalNestManager Instance;

    public GameObject startNest;
    public GameObject shopNest;
    public GameObject bossNest;

	// Use this for initialization
	void Start () {
        //Since each LocalNestManager is unique to their scene, the instance should update depending on the scene
        Instance = this;
        Debug.Log("LocalNestManager instance set");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLocalNests(bool startStatus, bool shopStatus, bool bossStatus) {
        startNest.GetComponent<Nest>().isActive = startStatus;

        if (shopNest != null) {
            shopNest.GetComponent<Nest>().isActive = shopStatus;
        }

        if (bossNest != null) {
            bossNest.GetComponent<Nest>().isActive = bossStatus;
        }
    }

    public void LoadNests() {
        GlobalNestManager.instance.LoadNests(startNest.GetComponent<Nest>().levelName.ToString());
    }

    public void ActivateLocalNest(LevelName levelName, LevelPosition levelPosition) {
        GlobalNestManager.instance.ActivateNest((int) levelName, (int) levelPosition);
    }

}
