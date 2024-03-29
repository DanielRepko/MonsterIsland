﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPicker : MonoBehaviour {

    public WeaponSlot weaponSlot;
    public Text weaponName;
    public Text weaponType;
    public Text weaponDesc;

    public ScrollRect weaponScroll;
    public List<string> availableWeapons;

    public void PopulateWeaponPicker()
    {
        var xOffset = 0f;
        if (availableWeapons.Count != 0)
        {
            //iterating through all of the items in the availbleWeapons array
            for (int i = 0; i < availableWeapons.Count; i++)
            {

                if (i == 0)
                {
                    xOffset = 60;
                }
                else
                {
                    xOffset += 120;
                }
                //loading the appropriate WeaponPicker prefab
                var pickerButtonPrefab = new GameObject();
                pickerButtonPrefab = Resources.Load<GameObject>("Prefabs/MonsterMaker/WeaponPickerButton");

                //instantiating the picker button
                var pickerButton = Instantiate(pickerButtonPrefab, Vector2.zero, Quaternion.identity);

                //initializing the pickerButton and also saving the created Weapon
                var weapon = pickerButton.GetComponent<WeaponPickerButton>().InitializePickerButton(availableWeapons[i]);
                //setting the onClick listener to the pickerButton
                pickerButton.GetComponent<Button>().onClick.AddListener(
                    delegate
                    {
                        weaponSlot.ChangeWeapon(weapon);
                        weaponName.text = weapon.WeaponName;
                        weaponType.text = weapon.WeaponType;
                        weaponDesc.text = weapon.WeaponDesc;
                    });

                //getting the rect transform of the button
                var pickerButtonTransform = pickerButton.GetComponent<RectTransform>();
                pickerButtonTransform.SetParent(weaponScroll.content);
                pickerButtonTransform.anchoredPosition = new Vector2(xOffset, 0);
                pickerButtonTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                weaponScroll.content.sizeDelta = new Vector2(weaponScroll.content.sizeDelta.x + 120f, weaponScroll.content.sizeDelta.y);
            }            
        }
    }

    public void RemoveWeapon()
    {
        weaponName.text = "";
        weaponType.text = "";
        weaponDesc.text = "";
        weaponSlot.ClearWeaponSlot();
    }

    public void ResetWeaponScroll()
    {
        weaponScroll.content.sizeDelta = new Vector2(-800, weaponScroll.content.sizeDelta.y);
        for (int i = 0; i < weaponScroll.content.childCount; i++)
        {
            Destroy(weaponScroll.content.GetChild(i).gameObject);
        }
    }

    public void OpenWeaponPicker(WeaponSlot weaponSlot)
    {
        this.weaponSlot = weaponSlot;
        this.weaponSlot.EnterWeaponPicker();
        PopulateWeaponPicker();
        weaponName.text = this.weaponSlot.weaponName;
        weaponType.text = this.weaponSlot.weaponType;
        weaponDesc.text = this.weaponSlot.weaponDesc;
        gameObject.SetActive(true);
    }

    public void CloseWeaponPicker()
    {
        if (weaponSlot != null)
        {
            weaponSlot.ExitWeaponPicker();
            ResetWeaponScroll();
            gameObject.SetActive(false);
        }
    }





}