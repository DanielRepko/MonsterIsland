using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DragonBossFight : MonoBehaviour {

    public static PlayableDirector DragonFightDirector;

    private void Start()
    {
        DragonFightDirector = GetComponent<PlayableDirector>();
    }
}
