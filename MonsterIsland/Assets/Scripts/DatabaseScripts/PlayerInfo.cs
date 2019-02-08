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
    public ArmPartInfo leftArm;
    public ArmPartInfo rightArm;
    public LegPartInfo legs;
    public string rightWeapon;
    public string leftWeapon;
    public InventoryInfo inventory;
}