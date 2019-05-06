using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TorsoSlot : PartSlot{
    public TorsoPartInfo partInfo;
    public Image torsoImage;

    public override void ChangePart(MonsterPartInfo newPart)
    {
        partInfo = (TorsoPartInfo)newPart;

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
            TorsoPartInfo newPart = new TorsoPartInfo()
            {
                monster = partInfo.monster,
                abilityName = partInfo.abilityName,
                abilityType = partInfo.abilityType,
                abilityDesc = partInfo.abilityDesc,
                abilityCooldown = partInfo.abilityCooldown,
                mainSprite = ChangeColor(partInfo.mainSprite, "PRIMARY", newColor)
            };


            partInfo = newPart;
            UpdateUI();
        }
    }

    public override void ChangeSecondaryColor(string newColor)
    {
        if (partInfo.monster != "")
        {
            TorsoPartInfo newPart = new TorsoPartInfo()
            {
                monster = partInfo.monster,
                abilityName = partInfo.abilityName,
                abilityType = partInfo.abilityType,
                abilityDesc = partInfo.abilityDesc,
                abilityCooldown = partInfo.abilityCooldown,
                mainSprite = ChangeColor(partInfo.mainSprite, "SECONDARY", newColor)
            };


            partInfo = newPart;
            UpdateUI();
        }
    }

    public override void UpdateUI()
    {
        torsoImage.sprite = Helper.CreateSprite(partInfo.mainSprite, Helper.TorsoImporter);
    }
}