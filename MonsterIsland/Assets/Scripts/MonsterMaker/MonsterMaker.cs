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

    private CollectedPartsInfo collectedParts;

    public void Start()
    {
        //this code is for testing purposes only
        collectedParts = new CollectedPartsInfo()
        {
            collectedHeads = new string[] { "Mitch", "Kangaroo" },
            collectedTorsos = new string[] { "Mitch", "Kangaroo"},
            collectedRightArms = new string[] { "Mitch", "Kangaroo" },
            collectedLeftArms = new string[] { "Mitch", "Kangaroo" },
            collectedLegs = new string[] { "Mitch", "Kangaroo" }
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