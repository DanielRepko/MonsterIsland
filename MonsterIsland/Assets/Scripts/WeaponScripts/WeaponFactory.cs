using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour {

	public static Weapon GetWeapon(string weaponName, string armEquippedOn, string weaponIsFor, SpriteRenderer weaponRenderer)
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
                    AttackRange = 3.3f,
                    AttackCooldown = 1f,
                    WeaponSpriteRenderer = weaponRenderer
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
                    AttackRange = 3f,
                    AttackCooldown = 0.4f,
                    WeaponSpriteRenderer = weaponRenderer
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
                    AttackRange = 2.6f,
                    AttackCooldown = 2f,
                    WeaponSpriteRenderer = weaponRenderer
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
                    AttackRange = 3.8f,
                    AttackCooldown = 1.5f,
                    WeaponSpriteRenderer = weaponRenderer
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
                    AttackCooldown = 1.3f,
                    WeaponSpriteRenderer = weaponRenderer
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
                    AttackCooldown = 0.7f,
                    WeaponSpriteRenderer = weaponRenderer
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
                    AttackCooldown = 2f,
                    WeaponSpriteRenderer = weaponRenderer
                };
                return squeakyHammer;
            case Helper.WeaponName.Bone:
                Weapon bone = new Weapon(weaponName)
                {
                    WeaponDesc = "The skeleton arm doesn't have very much muscle on it, so all it can really do is just huck this bone overhead.",
                    WeaponType = Helper.WeaponType.Projectile,
                    ArmEquippedOn = armEquippedOn,
                    AttackTarget = attackTarget,
                    Damage = 3,
                    AttackCooldown = 1.5f,
                    WeaponSpriteRenderer = weaponRenderer
                };
                return bone;
            case Helper.WeaponName.Boomerang:
                Weapon boomerang = new Weapon(weaponName)
                {
                    WeaponDesc = "Crikey! If you throw this here curved stick at a bloke, it'll fly right back to ya! And it'll damage any poor sops it happens to come across on the way!",
                    WeaponType = Helper.WeaponType.Projectile,
                    ArmEquippedOn = armEquippedOn,
                    AttackTarget = attackTarget,
                    Damage = 2,
                    AttackCooldown = 1.5f,
                    WeaponSpriteRenderer = weaponRenderer
                };
                return boomerang;
            case Helper.WeaponName.HarpoonGun:
                Weapon harpoonGun = new Weapon(weaponName)
                {
                    WeaponDesc = "Careful where you point this thing, the barbed tip of that harpoon is so sharp, it can lodge itself right into walls. It can get so well stuck, you can even jump right on it, and it holds just fine.",
                    WeaponType = Helper.WeaponType.Projectile,
                    ArmEquippedOn = armEquippedOn,
                    AttackTarget = attackTarget,
                    Damage = 2,
                    AttackCooldown = 1.5f,
                    WeaponSpriteRenderer = weaponRenderer
                };
                return harpoonGun;
            case Helper.WeaponName.Fan:
                Weapon fan = new Weapon(weaponName)
                {
                    WeaponDesc = "This paper fan blows at dealing damage, but it also blows gusts of wind that can push back enemies",
                    WeaponType = Helper.WeaponType.Melee,
                    ArmEquippedOn = armEquippedOn,
                    AttackTarget = attackTarget,
                    Damage = 1,
                    AttackRange = 2.8f,
                    AttackCooldown = 1.5f,
                    WeaponSpriteRenderer = weaponRenderer
                };
                return fan;
            default:
                return null;
        }
    }
}
