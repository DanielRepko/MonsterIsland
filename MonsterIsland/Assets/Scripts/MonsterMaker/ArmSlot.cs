using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArmSlot : PartSlot {
    public ArmPartInfo partInfo;
    public Image bicepImage;
    public Image forearmImage;
    public Image handImage;
    public Image fingersImage;

    public override void ChangePart(MonsterPartInfo newPart)
    {
        partInfo = (ArmPartInfo)newPart;

        abilitySignLabel.text = partInfo.abilityName;
        abilityName = partInfo.abilityName;
        abilityType = partInfo.abilityType;
        abilityDesc = partInfo.abilityDesc;

        UpdateUI();
    }

    public override void ChangePrimaryColor(string newColor)
    {
        if (partInfo.monster != "")
        {
            ArmPartInfo newPart = new ArmPartInfo()
            {
                monster = partInfo.monster,
                abilityName = partInfo.abilityName,
                abilityDesc = partInfo.abilityDesc,
                bicepSprite = ChangeColor(partInfo.bicepSprite, "PRIMARY", newColor),
                forearmSprite = ChangeColor(partInfo.forearmSprite, "PRIMARY", newColor),
                handBackSprite = ChangeColor(partInfo.handBackSprite, "PRIMARY", newColor),
                handFrontSprite = ChangeColor(partInfo.handFrontSprite, "PRIMARY", newColor),
                fingersClosedBackSprite = ChangeColor(partInfo.fingersClosedBackSprite, "PRIMARY", newColor),
                fingersClosedFrontSprite = ChangeColor(partInfo.fingersClosedFrontSprite, "PRIMARY", newColor),
                fingersOpenBackSprite = ChangeColor(partInfo.fingersOpenBackSprite, "PRIMARY", newColor),
                fingersOpenFrontSprite = ChangeColor(partInfo.fingersOpenFrontSprite, "PRIMARY", newColor)
            };


            partInfo = newPart;
            UpdateUI();
        }
    }

    public override void ChangeSecondaryColor(string newColor)
    {
        if (partInfo.monster != "")
        {
            ArmPartInfo newPart = new ArmPartInfo()
            {
                monster = partInfo.monster,
                abilityName = partInfo.abilityName,
                abilityDesc = partInfo.abilityDesc,
                bicepSprite = ChangeColor(partInfo.bicepSprite, "SECONDARY", newColor),
                forearmSprite = ChangeColor(partInfo.forearmSprite, "SECONDARY", newColor),
                handBackSprite = ChangeColor(partInfo.handBackSprite, "SECONDARY", newColor),
                handFrontSprite = ChangeColor(partInfo.handFrontSprite, "SECONDARY", newColor),
                fingersClosedBackSprite = ChangeColor(partInfo.fingersClosedBackSprite, "SECONDARY", newColor),
                fingersClosedFrontSprite = ChangeColor(partInfo.fingersClosedFrontSprite, "SECONDARY", newColor),
                fingersOpenBackSprite = ChangeColor(partInfo.fingersOpenBackSprite, "SECONDARY", newColor),
                fingersOpenFrontSprite = ChangeColor(partInfo.fingersOpenFrontSprite, "SECONDARY", newColor)
            };

            partInfo = newPart;
            UpdateUI();
        }
    }

    public override void UpdateUI()
    {
        bicepImage.sprite = Helper.CreateSprite(partInfo.bicepSprite, Helper.HeadImporter);
        forearmImage.sprite = Helper.CreateSprite(partInfo.forearmSprite, Helper.HeadImporter);

        if(partType == "RightArm")
        {
            handImage.sprite = Helper.CreateSprite(partInfo.handBackSprite, Helper.HeadImporter);
            fingersImage.sprite = Helper.CreateSprite(partInfo.fingersOpenBackSprite, Helper.HeadImporter);
        }
        else if(partType == "LeftArm")
        {
            handImage.sprite = Helper.CreateSprite(partInfo.handFrontSprite, Helper.HeadImporter);
            fingersImage.sprite = Helper.CreateSprite(partInfo.fingersOpenFrontSprite, Helper.HeadImporter);
        }
        
    }
}