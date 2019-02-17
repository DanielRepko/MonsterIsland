using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeadSlot : PartSlot{
    public HeadPartInfo partInfo;

    public override void ChangePart(MonsterPartInfo newPart)
    {
        throw new System.NotImplementedException();
    }

    public override void ChangePrimaryColor(string newColor)
    {
        HeadPartInfo newPart = new HeadPartInfo()
        {
            monster = partInfo.monster,
            mainSprite = ChangeColor(partInfo.mainSprite, "primary_color", newColor),
            neckSprite = ChangeColor(partInfo.neckSprite, "primary_color", newColor),
            hurtSprite = ChangeColor(partInfo.hurtSprite, "primary_color", newColor),
            attackSprite = ChangeColor(partInfo.attackSprite, "primary_color", newColor),
        };

        partInfo = newPart;
        UpdateUI();
    }

    public override void ChangeSecondaryColor(string newColor)
    {
        HeadPartInfo newPart = new HeadPartInfo()
        {
            monster = partInfo.monster,
            mainSprite = ChangeColor(partInfo.mainSprite, "secondary_color", newColor),
            neckSprite = ChangeColor(partInfo.neckSprite, "secondary_color", newColor),
            hurtSprite = ChangeColor(partInfo.hurtSprite, "secondary_color", newColor),
            attackSprite = ChangeColor(partInfo.attackSprite, "secondary_color", newColor),
        };

        partInfo = newPart;
        UpdateUI();
    }

    public override void UpdateUI()
    {
        throw new System.NotImplementedException();
    }
}