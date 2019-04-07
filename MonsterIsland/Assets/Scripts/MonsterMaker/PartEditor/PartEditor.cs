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
    public Text abilityType;
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
        var xOffset = 0f;
        //iterating through all of the items in the availbleParts array
        for (int i = 0; i < availableParts.Length; i++)
        {
            
            if(i == 0)
            {
                xOffset = 60;
            } else
            {
                xOffset += 120;
            }
            //loading the appropriate PartPickerPrefab
            var pickerButtonPrefab = new GameObject();
            if(partSlot.partType == "LeftArm" || partSlot.partType == "RightArm")
            {
                pickerButtonPrefab = Resources.Load("Prefabs/MonsterMaker/ArmPickerButton") as GameObject;
            }
            else
            {
                pickerButtonPrefab = Resources.Load("Prefabs/MonsterMaker/" + partSlot.partType + "PickerButton") as GameObject;
            }

            //instantiating the 
            var pickerButton = (GameObject)Instantiate(pickerButtonPrefab, Vector2.zero, Quaternion.identity);

            //initializing the pickerButton and also saving the created PartInfo
            var partInfo = pickerButton.GetComponent<PartPickerButton>().InitializePickerButton(availableParts[i], partSlot.partType);
            //setting the onClick listener to the pickerButton
            pickerButton.GetComponent<Button>().onClick.AddListener(
                delegate
                {
                    partSlot.ChangePart(partInfo);
                    abilityName.text = partInfo.abilityName;
                    abilityType.text = partInfo.abilityType;
                    abilityDesc.text = partInfo.abilityDesc;
                });

            //getting the recttransform of the button
            var pickerButtonTransform = pickerButton.GetComponent<RectTransform>();
            pickerButtonTransform.SetParent(partPicker.content);
            pickerButtonTransform.anchoredPosition = new Vector2(xOffset, 0);
            partPicker.content.sizeDelta = new Vector2(partPicker.content.sizeDelta.x+100.5f, partPicker.content.sizeDelta.y);
        }
    }

    public void ResetPartPicker()
    {
        partPicker.content.sizeDelta = new Vector2(-800, partPicker.content.sizeDelta.y);
        partPicker.horizontalScrollbar.value = 0;
        for(int i = 0; i < partPicker.content.childCount; i++)
        {
            Destroy(partPicker.content.GetChild(i).gameObject);
        }
    }

    public void OpenPartEditor(PartSlot partSlot)
    {
        this.partSlot = partSlot;
        this.partSlot.EnterPartEditor();
        PopulatePartPicker();
        abilityName.text = this.partSlot.abilityName;
        abilityType.text = this.partSlot.abilityType;
        abilityDesc.text = this.partSlot.abilityDesc;
        gameObject.SetActive(true);
    }

    public void ClosePartEditor()
    {
        if (partSlot != null)
        {
            partSlot.ExitPartEditor();
            primaryColorIndicator.color = Color.white;
            secondaryColorIndicator.color = Color.white;
            ResetPartPicker();
            gameObject.SetActive(false);
        }
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