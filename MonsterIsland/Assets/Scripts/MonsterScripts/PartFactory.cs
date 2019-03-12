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
            mainSprite = mainSprite.InnerXml,
            neckSprite = neckSprite.InnerXml,
            hurtSprite = hurtSprite.InnerXml,
            attackSprite = attackSprite.InnerXml
        };
        return headPart;
    }

    public static TorsoPartInfo GetTorsoPartInfo(string monsterName)
    {
        XmlDocument mainSprite = new XmlDocument();
        mainSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/Torso/Monster_" + monsterName + "_Torso.svg");

        TorsoPartInfo torsoPart = new TorsoPartInfo()
        {
            monster = monsterName,
            mainSprite = mainSprite.InnerXml
        };
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
            bicepSprite = bicepSprite.InnerXml,
            forearmSprite = forearmSprite.InnerXml,
            handBackSprite = handBackSprite.InnerXml,
            handFrontSprite = handFrontSprite.InnerXml,
            fingersOpenBackSprite = fingersOpenBackSprite.InnerXml,
            fingersOpenFrontSprite = fingersOpenFrontSprite.InnerXml,
            fingersClosedBackSprite = fingersClosedBackSprite.InnerXml,
            fingersClosedFrontSprite = fingersClosedFrontSprite.InnerXml
        };
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
            pelvisSprite = pelvisSprite.InnerXml,
            thighSprite = thighSprite.InnerXml,
            shinSprite = shinSprite.InnerXml,
            footSprite = footSprite.InnerXml
        };
        return legPart;
    }

}
