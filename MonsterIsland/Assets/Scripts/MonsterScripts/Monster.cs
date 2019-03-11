using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    //parts of the Monster
    public HeadPart headPart;
    public TorsoPart torsoPart;

    public void InitializeMonster(HeadPartInfo headInfo, TorsoPartInfo torsoInfo)
    {
        headPart.InitializePart(headInfo);
        torsoPart.InitializePart(torsoInfo);
    }
}
