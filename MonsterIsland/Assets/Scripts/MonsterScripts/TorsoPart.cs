using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoPart : MonoBehaviour {

    //holds information about the part
    public TorsoPartInfo partInfo;

    //holds the sprites for the part
    public Sprite bodySprite;

    //Sprite renderers for the gameObject
    public SpriteRenderer body;

    public void InitializePart(TorsoPartInfo torsoPartInfo)
    {
        if (torsoPartInfo != null)
        {
            partInfo = torsoPartInfo;

            bodySprite = Helper.CreateSprite(partInfo.mainSprite, Helper.TorsoImporter, false);

            body.sprite = bodySprite;
        }
    }
}
