using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class PartSlot : MonoBehaviour {
    public MonsterPartInfo partInfo;
    public Vector3 originalPosition;
    public string partType;

    abstract public void ChangePart(MonsterPartInfo newPart);

    abstract public void ChangePrimaryColor(string newColor);

    abstract public void ChangeSecondaryColor(string newColor);

    abstract public void RefreshPart();

    public void EnterPartEditor()
    {
        transform.localPosition = new Vector3(0, 50f, 0);
        transform.localScale = new Vector3(1.4f, 1.4f, 0);
        gameObject.GetComponent<Button>().interactable = false;
    }

    public void ExitPartEditor()
    {
        transform.localPosition = originalPosition;
        transform.localScale = new Vector3(1f, 1f, 0);
        gameObject.GetComponent<Button>().interactable = true;
    }
}