using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameFile {
    public string saveFileName;
    public CameraLocation cameraLocation;
    public decimal totalPlayTime;
    public string saveDate;
    public Player player;
    public GameProgression gameProgression;
}