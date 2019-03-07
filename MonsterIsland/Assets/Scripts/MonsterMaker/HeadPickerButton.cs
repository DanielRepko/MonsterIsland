﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadPickerButton : PartPickerButton {
    public HeadPartInfo partInfo;
    public Image faceImage;
    public Image neckImage;


    public override MonsterPartInfo InitializePickerButton(string monsterName, Material material, string partType)
    {
        partInfo = Helper.GetHeadPartInfo(monsterName);
        faceImage.sprite = Helper.CreateSprite(partInfo.mainSprite, Helper.HeadImporter, material);
        neckImage.sprite = Helper.CreateSprite(partInfo.neckSprite, Helper.HeadImporter, material);

        return partInfo;
    }
}