using System.Collections;
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


    public override MonsterPartInfo InitializePickerButton(string monsterName, Material material, string partType)
    {
        partInfo = Helper.GetArmPartInfo(monsterName, partType);
        bicepImage.sprite = Helper.CreateSprite(partInfo.bicepSprite, Helper.BicepImporter, material);
        forearmImage.sprite = Helper.CreateSprite(partInfo.forearmSprite, Helper.ForearmImporter, material);
        if(partType == "RightArm")
        {
            handImage.sprite = Helper.CreateSprite(partInfo.handBackSprite, Helper.HandImporter, material);
            fingersImage.sprite = Helper.CreateSprite(partInfo.fingersOpenBackSprite, Helper.HandImporter, material);
        }
        else if(partType == "LeftArm")
        {
            handImage.sprite = Helper.CreateSprite(partInfo.handFrontSprite, Helper.HandImporter, material);
            fingersImage.sprite = Helper.CreateSprite(partInfo.fingersOpenFrontSprite, Helper.HandImporter, material);
        }
        

        return partInfo;
    }
}