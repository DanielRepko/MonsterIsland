using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Xml;

public class MonsterMakerManager : MonoBehaviour {

    public HeadSlot headSlot;
    public Button torsoSlot;
    public Button rightArmSlot;
    public Button leftArmSlot;
    public Button legsSlot;
    public Button rightWeaponSlot;
    public Button leftWeaponSlot;

    public PartEditor partEditor;

    public CollectedPartsInfo availableParts;

    // Use this for initialization
    void Start() {
        headSlot.originalPosition = headSlot.transform.localPosition;


        //this code is for testing purposes
        XmlDocument head_Face_Idle = new XmlDocument();
        head_Face_Idle.LoadXml("Assets/Resources/Sprites/Monsters/Mitch/Head/Monster_Mitch_Head_Face_idle");
        XmlDocument head_Face_Attack = new XmlDocument();
        head_Face_Attack.LoadXml("Assets/Resources/Sprites/Monsters/Mitch/Head/Monster_Mitch_Head_Face_attack");
        XmlDocument head_Face_Hurt = new XmlDocument();
        head_Face_Hurt.LoadXml("Assets/Resources/Sprites/Monsters/Mitch/Head/Monster_Mitch_Head_Face_hurt");
        XmlDocument head_Neck = new XmlDocument();
        head_Neck.LoadXml("Assets/Resources/Sprites/Monsters/Mitch/Head/Monster_Mitch_Head_neck");

        headSlot.partInfo = new HeadPartInfo()
        {
            monster = "Mitch",
            mainSprite = head_Face_Idle.InnerXml,
            attackSprite = head_Face_Attack.InnerXml,
            hurtSprite = head_Face_Hurt.InnerXml,
            neckSprite = head_Neck.InnerXml
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