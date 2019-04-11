using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy{

    [Space(10)]
    [Header("ACTUAL BOSS FIELDS")]
    public string equippedRightWeapon;
    public string equippedLeftWeapon;
    public AbilityFactory.ArmAbility rightAttackDelegate;
    public AbilityFactory.ArmAbility leftAttackDelegate;

    private string attackingArm;

    public float rightAttackRange = 1.7f;
    public float leftAttackRange = 1.7f;

    public int rightAttackDamage = 1;
    public int leftAttackDamage = 1;
    private int attackDamage;

    //attack cooldown
    public float rightAttackCooldown = 0.5f;
    public float leftAttackCooldown = 0.5f;


    // Use this for initialization
    override public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.5f;

        InitializeEnemy();
    }

    // Update is called once per frame
    override public void Update()
    {
        if (health <= 0)
        {
            KillBoss();
        }
    }

    override public void FixedUpdate()
    {
        if (!PlayerController.Instance.isAlive)
        {
            target = null;
        }
        //attacking if aggro
        if (target != null && PlayerIsInAttackRange())
        {
            attackDelegate(attackingArm);
            SetNextAttack();
        }

        //running any necessary checks on the Enemy
        checkDelegate();

        //Handling Hitstun
        if (inHitStun && hitStunTimer == 0)
        {
            rb.velocity = new Vector2(-10 * facingDirection, 30);
        }
        if (inHitStun && hitStunTimer < hitStunCooldown)
        {
            hitStunTimer += Time.deltaTime;
        }
        else if (inHitStun && hitStunTimer >= hitStunCooldown)
        {
            hitStunTimer = 0;
            inHitStun = false;
        }
    }

    override public void InitializeEnemy()
    {
        //creating variables to initialize the monster
        //this code is for testing purposes, final product will pull this information from the database scripts
        var headInfo = PartFactory.GetHeadPartInfo(monsterName);
        var torsoInfo = PartFactory.GetTorsoPartInfo(monsterName);
        var rightArmInfo = PartFactory.GetArmPartInfo(monsterName, Helper.PartType.RightArm);
        var leftArmInfo = PartFactory.GetArmPartInfo(monsterName, Helper.PartType.LeftArm);
        var legPartInfo = PartFactory.GetLegPartInfo(monsterName);
        rightArmInfo.equippedWeapon = equippedRightWeapon;
        leftArmInfo.equippedWeapon = equippedLeftWeapon;       

        rightAttackDelegate = RightAttack;
        leftAttackDelegate = LeftAttack;

        monster.InitializeMonster(headInfo, torsoInfo, rightArmInfo, leftArmInfo, legPartInfo);

        SetNextAttack();
        attackCooldownTimer = attackCooldown;

        //adding methods to be run in fixed update to the check delegate
        //this allows multiple methods that need to constantly check some status of the Enemy
        //to be run in FixedUpdate without filling it with method calls
        checkDelegate += UpdateCooldowns;
        checkDelegate += FollowTarget;

        
        //setting the cooldown timers so that the player can use the inputs as soon as the game loads

        SetFacingDirection(transform.localScale.x);
        SetCooldowns();
    }

    virtual public void RightAttack(string armType)
    {
        animator.Play("RightArm" + Helper.GetAnimDirection(facingDirection, Helper.PartType.RightArm) + "MeleeAnim");
        PlayerController.Instance.TakeDamage(rightAttackDamage, Helper.GetKnockBackDirection(transform, PlayerController.Instance.transform));
    }

    virtual public void LeftAttack(string armType)
    {
        animator.Play("LeftArm" + Helper.GetAnimDirection(facingDirection, Helper.PartType.RightArm) + "MeleeAnim");
        PlayerController.Instance.TakeDamage(leftAttackDamage, Helper.GetKnockBackDirection(transform, PlayerController.Instance.transform));        
    }

    //Used to alternate between the attacks of each arm
    virtual public void SetNextAttack()
    {
        if (attackingArm == Helper.PartType.RightArm)
        {
            attackDelegate = leftAttackDelegate;
            attackingArm = Helper.PartType.LeftArm;
            attackRange = leftAttackRange;
            attackDamage = leftAttackDamage;
            attackCooldown = leftAttackCooldown;
        }
        else if (attackingArm == Helper.PartType.LeftArm || attackingArm == null)
        {
            attackDelegate = rightAttackDelegate;
            attackingArm = Helper.PartType.RightArm;
            attackRange = rightAttackRange;
            attackDamage = rightAttackDamage;
            attackCooldown = rightAttackCooldown;
        }
    }

    override public void SetFacingDirection(float scaleX)
    {
        //checking to see whether scaleX is indicating left or right (may not always be passed as -1 or 1)
        if (scaleX < 0)
        {
            facingDirection = -1;
        }
        else
        {
            facingDirection = 1;
        }

        transform.localScale = new Vector3(facingDirection, 1, 1);
        monster.ChangeDirection(facingDirection);
    }

    override public bool PlayerIsInAttackRange()
    {
        if (!attacksLocked)
        {
            Ray attackRay = new Ray();
            attackRay.origin = transform.position;
            attackRay.direction = new Vector2(facingDirection, 0);

            Debug.DrawRay(attackRay.origin, new Vector3(attackRange * facingDirection, 0, 0), Color.red);

            RaycastHit2D attackHit = Physics2D.Raycast(attackRay.origin, attackRay.direction, attackRange, 1 << LayerMask.NameToLayer("Player"));
            if (attackHit)
            {
                if (attackHit.collider == PlayerController.Instance.hurtBox && CheckCooldown("attack"))
                {
                    return true;
                }
                else if (attackHit.collider == PlayerController.Instance.shellCollider && CheckCooldown("attack"))
                {
                    if (equippedRightWeapon != "" || equippedLeftWeapon != "")
                    {
                        monster.rightArmPart.weapon.Damage = 0;
                        monster.leftArmPart.weapon.Damage = 0;
                        attackDelegate(attackingArm);
                        monster.rightArmPart.weapon.Damage = attackDamage;
                        monster.leftArmPart.weapon.Damage = attackDamage;
                    }
                    else
                    {
                        int rememberDamage = attackDamage;
                        attackDamage = 0;
                        attackDelegate(attackingArm);
                        attackDamage = rememberDamage;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    override public void TakeDamage(int damage, float knockBackDirection)
    {
        if (!inHitStun)
        {
            SetFacingDirection(knockBackDirection);
            animator.Play("KnockBack" + Helper.GetAnimDirection(facingDirection) + "Anim");
            health -= damage;
            inHitStun = true;
        }
    }

    override public void UpdateCooldowns()
    {
        if (attackCooldownTimer < attackCooldown)
        {
            attackCooldownTimer += Time.deltaTime;
        }
        if (jumpCooldownTimer < jumpCooldown)
        {
            jumpCooldownTimer += Time.deltaTime;
        }
    }

    override public bool CheckCooldown(string inputCooldown)
    {
        if (!attacksLocked)
        {
            switch (inputCooldown)
            {
                case "attack":
                    if (attackCooldownTimer >= attackCooldown)
                    {
                        attackCooldownTimer = 0;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "jump":
                    if (jumpCooldownTimer >= jumpCooldown)
                    {
                        jumpCooldownTimer = 0;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
        else
        {
            return false;
        }
    }

    override public void SetCooldowns()
    {
        attackCooldownTimer = attackCooldown;
        jumpCooldownTimer = jumpCooldown;
    }

    override public void OnTriggerEnter2D(Collider2D collision)
    {
        //checking to see if the enemy is underwater
        if (collision.tag == "Water")
        {
            isUnderwater = true;
            jumpCooldown = 0.2f;
        }

    }

    override public void OnTriggerExit2D(Collider2D collision)
    {
        //checking to see if the enemy is underwater
        if (collision.tag == "Water")
        {
            isUnderwater = false;
            jumpCooldown = 1f;
        }
    }

    virtual public void KillBoss()
    {
        Destroy(gameObject);
    }

    override public void KillEnemy()
    {

    }

    override public void OnTriggerStay2D(Collider2D collision)
    {

    }

    override public void CheckLineOfSight()
    {

    }

    override public void CheckAggro()
    {

    }

    override public void ContinuePatrol()
    {

    }
}
