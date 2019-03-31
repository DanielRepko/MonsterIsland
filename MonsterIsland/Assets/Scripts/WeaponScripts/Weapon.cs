using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon {

    //these fields are mainly used to display information about the weapon
    private string _weaponName;
    public string WeaponName { get { return _weaponName; } set { _weaponName = value; } }

    private string _weaponDesc;
    public string WeaponDesc { get { return _weaponDesc; } set { _weaponDesc = value; } }

    //holds the type of weapon (melee or projectile)
    //also sets the attackDelegate based on the type
    private string _weaponType;
    public string WeaponType { get { return _weaponType; }
        set {
            _weaponType = value;
            if(WeaponName == Helper.WeaponName.Fan)
            {
                AttackDelegate = FanAttack;
            }
            else if (_weaponType == Helper.WeaponType.Melee)
            {
                AttackDelegate = MeleeAttack;
            }
            else if (_weaponType == Helper.WeaponType.Projectile)
            {
                AttackDelegate = ProjectileAttack;
            }
        }
    }

    //holds which arm the weapon is equipped on
    private string _armEquippedOn;
    public string ArmEquippedOn { get { return _armEquippedOn; } set { _armEquippedOn = value; } }

    //holds the target of the weapon's attack
    //if the weapon is being used by an enemy the target will be Player
    //if the weapon is being used by the player the target will be Enemy
    private string _attackTarget;
    public string AttackTarget { get { return _attackTarget; } set { _attackTarget = value; } }

    //holds the damage dealt by the weapon
    private int _damage;
    public int Damage { get { return _damage; } set { _damage = value; } }

    //holds the range of the attack, used for melee weapons
    private float _attackRange;
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }


    //The cooldown on the weapon's attack (essentially attack speed)
    private float _attackCooldown;
    public float AttackCooldown { get { return _attackCooldown; } set { _attackCooldown = value; } }

    //the sprite to be used for the weapon
    private Sprite _weaponSprite;
    public Sprite WeaponSprite { get { return _weaponSprite; } set { _weaponSprite = value; } }

    private SpriteRenderer _weaponSpriteRenderer;
    public SpriteRenderer WeaponSpriteRenderer { get { return _weaponSpriteRenderer; } set { _weaponSpriteRenderer = value; } }

    //the prefab to use for the projectile, if the weapon is a projectile type
    private GameObject _projectilePrefab;
    public GameObject ProjectilePrefab { get { return _projectilePrefab; } set { _projectilePrefab = value; } }

    //used to store the attack delegate for the weapon
    //delegate is self-set based on the weapon type
    //using the AbilityFactory delegate type just so that I don't have to parse between different delegates in PlayerController
    private AbilityFactory.ArmAbility _attackDelegate;
    public AbilityFactory.ArmAbility AttackDelegate { get { return _attackDelegate; } set { _attackDelegate = value; } }

    public Weapon(string weaponName)
    {
        WeaponName = weaponName;
        WeaponSprite = Resources.Load<Sprite>("Sprites/Weapons/Weapon_" + weaponName);
        ProjectilePrefab = Resources.Load<GameObject>("Prefabs/Projectiles/Projectile_" + weaponName);
    }

    public void MeleeAttack(string armEquippedOn)
    {
        PlayerController player = PlayerController.Instance;

        //play attack animation
        player.animator.Play(ArmEquippedOn + Helper.GetAnimDirection(player.facingDirection, ArmEquippedOn) + "MeleeAnim");

        Ray attackRay = new Ray();
        attackRay.origin = player.transform.position;
        attackRay.direction = new Vector2(player.facingDirection, 0);

        Debug.DrawRay(attackRay.origin, new Vector2(AttackRange * player.facingDirection, 0), Color.green);
        RaycastHit2D hit = Physics2D.Raycast(attackRay.origin, attackRay.direction, _attackRange, 1 << LayerMask.NameToLayer(AttackTarget));
        if (hit)
        {
            if (AttackTarget == "Enemy")
            {
                Enemy enemy = hit.transform.GetComponentInParent<Enemy>();
                if (enemy != null && hit.collider == enemy.hurtBox)
                {
                    enemy.TakeDamage(Damage);
                }
            }
            else if(AttackTarget == "Player" && hit.collider == PlayerController.Instance.hurtBox)
            {
                PlayerController.Instance.TakeDamage(Damage);
            }
        }
    }

    public void ProjectileAttack(string armEquippedOn)
    {
        PlayerController player = PlayerController.Instance;

        if(WeaponName == Helper.WeaponName.Bone || WeaponName == Helper.WeaponName.Boomerang)
        {
            //play attack animation
            player.animator.Play(ArmEquippedOn + Helper.GetAnimDirection(player.facingDirection, ArmEquippedOn) + "MeleeAnim");
        }
        else
        {
            //play shoot animation
            player.animator.Play(ArmEquippedOn + Helper.GetAnimDirection(player.facingDirection, ArmEquippedOn) + "ShootAnim");
        }

        Vector2 projectilePosition = new Vector2();

        if (ArmEquippedOn == Helper.PartType.RightArm)
        {
            projectilePosition = player.monster.rightArmPart.hand.transform.position;
        }
        else if (ArmEquippedOn == Helper.PartType.LeftArm)
        {
            projectilePosition = player.monster.leftArmPart.hand.transform.position;
        }

        GameObject projectile = Object.Instantiate(ProjectilePrefab, projectilePosition, ProjectilePrefab.transform.rotation);
        projectile.GetComponent<Projectile>().target = AttackTarget;
        projectile.GetComponent<Projectile>().damage = Damage;
        projectile.GetComponent<Projectile>().weaponRenderer = WeaponSpriteRenderer;

        var facingDirection = WeaponSpriteRenderer.transform.GetComponentInParent<Rigidbody2D>().transform.localScale.x;
        projectile.transform.localScale *= facingDirection;

        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectile.GetComponent<Projectile>().speed * player.facingDirection, 0);
    }

    public void FanAttack(string armEquippedOn)
    {
        PlayerController player = PlayerController.Instance;

        //play attack animation
        player.animator.Play(ArmEquippedOn + Helper.GetAnimDirection(player.facingDirection, ArmEquippedOn) + "MeleeAnim");

        

        Vector2 projectilePosition = new Vector2();

        //the melee attack
        Ray attackRay = new Ray();
        attackRay.origin = player.transform.position;
        attackRay.direction = new Vector2(player.facingDirection, 0);

        Debug.DrawRay(attackRay.origin, new Vector2(AttackRange * player.facingDirection, 0), Color.green);
        RaycastHit2D hit = Physics2D.Raycast(attackRay.origin, attackRay.direction, _attackRange, 1 << LayerMask.NameToLayer(AttackTarget));
        if (hit)
        {
            if (AttackTarget == "Enemy")
            {
                Enemy enemy = hit.transform.GetComponentInParent<Enemy>();
                if (enemy != null && hit.collider == enemy.hurtBox)
                {
                    enemy.TakeDamage(Damage);
                }
            }
            else if (AttackTarget == "Player" && hit.collider == PlayerController.Instance.hurtBox)
            {
                PlayerController.Instance.TakeDamage(Damage);
            }
        }

        //the projectile
        if (ArmEquippedOn == Helper.PartType.RightArm)
        {
            projectilePosition = player.monster.rightArmPart.hand.transform.position;
        }
        else if (ArmEquippedOn == Helper.PartType.LeftArm)
        {
            projectilePosition = player.monster.leftArmPart.hand.transform.position;
        }

        GameObject projectile = Object.Instantiate(ProjectilePrefab, projectilePosition, ProjectilePrefab.transform.rotation);
        projectile.GetComponent<Projectile>().target = AttackTarget;
        projectile.GetComponent<Projectile>().damage = Damage;
        projectile.GetComponent<Projectile>().weaponRenderer = WeaponSpriteRenderer;

        var facingDirection = WeaponSpriteRenderer.transform.GetComponentInParent<Rigidbody2D>().transform.localScale.x;
        projectile.transform.localScale *= facingDirection;

        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectile.GetComponent<Projectile>().speed * player.facingDirection, 0);

        
    }

}
