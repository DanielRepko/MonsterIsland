using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Xml;

public class MonsterMaker : MonoBehaviour {

    public HeadSlot headSlot;
    public TorsoSlot torsoSlot;
    public ArmSlot rightArmSlot;
    public ArmSlot leftArmSlot;
    public LegsSlot legsSlot;
    public Button rightWeaponSlot;
    public Button leftWeaponSlot;

    public PartEditor partEditor;

    public CollectedPartsInfo availableParts;

    public void Start()
    {
        headSlot.partInfo = Helper.GetHeadPart("Mitch");
        torsoSlot.partInfo = Helper.GetTorsoPart("Mitch");
        rightArmSlot.partInfo = Helper.GetArmPart("Mitch", "RightArm");
        leftArmSlot.partInfo = Helper.GetArmPart("Mitch", "LeftArm");
        legsSlot.partInfo = Helper.GetLegPart("Mitch");
    }

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