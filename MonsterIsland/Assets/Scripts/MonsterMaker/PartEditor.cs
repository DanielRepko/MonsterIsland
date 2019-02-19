using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartEditor : MonoBehaviour
{

    public PartSlot partSlot;
    public Image primaryColorIndicator;
    public Image secondaryColorIndicator;

    public void PrimaryColorPressed(string color)
    {
        partSlot.ChangePrimaryColor(color);

        primaryColorIndicator.color = ConvertHexColor(color);
    }

    public void SecondaryColorPressed(string color)
    {
        partSlot.ChangeSecondaryColor(color);

        secondaryColorIndicator.color = ConvertHexColor(color);
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
        primaryColorIndicator.color = Color.white;
        secondaryColorIndicator.color = Color.white;
        gameObject.SetActive(false);
    }

    public Color ConvertHexColor(string hexColor)
    {
        Color parsedColor = new Color();
        if (ColorUtility.TryParseHtmlString(hexColor, out parsedColor))
        {
            return parsedColor;
        }
        else
        {
            return Color.white;
        }
    }
}