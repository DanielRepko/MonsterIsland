using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class PartFactory : MonoBehaviour {

    public static HeadPartInfo GetHeadPartInfo(string monsterName)
    {
        XmlDocument mainSprite = new XmlDocument();
        mainSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/Head/Monster_" + monsterName + "_Head_Face_idle.svg");
        XmlDocument neckSprite = new XmlDocument();
        neckSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/Head/Monster_" + monsterName + "_Head_neck.svg");
        XmlDocument hurtSprite = new XmlDocument();
        hurtSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/Head/Monster_" + monsterName + "_Head_Face_hurt.svg");
        XmlDocument attackSprite = new XmlDocument();
        attackSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/Head/Monster_" + monsterName + "_Head_Face_attack.svg");

        HeadPartInfo headPart = new HeadPartInfo()
        {
            monster = monsterName,
            partType = Helper.PartType.Head,
            mainSprite = mainSprite.InnerXml,
            neckSprite = neckSprite.InnerXml,
            hurtSprite = hurtSprite.InnerXml,
            attackSprite = attackSprite.InnerXml
        };

        headPart = GetHeadAbilityInfo(headPart);

        return headPart;
    }

    public static TorsoPartInfo GetTorsoPartInfo(string monsterName)
    {
        XmlDocument mainSprite = new XmlDocument();
        mainSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/Torso/Monster_" + monsterName + "_Torso.svg");

        TorsoPartInfo torsoPart = new TorsoPartInfo()
        {
            monster = monsterName,
            partType = Helper.PartType.Torso,
            mainSprite = mainSprite.InnerXml
        };

        torsoPart = GetTorsoAbilityInfo(torsoPart);

        return torsoPart;
    }

    public static ArmPartInfo GetArmPartInfo(string monsterName, string armType)
    {
        XmlDocument bicepSprite = new XmlDocument();
        bicepSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/" + armType + "/Monster_" + monsterName + "_" + armType + "_bicep.svg");
        XmlDocument forearmSprite = new XmlDocument();
        forearmSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/" + armType + "/Monster_" + monsterName + "_" + armType + "_forearm.svg");
        XmlDocument handBackSprite = new XmlDocument();
        handBackSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/" + armType + "/Monster_" + monsterName + "_" + armType + "_handBack.svg");
        XmlDocument handFrontSprite = new XmlDocument();
        handFrontSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/" + armType + "/Monster_" + monsterName + "_" + armType + "_handFront.svg");
        XmlDocument fingersOpenBackSprite = new XmlDocument();
        fingersOpenBackSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/" + armType + "/Monster_" + monsterName + "_" + armType + "_fingersOpenBack.svg");
        XmlDocument fingersOpenFrontSprite = new XmlDocument();
        fingersOpenFrontSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/" + armType + "/Monster_" + monsterName + "_" + armType + "_fingersOpenFront.svg");
        XmlDocument fingersClosedBackSprite = new XmlDocument();
        fingersClosedBackSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/" + armType + "/Monster_" + monsterName + "_" + armType + "_fingersClosedBack.svg");
        XmlDocument fingersClosedFrontSprite = new XmlDocument();
        fingersClosedFrontSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/" + armType + "/Monster_" + monsterName + "_" + armType + "_fingersClosedFront.svg");

        ArmPartInfo armPart = new ArmPartInfo()
        {
            monster = monsterName,
            partType = armType,
            bicepSprite = bicepSprite.InnerXml,
            forearmSprite = forearmSprite.InnerXml,
            handBackSprite = handBackSprite.InnerXml,
            handFrontSprite = handFrontSprite.InnerXml,
            fingersOpenBackSprite = fingersOpenBackSprite.InnerXml,
            fingersOpenFrontSprite = fingersOpenFrontSprite.InnerXml,
            fingersClosedBackSprite = fingersClosedBackSprite.InnerXml,
            fingersClosedFrontSprite = fingersClosedFrontSprite.InnerXml
        };

        armPart = GetArmAbilityInfo(armPart, armType);

        return armPart;
    }

    public static LegPartInfo GetLegPartInfo(string monsterName)
    {
        XmlDocument pelvisSprite = new XmlDocument();
        pelvisSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/Legs/Monster_" + monsterName + "_Legs_pelvis.svg");
        XmlDocument thighSprite = new XmlDocument();
        thighSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/Legs/Monster_" + monsterName + "_Legs_thigh.svg");
        XmlDocument shinSprite = new XmlDocument();
        shinSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/Legs/Monster_" + monsterName + "_Legs_shin.svg");
        XmlDocument footSprite = new XmlDocument();
        footSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/Legs/Monster_" + monsterName + "_Legs_foot.svg");

        LegPartInfo legPart = new LegPartInfo()
        {
            monster = monsterName,
            partType = Helper.PartType.Legs,
            pelvisSprite = pelvisSprite.InnerXml,
            thighSprite = thighSprite.InnerXml,
            shinSprite = shinSprite.InnerXml,
            footSprite = footSprite.InnerXml
        };

        legPart = GetLegAbilityInfo(legPart);

        return legPart;
    }

    //fills the partInfo with the monster's ability name and description and returns it
    private static HeadPartInfo GetHeadAbilityInfo(HeadPartInfo partInfo)
    {
        switch(partInfo.monster)
        {
            case Helper.MonsterName.Robot:
                partInfo.abilityName = "Laser Eyes";
                partInfo.abilityType = "Activate";
                partInfo.abilityDesc = "Fire a single laser beam from your eyes";
                partInfo.abilityCooldown = 1;
                return partInfo;
            case Helper.MonsterName.Frog:
                partInfo.abilityName = "Tongue Flick";
                partInfo.abilityType = "Activate";
                partInfo.abilityDesc = "Quickly flick out your tongue to attack enemies from a medium range";
                partInfo.abilityCooldown = 0.5f;
                return partInfo;
            case Helper.MonsterName.Lion:
                partInfo.abilityName = "Lion's Roar";
                partInfo.abilityType = "Activate";
                partInfo.abilityDesc = "Let out a courageous roar to knock back enemies around you";
                partInfo.abilityCooldown = 1;
                return partInfo;
            case Helper.MonsterName.Mummy:
                partInfo.abilityName = "Acid Breath";
                partInfo.abilityType = "Activate";
                partInfo.abilityDesc = "Cough up a cloud of acid that lingers in space and deals damage over time";
                partInfo.abilityCooldown = 4;
                return partInfo;
            case Helper.MonsterName.Dingus:
                partInfo.abilityName = "Big Beak";
                partInfo.abilityType = "Activate";
                partInfo.abilityDesc = "Use your beak to peck at enemies";
                partInfo.abilityCooldown = 0;
                return partInfo;
            default:
                return partInfo;
        }
    }

    //fills the partInfo with the monster's ability name and description and returns it
    private static TorsoPartInfo GetTorsoAbilityInfo(TorsoPartInfo partInfo)
    {
        switch (partInfo.monster)
        {
            case Helper.MonsterName.Robot:
                partInfo.abilityName = "Armored Body";
                partInfo.abilityType = "Passive";
                partInfo.abilityDesc = "Gain an extra heart of health";
                partInfo.abilityCooldown = 0;
                return partInfo;
            case Helper.MonsterName.Shark:
                partInfo.abilityName = "Gills";
                partInfo.abilityType = "Passive";
                partInfo.abilityDesc = "Gain the ability to breath underwater";
                partInfo.abilityCooldown = 0;
                return partInfo;
            case Helper.MonsterName.Turtle:
                partInfo.abilityName = "Hard Shell";
                partInfo.abilityType = "Passive";
                partInfo.abilityDesc = "You cannot be hurt by attacks from behind, but melee attacks will still knock you back";
                partInfo.abilityCooldown = 0;
                return partInfo;
            case Helper.MonsterName.Knight:
                partInfo.abilityName = "Ghost Walk";
                partInfo.abilityType = "Activate";
                partInfo.abilityDesc = "Dissappear, then reappear further ahead in the direction you are facing";
                partInfo.abilityCooldown = 1;
                return partInfo;
            case Helper.MonsterName.Wingus:
                partInfo.abilityName = "Swoop da Woop";
                partInfo.abilityType = "Activate";
                partInfo.abilityDesc = "Fly backwards into the air to escape any danger coming from the front, does not work underwater";
                partInfo.abilityCooldown = 0;
                return partInfo;
            default:
                return partInfo;
        }
    }

    //fills the partInfo with the monster's ability name and description and returns it
    private static ArmPartInfo GetArmAbilityInfo(ArmPartInfo partInfo, string armType)
    {
        switch (partInfo.monster)
        {
            case Helper.MonsterName.Robot:
                if(armType == Helper.PartType.RightArm)
                {
                    partInfo.abilityName = "Sticky Bomb";
                    partInfo.abilityType = "Activate";
                    partInfo.abilityDesc = "Shoot a bomb that explodes after a few seconds. Sticks to walls and enemies";
                    partInfo.abilityCooldown = 1;
                } else if(armType == Helper.PartType.LeftArm)
                {
                    partInfo.abilityName = "Drill Fist";
                    partInfo.abilityType = "Activate";
                    partInfo.abilityDesc = "Shoot out a drill that can bore through an entire wall before breaking. Can also go through enemies";
                    partInfo.abilityCooldown = 2f;
                }
                return partInfo;
            case Helper.MonsterName.Sam:
                partInfo.abilityName = "Strong Arm";
                partInfo.abilityType = "Passive";
                partInfo.abilityDesc = "Increased melee damage. Does not affect melee weapon damage";
                partInfo.abilityCooldown = 0;
                return partInfo;
            case Helper.MonsterName.Charles:
                partInfo.abilityName = "Weapons Training";
                partInfo.abilityType = "Passive";
                partInfo.abilityDesc = "Increased melee weapon damage. Does not affect projectile weapon damage";
                partInfo.abilityCooldown = 0;
                return partInfo;
            case Helper.MonsterName.Cactus:
                partInfo.abilityName = "Needle Shot";
                partInfo.abilityType = "Activate";
                partInfo.abilityDesc = "Shoot a spread of three needles";
                partInfo.abilityCooldown = 1;
                return partInfo;
            case Helper.MonsterName.Vulture:
                partInfo.abilityName = "Feather Fall";
                partInfo.abilityType = "Passive";
                partInfo.abilityDesc = "Decrease fall speed. Effect can stack with each arm, does not work underwater";
                partInfo.abilityCooldown = 0;
                return partInfo;
            case Helper.MonsterName.Lobster:
                partInfo.abilityName = "Pincer Pistol";
                partInfo.abilityType = "Activate";
                partInfo.abilityDesc = "Extend and shoot your arm out to attack enemies from a medium range";
                partInfo.abilityCooldown = 1.7f;
                return partInfo;
            case Helper.MonsterName.Skeleton:
                partInfo.abilityName = "Bone Toss";
                partInfo.abilityType = "Passive";
                partInfo.abilityDesc = "Throw bones in an overhead arc. Takes up this arm's weapon slot";
                partInfo.abilityCooldown = 0;
                return partInfo;
            default:
                return partInfo;
        }
    }

    //fills the partInfo with the monster's ability name and description and returns it
    private static LegPartInfo GetLegAbilityInfo(LegPartInfo partInfo)
    {
        switch (partInfo.monster)
        {
            case Helper.MonsterName.Robot:
                partInfo.abilityName = "Spiked Feet";
                partInfo.abilityType = "Passive";
                partInfo.abilityDesc = "Hurt enemies by landing on them";
                partInfo.abilityCooldown = 0;
                return partInfo;
            case Helper.MonsterName.Randall:
                partInfo.abilityName = "Quick Feet";
                partInfo.abilityType = "Passive";
                partInfo.abilityDesc = "Gain faster move speed";
                partInfo.abilityCooldown = 0;
                return partInfo;
            case Helper.MonsterName.Monkey:
                partInfo.abilityName = "Acrobat";
                partInfo.abilityType = "Activate";
                partInfo.abilityDesc = "Jump in mid-air to perform a second jump.";
                partInfo.abilityCooldown = 0;
                return partInfo;
            case Helper.MonsterName.Kangaroo:
                partInfo.abilityName = "Joey Jump";
                partInfo.abilityType = "Passive";
                partInfo.abilityDesc = "Go higher when jumping, does not work underwater";
                partInfo.abilityCooldown = 0;
                return partInfo;
            case Helper.MonsterName.Hingus:
                partInfo.abilityName = "Talon Flurry";
                partInfo.abilityType = "Activate";
                partInfo.abilityDesc = "Press jump in mid-air to attack with taloned feet";
                partInfo.abilityCooldown = 0;
                return partInfo;
            default:
                return partInfo;
        }
    }
}

