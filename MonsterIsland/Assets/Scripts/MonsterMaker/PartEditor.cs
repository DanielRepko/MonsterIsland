using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartEditor : MonoBehaviour {

    public PartSlot partSlot;
    public Image primaryColorIndicator;
    public Image secondaryColorIndicator;

    public void PrimaryColorPressed(string color)
    {
        partSlot.ChangePrimaryColor(color);

        Color parsedColor = new Color();
        if (ColorUtility.TryParseHtmlString(color, out parsedColor))
        {
            primaryColorIndicator.color = parsedColor;
        }
         
    }

    public void SecondaryColorPressed(string color)
    {
        partSlot.ChangeSecondaryColor(color);

        Color parsedColor = new Color();
        if (ColorUtility.TryParseHtmlString(color, out parsedColor))
        {
            secondaryColorIndicator.color = parsedColor;
        }
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