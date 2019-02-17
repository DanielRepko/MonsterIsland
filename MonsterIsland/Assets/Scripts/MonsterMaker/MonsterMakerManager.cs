using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MonsterMakerManager : MonoBehaviour {

    public Button headSlot;
    public Button torsoSlot;
    public Button rightArmSlot;
    public Button leftArmSlot;
    public Button legsSlot;
    public Button rightWeaponSlot;
    public Button leftWeaponSlot;
	
	// Use this for initialization
	void Start () {
        
	}

	// Update is called once per frame
	void Update () { 
		
	}

    public void ShowPartEditor(string partType)
    {
        foreach(Transform child in transform)
        {
            if(child.gameObject.name == (partType + "Slot"))
            {
                child.localPosition = new Vector3(0, 50f, 0);
                child.localScale = new Vector3(1.4f, 1.4f, 0);

            } else if(child.gameObject.name == "PartEditor")
            {
                child.gameObject.SetActive(true);

            } else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}