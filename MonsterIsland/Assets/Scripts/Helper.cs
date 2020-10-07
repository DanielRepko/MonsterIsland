using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics;
using System.IO;
using System.Xml;
using System;

public class Helper : MonoBehaviour {

    //holds the info for creating the monster part images from scratch
    public struct Importer 
    {
        public const float MaxCordDeviation = float.MaxValue;
        public const float MaxTangentAngle = Mathf.PI / 2f;
        public const float SamplingStepDistance = 100;
        public const float StepDistance = 1;
        public const float SvgPixelsPerUnit = 10;
        public const VectorUtils.Alignment Alignment = VectorUtils.Alignment.Custom;
        public float CustomPivotX;
        public float CustomPivotY;
        public const ushort GradientResolution = 128;
    };

    //Fields of type Importer that set the CustomPivots for each kind of part

    /**
     * Head
     **/
    public static Importer HeadImporter
    {
        get => new Importer { CustomPivotX = 0.4943407f, CustomPivotY = 0.1643327f };
            
    }

    /**
     * Torso
     **/
    public static Importer TorsoImporter
    {
        get => new Importer { CustomPivotX = 0.5f, CustomPivotY = 0.5f };

    }

    /**
     * Arms
     **/
    public static Importer BicepImporter
    {
        get => new Importer { CustomPivotX = 0.5f, CustomPivotY = 0.5f };

    }
    public static Importer ForearmImporter
    {
        get => new Importer { CustomPivotX = 0.4908897f, CustomPivotY = 0.9051564f };

    }
    public static Importer HandImporter
    {
        get => new Importer { CustomPivotX = 0.5f, CustomPivotY = 0.95f };

    }

    /**
     * Legs
     **/
    public static Importer PelvisImporter
    {
        get => new Importer { CustomPivotX = 0.5f, CustomPivotY = 0.5f };

    }
    public static Importer ThighImporter
    {
        get => new Importer { CustomPivotX = 0.5f, CustomPivotY = 0.9f };

    }
    public static Importer ShinImporter
    {
        get => new Importer { CustomPivotX = 0.5f, CustomPivotY = 0.9f };

    }
    public static Importer FootImporter
    {
        get => new Importer { CustomPivotX = 0.3657119f, CustomPivotY = 0.5829804f };

    }



    //public static SVGImporter HeadImporter = new SVGImporter()
    //{
    //    MaxCordDeviation = float.MaxValue,
    //    MaxTangentAngle = Mathf.PI / 2f,
    //    SamplingStepDistance = 100,
    //    StepDistance = 1,
    //    SvgPixelsPerUnit = 10,
    //    Alignment = VectorUtils.Alignment.Custom,
    //    CustomPivot = new Vector2(0.4943407f, 0.1643327f),
    //    GradientResolution = 128
    //};

    
    //public static SVGImporter TorsoImporter = new SVGImporter()
    //{
    //    MaxCordDeviation = float.MaxValue,
    //    MaxTangentAngle = Mathf.PI / 2f,
    //    SamplingStepDistance = 100,
    //    StepDistance = 1,
    //    SvgPixelsPerUnit = 10,
    //    Alignment = VectorUtils.Alignment.Custom,
    //    CustomPivot = new Vector2(0.5f,0.5f),
    //    GradientResolution = 128
    //};

    
    //Bicep
    //public static SVGImporter BicepImporter = new SVGImporter()
    //{
    //    MaxCordDeviation = float.MaxValue,
    //    MaxTangentAngle = Mathf.PI / 2f,
    //    SamplingStepDistance = 100,
    //    StepDistance = 1,
    //    SvgPixelsPerUnit = 10,
    //    Alignment = VectorUtils.Alignment.Custom,
    //    CustomPivot = new Vector2(0.5f,0.5f),
    //    GradientResolution = 128
    //};
    //Forearm
    //public static SVGImporter ForearmImporter = new SVGImporter()
    //{
    //    MaxCordDeviation = float.MaxValue,
    //    MaxTangentAngle = Mathf.PI / 2f,
    //    SamplingStepDistance = 100,
    //    StepDistance = 1,
    //    SvgPixelsPerUnit = 10,
    //    Alignment = VectorUtils.Alignment.Custom,
    //    CustomPivot = new Vector2(0.4908897f, 0.9051564f),
    //    GradientResolution = 128
    //};
    //Hand/Fingers
    //public static SVGImporter HandImporter = new SVGImporter()
    //{
    //    MaxCordDeviation = float.MaxValue,
    //    MaxTangentAngle = Mathf.PI / 2f,
    //    SamplingStepDistance = 100,
    //    StepDistance = 1,
    //    SvgPixelsPerUnit = 10,
    //    Alignment = VectorUtils.Alignment.Custom,
    //    CustomPivot = new Vector2(0.5f, 0.95f),
    //    GradientResolution = 128
    //};

    
    //Pelvis
    //public static SVGImporter PelvisImporter = new SVGImporter()
    //{
    //    MaxCordDeviation = float.MaxValue,
    //    MaxTangentAngle = Mathf.PI / 2f,
    //    SamplingStepDistance = 100,
    //    StepDistance = 1,
    //    SvgPixelsPerUnit = 10,
    //    Alignment = VectorUtils.Alignment.Custom,
    //    CustomPivot = new Vector2(0.5f, 0.5f),
    //    GradientResolution = 128
    //};
    //Thigh
    //public static SVGImporter ThighImporter = new SVGImporter()
    //{
    //    MaxCordDeviation = float.MaxValue,
    //    MaxTangentAngle = Mathf.PI / 2f,
    //    SamplingStepDistance = 100,
    //    StepDistance = 1,
    //    SvgPixelsPerUnit = 10,
    //    Alignment = VectorUtils.Alignment.Custom,
    //    CustomPivot = new Vector2(0.5f, 0.9f),
    //    GradientResolution = 128
    //};
    //Shin
    //public static SVGImporter ShinImporter = new SVGImporter()
    //{

    //    MaxCordDeviation = float.MaxValue,
    //    MaxTangentAngle = Mathf.PI / 2f,
    //    SamplingStepDistance = 100,
    //    StepDistance = 1,
    //    SvgPixelsPerUnit = 10,
    //    Alignment = VectorUtils.Alignment.Custom,
    //    CustomPivot = new Vector2(0.5f, 0.9f),
    //    GradientResolution = 128
    //};
    //Foot
    //public static SVGImporter FootImporter = new SVGImporter()
    //{
    //    MaxCordDeviation = float.MaxValue,
    //    MaxTangentAngle = Mathf.PI / 2f,
    //    SamplingStepDistance = 100,
    //    StepDistance = 1,
    //    SvgPixelsPerUnit = 10,
    //    Alignment = VectorUtils.Alignment.Custom,
    //    CustomPivot = new Vector2(0.3657119f, 0.5829804f),
    //    GradientResolution = 128
    //};

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

    public static float GetKnockBackDirection(Transform attacker, Transform target)
    {
        //attack is hitting target from the left
        if(attacker.position.x < target.position.x)
        {
            return -1;
        }
        //attack is hitting target from the right
        else if (attacker.position.x > target.position.x)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    //used to get strings Front or Back from 1 or -1 based on the side passed
    //sides can be passed in the form of armTypes (RightArm/LeftArm) or basic sides (Right/Left)
    //if side is null, returns Left or Right based on the value of scaleX
    public static string GetAnimDirection(float scaleX, string side = null)
    {
        if (side == null)
        {
            if (scaleX < 0)
            {
                return "Left";
            }
            else if (scaleX > 0)
            {
                return "Right";
            }
            else
            {
                return null;
            }
        }
        else
        {
            string sideTrimmed = "";
            if (side.Length > 5)
            {
                sideTrimmed = side.Substring(0, side.Length - 3);
            }
            else
            {
                sideTrimmed = side;
            }

            //checking if the side is Right
            if (sideTrimmed == "Right" && scaleX > 0)
            {
                return "Back";
            }
            else if (sideTrimmed == "Right" && scaleX < 0)
            {
                return "Front";
            }
            //checking if the side is Left
            else if (sideTrimmed == "Left" && scaleX > 0)
            {
                return "Front";
            }
            else if (sideTrimmed == "Left" && scaleX < 0)
            {
                return "Back";
            }
            else
            {
                return null;
            }
        }
    }

    //helper method used to convert the imageStrings to Sprites
    public static Sprite CreateSprite(string partString, Importer imageSettings)
    {
        StringReader reader = new StringReader(partString);

        var sceneInfo = SVGParser.ImportSVG(reader);

        //creating TessellationOptions
        var options = new VectorUtils.TessellationOptions
        {
            MaxCordDeviation = Importer.MaxCordDeviation,
            MaxTanAngleDeviation = Importer.MaxTangentAngle,
            SamplingStepSize = Importer.SamplingStepDistance,
            StepDistance = Importer.StepDistance
        };

        //loading a material
        Material material = Resources.Load<Material>("Monster Part Importers/Unlit_Vector");

        //creating the geometry list
        var geometryList = VectorUtils.TessellateScene(sceneInfo.Scene, options);

        Vector2 imagePivot = new Vector2(imageSettings.CustomPivotX, imageSettings.CustomPivotY);

        //building the initial sprite
        Sprite partSprite = VectorUtils.BuildSprite(geometryList, Importer.SvgPixelsPerUnit, Importer.Alignment, imagePivot, Importer.GradientResolution, true);

        int spriteWidth = (int)partSprite.rect.width;
        int spriteHeight = (int)partSprite.rect.height;

        //creating the texture
        Texture2D partTexture = VectorUtils.RenderSpriteToTexture2D(partSprite, spriteWidth, spriteHeight, material);

        //creating the final textured sprite
        Sprite texturedSprite = Sprite.Create(partTexture, new Rect(0, 0, spriteWidth, spriteHeight), imagePivot);

        return texturedSprite;
    }
}