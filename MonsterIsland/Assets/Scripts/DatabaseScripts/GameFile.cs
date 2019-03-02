﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameFile {
    public string saveFileName;
    public decimal totalPlayTime;
    public string saveDate;
    public string saveArea;
    public int saveNest;
    public PlayerInfo player;
    public GameProgression gameProgression;
}