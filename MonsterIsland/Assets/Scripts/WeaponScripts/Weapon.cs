using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    //these fields are mainly used to display information about the weapon
    private string _weaponName;
    public string WeaponName { get { return _weaponName; } set { _weaponName = value; } }

    private string _weaponDesc;
    public string WeaponDesc { get { return _weaponDesc; } set { _weaponDesc = value; } }

    private string _weaponType;
    public string WeaponType { get { return _weaponType; } set { _weaponType = value; } }

    //holds the damage dealt by the weapon
    public static int _damage;
    public int Damage { get { return _damage; } set { _damage = value; } }

    //holds the range of the attack, used for melee weapons
    public static float _attackRange;
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }


    //The cooldown on the weapon's attack (essentially attack speed)
    public static float _attackCooldown;
    public float AttackCooldown { get { return _attackCooldown; } set { _attackCooldown = value; } }

    //the sprite to be used for the weapon
    private Sprite _weaponSprite;
    public Sprite WeaponSprite { get { return _weaponSprite; } set { _weaponSprite = value; } }

    //the prefab to use for the projectile, if the weapon is a projectile type
    public static GameObject _projectilePrefab;
    public GameObject ProjectilePrefab { get { return _projectilePrefab; } set { _projectilePrefab = value; } }

    //used to store the attack delegate for the weapon
    //delegate is self-set based on the weapon type
    //using the AbilityFactory delegate type just so that I don't have to parse between different delegates in PlayerController
    public static AbilityFactory.Ability _attackDelegate;
    public AbilityFactory.Ability AttackDelegate { get { return _attackDelegate; }
        set
        {
            if(_weaponType == "Melee")
            {
                _attackDelegate = MeleeAttack;
            }
            else if(_weaponType == "Projectile")
            {
                _attackDelegate = ProjectileAttack;
            }
        }
    }

    public void MeleeAttack()
    {
        PlayerController player = PlayerController.Instance;

        Ray attackRay = new Ray();
        attackRay.origin = player.transform.position;
        attackRay.direction = new Vector2(player.facingDirection, 0);

        Debug.DrawRay(attackRay.origin, new Vector2(_attackRange * player.facingDirection, 0), Color.green);

        RaycastHit2D hit = Physics2D.Raycast(attackRay.origin, attackRay.direction, _attackRange, 1 << LayerMask.NameToLayer("Enemy"));
        if (hit)
        {
            Enemy enemy = hit.transform.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }
        }
    }

    public void ProjectileAttack()
    {

    }

}
