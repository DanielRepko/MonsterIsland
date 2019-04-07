using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour {

    public Weapon weapon;
    public Image weaponImage;
    public Vector3 originalPosition;
    public string weaponName;
    public string weaponType;
    public string weaponDesc;
    public Text abilitySignLabel;

    public void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;

        UpdateAbilityBoard();

        UpdateUI();
    }

    public void UpdateAbilityBoard()
    {
        string weaponHand = gameObject.name.Substring(0, gameObject.name.Length - 10);
        if (weaponHand == "Right")
        {
            MonsterPartInfo armPart = GetComponentInParent<MonsterMaker>().rightArmSlot.partInfo;
            if (armPart.abilityType == "Activate")
            {
                abilitySignLabel.text = "";
            }
        }
        else if (weaponHand == "Left")
        {
            MonsterPartInfo armPart = GetComponentInParent<MonsterMaker>().leftArmSlot.partInfo;
            if (armPart.abilityType == "Activate")
            {
                abilitySignLabel.text = "";
            }
        }
    }

    public void EnterWeaponPicker()
    {
        transform.localPosition = new Vector3(0, 50f, 0);
        transform.localScale = new Vector3(1.4f, 1.4f, 0);
        gameObject.GetComponent<Button>().interactable = false;
    }

    public void ExitWeaponPicker()
    {
        transform.localPosition = originalPosition;
        transform.localScale = new Vector3(1f, 1f, 0);
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void UpdateUI()
    {
        weaponImage.sprite = weapon.WeaponSprite;
    }
}
