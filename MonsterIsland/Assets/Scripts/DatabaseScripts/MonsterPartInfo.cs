﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class MonsterPartInfo{
    public string monster;
    public string partType;
    public string abilityName;
    public string abilityType;
    public string abilityDesc;
    public float abilityCooldown;
}