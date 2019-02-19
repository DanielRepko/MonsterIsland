using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TorsoSlot : PartSlot{
    public TorsoPartInfo partInfo;
    public Image torsoImage;
    public Image leftWingImage;
    public Image rightWingImage;

    public override void ChangePart(MonsterPartInfo newPart)
    {
        throw new System.NotImplementedException();
    }

    public override void ChangePrimaryColor(string newColor)
    {
        TorsoPartInfo newPart = new TorsoPartInfo()
        {
            monster = partInfo.monster,
            mainSprite = ChangeColor(partInfo.mainSprite, "primary_color", newColor),
            hasWings = partInfo.hasWings 
        };

        if (partInfo.hasWings)
        {
            newPart.leftWingSprite = ChangeColor(partInfo.leftWingSprite, "primary_color", newColor);
            newPart.rightWingSprite = ChangeColor(partInfo.rightWingSprite, "primary_color", newColor);
        }


        partInfo = newPart;
        UpdateUI();
    }

    public override void ChangeSecondaryColor(string newColor)
    {
        TorsoPartInfo newPart = new TorsoPartInfo()
        {
            monster = partInfo.monster,
            mainSprite = ChangeColor(partInfo.mainSprite, "secondary_color", newColor),
            hasWings = partInfo.hasWings
        };

        if (partInfo.hasWings)
        {
            newPart.leftWingSprite = ChangeColor(partInfo.leftWingSprite, "secondary_color", newColor);
            newPart.rightWingSprite = ChangeColor(partInfo.rightWingSprite, "secondary_color", newColor);
        }


        partInfo = newPart;
        UpdateUI();
    }

    public override void UpdateUI()
    {
        torsoImage.sprite = Helper.CreateSprite(partInfo.mainSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
        if (partInfo.hasWings)
        {
            leftWingImage.sprite = Helper.CreateSprite(partInfo.leftWingSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
            rightWingImage.sprite = Helper.CreateSprite(partInfo.rightWingSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
        }
        
    }
}