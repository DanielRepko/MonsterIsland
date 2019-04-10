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

    public InputField nameField;

    public Image donePanel;

    private CollectedPartsInfo collectedParts;
    private List<string> collectedWeapons = new List<string>();

    public void Start()
    {
        if(PlayerController.Instance != null)
        {
            PlayerController.Instance.gameObject.SetActive(false);
            PopulateSlots(PlayerController.Instance.monster, GameManager.instance.gameFile.player.name);
        }

        collectedParts = GameManager.instance.gameFile.player.inventory.collectedParts;
        collectedWeapons = GameManager.instance.gameFile.player.inventory.collectedWeapons;
    }

    public void PopulateSlots(Monster monster, string playerName)
    {
        //getting the part infos from the monster
        HeadPartInfo headInfo = monster.headPart.partInfo;
        TorsoPartInfo torsoInfo = monster.torsoPart.partInfo;
        ArmPartInfo rightArmInfo = monster.rightArmPart.partInfo;
        ArmPartInfo leftArmInfo = monster.leftArmPart.partInfo;
        LegPartInfo legsInfo = monster.legPart.partInfo;

        //populating the parts in to each slot
        headSlot.ChangePart(headInfo);
        torsoSlot.ChangePart(torsoInfo);
        rightArmSlot.ChangePart(rightArmInfo);
        leftArmSlot.ChangePart(leftArmInfo);
        legsSlot.ChangePart(legsInfo);

        //populating the weapons into each slot, if there are any equipped
        string rightWeapon = rightArmInfo.equippedWeapon;
        string leftWeapon = leftArmInfo.equippedWeapon;

        if(rightWeapon != "")
        {
            rightWeaponSlot.ChangeWeapon(WeaponFactory.GetWeapon(rightWeapon, null, null, null));
        }
        if (leftWeapon != "")
        {
            leftWeaponSlot.ChangeWeapon(WeaponFactory.GetWeapon(leftWeapon, null, null, null));
        }

        //setting the name field text 
        nameField.text = playerName;
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

    public bool FieldsAreFilled()
    {
        //creating a dictionary storing bools for whether each required field is filled and that respective field's animator
        Dictionary<Animator, bool> fieldsFilled = new Dictionary<Animator, bool>();
        fieldsFilled.Add(headSlot.GetComponent<Animator>(), headSlot.partInfo.monster != "");
        fieldsFilled.Add(torsoSlot.GetComponent<Animator>(), torsoSlot.partInfo.monster != "");
        fieldsFilled.Add(rightArmSlot.GetComponent<Animator>(), rightArmSlot.partInfo.monster != "");
        fieldsFilled.Add(leftArmSlot.GetComponent<Animator>(), leftArmSlot.partInfo.monster != "");
        fieldsFilled.Add(legsSlot.GetComponent<Animator>(), legsSlot.partInfo.monster != "");
        fieldsFilled.Add(nameField.GetComponent<Animator>(), nameField.text != "");

        bool allFieldsFilled = true;

        foreach(KeyValuePair<Animator, bool> fieldFilled in fieldsFilled)
        {
            if (!fieldFilled.Value)
            {
                Debug.Log(fieldFilled.Key);
                fieldFilled.Key.Play("ErrorFlash");
                allFieldsFilled = false;
            }
        }

        return allFieldsFilled;

    }

    public void LeaveMonsterMaker()
    {
        SaveChanges();
        if(PlayerController.Instance != null)
        {
            PlayerController.Instance.ReinitializePlayer();
        }
        GameManager.instance.LoadToLastNestUsed();
    }

    public void SaveChanges()
    {
        PlayerInfo playerInfo = GameManager.instance.gameFile.player;
        playerInfo.headPart = headSlot.partInfo;
        playerInfo.torsoPart = torsoSlot.partInfo;
        rightArmSlot.partInfo.equippedWeapon = (rightWeaponSlot.weapon != null && rightWeaponSlot.weapon.WeaponName != "") ? rightWeaponSlot.weapon.WeaponName : "";
        playerInfo.rightArmPart = rightArmSlot.partInfo;
        leftArmSlot.partInfo.equippedWeapon = (leftWeaponSlot.weapon != null && leftWeaponSlot.weapon.WeaponName != "") ? leftWeaponSlot.weapon.WeaponName : "";
        playerInfo.leftArmPart = leftArmSlot.partInfo;
        playerInfo.legsPart = legsSlot.partInfo;
        playerInfo.name = nameField.text;

        GameManager.instance.gameFile.player = playerInfo;
        GameManager.instance.FinalizeSave();
    }

    public void ShowDonePanel()
    {
        //checking to see if all the required slots and info are filled
        if (FieldsAreFilled())
        {
            donePanel.gameObject.SetActive(true);
        }
    }

    public void HideDonePanel()
    {
        donePanel.gameObject.SetActive(false);
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
            else if(child.gameObject.name == "DonePanel")
            {
                HideDonePanel();
            }
            //anything else should be enabled
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}