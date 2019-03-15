﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFactory : MonoBehaviour {

    public delegate void Ability();

    public static Ability GetPartAbility(string abilityName)
    {
        Ability ability = null;

        switch (abilityName)
        {
            case "Laser Eyes":
                ability = Ability_LaserEyes;
                return ability;
            case "Tongue Flick":
                ability = Ability_TongueFlick;
                return ability;
            case "Lion's Roar":
                ability = Ability_LionsRoar;
                return ability;
            case "Acid Breath":
                ability = Ability_AcidBreath;
                return ability;
            case "Big Beak":
                ability = Ability_BigBeak;
                return ability;
            case "Armored Body":
                ability = Ability_ArmoredBody;
                return ability;
            case "Gills":
                ability = Ability_Gills;
                return ability;
            case "Hard Shell":
                ability = Ability_HardShell;
                return ability;
            case "Ghost Walk":
                ability = Ability_GhostWalk;
                return ability;
            case "Swoop da Woop":
                ability = Ability_SwoopDaWoop;
                return ability;
            case "Sticky Bomb":
                ability = Ability_StickyBomb;
                return ability;
            case "Drill Fist":
                ability = Ability_DrillFist;
                return ability;
            case "Strong Arm":
                ability = Ability_StrongArm;
                return ability;
            case "Weapons Training":
                ability = Ability_WeaponsTraining;
                return ability;
            case "Needle Shot":
                ability = Ability_NeedleShot;
                return ability;
            case "Feather Fall":
                ability = Ability_FeatherFall;
                return ability;
            case "Pincer Pistol":
                ability = Ability_PincerPistol;
                return ability;
            case "Bone Toss":
                ability = Ability_BoneToss;
                return ability;
            case "Spiked Feet":
                ability = Ability_SpikedFeet;
                return ability;
            case "Quick Feet":
                ability = Ability_QuickFeet;
                return ability;
            case "Acrobat":
                ability = Ability_Acrobat;
                return ability;
            case "Joey Jump":
                ability = Ability_JoeyJump;
                return ability;
            case "Talon Flurry":
                ability = Ability_TalonFlurry;
                return ability;
            default:
                return null;
        }
    }

    //Head Ability (Activate): Allows the player to shoot a laser beam
    public static void Ability_LaserEyes()
    {

    }

    //Head Ability (Activate): Allows the player to attack with a tongue flick
    public static void Ability_TongueFlick()
    {

    }

    //Head Ability (Activate): Allows the player to knock back enemies with 
    //an AOE roar attack. Does not deal damage
    public static void Ability_LionsRoar()
    {

    }

    //Head Ability (Activate): Allows the player to spit out a cloud of acid 
    //that lingers for several seconds and deals damage over time
    public static void Ability_AcidBreath()
    {

    }

    //Head Ability (Activate): Allows the player to attack with large beak
    public static void Ability_BigBeak()
    {
       
    }

    //Torso Ability (Passive): Grants the player an extra heart of health
    public static void Ability_ArmoredBody()
    {
        GameManager.instance.player.health += 1;
    }

    //Torso Ability (Passive): Allows the player to breath underwater
    public static void Ability_Gills()
    {

    }

    //Torso Ability (Passive): Prevents the player from being damaged by attacks from behind
    public static void Ability_HardShell()
    {

    }

    //Torso Ability (Activate): Allows the player to teleport short distances
    public static void Ability_GhostWalk()
    {

    }

    //Torso Ability (Activate): Allows the player to fly up forwards while in the air, or 
    //fly up backwards while on the ground
    public static void Ability_SwoopDaWoop()
    {

    }

    //Arm Ability (Activate): Lets the player shoot out a bomb that explodes after 
    //a few seconds. Sticks to walls and enemies
    public static void Ability_StickyBomb()
    {

    }

    //Arm Ability (Activate): Allows the player to attack with a drill, deals multiple hits of 
    //damage, all other actions and movement are locked for the ability's duration
    public static void Ability_DrillFist()
    {

    }

    //Arm Ability (Passive): Increases the player's melee damage, does not affect weapon damage
    public static void Ability_StrongArm()
    {

    }

    //Arm Ability (Passive): Increases the player's melee weapon damage, does not affect projectile weapon damage
    public static void Ability_WeaponsTraining()
    {

    }

    //Arm Ability (Activate): Allow the player to shoot needles in three shot bursts
    public static void Ability_NeedleShot()
    {

    }

    //Arm Ability (Passive): Makes the player fall slower, effect can stack with other arm
    //due to nature of the ability, needs to pass on a separate method (see FeatherFall())
    public static void Ability_FeatherFall()
    {
        GameManager.instance.player.playerCheckDelegate += FeatherFall;
    }

    //Arm Ability (Activate): Allows the player to extend and shoot their arm out to 
    //attack enemies at medium range
    public static void Ability_PincerPistol()
    {

    }

    //Arm Ability (Passive): Equips the player with the bone weapon, 
    //takes up and locks this arm's weapon slot
    public static void Ability_BoneToss()
    {

    }

    //Leg Ability (Passive): Allows the player to damage enemies by jumping on them
    public static void Ability_SpikedFeet()
    {

    }

    //Leg Ability (Passive): Increases the player's move speed
    public static void Ability_QuickFeet()
    {
        GameManager.instance.player.moveSpeed = 19;
    }

    //Leg Ability (Activate): Allows the player to perform a single jump in mid-air
    public static void Ability_Acrobat()
    {
        PlayerController player = GameManager.instance.player;

        if (!player.isUnderwater)
        {
            if (player.PlayerIsOnGround())
            {
                player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
            }
            else if (!player.PlayerIsOnGround() && player.hasExtraJump)
            {
                player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
                player.hasExtraJump = false;
            }
        }
    }

    //Leg Ability (Passive): Increases the player's jump height
    public static void Ability_JoeyJump()
    {
        if (!GameManager.instance.player.isUnderwater)
        {
            GameManager.instance.player.jumpForce = 70;
        }
    }

    //Leg Ability (Activate): Allows the player to attack with taloned feet
    public static void Ability_TalonFlurry()
    {

    }    

    //These methods are implemented on the PlayerController by certain methods for Passive abilities
    public static void FeatherFall()
    {
        PlayerController player = GameManager.instance.player;

        if (!player.isUnderwater)
        {
            if (player.rb.velocity.y < 0)
            {
                player.rb.gravityScale -= 2f;
            }
            else
            {
                player.rb.gravityScale = 20;
            }
        }        
    }
}