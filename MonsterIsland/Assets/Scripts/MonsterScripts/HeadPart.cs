using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPart : MonoBehaviour {

    //holds information about the part
    public HeadPartInfo partInfo;

    //holds the actual sprites for the part
    public Sprite idleFaceSprite;
    public Sprite hurtFaceSprite;
    public Sprite attackFaceSprite;
    public Sprite neckSprite;

    public HeadPart(HeadPartInfo headPartInfo)
    {
        if(headPartInfo != null)
        {
            partInfo = headPartInfo;

            //idleFaceSprite = Helper.CreateSprite(partInfo.mainSprite, Helper.HeadImporter, );
        }
    }

}
