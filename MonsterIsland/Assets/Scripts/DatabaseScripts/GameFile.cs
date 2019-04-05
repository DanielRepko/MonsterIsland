using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameFile {
    public int fileID;
    public float totalPlayTime;
    public string saveDate;
    public string saveArea;
    public string saveNest;
    public PlayerInfo player;
    public GameProgression gameProgression;
}