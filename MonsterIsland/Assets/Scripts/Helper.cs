using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics.Editor;
using Unity.VectorGraphics;
using System.IO;
using System.Xml;
using System;

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
        CustomPivot = new Vector2(0.3657119f, 0.5829804f),
        GradientResolution = 128
    };

    //Used for part types
    public struct PartType 
    {
        public const string Head = "Head";
        public const string Torso = "Torso";
        public const string RightArm = "RightArm";
        public const string LeftArm = "LeftArm";
        public const string Legs = "Legs";
    }

    //used for monster names
    public struct MonsterName
    {
        public const string Mitch = "Mitch";
        public const string Sam = "Sam";
        public const string Charles = "Charles";
        public const string Randall = "Randall";
        public const string Robot = "Robot";
        public const string Vulture = "Vulture";
        public const string Cactus = "Cactus";
        public const string Kangaroo = "Kangaroo";
        public const string Shark = "Shark";
        public const string Turtle = "Turtle";
        public const string Lobster = "Lobster";
        public const string Frog = "Frog";
        public const string Lion = "Lion";
        public const string Monkey = "Monkey";
        public const string Wingus = "Wingus";
        public const string Dingus = "Dingus";
        public const string Hingus = "Hingus";
        public const string Skeleton = "Skeleton";
        public const string Knight = "Knight";
        public const string Mummy = "Mummy";
    }

    //used for weapon names
    public struct WeaponName
    {
        public const string Stick = "Stick";
        public const string PeaShooter = "Pea Shooter";
        public const string Boomerang = "Boomerang";
        public const string Scimitar = "Scimitar";
        public const string BananaGun = "Banana Gun";
        public const string Club = "Club";
        public const string Swordfish = "Swordfish";
        public const string HarpoonGun = "Harpoon Gun";
        public const string SqueakyHammer = "Squeaky Hammer";
        public const string Fan = "Fan";
        public const string Bone = "Bone";
    }

    public struct WeaponType
    {
        public const string Melee = "Melee";
        public const string Projectile = "Projectile";
    }

    //helper method used to convert the imageStrings to Sprites
    //inMonsterMaker parameter used to determine whether the call is happening inside the MonsterMaker
    //if it is, the method does not adjust the scaling of the images
    //this is necessary because scaling the images properly is very taxing, and would put
    //too much loading into the Monster Maker
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

        int spriteWidth = (int)partSprite.rect.width;
        int spriteHeight = (int)partSprite.rect.height;

        //creating the texture
        Texture2D partTexture = VectorUtils.RenderSpriteToTexture2D(partSprite, spriteWidth, spriteHeight, material);

        //creating the final textured sprite
        Sprite texturedSprite = Sprite.Create(partTexture, new Rect(0, 0, spriteWidth, spriteHeight), importer.CustomPivot);

        return texturedSprite;
    }
}