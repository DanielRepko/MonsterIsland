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
    //inMonsterMaker parameter used to determine whether the call is happening inside the MonsterMaker
    //if it is, the method does not adjust the scaling of the images
    //this is necessary because scaling the images properly is very taxing, and would put
    //too much loading into the Monster Maker
    public static Sprite CreateSprite(string partString, SVGImporter importer, bool inMonsterMaker)
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

        if (!inMonsterMaker)
        {
            if (spriteWidth > spriteHeight)
            {
                float dimenMultiplier = (float)Math.Round((float)spriteWidth / 256, 3);
                spriteWidth = 2560;
                spriteHeight = ((int)(spriteHeight / dimenMultiplier)) * 10;
            }
            else
            {
                float dimenMultiplier = (float)Math.Round((float)spriteHeight / 256, 3);
                spriteWidth = ((int)(spriteWidth / dimenMultiplier)) * 10;
                spriteHeight = 2560;
            }
        }

        //creating the texture
        Texture2D partTexture = VectorUtils.RenderSpriteToTexture2D(partSprite, spriteWidth, spriteHeight, material);

        //creating the final textured sprite
        Sprite texturedSprite = Sprite.Create(partTexture, new Rect(0, 0, spriteWidth, spriteHeight), importer.CustomPivot);

        return texturedSprite;
    }
}