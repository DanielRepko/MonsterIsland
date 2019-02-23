using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml;
using System.IO;
using Unity.VectorGraphics;
using Unity.VectorGraphics.Editor;

public abstract class PartSlot : MonoBehaviour {
    public Vector3 originalPosition;
    public string partType;
    public string abilityName;
    public string abilityDesc;
    public Text abilitySignLabel;

    abstract public void ChangePart(MonsterPartInfo newPart);

    abstract public void ChangePrimaryColor(string newColor);

    abstract public void ChangeSecondaryColor(string newColor);

    //used to update the ui with the new/recolored part
    abstract public void UpdateUI();

    public void Start()
    {
        originalPosition = transform.localPosition;
    }

    //helper method used to change the colors of each section of the part
    public string ChangeColor(string partString, string colorID, string color)
    {
        XmlDocument partXml = new XmlDocument();
        partXml.LoadXml(partString);

        foreach (XmlNode node in partXml.DocumentElement.ChildNodes)
        {
            if (node.Attributes != null)
            {
                var nodeID = node.Attributes.GetNamedItem("id") as XmlAttribute;

                if (nodeID.Value.Contains(colorID))
                {
                    node.Attributes.GetNamedItem("fill").Value = color;
                }
            }
        }

        return partXml.InnerXml;
    }

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