using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartEditor : MonoBehaviour
{
    public PartSlot partSlot;
    public Image primaryColorIndicator;
    public Image secondaryColorIndicator;
    public Text abilityName;
    public Text abilityDesc;

    public ScrollRect partPicker;
    public string[] availableParts;

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

    public void PopulatePartPicker()
    {
        //iterating through all of the items in the availbleParts array
        for (int i = 0; i < availableParts.Length; i++)
        {
            //loading the appropriate PartPickerPrefab
            var pickerButtonPrefab = Resources.Load("Prefabs/MonsterMaker/"+partSlot.partType+"PickerButton") as GameObject;

            //instantiating the 
            var pickerButton = (GameObject)Instantiate(pickerButtonPrefab, Vector2.zero, Quaternion.identity);

            //initializing the pickerButton and also saving the created PartInfo
            var partInfo = pickerButton.GetComponent<PartPickerButton>().InitializePickerButton(availableParts[i], partSlot.GetComponent<Image>().material, partSlot.partType);
            //setting the onClick listener to the pickerButton
            pickerButton.GetComponent<Button>().onClick.AddListener(
                delegate
                {
                    partSlot.ChangePart(partInfo);
                });

            //getting the recttransform of the button
            var pickerButtonTransform = pickerButton.GetComponent<RectTransform>();
            pickerButtonTransform.SetParent(partPicker.content);
        }
    }

    public void OpenPartEditor(PartSlot partSlot)
    {
        this.partSlot = partSlot;
        this.partSlot.EnterPartEditor();
        PopulatePartPicker();
        abilityName.text = this.partSlot.abilityName;
        abilityDesc.text = this.partSlot.abilityDesc;
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