using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Actor {

    [Header("PATROL POINTS")]
    //a list to store the enemy's patrol points
    public List<GameObject> patrolPoints = new List<GameObject>();

    [Space(10)]

    public float moveSpeed = 10;

    public string monsterName;
    public bool alwaysDropPart = false;
    public string partToAlwaysDrop;
    public string equippedWeapon;
    public AbilityFactory.ArmAbility attackDelegate;
    public AbilityFactory.Ability abilityDelegate;

    public delegate void CheckDelegate();
    public CheckDelegate checkDelegate;

    public float attackRange = 1.7f;
    public int damage = 2;

    //attack cooldown
    public float attackCooldown = 0.5f;
    private float attackCooldownTimer;
    //ability cooldown
    public float abilityCooldown;
    private float abilityCooldownTimer;
    //jump cooldown (to prevent them from jumping every possible frame)
    public float jumpCooldown = 1;
    private float jumpCooldownTimer;

    //fields for handling aggro
    public bool isAggro;
    public float aggroRange = 8;
    public float aggroTime = 5;
    private float aggroTimer;

    //the target for the enemy to follow, can be the player or patrol points
    public GameObject target;
    

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.5f;

        InitializeEnemy();
        ContinuePatrol();
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0) {
            KillEnemy();
        }
	}

    virtual public void FixedUpdate()
    {
        //attacking if aggro
        if (isAggro && PlayerIsInAttackRange())
        {
            attackDelegate(Helper.PartType.RightArm);
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
        //creating variables to initialize the player monster
        //this code is for testing purposes, final product will pull this information from the database scripts
        var headInfo = PartFactory.GetHeadPartInfo(monsterName);
        var torsoInfo = PartFactory.GetTorsoPartInfo(monsterName);
        var rightArmInfo = PartFactory.GetArmPartInfo(monsterName, Helper.PartType.RightArm);
        var leftArmInfo = PartFactory.GetArmPartInfo(monsterName, Helper.PartType.LeftArm);
        var legPartInfo = PartFactory.GetLegPartInfo(monsterName);
        rightArmInfo.equippedWeapon = equippedWeapon;

        attackDelegate = Attack;
        abilityDelegate = Ability;

        //adding methods to be run in fixed update to the check delegate
        //this allows multiple methods that need to constantly check some status of the Enemy
        //to be run in FixedUpdate without filling it with method calls
        checkDelegate += UpdateCooldowns;
        checkDelegate += CheckLineOfSight;
        checkDelegate += CheckAggro;
        checkDelegate += FollowTarget;

        monster.InitializeMonster(headInfo, torsoInfo, rightArmInfo, leftArmInfo, legPartInfo);
        //setting the cooldown timers so that the player can use the inputs as soon as the game loads
        attackCooldownTimer = attackCooldown;
        abilityCooldownTimer = abilityCooldown;

        SetFacingDirection(transform.localScale.x);
    }

   virtual public void Attack(string armType = "RightArm")
    {
        animator.Play("RightArm" + Helper.GetAnimDirection(facingDirection, Helper.PartType.RightArm) + "MeleeAnim");
        PlayerController.Instance.TakeDamage(damage, Helper.GetKnockBackDirection(transform, PlayerController.Instance.transform));
    }

    virtual public void Ability()
    {

    }

    public void FollowTarget()
    {
        if (target != null && !inHitStun && !movementLocked)
        {
            //having the enemy face towards the target
            SetFacingDirection((target.transform.position - transform.position).normalized.x);

            //making the enemy jump if they need to
            MakeEnemyJump();
            
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

    //used to make the enemy patrol the area between their assigned patrol points
    //if there is only one assigned patrol point, then it is a sentry position,
    //in which case the enemy will simply wait at that point for the player to come along
    public void ContinuePatrol()
    {
        if (!isAggro)
        {
            //handing the patrol if there is only one patrol point (sentry position)
            if(patrolPoints.Count == 1)
            {
                //enemy is restarting their patrol
                if(target != patrolPoints[0])
                {
                    target = patrolPoints[0];
                }
                //enemy has reached their sentry position
                else
                {
                    //turn the enemy towards the direction they came from
                    SetFacingDirection(-facingDirection);
                    target = null;
                }
            }
            else if (patrolPoints.Count == 2)
            {
                //enemy is restarting their patrol
                if(target != patrolPoints[0] && target != patrolPoints[1])
                {
                    float distanceToPoint1 = Vector2.Distance(transform.position, patrolPoints[0].transform.position);
                    float distanceToPoint2 = Vector2.Distance(transform.position, patrolPoints[1].transform.position);

                    target = (distanceToPoint1 < distanceToPoint2) ? patrolPoints[0] : patrolPoints[1];
                }
                //enemy has reached one of the patrol points
                else
                {
                    //setting the target to the next patrol point
                    GameObject nextPatrolPoint = (target == patrolPoints[1]) ? patrolPoints[0] : patrolPoints[1];
                    target = nextPatrolPoint;
                }
            }
            //if the above statements are false, their must be no patrol points set
            else
            {
                Debug.LogWarning("PATROL POINT ERROR: This enemy has "+patrolPoints.Count+" patrol points assigned to it. Make sure you correctly set the patrol points for \""+gameObject.name+"\". Each enemy must have either 1 or 2 patrol points set");
            }
            
            
        }
    }

    //this method contains all code to check for the various
    //conditions under which the enemy will need to jump
    public void MakeEnemyJump()
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
            if(HasReachedLedge() && CanJumpOverGap())
            {
                Jump();
            }
        }
    }

    //sends out a raycast to see if the enemy has reached a ledge
    public bool HasReachedLedge()
    {
        Ray gapRay = new Ray();
        gapRay.origin = new Vector2(transform.position.x, transform.position.y + 1.4f);
        gapRay.direction = new Vector2(1.5f * facingDirection, -3.4f).normalized;

        Debug.DrawRay(gapRay.origin, new Vector2(1.5f * facingDirection, -3.4f), Color.blue);

        RaycastHit2D gapHit = Physics2D.Raycast(gapRay.origin, gapRay.direction, new Vector2(1.5f, -3.4f).magnitude, 1 << LayerMask.NameToLayer("Terrain"));

        return !gapHit;
    }

    //sends out a raycast to see if there is terrain at the end of their jump range
    public bool CanJumpOverGap()
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
    public bool TargetIsOnHigherPlatform()
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

    public void SetFacingDirection(float scaleX)
    {
        //checking to see whether scaleX is indicating left or right (may not always be passed as -1 or 1)
        if(scaleX < 0)
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

    public bool PlayerIsInAttackRange()
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
                    if (equippedWeapon != "")
                    {
                        monster.rightArmPart.weapon.Damage = 0;
                        attackDelegate(Helper.PartType.RightArm);
                        monster.rightArmPart.weapon.Damage = damage;
                    }
                    else
                    {
                        int rememberDamage = damage;
                        damage = 0;
                        attackDelegate(Helper.PartType.RightArm);
                        damage = rememberDamage;
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
            isAggro = true;
            SetFacingDirection(knockBackDirection);
            animator.Play("KnockBack" + Helper.GetAnimDirection(facingDirection) + "Anim");
            health -= damage;
            inHitStun = true;
        }
    }

    public void UpdateCooldowns()
    {
        if (attackCooldownTimer < attackCooldown)
        {
            attackCooldownTimer += Time.deltaTime;
        }
        if (abilityCooldownTimer < attackCooldown)
        {
            abilityCooldownTimer += Time.deltaTime;
        }
        if (abilityCooldownTimer < abilityCooldown)
        {
            abilityCooldownTimer += Time.deltaTime;
        }
        if (jumpCooldownTimer < jumpCooldown)
        {
            jumpCooldownTimer += Time.deltaTime;
        }
    }

    public bool CheckCooldown(string inputCooldown)
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
                case "ability":
                    if (abilityCooldownTimer >= abilityCooldown)
                    {
                        abilityCooldownTimer = 0;
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

    public void SetCooldowns()
    {
        attackCooldownTimer = attackCooldown;
        abilityCooldownTimer = abilityCooldown;
        jumpCooldownTimer = jumpCooldown;
    }

    public void CheckAggro()
    {
        if (isAggro && aggroTimer < aggroTime)
        {
            target = PlayerController.Instance.gameObject;
            aggroTimer += Time.deltaTime;
        }
        else if (isAggro && aggroTimer >= aggroTime)
        {
            isAggro = false;
            aggroTimer = 0;
            ContinuePatrol();
        }
        else if (!isAggro)
        {
            aggroTimer = 0;
        }
    }

    public void CheckLineOfSight()
    {
        if (!isAggro)
        {
            Ray lineOfSight = new Ray();
            lineOfSight.origin = transform.position;
            lineOfSight.direction = new Vector2(facingDirection, 0);

            Debug.DrawRay(lineOfSight.origin, new Vector3(aggroRange * facingDirection, 0, 0), Color.yellow);

            RaycastHit2D hit = Physics2D.Raycast(lineOfSight.origin, lineOfSight.direction, aggroRange, 1 << LayerMask.NameToLayer("Player"));
            if (hit)
            {
                isAggro = true;
                target = PlayerController.Instance.gameObject;
            }
        }
    }

    virtual public void OnTriggerEnter2D(Collider2D collision)
    {
        //checking to see if the enemy reached a patrol point
        if(collision.tag == "PatrolPoint" && target != null && target == collision.gameObject && transform.position.y >= collision.transform.position.y)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ContinuePatrol();
        }

        //checking to see if the enemy is underwater
        if(collision.tag == "Water")
        {
            isUnderwater = true;
            jumpCooldown = 0.2f;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //checking to see if the enemy is underwater
        if (collision.tag == "Water")
        {
            isUnderwater = false;
        }
    }

    private void KillEnemy() {
        int coinChance = Random.Range(0, 10) + 1;
        int partChance = Random.Range(0, 10) + 1;

        //If the enemy is supposed to always drop a part, overwrite partChance to be 10
        if(alwaysDropPart) {
            partChance = 10;
        }

        //6 to 10, 50% chance of getting coins
        if(coinChance >= 6) {
            //Grab a random coin value from 1 to 5, create the coin, and set it's value
            int coinValue = Random.Range(0, 5) + 1;
            GameObject coin = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItems/Coin"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            coin.GetComponent<Coin>().value = coinValue;
        }

        //6 to 10, 50% chance of getting a monster part
        if(partChance >= 6) {
            //Grab a random number from 1 to 5. This number represents one of the 5 parts (Head, Torso, Left Arm, Right Arm, Legs)
            int partToGet = Random.Range(0, 4) + 1;

            //If the enemy is always supposed to drop a part, set partToGet to the correct value based on what part it's supposed to drop
            if(alwaysDropPart) {
                switch(partToAlwaysDrop) {
                    case Helper.PartType.Head:
                        partToGet = 1;
                        break;
                    case Helper.PartType.Torso:
                        partToGet = 2;
                        break;
                    case "Arms":
                        partToGet = 3;
                        break;
                    case Helper.PartType.Legs:
                        partToGet = 4;
                        break;
                }
            }

            if (monsterName != "")
            {
                if (partToGet == 1)
                {
                    //Head
                    GameObject droppedHead = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItems/DroppedHead"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    droppedHead.GetComponent<DroppedPart>().partType = Helper.PartType.Head;
                    droppedHead.GetComponent<DroppedPart>().monsterName = monsterName;
                    droppedHead.GetComponent<HeadPart>().InitializePart(PartFactory.GetHeadPartInfo(monsterName));
                }
                else if (partToGet == 2)
                {
                    //Torso
                    GameObject droppedTorso = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItems/DroppedTorso"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    droppedTorso.GetComponent<DroppedPart>().partType = Helper.PartType.Torso;
                    droppedTorso.GetComponent<DroppedPart>().monsterName = monsterName;
                    droppedTorso.GetComponent<TorsoPart>().InitializePart(PartFactory.GetTorsoPartInfo(monsterName));
                }
                else if (partToGet == 3)
                {
                    //deciding whether to drop the left or right arm
                    int armToGet = Random.Range(0, 2) + 1;
                    if (armToGet == 1)
                    {
                        //Left Arm
                        GameObject droppedLeftArm = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItems/DroppedLeftArm"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                        droppedLeftArm.GetComponent<DroppedPart>().partType = Helper.PartType.LeftArm;
                        droppedLeftArm.GetComponent<DroppedPart>().monsterName = monsterName;
                        droppedLeftArm.GetComponent<ArmPart>().InitializePart(PartFactory.GetArmPartInfo(monsterName, "LeftArm"));
                    }
                    else if (armToGet == 2)
                    {
                        //Right Arm
                        GameObject droppedRightArm = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItems/DroppedRightArm"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                        droppedRightArm.GetComponent<DroppedPart>().partType = Helper.PartType.RightArm;
                        droppedRightArm.GetComponent<DroppedPart>().monsterName = monsterName;
                        droppedRightArm.GetComponent<ArmPart>().InitializePart(PartFactory.GetArmPartInfo(monsterName, "RightArm"));
                    }
                }
                else if (partToGet == 4)
                {
                    //Legs
                    GameObject droppedLegs = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItems/DroppedLegs"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    droppedLegs.GetComponent<DroppedPart>().partType = Helper.PartType.Legs;
                    droppedLegs.GetComponent<DroppedPart>().monsterName = monsterName;
                    droppedLegs.GetComponent<LegPart>().InitializePart(PartFactory.GetLegPartInfo(monsterName));
                }
            }
        }

        Destroy(gameObject);
    }
}
