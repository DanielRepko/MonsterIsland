﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorsoPickerButton : PartPickerButton
{
    public TorsoPartInfo partInfo;
    public Image torsoImage;


    public override MonsterPartInfo InitializePickerButton(string monsterName, Material material, string partType)
    {
        partInfo = Helper.GetTorsoPartInfo(monsterName);
        torsoImage.sprite = Helper.CreateSprite(partInfo.mainSprite, Helper.TorsoImporter, material);

        return partInfo;
    }
}