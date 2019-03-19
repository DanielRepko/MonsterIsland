using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPart : MonoBehaviour {

    //holds information about the part
    public HeadPartInfo partInfo;

    //holds the sprites for the part
    public Sprite idleFaceSprite;
    public Sprite hurtFaceSprite;
    public Sprite attackFaceSprite;
    public Sprite neckSprite;

    //Sprite renderers for the gameObject
    public SpriteRenderer face;
    public SpriteRenderer neck;

    //stores the part's ability delegate
    public AbilityFactory.Ability partAbility = null;

    public void InitializePart(HeadPartInfo headPartInfo)
    {
        if(headPartInfo != null)
        {
            partInfo = headPartInfo;

            //checking whether this part has an ability
            if (partInfo.abilityName != null)
            {
                //populating the partAbility field with the appropriate ability delegate
                partAbility = AbilityFactory.GetPartAbility(partInfo.abilityName);

                //if the type is Passive, run the delegate method to apply the buff to the player
                if (partInfo.abilityType == "Passive")
                {
                    partAbility();

                }//if the type is Activate, set the ability to the Player action delegate
                else if (partInfo.abilityType == "Activate")
                {
                    PlayerController.Instance.headAbilityDelegate = partAbility;
                }//if the value is anything else, then a typo must have occured when creating the ability info
                else
                {
                    Debug.Log("Error: Invalid ability type");
                }
            }

            idleFaceSprite = Helper.CreateSprite(partInfo.mainSprite, Helper.HeadImporter, false);
            hurtFaceSprite = Helper.CreateSprite(partInfo.hurtSprite, Helper.HeadImporter, false);
            attackFaceSprite = Helper.CreateSprite(partInfo.attackSprite, Helper.HeadImporter, false);
            neckSprite = Helper.CreateSprite(partInfo.neckSprite, Helper.HeadImporter, false);

            face.sprite = idleFaceSprite;
            neck.sprite = neckSprite;
        }
    }

    public void ChangeDirection(int scaleX)
    {
        gameObject.transform.localScale = new Vector2(scaleX, 1);
    }

}
