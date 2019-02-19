﻿using System.Collections;
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
        UpdateUI();
    }

    public override void ChangePrimaryColor(string newColor)
    {
        HeadPartInfo newPart = new HeadPartInfo()
        {
            monster = partInfo.monster,
            abilityName = partInfo.abilityName,
            abilityDesc = partInfo.abilityDesc,
            mainSprite = ChangeColor(partInfo.mainSprite, "primary_color", newColor),
            neckSprite = ChangeColor(partInfo.neckSprite, "primary_color", newColor),
            hurtSprite = ChangeColor(partInfo.hurtSprite, "primary_color", newColor),
            attackSprite = ChangeColor(partInfo.attackSprite, "primary_color", newColor)
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
            mainSprite = ChangeColor(partInfo.mainSprite, "secondary_color", newColor),
            neckSprite = ChangeColor(partInfo.neckSprite, "secondary_color", newColor),
            hurtSprite = ChangeColor(partInfo.hurtSprite, "secondary_color", newColor),
            attackSprite = ChangeColor(partInfo.attackSprite, "secondary_color", newColor)
        };

        partInfo = newPart;
        UpdateUI();
    }

    public override void UpdateUI()
    {
        faceImage.sprite = Helper.CreateSprite(partInfo.mainSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
        neckImage.sprite = Helper.CreateSprite(partInfo.neckSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
    }
}