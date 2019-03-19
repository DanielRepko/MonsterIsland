﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPart : MonoBehaviour {

    //holds information about the part
    public ArmPartInfo partInfo;

    //used to tell whether this class represents the right or left arm
    public string partType;

    //holds the sprites for the part
    public Sprite bicepSprite;
    public Sprite forearmSprite;
    public Sprite handBackSprite;
    public Sprite handFrontSprite;
    public Sprite fingersOpenBackSprite;
    public Sprite fingersOpenFrontSprite;
    public Sprite fingersClosedBackSprite;
    public Sprite fingersClosedFrontSprite;

    //Sprite renderers for the gameObject
    public SpriteRenderer bicep;
    public SpriteRenderer forearm;
    public SpriteRenderer hand;
    public SpriteRenderer fingers;

    //stores the part's ability delegate
    public AbilityFactory.ArmAbility partAbility = null;

    public void InitializePart(ArmPartInfo armPartInfo)
    {
        if (armPartInfo != null)
        {
            partInfo = armPartInfo;

            //checking whether this part has an ability
            if (partInfo.abilityName != null)
            {
                //populating the partAbility field with the appropriate ability delegate
                partAbility = AbilityFactory.GetArmPartAbility(partInfo.abilityName);

                //if the type is Passive, run the delegate method to apply the buff to the player
                if (partInfo.abilityType == "Passive")
                {
                    partAbility(partType);

                }//if the type is Activate, set the ability to the Player action delegate
                else if (partInfo.abilityType == "Activate")
                {
                    //checking which arm to apply the ability to
                    if(partType == "RightArm")
                    {
                        PlayerController.Instance.rightAttackDelegate = partAbility;
                    } else if(partType == "LeftArm")
                    {
                        PlayerController.Instance.rightAttackDelegate = partAbility;
                    }
                }//if the value is anything else, then a typo must have occured when creating the ability info
                else
                {
                    Debug.Log("Error: Invalid ability type");
                }
            }

            bicepSprite = Helper.CreateSprite(partInfo.bicepSprite, Helper.BicepImporter, false);
            forearmSprite = Helper.CreateSprite(partInfo.forearmSprite, Helper.ForearmImporter, false);
            handBackSprite = Helper.CreateSprite(partInfo.handBackSprite, Helper.HandImporter, false);
            handFrontSprite = Helper.CreateSprite(partInfo.handFrontSprite, Helper.HandImporter, false);
            fingersOpenBackSprite = Helper.CreateSprite(partInfo.fingersOpenBackSprite, Helper.HandImporter, false);
            fingersOpenFrontSprite = Helper.CreateSprite(partInfo.fingersOpenFrontSprite, Helper.HandImporter, false);
            fingersClosedBackSprite = Helper.CreateSprite(partInfo.fingersClosedBackSprite, Helper.HandImporter, false);
            fingersClosedFrontSprite = Helper.CreateSprite(partInfo.fingersClosedFrontSprite, Helper.HandImporter, false);

            bicep.sprite = bicepSprite;
            forearm.sprite = forearmSprite;

            if (partType == Helper.PartType.RightArm)
            {
                hand.sprite = handBackSprite;
                fingers.sprite = fingersOpenBackSprite;
            } else if (partType == Helper.PartType.LeftArm)
            {
                hand.sprite = handFrontSprite;
                fingers.sprite = fingersOpenFrontSprite;
            }
           
        }
    }
    
    //changes the facing direction of the right arm
    public void ChangeRightArmDirection(int scaleX)
    {
        gameObject.transform.localScale = new Vector2(scaleX, 1);

        //facing right
        if (scaleX == 1)
        {
            //positioning the arm
            gameObject.transform.localPosition = new Vector2(-0.69f, 0);

            //flipping the sprite renderers to ensure the sprites face the same direction
            //regardless of the gameObject facing direction
            bicep.flipX = false;
            forearm.flipX = false;
            hand.flipX = false;
            fingers.flipX = false;

            //applying the appropriate hand and finger sprites
            hand.sprite = handBackSprite;
            fingers.sprite = fingersOpenBackSprite;

            //setting the sprite renderers to the correct order in the sorting layer
            bicep.sortingOrder = 12;
            forearm.sortingOrder = 8;
            hand.sortingOrder = 10;
            fingers.sortingOrder = 11;
        }
        //facing left
        else if (scaleX == -1)
        {
            //positioning the arm
            gameObject.transform.localPosition = new Vector2(-0.63f, 0);

            //flipping the sprite renderers to ensure the sprites face the same direction
            //regardless of the gameObject facing direction
            bicep.flipX = true;
            forearm.flipX = true;
            hand.flipX = true;
            fingers.flipX = true;

            //applying the appropriate hand and finger sprites
            hand.sprite = handFrontSprite;
            fingers.sprite = fingersOpenFrontSprite;

            //setting the sprite renderers to the correct order in the sorting layer
            bicep.sortingOrder = -1;
            forearm.sortingOrder = -5;
            hand.sortingOrder = -4;
            fingers.sortingOrder = -2;
        }
    }

    //changes the facing direction of the left arm
    public void ChangeLeftArmDirection(int scaleX)
    {
        gameObject.transform.localScale = new Vector2(scaleX, 1);

        //facing right
        if (scaleX == 1)
        {
            //positioning the arm
            gameObject.transform.localPosition = new Vector2(0.63f, 0);

            //flipping the sprite renderers to ensure the sprites face the same direction
            //regardless of the gameObject facing direction
            bicep.flipX = false;
            forearm.flipX = false;
            hand.flipX = false;
            fingers.flipX = false;

            //applying the appropriate hand and finger sprites
            hand.sprite = handFrontSprite;
            fingers.sprite = fingersOpenFrontSprite;

            //setting the sprite renderers to the correct order in the sorting layer
            bicep.sortingOrder = -1;
            forearm.sortingOrder = -5;
            hand.sortingOrder = -4;
            fingers.sortingOrder = -2;
        }
        //facing left
        else if (scaleX == -1)
        {
            //positioning the arm
            gameObject.transform.localPosition = new Vector2(0.69f, 0);

            //flipping the sprite renderers to ensure the sprites face the same direction
            //regardless of the gameObject facing direction
            bicep.flipX = true;
            forearm.flipX = true;
            hand.flipX = true;
            fingers.flipX = true;

            //applying the appropriate hand and finger sprites
            hand.sprite = handBackSprite;
            fingers.sprite = fingersOpenBackSprite;

            //setting the sprite renderers to the correct order in the sorting layer
            bicep.sortingOrder = 12;
            forearm.sortingOrder = 8;
            hand.sortingOrder = 10;
            fingers.sortingOrder = 11;
        }
    }
}