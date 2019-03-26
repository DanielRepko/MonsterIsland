using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour {

	public static Weapon GetWeapon(string weaponName, string armEquippedOn, string weaponIsFor)
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
                Weapon stick = new Weapon(weaponName)
                {
                    WeaponDesc = "A really good stick. Perfect for wacking people, and taking through walks in the woods.",
                    WeaponType = Helper.WeaponType.Melee,
                    ArmEquippedOn = armEquippedOn,
                    AttackTarget = attackTarget,
                    Damage = 3,
                    AttackRange = 2,
                    AttackCooldown = 0.7f
                };
                return stick;
            default:
                return null;
        }
    }
}
