using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonster : MonoBehaviour {

    //parts of the Monster
    public HeadPart headPart;
    public TorsoPart torsoPart;
    public ArmPart rightArm;
    public ArmPart leftArm;
    public LegPart legPart;

    public void InitializeMonster(HeadPartInfo headInfo, TorsoPartInfo torsoInfo, ArmPartInfo rightArmInfo, ArmPartInfo leftArmInfo, LegPartInfo legPartInfo)
    {
        headPart.InitializePart(headInfo);
        torsoPart.InitializePart(torsoInfo);
        rightArm.InitializePart(rightArmInfo);
        leftArm.InitializePart(leftArmInfo);
        legPart.InitializePart(legPartInfo);
    }
}
