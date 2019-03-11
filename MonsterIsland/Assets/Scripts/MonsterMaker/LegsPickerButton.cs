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


    public override MonsterPartInfo InitializePickerButton(string monsterName, string partType)
    {
        partInfo = Helper.GetLegPartInfo(monsterName);
        pelvisImage.sprite = Helper.CreateSprite(partInfo.pelvisSprite, Helper.BicepImporter);
        rightThighImage.sprite = Helper.CreateSprite(partInfo.thighSprite, Helper.ForearmImporter);
        rightShinImage.sprite = Helper.CreateSprite(partInfo.shinSprite, Helper.BicepImporter);
        rightFootImage.sprite = Helper.CreateSprite(partInfo.footSprite, Helper.ForearmImporter);
        leftThighImage.sprite = Helper.CreateSprite(partInfo.thighSprite, Helper.ForearmImporter);
        leftShinImage.sprite = Helper.CreateSprite(partInfo.shinSprite, Helper.BicepImporter);
        leftFootImage.sprite = Helper.CreateSprite(partInfo.footSprite, Helper.ForearmImporter);

        return partInfo;
    }
}