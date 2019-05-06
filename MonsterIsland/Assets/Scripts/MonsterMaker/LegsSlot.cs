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
        abilityName = partInfo.abilityName;
        abilityType = partInfo.abilityType;
        abilityDesc = partInfo.abilityDesc;

        UpdateUI();
    }

    public override void ChangePrimaryColor(string newColor)
    {
        if (partInfo.monster != "")
        {
            LegPartInfo newPart = new LegPartInfo()
            {
                monster = partInfo.monster,
                abilityName = partInfo.abilityName,
                abilityType = partInfo.abilityType,
                abilityDesc = partInfo.abilityDesc,
                abilityCooldown = partInfo.abilityCooldown,
                pelvisSprite = ChangeColor(partInfo.pelvisSprite, "PRIMARY", newColor),
                thighSprite = ChangeColor(partInfo.thighSprite, "PRIMARY", newColor),
                shinSprite = ChangeColor(partInfo.shinSprite, "PRIMARY", newColor),
                footSprite = ChangeColor(partInfo.footSprite, "PRIMARY", newColor)
            };


            partInfo = newPart;
            UpdateUI();
        }
    }

    public override void ChangeSecondaryColor(string newColor)
    {
        if (partInfo.monster != "")
        {
            LegPartInfo newPart = new LegPartInfo()
            {
                monster = partInfo.monster,
                abilityName = partInfo.abilityName,
                abilityType = partInfo.abilityType,
                abilityDesc = partInfo.abilityDesc,
                abilityCooldown = partInfo.abilityCooldown,
                pelvisSprite = ChangeColor(partInfo.pelvisSprite, "SECONDARY", newColor),
                thighSprite = ChangeColor(partInfo.thighSprite, "SECONDARY", newColor),
                shinSprite = ChangeColor(partInfo.shinSprite, "SECONDARY", newColor),
                footSprite = ChangeColor(partInfo.footSprite, "SECONDARY", newColor)
            };

            partInfo = newPart;
            UpdateUI();
        }
    }

    public override void UpdateUI()
    {
        pelvisImage.sprite = Helper.CreateSprite(partInfo.pelvisSprite, Helper.HeadImporter);
        rightThighImage.sprite = Helper.CreateSprite(partInfo.thighSprite, Helper.HeadImporter);
        rightShinImage.sprite = Helper.CreateSprite(partInfo.shinSprite, Helper.HeadImporter);
        rightFootImage.sprite = Helper.CreateSprite(partInfo.footSprite, Helper.HeadImporter);
        leftThighImage.sprite = Helper.CreateSprite(partInfo.thighSprite, Helper.HeadImporter);
        leftShinImage.sprite = Helper.CreateSprite(partInfo.shinSprite, Helper.HeadImporter);
        leftFootImage.sprite = Helper.CreateSprite(partInfo.footSprite, Helper.HeadImporter);
    }
}