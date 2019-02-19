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

    // Use this for initialization
    void Start() {

        //this code is for testing purposes

        //instatiating HeadSlot info
        XmlDocument head_Face_Idle = new XmlDocument();
        head_Face_Idle.Load("Assets/Resources/Sprites/Monsters/Mitch/Head/Monster_Mitch_Head_Face_idle.svg");
        XmlDocument head_Face_Attack = new XmlDocument();
        head_Face_Attack.Load("Assets/Resources/Sprites/Monsters/Mitch/Head/Monster_Mitch_Head_Face_attack.svg");
        XmlDocument head_Face_Hurt = new XmlDocument();
        head_Face_Hurt.Load("Assets/Resources/Sprites/Monsters/Mitch/Head/Monster_Mitch_Head_Face_hurt.svg");
        XmlDocument head_Neck = new XmlDocument();
        head_Neck.Load("Assets/Resources/Sprites/Monsters/Mitch/Head/Monster_Mitch_Head_neck.svg");

        headSlot.partInfo = new HeadPartInfo()
        {
            monster = "Mitch",
            mainSprite = head_Face_Idle.InnerXml,
            attackSprite = head_Face_Attack.InnerXml,
            hurtSprite = head_Face_Hurt.InnerXml,
            neckSprite = head_Neck.InnerXml
        };

        //instatiating TorsoSlot info
        XmlDocument torso = new XmlDocument();
        torso.Load("Assets/Resources/Sprites/Monsters/Mitch/Torso/Monster_Mitch_Torso.svg");

        torsoSlot.partInfo = new TorsoPartInfo()
        {
            monster = "Mitch",
            mainSprite = torso.InnerXml,
            hasWings = false
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