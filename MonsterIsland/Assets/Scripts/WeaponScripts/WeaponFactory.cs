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
            //Stick
            case Helper.WeaponName.Stick:
                Weapon stick = new Weapon(weaponName)
                {
                    WeaponDesc = "A really good stick. Perfect for wacking people, and taking through walks in the woods.",
                    WeaponType = Helper.WeaponType.Melee,
                    ArmEquippedOn = armEquippedOn,
                    AttackTarget = attackTarget,
                    Damage = 3,
                    AttackRange = 2.5f,
                    AttackCooldown = 1f
                };
                return stick;
            //Scimitar
            case Helper.WeaponName.Scimitar:
                Weapon scimitar = new Weapon(weaponName)
                {
                    WeaponDesc = "This sleak piece of metal might not pack as much punch compared to other melee weapons, but it more than makes up for it in speed",
                    WeaponType = Helper.WeaponType.Melee,
                    ArmEquippedOn = armEquippedOn,
                    AttackTarget = attackTarget,
                    Damage = 2,
                    AttackRange = 2.2f,
                    AttackCooldown = 0.4f
                };
                return scimitar;
            //Club
            case Helper.WeaponName.Club:
                Weapon club = new Weapon(weaponName)
                {
                    WeaponDesc = "While its weight makes it slow to swing, this monkey-made hit stick packs a real wallop",
                    WeaponType = Helper.WeaponType.Melee,
                    ArmEquippedOn = armEquippedOn,
                    AttackTarget = attackTarget,
                    Damage = 4,
                    AttackRange = 1.8f,
                    AttackCooldown = 2f
                };
                return club;
            //Swordfish
            case Helper.WeaponName.Swordfish:
                Weapon swordFish = new Weapon(weaponName)
                {
                    WeaponDesc = "Something about this weapon seems... fishy. Oh, that's it! It has longer range than other melees!",
                    WeaponType = Helper.WeaponType.Melee,
                    ArmEquippedOn = armEquippedOn,
                    AttackTarget = attackTarget,
                    Damage = 2,
                    AttackRange = 3f,
                    AttackCooldown = 1.5f
                };
                return swordFish;
            default:
                return null;
        }
    }
}
