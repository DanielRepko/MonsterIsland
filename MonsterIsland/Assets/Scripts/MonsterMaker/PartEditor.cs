using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartEditor : MonoBehaviour {

    public PartSlot partSlot;
	
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () { 
		
	}

    public void OpenPartEditor(PartSlot partSlot)
    {
        this.partSlot = partSlot;
        this.partSlot.EnterPartEditor();
        gameObject.SetActive(true);
    }

    public void ClosePartEditor()
    {
        partSlot.ExitPartEditor();
        gameObject.SetActive(false);
    }
}