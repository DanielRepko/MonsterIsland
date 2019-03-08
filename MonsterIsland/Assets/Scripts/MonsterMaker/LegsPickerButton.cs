using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegsPickerButton : PartPickerButton
{
    public LegPartInfo partInfo;
    public Image pelvisImage;
    public Image rightThighImage;
    public Image rightShinImage;
    public Image rightFootImage;
    public Image leftThighImage;
    public Image leftShinImage;
    public Image leftFootImage;


    public override MonsterPartInfo InitializePickerButton(string monsterName, Material material, string partType)
    {
        partInfo = Helper.GetLegPartInfo(monsterName);
        pelvisImage.sprite = Helper.CreateSprite(partInfo.pelvisSprite, Helper.BicepImporter, material);
        rightThighImage.sprite = Helper.CreateSprite(partInfo.thighSprite, Helper.ForearmImporter, material);
        rightShinImage.sprite = Helper.CreateSprite(partInfo.shinSprite, Helper.BicepImporter, material);
        rightFootImage.sprite = Helper.CreateSprite(partInfo.footSprite, Helper.ForearmImporter, material);
        leftThighImage.sprite = Helper.CreateSprite(partInfo.thighSprite, Helper.ForearmImporter, material);
        leftShinImage.sprite = Helper.CreateSprite(partInfo.shinSprite, Helper.BicepImporter, material);
        leftFootImage.sprite = Helper.CreateSprite(partInfo.footSprite, Helper.ForearmImporter, material);

        return partInfo;
    }
}