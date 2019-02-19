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
        UpdateUI();
    }

    public override void ChangePrimaryColor(string newColor)
    {
        ArmPartInfo newPart = new ArmPartInfo()
        {
            monster = partInfo.monster,
            abilityName = partInfo.abilityName,
            abilityDesc = partInfo.abilityDesc,
            bicepSprite = ChangeColor(partInfo.bicepSprite, "primary_color", newColor),
            forearmSprite = ChangeColor(partInfo.forearmSprite, "primary_color", newColor),
            handBackSprite = ChangeColor(partInfo.handBackSprite, "primary_color", newColor),
            handFrontSprite = ChangeColor(partInfo.handFrontSprite, "primary_color", newColor),
            fingersClosedBackSprite = ChangeColor(partInfo.fingersClosedBackSprite, "primary_color", newColor),
            fingersClosedFrontSprite = ChangeColor(partInfo.fingersClosedFrontSprite, "primary_color", newColor),
            fingersOpenBackSprite = ChangeColor(partInfo.fingersOpenBackSprite, "primary_color", newColor),
            fingersOpenFrontSprite = ChangeColor(partInfo.fingersOpenFrontSprite, "primary_color", newColor)
        };


        partInfo = newPart;
        UpdateUI();
    }

    public override void ChangeSecondaryColor(string newColor)
    {
        ArmPartInfo newPart = new ArmPartInfo()
        {
            monster = partInfo.monster,
            abilityName = partInfo.abilityName,
            abilityDesc = partInfo.abilityDesc,
            bicepSprite = ChangeColor(partInfo.bicepSprite, "secondary_color", newColor),
            forearmSprite = ChangeColor(partInfo.forearmSprite, "secondary_color", newColor),
            handBackSprite = ChangeColor(partInfo.handBackSprite, "secondary_color", newColor),
            handFrontSprite = ChangeColor(partInfo.handFrontSprite, "secondary_color", newColor),
            fingersClosedBackSprite = ChangeColor(partInfo.fingersClosedBackSprite, "secondary_color", newColor),
            fingersClosedFrontSprite = ChangeColor(partInfo.fingersClosedFrontSprite, "secondary_color", newColor),
            fingersOpenBackSprite = ChangeColor(partInfo.fingersOpenBackSprite, "secondary_color", newColor),
            fingersOpenFrontSprite = ChangeColor(partInfo.fingersOpenFrontSprite, "secondary_color", newColor)
        };

        partInfo = newPart;
        UpdateUI();
    }

    public override void UpdateUI()
    {
        bicepImage.sprite = Helper.CreateSprite(partInfo.bicepSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
        forearmImage.sprite = Helper.CreateSprite(partInfo.forearmSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);

        if(partType == "RightArm")
        {
            handImage.sprite = Helper.CreateSprite(partInfo.handBackSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
            fingersImage.sprite = Helper.CreateSprite(partInfo.fingersOpenBackSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
        }
        else if(partType == "LeftArm")
        {
            handImage.sprite = Helper.CreateSprite(partInfo.handFrontSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
            fingersImage.sprite = Helper.CreateSprite(partInfo.fingersOpenFrontSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
        }
        
    }
}