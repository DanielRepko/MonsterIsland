using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPart : MonoBehaviour {

    //holds information about the part
    public ArmPartInfo partInfo;

    //holds the equipped weapon, if any
    public Weapon weapon;

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

    public Sprite fingersBack;
    public Sprite fingersFront;

    //holds the sprite for the weapon
    public Sprite weaponSprite;

    //Sprite renderers for the gameObject
    public SpriteRenderer bicep;
    public SpriteRenderer forearm;
    public SpriteRenderer hand;
    public SpriteRenderer fingers;
    public SpriteRenderer weaponRenderer;

    //stores the part's ability delegate
    //for the sake of simplicity, this is also used to store 
    //the attack delegate for the weapon, if one is equipped
    public AbilityFactory.ArmAbility ability = null;

    public void InitializePart(ArmPartInfo armPartInfo)
    {
        //this mainly is used to check whether the part is attached to the player
        PlayerController player = GetComponentInParent<PlayerController>();

        Enemy enemy = GetComponentInParent<Enemy>();

        if (armPartInfo != null)
        {
            partInfo = armPartInfo;

            if(player != null)
            {
                weapon = WeaponFactory.GetWeapon(partInfo.equippedWeapon, partType, "Player", weaponRenderer);
            }
            else if(enemy != null)
            {
                weapon = WeaponFactory.GetWeapon(partInfo.equippedWeapon, partType, "Enemy", weaponRenderer);
            }

            //checking whether this part has an ability
            if (partInfo.abilityName != null && player != null)
            {
                //populating the partAbility field with the appropriate ability delegate
                ability = AbilityFactory.GetArmPartAbility(partInfo.abilityName);

                //if the type is Passive, run the delegate method to apply the buff to the player
                if (partInfo.abilityType == "Passive")
                {
                    ability(partType);

                }//if the type is Activate, set the ability to the Player action delegate
                else if (partInfo.abilityType == "Activate")
                {
                    //checking which arm to apply the ability to
                    if(partType == Helper.PartType.RightArm)
                    {
                        player.rightAttackDelegate = ability;
                    } else if(partType == Helper.PartType.LeftArm)
                    {
                        player.leftAttackDelegate = ability;
                    }
                }//if the value is anything else, then a typo must have occured when creating the ability info
                else
                {
                    Debug.Log("Error: Invalid ability type");
                }
            }            

            if (enemy == null)
            {
                //setting all of the sprite fields
                bicepSprite = Helper.CreateSprite(partInfo.bicepSprite, Helper.BicepImporter);
                forearmSprite = Helper.CreateSprite(partInfo.forearmSprite, Helper.ForearmImporter);
                handBackSprite = Helper.CreateSprite(partInfo.handBackSprite, Helper.HandImporter);
                handFrontSprite = Helper.CreateSprite(partInfo.handFrontSprite, Helper.HandImporter);
                fingersOpenBackSprite = Helper.CreateSprite(partInfo.fingersOpenBackSprite, Helper.HandImporter);
                fingersOpenFrontSprite = Helper.CreateSprite(partInfo.fingersOpenFrontSprite, Helper.HandImporter);
                fingersClosedBackSprite = Helper.CreateSprite(partInfo.fingersClosedBackSprite, Helper.HandImporter);
                fingersClosedFrontSprite = Helper.CreateSprite(partInfo.fingersClosedFrontSprite, Helper.HandImporter);
            }

            if (weapon != null)
            {
                ability = weapon.AttackDelegate;

                fingersBack = fingersClosedBackSprite;
                fingersFront = fingersClosedFrontSprite;

                weaponSprite = weapon.WeaponSprite;
                weaponRenderer.sprite = weaponSprite;                

                if (partType == Helper.PartType.RightArm)
                {
                    if (player != null)
                    {
                        player.rightAttackDelegate = ability;
                        player.RightAttackCooldown = weapon.AttackCooldown;
                    }
                    else if (enemy != null)
                    {
                        enemy.armAttackDelegate = ability;
                        enemy.attackCooldown = weapon.AttackCooldown;
                        weapon.Damage = enemy.damage;
                        if (weapon.WeaponType == Helper.WeaponType.Melee)
                        {
                            enemy.attackRange = weapon.AttackRange;
                        }
                        else if (weapon.WeaponType == Helper.WeaponType.Projectile)
                        {
                            weapon.AttackRange = enemy.attackRange;
                        }
                    }
                }
                else if (partType == Helper.PartType.LeftArm)
                {
                    if (player != null)
                    {
                        player.leftAttackDelegate = ability;
                        player.LeftAttackCooldown = weapon.AttackCooldown;
                    }
                    else if (enemy != null)
                    {
                        enemy.armAttackDelegate = ability;
                        enemy.attackCooldown = weapon.AttackCooldown;
                        if (weapon.WeaponType == Helper.WeaponType.Melee)
                        {
                            enemy.attackRange = weapon.AttackRange;
                        }
                        else if(weapon.WeaponType == Helper.WeaponType.Projectile)
                        {
                            weapon.AttackRange = enemy.attackRange;
                        }                        
                        weapon.Damage = enemy.damage;
                    }
                }
            }
            else
            {
                fingersBack = fingersOpenBackSprite;
                fingersFront = fingersOpenFrontSprite;
            }

            bicep.sprite = bicepSprite;
            forearm.sprite = forearmSprite;

            if (partType == Helper.PartType.RightArm)
            {
                hand.sprite = handBackSprite;
                fingers.sprite = fingersBack;
            } else if (partType == Helper.PartType.LeftArm)
            {
                hand.sprite = handFrontSprite;
                fingers.sprite = fingersFront;
            }
           
        }
    }
    
    //changes the facing direction of the right arm
    public void ChangeRightArmDirection(float scaleX)
    {
        gameObject.transform.localScale = new Vector2(scaleX, 1);

        //facing right
        if (scaleX == 1)
        {
            //flipping the sprite renderers to ensure the sprites face the same direction
            //regardless of the gameObject facing direction
            bicep.flipX = false;
            forearm.flipX = false;
            hand.flipX = false;
            fingers.flipX = false;

            //applying the appropriate hand and finger sprites
            hand.sprite = handBackSprite;
            fingers.sprite = fingersBack;

            //setting the sprite renderers to the correct order in the sorting layer
            bicep.sortingOrder = 12;
            forearm.sortingOrder = 8;
            hand.sortingOrder = 11;
            weaponRenderer.sortingOrder = 10;
            fingers.sortingOrder = 9;
        }
        //facing left
        else if (scaleX == -1)
        {
            //flipping the sprite renderers to ensure the sprites face the same direction
            //regardless of the gameObject facing direction
            bicep.flipX = true;
            forearm.flipX = true;
            hand.flipX = true;
            fingers.flipX = true;

            //applying the appropriate hand and finger sprites
            hand.sprite = handFrontSprite;
            fingers.sprite = fingersFront;

            //setting the sprite renderers to the correct order in the sorting layer
            bicep.sortingOrder = -1;
            forearm.sortingOrder = -5;
            hand.sortingOrder = -4;
            weaponRenderer.sortingOrder = -3;
            fingers.sortingOrder = -2;
        }
    }

    //changes the facing direction of the left arm
    public void ChangeLeftArmDirection(float scaleX)
    {
        gameObject.transform.localScale = new Vector2(scaleX, 1);

        //facing right
        if (scaleX == 1)
        {
            //flipping the sprite renderers to ensure the sprites face the same direction
            //regardless of the gameObject facing direction
            bicep.flipX = false;
            forearm.flipX = false;
            hand.flipX = false;
            fingers.flipX = false;

            //applying the appropriate hand and finger sprites
            hand.sprite = handFrontSprite;
            fingers.sprite = fingersFront;

            //setting the sprite renderers to the correct order in the sorting layer
            bicep.sortingOrder = -1;
            forearm.sortingOrder = -5;
            hand.sortingOrder = -4;
            weaponRenderer.sortingOrder = -3;
            fingers.sortingOrder = -2;
        }
        //facing left
        else if (scaleX == -1)
        {
            //flipping the sprite renderers to ensure the sprites face the same direction
            //regardless of the gameObject facing direction
            bicep.flipX = true;
            forearm.flipX = true;
            hand.flipX = true;
            fingers.flipX = true;

            //applying the appropriate hand and finger sprites
            hand.sprite = handBackSprite;
            fingers.sprite = fingersBack;

            //setting the sprite renderers to the correct order in the sorting layer
            bicep.sortingOrder = 12;
            forearm.sortingOrder = 8;
            hand.sortingOrder = 11;
            weaponRenderer.sortingOrder = 10;
            fingers.sortingOrder = 9;
        }
    }
}
