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
                    WeaponDesc = "This sleak piece of metal might not pack as much punch compared to other melee weapons, but it more than makes up for it in speed.",
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
                    WeaponDesc = "While its weight makes it slow to swing, this monkey-made hit stick packs a real wallop.",
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
            //Pea Shooter
            case Helper.WeaponName.PeaShooter:
                Weapon peaShooter = new Weapon(weaponName)
                {
                    WeaponDesc = "This is why your mother always tells you to eat your greens.",
                    WeaponType = Helper.WeaponType.Projectile,
                    ArmEquippedOn = armEquippedOn,
                    AttackTarget = attackTarget,
                    Damage = 2,
                    AttackCooldown = 3.5f
                };
                return peaShooter;
            //Banana Gun
            case Helper.WeaponName.BananaGun:
                Weapon bananaGun = new Weapon(weaponName)
                {
                    WeaponDesc = "Banana peels slip on the ground, so it only makes sense that the insides would slip through the air, making them the speediest, most delicious bullets you'll ever find.",
                    WeaponType = Helper.WeaponType.Projectile,
                    ArmEquippedOn = armEquippedOn,
                    AttackTarget = attackTarget,
                    Damage = 1,
                    AttackCooldown = 2.8f
                };
                return bananaGun;
            //Squeaky Hammer
            case Helper.WeaponName.SqueakyHammer:
                Weapon squeakyHammer = new Weapon(weaponName)
                {
                    WeaponDesc = "This little knick-knack was won at a carnival hammer game by a norse god in his toddler years. Its surprisingly heavy, so it's pretty slow, but its power is shocking to say the least.",
                    WeaponType = Helper.WeaponType.Projectile,
                    ArmEquippedOn = armEquippedOn,
                    AttackTarget = attackTarget,
                    Damage = 4,
                    AttackCooldown = 2.5f
                };
                return squeakyHammer;
            default:
                return null;
        }
    }
}
