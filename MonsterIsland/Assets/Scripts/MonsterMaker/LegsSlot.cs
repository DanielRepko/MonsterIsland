using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LegsSlot : PartSlot {
    public LegPartInfo partInfo;
    public Image pelvisImage;
    public Image rightThighImage;
    public Image rightShinImage;
    public Image rightFootImage;
    public Image leftThighImage;
    public Image leftShinImage;
    public Image leftFootImage;

    public override void ChangePart(MonsterPartInfo newPart)
    {
        partInfo = (LegPartInfo)newPart;
        abilitySignLabel.text = partInfo.abilityName;
        UpdateUI();
    }

    public override void ChangePrimaryColor(string newColor)
    {
        LegPartInfo newPart = new LegPartInfo()
        {
            monster = partInfo.monster,
            abilityName = partInfo.abilityName,
            abilityDesc = partInfo.abilityDesc,
            pelvisSprite = ChangeColor(partInfo.pelvisSprite, "PRIMARY", newColor),
            thighSprite = ChangeColor(partInfo.thighSprite, "PRIMARY", newColor),
            shinSprite = ChangeColor(partInfo.shinSprite, "PRIMARY", newColor),
            footSprite = ChangeColor(partInfo.footSprite, "PRIMARY", newColor)
        };


        partInfo = newPart;
        UpdateUI();
    }

    public override void ChangeSecondaryColor(string newColor)
    {
        LegPartInfo newPart = new LegPartInfo()
        {
            monster = partInfo.monster,
            abilityName = partInfo.abilityName,
            abilityDesc = partInfo.abilityDesc,
            pelvisSprite = ChangeColor(partInfo.pelvisSprite, "SECONDARY", newColor),
            thighSprite = ChangeColor(partInfo.thighSprite, "SECONDARY", newColor),
            shinSprite = ChangeColor(partInfo.shinSprite, "SECONDARY", newColor),
            footSprite = ChangeColor(partInfo.footSprite, "SECONDARY", newColor)
        };

        partInfo = newPart;
        UpdateUI();
    }

    public override void UpdateUI()
    {
        pelvisImage.sprite = Helper.CreateSprite(partInfo.pelvisSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
        rightThighImage.sprite = Helper.CreateSprite(partInfo.thighSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
        rightShinImage.sprite = Helper.CreateSprite(partInfo.shinSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
        rightFootImage.sprite = Helper.CreateSprite(partInfo.footSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
        leftThighImage.sprite = Helper.CreateSprite(partInfo.thighSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
        leftShinImage.sprite = Helper.CreateSprite(partInfo.shinSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
        leftFootImage.sprite = Helper.CreateSprite(partInfo.footSprite, Helper.HeadImporter, gameObject.GetComponent<Image>().material);
    }
}