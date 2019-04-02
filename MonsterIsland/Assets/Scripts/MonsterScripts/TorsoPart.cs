using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoPart : MonoBehaviour {

    //holds information about the part
    public TorsoPartInfo partInfo;

    //holds the sprites for the part
    public Sprite bodySprite;

    //Sprite renderers for the gameObject
    public SpriteRenderer body;

    //stores the part's ability delegate
    public AbilityFactory.Ability partAbility = null;

    public void InitializePart(TorsoPartInfo torsoPartInfo)
    {
        //this mainly is used to check whether the part is attached to the player
        PlayerController player = GetComponentInParent<PlayerController>();

        if (torsoPartInfo != null)
        {
            partInfo = torsoPartInfo;

            //checking whether this part has an ability
            if (partInfo.abilityName != null && player != null)
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
                    player.torsoAbilityDelegate = partAbility;
                }//if the value is anything else, then a typo must have occured when creating the ability info
                else
                {
                    Debug.Log("Error: Invalid ability type");
                }
            }

            bodySprite = Helper.CreateSprite(partInfo.mainSprite, Helper.TorsoImporter);

            body.sprite = bodySprite;
        }
    }

    public void ChangeDirection(int scaleX)
    {
        gameObject.transform.localScale = new Vector2(scaleX, 1);
    }
}
