using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickerButton : MonoBehaviour {

    public Weapon weapon;
    public Image weaponImage;

	public Weapon InitializePickerButton(string weaponName)
    {
        weapon = WeaponFactory.GetWeapon(weaponName, null, null, null);
        weaponImage.sprite = weapon.WeaponSprite;
        return weapon;
    }
}
