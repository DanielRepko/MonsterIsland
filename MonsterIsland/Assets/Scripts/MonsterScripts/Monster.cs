using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    //parts of the Monster
    public HeadPart headPart;
    public TorsoPart torsoPart;
    public ArmPart rightArmPart;
    public ArmPart leftArmPart;
    public LegPart legPart;

    public void InitializeMonster(HeadPartInfo headInfo, 
                                  TorsoPartInfo torsoInfo, 
                                  ArmPartInfo rightArmInfo, ArmPartInfo leftArmInfo, 
                                  LegPartInfo legPartInfo)
    {
        headPart.InitializePart(headInfo);
        torsoPart.InitializePart(torsoInfo);
        rightArmPart.InitializePart(rightArmInfo);
        leftArmPart.InitializePart(leftArmInfo);
        legPart.InitializePart(legPartInfo);
    }

    //used to change the direction the monster is facing, while keeping each
    //arm on the correct side, scaleX is the value to be applied to each part's local scale x
    public void ChangeDirection(float scaleX)
    {
        //setting the scale of the monster object (a child of, but different from the player object)
        transform.localScale = new Vector2(scaleX, transform.localScale.y);
        headPart.ChangeDirection(scaleX);
        torsoPart.ChangeDirection(scaleX);
        rightArmPart.ChangeRightArmDirection(scaleX);
        leftArmPart.ChangeLeftArmDirection(scaleX);
        legPart.ChangeDirection(scaleX);
    }
}
