using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PartPickerButton : MonoBehaviour {
    public string partType;

    abstract public MonsterPartInfo InitializePickerButton(string monsterName, string partType);
}