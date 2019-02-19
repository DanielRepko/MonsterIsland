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
    public static SVGImporter HeadImporter = new SVGImporter()
    {
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
        Alignment = VectorUtils.Alignment.Center,
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
        Alignment = VectorUtils.Alignment.Center,
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
        Alignment = VectorUtils.Alignment.Center,
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

    //helper method used to convert the imageStrings to Sprites
    public static Sprite CreateSprite(string partString, SVGImporter importer, Material material)
    {
        StringReader reader = new StringReader(partString);

        var sceneInfo = SVGParser.ImportSVG(reader);

        var options = new VectorUtils.TessellationOptions
        {
            MaxCordDeviation = importer.MaxCordDeviation,
            MaxTanAngleDeviation = importer.MaxTangentAngle,
            SamplingStepSize = importer.SamplingStepDistance,
            StepDistance = importer.StepDistance
        };

        var geometryList = VectorUtils.TessellateScene(sceneInfo.Scene, options);

        Sprite partSprite = VectorUtils.BuildSprite(geometryList, importer.SvgPixelsPerUnit, importer.Alignment, importer.CustomPivot, importer.GradientResolution, true);

        Texture2D partTexture = VectorUtils.RenderSpriteToTexture2D(partSprite, (int)partSprite.rect.width, (int)partSprite.rect.height, material);

        Sprite texturedSprite = Sprite.Create(partTexture, new Rect(0, 0, (int)partSprite.rect.width, (int)partSprite.rect.height), importer.CustomPivot);

        return texturedSprite;
    }

    //helper method used to return the HeadPartInfo for a certain monster
    public static HeadPartInfo GetHeadPart(string monsterName)
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

    //helper method used to return the HeadPartInfo for a certain monster
    public static TorsoPartInfo GetTorsoPart(string monsterName)
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
}