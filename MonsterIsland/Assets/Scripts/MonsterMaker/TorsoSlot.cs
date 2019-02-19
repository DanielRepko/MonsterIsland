﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TorsoSlot : PartSlot{
    public TorsoPartInfo partInfo;
    public Image torsoImage;

    public override void ChangePart(MonsterPartInfo newPart)
    {
        throw new System.NotImplementedException();
    }

    public override void ChangePrimaryColor(string newColor)
    {
        TorsoPartInfo newPart = new TorsoPartInfo()
        {
            monster = partInfo.monster,
            mainSprite = ChangeColor(partInfo.mainSprite, "primary_color", newColor)
        };


        partInfo = newPart;
        UpdateUI();
    }

    public override void ChangeSecondaryColor(string newColor)
    {
        TorsoPartInfo newPart = new TorsoPartInfo()
        {
            monster = partInfo.monster,
            mainSprite = ChangeColor(partInfo.mainSprite, "secondary_color", newColor)
        };


        partInfo = newPart;
        UpdateUI();
    }

    public override void UpdateUI()
    {
        torsoImage.sprite = Helper.CreateSprite(partInfo.mainSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
    }
}