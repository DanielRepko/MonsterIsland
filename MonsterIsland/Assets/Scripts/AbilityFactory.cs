using System.Collections;
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

    public static void Ability_LaserEyes()
    {

    }

    public static void Ability_TongueFlick()
    {

    }

    public static void Ability_LionsRoar()
    {

    }

    public static void Ability_AcidBreath()
    {

    }

    public static void Ability_BigBeak()
    {
       
    }

    public static void Ability_ArmoredBody()
    {

    }

    public static void Ability_Gills()
    {

    }

    public static void Ability_HardShell()
    {

    }

    public static void Ability_GhostWalk()
    {

    }

    public static void Ability_SwoopDaWoop()
    {

    }

    public static void Ability_StickyBomb()
    {

    }

    public static void Ability_DrillFist()
    {

    }

    public static void Ability_StrongArm()
    {

    }

    public static void Ability_WeaponsTraining()
    {

    }

    public static void Ability_NeedleShot()
    {

    }

    public static void Ability_FeatherFall()
    {

    }

    public static void Ability_PincerPistol()
    {

    }

    public static void Ability_BoneToss()
    {

    }

    public static void Ability_SpikedFeet()
    {

    }
    public static void Ability_QuickFeet()
    {
        Debug.Log("Issei ohseee, no qui something na na na da da da da, nani mo, nani mo");
    }

    public static void Ability_Acrobat()
    {

    }
    public static void Ability_JoeyJump()
    {

    }

    public static void Ability_TalonFlurry()
    {

    }    
}
