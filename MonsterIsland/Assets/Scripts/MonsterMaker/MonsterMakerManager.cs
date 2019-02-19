using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Xml;

public class MonsterMakerManager : MonoBehaviour {

    public HeadSlot headSlot;
    public TorsoSlot torsoSlot;
    public Button rightArmSlot;
    public Button leftArmSlot;
    public Button legsSlot;
    public Button rightWeaponSlot;
    public Button leftWeaponSlot;

    public PartEditor partEditor;

    public CollectedPartsInfo availableParts;

    public void ShowPartEditor(string partType)
    {
        //iterating through all objects inside the MonsterMaker canvas
        foreach(Transform child in transform)
        {
            //if child is the selected slot
            if(child.gameObject.name == (partType + "Slot"))
            {
                partEditor.OpenPartEditor(child.gameObject.GetComponent<PartSlot>());
            }
            else if(child.gameObject.name == "PartEditor")
            {
                //child is the Part Editor, don't do anything
            }
            //anything else should be disabled
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void HidePartEditor()
    {
        //iterating through all objects inside the MonsterMaker canvas
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
            //anything else should be enabled
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}