using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegPart : MonoBehaviour {

    //holds information about the part
    public LegPartInfo partInfo;

    //holds the sprites for the part
    public Sprite pelvisSprite;
    public Sprite thighSprite;
    public Sprite shinSprite;
    public Sprite footSprite;

    //Sprite renderers for the gameObject
    public SpriteRenderer pelvis;
    public SpriteRenderer thighR;
    public SpriteRenderer thighL;
    public SpriteRenderer shinR;
    public SpriteRenderer shinL;
    public SpriteRenderer footR;
    public SpriteRenderer footL;

    //stores the part's ability delegate
    public AbilityFactory.Ability partAbility = null;

    public void InitializePart(LegPartInfo legPartInfo)
    {
        //this mainly is used to check whether the part is attached to the player
        PlayerController player = GetComponentInParent<PlayerController>();

        if (legPartInfo != null)
        {
            partInfo = legPartInfo;

            //populating the partAbility field
            if (partInfo.abilityName != "" && player != null)
            {
                partAbility = AbilityFactory.GetPartAbility(partInfo.abilityName);

                //if the type is Passive, run the delegate method to apply the buff to the player
                if(partInfo.abilityType == "Passive")
                {
                    partAbility();

                }//if the type is Activate, set the ability to the Player action delegate
                else if(partInfo.abilityType == "Activate")
                {
                    player.jumpDelegate = partAbility;
                }//if the value is anything else, then a typo must have occured when creating the ability info
                else
                {
                    Debug.Log("Error: Invalid ability type");
                }
            }

            if (GetComponentInParent<Enemy>() == null)
            {
                pelvisSprite = Helper.CreateSprite(partInfo.pelvisSprite, Helper.PelvisImporter);
                thighSprite = Helper.CreateSprite(partInfo.thighSprite, Helper.ThighImporter);
                shinSprite = Helper.CreateSprite(partInfo.shinSprite, Helper.ShinImporter);
                footSprite = Helper.CreateSprite(partInfo.footSprite, Helper.FootImporter);
            }

            pelvis.sprite = pelvisSprite;
            thighR.sprite = thighSprite;
            thighL.sprite = thighSprite;
            shinR.sprite = shinSprite;
            shinL.sprite = shinSprite;
            footR.sprite = footSprite;
            footL.sprite = footSprite;
        }
    }

    public void ChangeDirection(float scaleX)
    {
        gameObject.transform.localScale = new Vector2(scaleX, 1);
    }
}
