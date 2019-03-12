using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPart : MonoBehaviour {

    //holds information about the part
    public ArmPartInfo partInfo;

    //used to tell whether this class represents the right or left arm
    public string partType;

    //holds the sprites for the part
    public Sprite bicepSprite;
    public Sprite forearmSprite;
    public Sprite handBackSprite;
    public Sprite handFrontSprite;
    public Sprite fingersOpenBackSprite;
    public Sprite fingersOpenFrontSprite;
    public Sprite fingersClosedBackSprite;
    public Sprite fingersClosedFrontSprite;

    //Sprite renderers for the gameObject
    public SpriteRenderer bicep;
    public SpriteRenderer forearm;
    public SpriteRenderer hand;
    public SpriteRenderer fingers;

    public void InitializePart(ArmPartInfo armPartInfo)
    {
        if (armPartInfo != null)
        {
            partInfo = armPartInfo;

            bicepSprite = Helper.CreateSprite(partInfo.bicepSprite, Helper.BicepImporter, false);
            forearmSprite = Helper.CreateSprite(partInfo.forearmSprite, Helper.ForearmImporter, false);
            handBackSprite = Helper.CreateSprite(partInfo.handBackSprite, Helper.HandImporter, false);
            handFrontSprite = Helper.CreateSprite(partInfo.handFrontSprite, Helper.HandImporter, false);
            fingersOpenBackSprite = Helper.CreateSprite(partInfo.fingersOpenBackSprite, Helper.HandImporter, false);
            fingersOpenFrontSprite = Helper.CreateSprite(partInfo.fingersOpenFrontSprite, Helper.HandImporter, false);
            fingersClosedBackSprite = Helper.CreateSprite(partInfo.fingersClosedBackSprite, Helper.HandImporter, false);
            fingersClosedFrontSprite = Helper.CreateSprite(partInfo.fingersClosedFrontSprite, Helper.HandImporter, false);

            bicep.sprite = bicepSprite;
            forearm.sprite = forearmSprite;

            if (partType == Helper.PartType.RightArm)
            {
                hand.sprite = handBackSprite;
                fingers.sprite = fingersOpenBackSprite;
            } else if (partType == Helper.PartType.LeftArm)
            {
                hand.sprite = handFrontSprite;
                fingers.sprite = fingersOpenFrontSprite;
            }
           
        }
    }
}
