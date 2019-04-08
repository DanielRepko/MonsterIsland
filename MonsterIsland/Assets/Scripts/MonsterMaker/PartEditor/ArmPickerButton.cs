﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmPickerButton : PartPickerButton
{
    public ArmPartInfo partInfo;
    public Image bicepImage;
    public Image forearmImage;
    public Image handImage;
    public Image fingersImage;


    public override MonsterPartInfo InitializePickerButton(string monsterName, string partType)
    {
        partInfo = PartFactory.GetArmPartInfo(monsterName, partType);
        bicepImage.sprite = Helper.CreateSprite(partInfo.bicepSprite, Helper.BicepImporter);
        forearmImage.sprite = Helper.CreateSprite(partInfo.forearmSprite, Helper.ForearmImporter);
        if(partType == "RightArm")
        {
            handImage.sprite = Helper.CreateSprite(partInfo.handBackSprite, Helper.HandImporter);
            fingersImage.sprite = Helper.CreateSprite(partInfo.fingersOpenBackSprite, Helper.HandImporter);
        }
        else if(partType == "LeftArm")
        {
            handImage.sprite = Helper.CreateSprite(partInfo.handFrontSprite, Helper.HandImporter);
            fingersImage.sprite = Helper.CreateSprite(partInfo.fingersOpenFrontSprite, Helper.HandImporter);
        }
        

        return partInfo;
    }
}