using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorsoPickerButton : PartPickerButton
{
    public TorsoPartInfo partInfo;
    public Image torsoImage;


    public override MonsterPartInfo InitializePickerButton(string monsterName, string partType)
    {
        partInfo = PartFactory.GetTorsoPartInfo(monsterName);
        torsoImage.sprite = Helper.CreateSprite(partInfo.mainSprite, Helper.TorsoImporter, true);

        return partInfo;
    }
}