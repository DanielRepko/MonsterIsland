using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeadSlot : PartSlot{
    public HeadPartInfo partInfo;
    public Image faceImage;
    public Image neckImage;

    public override void ChangePart(MonsterPartInfo newPart)
    {
        partInfo = (HeadPartInfo)newPart;

        abilitySignLabel.text = partInfo.abilityName;
        abilityName = partInfo.abilityName;
        abilityType = partInfo.abilityType;
        abilityDesc = partInfo.abilityDesc;

        UpdateUI();
    }

    public override void ChangePrimaryColor(string newColor)
    {
        HeadPartInfo newPart = new HeadPartInfo()
        {
            monster = partInfo.monster,
            abilityName = partInfo.abilityName,
            abilityDesc = partInfo.abilityDesc,
            mainSprite = ChangeColor(partInfo.mainSprite, "PRIMARY", newColor),
            neckSprite = ChangeColor(partInfo.neckSprite, "PRIMARY", newColor),
            hurtSprite = ChangeColor(partInfo.hurtSprite, "PRIMARY", newColor),
            attackSprite = ChangeColor(partInfo.attackSprite, "PRIMARY", newColor)
        };


        partInfo = newPart;
        UpdateUI();
    }

    public override void ChangeSecondaryColor(string newColor)
    {
        HeadPartInfo newPart = new HeadPartInfo()
        {
            monster = partInfo.monster,
            abilityName = partInfo.abilityName,
            abilityDesc = partInfo.abilityDesc,
            mainSprite = ChangeColor(partInfo.mainSprite, "SECONDARY", newColor),
            neckSprite = ChangeColor(partInfo.neckSprite, "SECONDARY", newColor),
            hurtSprite = ChangeColor(partInfo.hurtSprite, "SECONDARY", newColor),
            attackSprite = ChangeColor(partInfo.attackSprite, "SECONDARY", newColor)
        };

        partInfo = newPart;
        UpdateUI();
    }

    public override void UpdateUI()
    {
        faceImage.sprite = Helper.CreateSprite(partInfo.mainSprite, Helper.HeadImporter);
        neckImage.sprite = Helper.CreateSprite(partInfo.neckSprite, Helper.HeadImporter);
    }
}