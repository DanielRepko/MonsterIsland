using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class PartSlot : MonoBehaviour {
    public MonsterPartInfo partInfo;
    public Vector3 originalPosition;

    abstract public void ChangePart(MonsterPartInfo newPart);

    abstract public void ChangePrimaryColor(string newColor);

    abstract public void ChangeSecondaryColor(string newColor);
}