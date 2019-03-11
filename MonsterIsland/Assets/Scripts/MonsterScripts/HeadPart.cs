using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPart : MonoBehaviour {

    //holds information about the part
    public HeadPartInfo partInfo;

    //holds the sprites for the part
    public Sprite idleFaceSprite;
    public Sprite hurtFaceSprite;
    public Sprite attackFaceSprite;
    public Sprite neckSprite;

    //Sprite renderers for the gameObject
    public SpriteRenderer face;
    public SpriteRenderer neck;

    public void InitializePart(HeadPartInfo headPartInfo)
    {
        if(headPartInfo != null)
        {
            partInfo = headPartInfo;

            idleFaceSprite = Helper.CreateSprite(partInfo.mainSprite, Helper.HeadImporter);
            hurtFaceSprite = Helper.CreateSprite(partInfo.hurtSprite, Helper.HeadImporter);
            attackFaceSprite = Helper.CreateSprite(partInfo.attackSprite, Helper.HeadImporter);
            neckSprite = Helper.CreateSprite(partInfo.neckSprite, Helper.HeadImporter);

            face.sprite = idleFaceSprite;
            neck.sprite = neckSprite;
        }
    }

}
