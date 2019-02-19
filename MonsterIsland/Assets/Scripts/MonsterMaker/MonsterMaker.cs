using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Xml;
using Unity.VectorGraphics.Editor;
using Unity.VectorGraphics;

public class MonsterMaker : MonoBehaviour {

    public HeadSlot headSlot;
    public TorsoSlot torsoSlot;
    public ArmSlot rightArmSlot;
    public ArmSlot leftArmSlot;
    public LegsSlot legsSlot;
    public Button rightWeaponSlot;
    public Button leftWeaponSlot;

    public PartEditor partEditor;

    public CollectedPartsInfo collectedParts;

    public void Start()
    {
        headSlot.partInfo = Helper.GetHeadPartInfo("Mitch");
        torsoSlot.partInfo = Helper.GetTorsoPartInfo("Mitch");
        rightArmSlot.partInfo = Helper.GetArmPartInfo("Mitch", "RightArm");
        leftArmSlot.partInfo = Helper.GetArmPartInfo("Mitch", "LeftArm");
        legsSlot.partInfo = Helper.GetLegPartInfo("Mitch");

        collectedParts = new CollectedPartsInfo()
        {
            collectedHeads = new string[] { "Mitch" },
            collectedTorsos = new string[] { "Mitch" },
            collectedRightArms = new string[] { "Mitch" },
            collectedLeftArms = new string[] { "Mitch" },
            collectedLegs = new string[] { "Mitch" }
        };
    }

    public void ShowPartEditor(string partType)
    {
        //iterating through all objects inside the MonsterMaker canvas
        foreach(Transform child in transform)
        {
            //if child is the selected slot
            if(child.gameObject.name == (partType + "Slot"))
            {
                switch (partType)
                {
                    case "Head":
                        partEditor.availableParts = collectedParts.collectedHeads;
                        break;
                    case "Torso":
                        partEditor.availableParts = collectedParts.collectedTorsos;
                        break;
                    case "RightArm":
                        partEditor.availableParts = collectedParts.collectedRightArms;
                        break;
                    case "LeftArm":
                        partEditor.availableParts = collectedParts.collectedLeftArms;
                        break;
                    case "Legs":
                        partEditor.availableParts = collectedParts.collectedLegs;
                        break;
                    default:
                        break;
                }

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