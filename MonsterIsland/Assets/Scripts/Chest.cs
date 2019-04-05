using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    public LevelName levelName;
    public int chestID;
    public bool isOpen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Open() {
        if (!isOpen) {
            isOpen = true;
            LocalObjectManager.Instance.ActivateLocalChest(levelName, chestID);
            GameObject coin = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItems/Coin"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2), Quaternion.identity);
            coin.GetComponent<Coin>().value = 35;
        }
    }
}
