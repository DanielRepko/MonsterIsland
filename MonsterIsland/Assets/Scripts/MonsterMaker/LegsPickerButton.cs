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
        partInfo = PartFactory.GetLegPartInfo(monsterName);
        pelvisImage.sprite = Helper.CreateSprite(partInfo.pelvisSprite, Helper.BicepImporter, true);
        rightThighImage.sprite = Helper.CreateSprite(partInfo.thighSprite, Helper.ForearmImporter, true);
        rightShinImage.sprite = Helper.CreateSprite(partInfo.shinSprite, Helper.BicepImporter, true);
        rightFootImage.sprite = Helper.CreateSprite(partInfo.footSprite, Helper.ForearmImporter, true);
        leftThighImage.sprite = Helper.CreateSprite(partInfo.thighSprite, Helper.ForearmImporter, true);
        leftShinImage.sprite = Helper.CreateSprite(partInfo.shinSprite, Helper.BicepImporter, true);
        leftFootImage.sprite = Helper.CreateSprite(partInfo.footSprite, Helper.ForearmImporter, true);

        return partInfo;
    }
}