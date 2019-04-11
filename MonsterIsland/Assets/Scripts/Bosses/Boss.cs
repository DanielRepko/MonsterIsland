using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Actor{

    public float moveSpeed = 10;

    public string monsterName;
    public string equippedRightWeapon;
    public string equippedLeftWeapon;
    public AbilityFactory.ArmAbility rightAttackDelegate;
    public AbilityFactory.ArmAbility leftAttackDelegate;
    public AbilityFactory.ArmAbility attackDelegate;

    private string attackingArm;

    public delegate void CheckDelegate();
    public CheckDelegate checkDelegate;

    public float rightAttackRange = 1.7f;
    public float leftAttackRange = 1.7f;
    private float attackRange;

    public int rightAttackDamage = 1;
    public int leftAttackDamage = 1;
    private int attackDamage;

    //attack cooldown
    public float rightAttackCooldown = 0.5f;
    public float leftAttackCooldown = 0.5f;
    private float attackCooldown;
    private float attackCooldownTimer = 0;

    //jump cooldown (to prevent them from jumping every possible frame)
    public float jumpCooldown = 1;
    private float jumpCooldownTimer;

    //the target for the enemy to follow
    public GameObject target;


    // Use this for initialization
    virtual public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.5f;

        InitializeEnemy();
    }

    // Update is called once per frame
    virtual public void Update()
    {
        if (health <= 0)
        {
            KillBoss();
        }
    }

    virtual public void FixedUpdate()
    {
        //attacking if aggro
        if (target != null && PlayerIsInAttackRange())
        {
            attackDelegate(attackingArm);
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

    virtual public void InitializeEnemy()
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

        monster.InitializeMonster(headInfo, torsoInfo, rightArmInfo, leftArmInfo, legPartInfo);

        rightAttackDelegate = RightAttack;
        leftAttackDelegate = LeftAttack; 

        SetNextAttack();
        attackCooldownTimer = attackCooldown;

        //adding methods to be run in fixed update to the check delegate
        //this allows multiple methods that need to constantly check some status of the Enemy
        //to be run in FixedUpdate without filling it with method calls
        checkDelegate += UpdateCooldowns;
        checkDelegate += FollowTarget;

        
        //setting the cooldown timers so that the player can use the inputs as soon as the game loads

        SetFacingDirection(transform.localScale.x);
    }

    virtual public void RightAttack(string armType)
    {
        animator.Play("RightArm" + Helper.GetAnimDirection(facingDirection, Helper.PartType.RightArm) + "MeleeAnim");
        PlayerController.Instance.TakeDamage(rightAttackDamage, Helper.GetKnockBackDirection(transform, PlayerController.Instance.transform));
        SetNextAttack();
    }

    virtual public void LeftAttack(string armType)
    {
        animator.Play("LeftArm" + Helper.GetAnimDirection(facingDirection, Helper.PartType.RightArm) + "MeleeAnim");
        PlayerController.Instance.TakeDamage(leftAttackDamage, Helper.GetKnockBackDirection(transform, PlayerController.Instance.transform));
        SetNextAttack();
    }

    //Used to alternate between the attacks of each arm
    virtual public void SetNextAttack()
    {
        if (attackingArm == Helper.PartType.RightArm)
        {
            attackDelegate = LeftAttack;
            attackingArm = Helper.PartType.LeftArm;
            attackRange = leftAttackRange;
            attackDamage = leftAttackDamage;
            attackCooldown = leftAttackCooldown;
        }
        else if (attackingArm == Helper.PartType.LeftArm || attackingArm == null)
        {
            attackDelegate = RightAttack;
            attackingArm = Helper.PartType.RightArm;
            attackRange = rightAttackRange;
            attackDamage = rightAttackDamage;
            attackCooldown = rightAttackCooldown;
        }
    }

    virtual public void FollowTarget()
    {
        if (target != null && !inHitStun && !movementLocked)
        {
            //having the enemy face towards the target
            SetFacingDirection((target.transform.position - transform.position).normalized.x);

            //making the enemy jump if they need to
            MakeBossJump();

            //making the enemy move towards the player
            rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);
            //playing the correct animation
            animator.SetBool("IsRunningRight", rb.velocity.x > 0);
            animator.SetBool("IsRunningLeft", rb.velocity.x < 0);
            animator.SetFloat("FacingDirection", facingDirection);

        }
        else
        {
            if (rb.velocity.x == 0)
            {
                animator.SetBool("IsRunningRight", false);
                animator.SetBool("IsRunningLeft", false);
            }
        }
    }    

    //this method contains all code to check for the various
    //conditions under which the enemy will need to jump
    virtual public void MakeBossJump()
    {
        //swim up to the target if underwater
        if (isUnderwater)
        {
            bool targetIsHigher = target.transform.position.y > transform.position.y && (target.transform.position.y - transform.position.y) >= 1;
            if (targetIsHigher)
            {
                Jump();
            }
        }
        //otherwise the enemy must be on land, so check when they need to jump
        else
        {
            //checking if the target is on a higher platform         
            if (TargetIsOnHigherPlatform())
            {
                Jump();
            }

            //checking if they need to jump over an obstacle
            Ray jumpRay = new Ray();
            jumpRay.origin = new Vector2(transform.position.x, transform.position.y - 1);
            jumpRay.direction = new Vector2(facingDirection, 0);

            Debug.DrawRay(jumpRay.origin, new Vector2(1 * facingDirection, 0), Color.cyan);

            RaycastHit2D jumpHit = Physics2D.Raycast(jumpRay.origin, jumpRay.direction, 1.2f, 1 << LayerMask.NameToLayer("Terrain"));
            if (jumpHit)
            {
                Jump();
            }

            //checking if they need to jump over a gap (and whether they can make the jump)
            if (HasReachedLedge() && CanJumpOverGap())
            {
                Jump();
            }
        }
    }

    //sends out a raycast to see if the enemy has reached a ledge
    virtual public bool HasReachedLedge()
    {
        Ray gapRay = new Ray();
        gapRay.origin = new Vector2(transform.position.x, transform.position.y + 1.4f);
        gapRay.direction = new Vector2(1.5f * facingDirection, -3.4f).normalized;

        Debug.DrawRay(gapRay.origin, new Vector2(1.5f * facingDirection, -3.4f), Color.blue);

        RaycastHit2D gapHit = Physics2D.Raycast(gapRay.origin, gapRay.direction, new Vector2(1.5f, -3.4f).magnitude, 1 << LayerMask.NameToLayer("Terrain"));

        return !gapHit;
    }

    //sends out a raycast to see if there is terrain at the end of their jump range
    virtual public bool CanJumpOverGap()
    {
        Ray canCrossRay = new Ray();
        canCrossRay.origin = new Vector2(transform.position.x, transform.position.y + 1.4f);
        canCrossRay.direction = new Vector2(7 * facingDirection, -3.4f).normalized;

        Debug.DrawRay(canCrossRay.origin, new Vector2(7 * facingDirection, -3.4f), Color.blue);

        RaycastHit2D canCrossHit = Physics2D.Raycast(canCrossRay.origin, canCrossRay.direction, new Vector2(6, -3.4f).magnitude, 1 << LayerMask.NameToLayer("Terrain"));

        return canCrossHit;
    }

    //used to check whether the target is on a platform above the enemy
    private float highTime = 0.4f;
    private float highTimer = 0;
    private float lastTargetHeight;
    virtual public bool TargetIsOnHigherPlatform()
    {
        bool targetIsHigher = target.transform.position.y > transform.position.y && (target.transform.position.y - transform.position.y) >= 1;
        float targetHeight = target.transform.position.y;
        if (targetIsHigher)
        {
            if (targetHeight == lastTargetHeight)
            {
                if (highTimer < highTime)
                {
                    highTimer += Time.deltaTime;
                    return false;
                }
                else if (highTimer >= highTime)
                {
                    highTimer = 0;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                lastTargetHeight = targetHeight;
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    virtual public void Jump()
    {
        if (IsOnGround() && CheckCooldown("jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.Play("Jump" + Helper.GetAnimDirection(facingDirection) + "Anim");
        }
    }

    virtual public void SetFacingDirection(float scaleX)
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

    virtual public bool PlayerIsInAttackRange()
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

    virtual public void UpdateCooldowns()
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

    virtual public bool CheckCooldown(string inputCooldown)
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

    virtual public void SetCooldowns()
    {
        attackCooldownTimer = attackCooldown;
        jumpCooldownTimer = jumpCooldown;
    }

    virtual public void OnTriggerEnter2D(Collider2D collision)
    {
        //checking to see if the enemy is underwater
        if (collision.tag == "Water")
        {
            isUnderwater = true;
            jumpCooldown = 0.2f;
        }

    }

    virtual public void OnTriggerExit2D(Collider2D collision)
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
}
