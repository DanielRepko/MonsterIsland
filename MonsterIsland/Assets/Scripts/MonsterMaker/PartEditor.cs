using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartEditor : MonoBehaviour {

    public PartSlot partSlot;

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