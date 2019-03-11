using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics.Editor;
using Unity.VectorGraphics;
using System.IO;
using System.Xml;

public class Helper : MonoBehaviour {

    //Importers for the monster part images
    /**
     * Head
     **/
    public static SVGImporter HeadImporter = new SVGImporter(){
        MaxCordDeviation = float.MaxValue,
        MaxTangentAngle = Mathf.PI / 2f,
        SamplingStepDistance = 100,
        StepDistance = 1,
        SvgPixelsPerUnit = 10,
        Alignment = VectorUtils.Alignment.Custom,
        CustomPivot = new Vector2(0.4943407f, 0.1643327f),
        GradientResolution = 128
    };

    /**
     * Torso
     **/
    public static SVGImporter TorsoImporter = new SVGImporter()
    {
        MaxCordDeviation = float.MaxValue,
        MaxTangentAngle = Mathf.PI / 2f,
        SamplingStepDistance = 100,
        StepDistance = 1,
        SvgPixelsPerUnit = 10,
        Alignment = VectorUtils.Alignment.Custom,
        CustomPivot = new Vector2(0.5f,0.5f),
        GradientResolution = 128
    };

    /**
     * Arms
     **/
    //Bicep
    public static SVGImporter BicepImporter = new SVGImporter()
    {
        MaxCordDeviation = float.MaxValue,
        MaxTangentAngle = Mathf.PI / 2f,
        SamplingStepDistance = 100,
        StepDistance = 1,
        SvgPixelsPerUnit = 10,
        Alignment = VectorUtils.Alignment.Custom,
        CustomPivot = new Vector2(0.5f,0.5f),
        GradientResolution = 128
    };
    //Forearm
    public static SVGImporter ForearmImporter = new SVGImporter()
    {
        MaxCordDeviation = float.MaxValue,
        MaxTangentAngle = Mathf.PI / 2f,
        SamplingStepDistance = 100,
        StepDistance = 1,
        SvgPixelsPerUnit = 10,
        Alignment = VectorUtils.Alignment.Custom,
        CustomPivot = new Vector2(0.4908897f, 0.9051564f),
        GradientResolution = 128
    };
    //Hand/Fingers
    public static SVGImporter HandImporter = new SVGImporter()
    {
        MaxCordDeviation = float.MaxValue,
        MaxTangentAngle = Mathf.PI / 2f,
        SamplingStepDistance = 100,
        StepDistance = 1,
        SvgPixelsPerUnit = 10,
        Alignment = VectorUtils.Alignment.Custom,
        CustomPivot = new Vector2(0.5f, 0.95f),
        GradientResolution = 128
    };

    /**
     * Legs
     **/
    //Pelvis
    public static SVGImporter PelvisImporter = new SVGImporter()
    {
        MaxCordDeviation = float.MaxValue,
        MaxTangentAngle = Mathf.PI / 2f,
        SamplingStepDistance = 100,
        StepDistance = 1,
        SvgPixelsPerUnit = 10,
        Alignment = VectorUtils.Alignment.Custom,
        CustomPivot = new Vector2(0.5f, 0.5f),
        GradientResolution = 128
    };
    //Thigh
    public static SVGImporter ThighImporter = new SVGImporter()
    {
        MaxCordDeviation = float.MaxValue,
        MaxTangentAngle = Mathf.PI / 2f,
        SamplingStepDistance = 100,
        StepDistance = 1,
        SvgPixelsPerUnit = 10,
        Alignment = VectorUtils.Alignment.Custom,
        CustomPivot = new Vector2(0.5f, 0.9f),
        GradientResolution = 128
    };
    //Shin
    public static SVGImporter ShinImporter = new SVGImporter()
    {
        
        MaxCordDeviation = float.MaxValue,
        MaxTangentAngle = Mathf.PI / 2f,
        SamplingStepDistance = 100,
        StepDistance = 1,
        SvgPixelsPerUnit = 10,
        Alignment = VectorUtils.Alignment.Custom,
        CustomPivot = new Vector2(0.5f, 0.9f),
        GradientResolution = 128
    };
    //Foot
    public static SVGImporter FootImporter = new SVGImporter()
    {
        MaxCordDeviation = float.MaxValue,
        MaxTangentAngle = Mathf.PI / 2f,
        SamplingStepDistance = 100,
        StepDistance = 1,
        SvgPixelsPerUnit = 10,
        Alignment = VectorUtils.Alignment.Custom,
        CustomPivot = new Vector2(0.262309f, 0.6385142f),
        GradientResolution = 128
    };

    //Used for part types
    public struct PartType 
    {
        public static string Head = "Head";
        public static string Torso = "Torso";
        public static string RightArm = "RightArm";
        public static string LeftArm = "LeftArm";
        public static string Legs = "Legs";
    }

    //used for monster names
    public struct MonsterName
    {
        public static string Mitch = "Mitch";
        public static string Sam = "Sam";
        public static string Charles = "Charles";
        public static string Randall = "Randall";
        public static string Robot = "Robot";
        public static string Vulture = "Vulture";
        public static string Cactus = "Cactus";
        public static string Kangaroo = "Kangaroo";
        public static string Shark = "Shark";
        public static string Turtle = "Turtle";
        public static string Lobster = "Lobster";
        public static string Frog = "Frog";
        public static string Lion = "Lion";
        public static string Monkey = "Monkey";
        public static string Wingus = "Wingus";
        public static string Dingus = "Dingus";
        public static string Hingus = "Hingus";
        public static string Skeleton = "Skeleton";
        public static string Knight = "Knight";
        public static string Mummy = "Mummy";
    }

    //used for weapon names
    public struct WeaponName
    {
        public static string Stick = "Stick";
        public static string PeaShooter = "Pea Shooter";
        public static string Boomerang = "Boomerang";
        public static string Scimitar = "Scimitar";
        public static string BananaGun = "Banana Gun";
        public static string Club = "Club";
        public static string Swordfish = "Swordfish";
        public static string HarpoonGun = "Harpoon Gun";
        public static string SqueakyHammer = "Squeaky Hammer";
        public static string Fan = "Fan";
    }

    //helper method used to convert the imageStrings to Sprites
    public static Sprite CreateSprite(string partString, SVGImporter importer)
    {
        StringReader reader = new StringReader(partString);

        var sceneInfo = SVGParser.ImportSVG(reader);

        //creating TessellationOptions
        var options = new VectorUtils.TessellationOptions
        {
            MaxCordDeviation = importer.MaxCordDeviation,
            MaxTanAngleDeviation = importer.MaxTangentAngle,
            SamplingStepSize = importer.SamplingStepDistance,
            StepDistance = importer.StepDistance
        };
        
        //loading a material
        Material material = Resources.Load<Material>("Monster Part Importers/Unlit_Vector");

        //creating the geometry list
        var geometryList = VectorUtils.TessellateScene(sceneInfo.Scene, options);

        //building the initial sprite
        Sprite partSprite = VectorUtils.BuildSprite(geometryList, importer.SvgPixelsPerUnit, importer.Alignment, importer.CustomPivot, importer.GradientResolution, true);

        //calculating the multiplier to apply to the dimensions
        //(this accounts for some weirdness in the scaling)
        int spriteWidth = (int)partSprite.rect.width;
        int spriteHeight = (int)partSprite.rect.height;

        float largestSpriteDimen = (spriteWidth > spriteHeight) ? spriteWidth : spriteHeight;
        float dimenMultiplier = Mathf.Round((largestSpriteDimen / 256) / 10);

        int adjustedWidth = (int)(spriteWidth / dimenMultiplier);
        int adjustedHeight = (int)(spriteHeight / dimenMultiplier);

        Debug.Log(dimenMultiplier);

        //creating the texture
        Texture2D partTexture = VectorUtils.RenderSpriteToTexture2D(partSprite, adjustedWidth, adjustedHeight, material);

        //creating the final textured sprite
        Sprite texturedSprite = Sprite.Create(partTexture, new Rect(0, 0, adjustedWidth, adjustedHeight), importer.CustomPivot);

        return texturedSprite;
    }
    

    //helper method used to return the TorsoPartInfo for a certain monster
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

    //helper method used to return the ArmPartInfo for a certain monster
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

    //helper method used to return the HeadPartInfo for a certain monster
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