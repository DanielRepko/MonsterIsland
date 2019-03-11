using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    //parts of the Monster
    public HeadPart headPart;

    public void InitializeMonster(HeadPartInfo headInfo)
    {
        headPart.InitializePart(headInfo);
    }
}
