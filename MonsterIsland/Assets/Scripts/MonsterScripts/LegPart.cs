using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegPart : MonoBehaviour {

    //holds information about the part
    public LegPartInfo partInfo;

    //holds the sprites for the part
    public Sprite pelvisSprite;
    public Sprite thighSprite;
    public Sprite shinSprite;
    public Sprite footSprite;

    //Sprite renderers for the gameObject
    public SpriteRenderer pelvis;
    public SpriteRenderer thighR;
    public SpriteRenderer thighL;
    public SpriteRenderer shinR;
    public SpriteRenderer shinL;
    public SpriteRenderer footR;
    public SpriteRenderer footL;

    public void InitializePart(LegPartInfo legPartInfo)
    {
        if (legPartInfo != null)
        {
            partInfo = legPartInfo;

            pelvisSprite = Helper.CreateSprite(partInfo.pelvisSprite, Helper.PelvisImporter);
            thighSprite = Helper.CreateSprite(partInfo.thighSprite, Helper.ThighImporter);
            shinSprite = Helper.CreateSprite(partInfo.shinSprite, Helper.ShinImporter);
            footSprite = Helper.CreateSprite(partInfo.footSprite, Helper.FootImporter);

            pelvis.sprite = pelvisSprite;
            thighR.sprite = thighSprite;
            thighL.sprite = thighSprite;
            shinR.sprite = shinSprite;
            shinL.sprite = shinSprite;
            footR.sprite = footSprite;
            footL.sprite = footSprite;
        }
    }
}
