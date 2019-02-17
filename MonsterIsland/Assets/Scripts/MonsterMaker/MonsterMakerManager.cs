using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MonsterMakerManager : MonoBehaviour {

    public HeadSlot headSlot;
    public Button torsoSlot;
    public Button rightArmSlot;
    public Button leftArmSlot;
    public Button legsSlot;
    public Button rightWeaponSlot;
    public Button leftWeaponSlot;

    public PartEditor partEditor;
	
	// Use this for initialization
	void Start () {
        headSlot.originalPosition = headSlot.transform.localPosition;
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
                partEditor.OpenPartEditor(child.gameObject.GetComponent<PartSlot>());
            }
            else if(child.gameObject.name == "PartEditor")
            {
                //child is the Part Editor, don't do anything
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void HidePartEditor()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "WeaponPicker")
            {
                child.gameObject.SetActive(false);
            }
            else if (child.gameObject.name == "PartEditor")
            {
                partEditor.ClosePartEditor();
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}