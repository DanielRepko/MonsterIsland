using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerInfo {
    public string name;
    public int totalHearts;
    public HeadPartInfo headPart;
    public TorsoPartInfo torsoPart;
    public ArmPartInfo leftArmPart;
    public ArmPartInfo rightArmPart;
    public LegPartInfo legsPart;
    public InventoryInfo inventory;
}