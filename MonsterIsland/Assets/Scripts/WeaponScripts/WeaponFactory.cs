using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour {

	public Weapon GetWeapon(string weaponName, string armEquippedOn, string weaponIsFor)
    {
        string attackTarget = "";

        if(weaponIsFor == "Player")
        {
            attackTarget = "Enemy";
        }
        else if (weaponIsFor == "Enemy")
        {
            attackTarget = "Player";
        }

        switch (weaponName)
        {
            case Helper.WeaponName.Stick:
                Weapon stick = new Weapon(weaponName);
                stick.WeaponDesc = "A really good stick. Perfect for wacking people, and taking through walks in the woods.";
                stick.WeaponType = Helper.WeaponType.Melee;
                stick.ArmEquippedOn = armEquippedOn;
                stick.AttackTarget = attackTarget;
                stick.Damage = 3;
                stick.AttackRange = 2;
                stick.AttackCooldown = 0.7f;

                return stick;
            default:
                return null;
        }
    }
}
