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
    public WeaponSlot rightWeaponSlot;
    public WeaponSlot leftWeaponSlot;

    public PartEditor partEditor;
    public WeaponPicker weaponPicker;

    private CollectedPartsInfo collectedParts;
    private List<string> collectedWeapons = new List<string>();

    public void Start()
    {
        //this code is for testing purposes only
        collectedParts = new CollectedPartsInfo()
        {
            collectedHeads = new List<string>(),
            collectedTorsos = new List<string>(),
            collectedLeftArms = new List<string>(),
            collectedRightArms = new List<string>(),
            collectedLegs = new List<string>(),
        };
        collectedParts.collectedHeads.Add("Mitch");
        collectedParts.collectedHeads.Add("Skeleton");
        collectedParts.collectedTorsos.Add("Mitch");
        collectedParts.collectedTorsos.Add("Skeleton");
        collectedParts.collectedLeftArms.Add("Mitch");
        collectedParts.collectedLeftArms.Add("Skeleton");
        collectedParts.collectedRightArms.Add("Mitch");
        collectedParts.collectedRightArms.Add("Skeleton");
        collectedParts.collectedRightArms.Add("Robot");
        collectedParts.collectedLegs.Add("Mitch");
        collectedParts.collectedLegs.Add("Skeleton");

        collectedWeapons.Add(Helper.WeaponName.PeaShooter);
        collectedWeapons.Add(Helper.WeaponName.Stick);
        collectedWeapons.Add(Helper.WeaponName.Boomerang);
        collectedWeapons.Add(Helper.WeaponName.Club);
        collectedWeapons.Add(Helper.WeaponName.Scimitar);
        collectedWeapons.Add(Helper.WeaponName.SqueakyHammer);
        collectedWeapons.Add(Helper.WeaponName.HarpoonGun);
        collectedWeapons.Add(Helper.WeaponName.Swordfish);
        collectedWeapons.Add(Helper.WeaponName.BananaGun);
        collectedWeapons.Add(Helper.WeaponName.Fan);
    }

    public void ShowWeaponPicker(string weaponHand)
    {
        weaponPicker.availableWeapons = collectedWeapons;
        //iterating through all objects inside the MonsterMaker canvas
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == weaponHand+"WeaponSlot" && weaponHand == "Right")
            {
                weaponPicker.OpenWeaponPicker(rightWeaponSlot);
            }
            else if (child.gameObject.name == weaponHand + "WeaponSlot" && weaponHand == "Left")
            {
                weaponPicker.OpenWeaponPicker(leftWeaponSlot);
            }
            else if (child.gameObject.name == "WeaponPicker")
            {
                //child is the Weapon Picker, don't do anything
            }
            //anything else should be disabled
            else
            {
                child.gameObject.SetActive(false);
            }
        }
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
                        partEditor.availableParts = collectedParts.collectedHeads.ToArray();
                        break;
                    case "Torso":
                        partEditor.availableParts = collectedParts.collectedTorsos.ToArray();
                        break;
                    case "RightArm":
                        partEditor.availableParts = collectedParts.collectedRightArms.ToArray();
                        break;
                    case "LeftArm":
                        partEditor.availableParts = collectedParts.collectedLeftArms.ToArray();
                        break;
                    case "Legs":
                        partEditor.availableParts = collectedParts.collectedLegs.ToArray();
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

    public void HideEditors()
    {
        //iterating through all objects inside the MonsterMaker canvas
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "WeaponPicker")
            {
                weaponPicker.CloseWeaponPicker() ;
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