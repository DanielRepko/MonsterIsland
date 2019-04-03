using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Actor {

    public Text text;
    public string monsterName;
    public bool alwaysDropPart = false;
    public string partToAlwaysDrop;
    public string equippedWeapon;
    public AbilityFactory.ArmAbility armAttackDelegate;
    public AbilityFactory.Ability abilityDelegate;

    public delegate void CheckDelegate();
    public CheckDelegate checkDelegate;

    //attack cooldown
    public float attackCooldown;
    private float attackCooldownTimer;
    //ability cooldown
    public float abilityCooldown;
    private float abilityCooldownTimer;

    public Canvas healthBar;

    //fields for handling aggro
    public bool isAggro;
    public float aggroRange = 10f;
    public float aggroTime;
    private float aggroTimer;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        InitializeEnemy();
        SetFacingDirection(transform.localScale.x);
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0) {
            KillEnemy();
        }
	}

    void FixedUpdate()
    {
        text.text = health.ToString();

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
        //Ray attack = new Ray();
        //attack.origin = transform.position;
        //attack.direction = new Vector2(-1, 0);

        //Debug.DrawRay(attack.origin, new Vector3(1.7f * -1f, 0, 0), Color.green);

        //RaycastHit2D hit = Physics2D.Raycast(attack.origin, attack.direction, 1.7f, 1 << LayerMask.NameToLayer("Player"));
        //if (hit)
        //{
        //    if (hit.collider == PlayerController.Instance.hurtBox)
        //    {
        //        Debug.Log("hit the player");
        //    }
        //}

        //GetComponent<Animator>().Play("FrontArmMeleeAnim");
    }

    public void InitializeEnemy()
    {
        //creating variables to initialize the player monster
        //this code is for testing purposes, final product will pull this information from the database scripts
        var headInfo = PartFactory.GetHeadPartInfo(monsterName);
        var torsoInfo = PartFactory.GetTorsoPartInfo(monsterName);
        var rightArmInfo = PartFactory.GetArmPartInfo(monsterName, Helper.PartType.RightArm);
        var leftArmInfo = PartFactory.GetArmPartInfo(monsterName, Helper.PartType.LeftArm);
        var legPartInfo = PartFactory.GetLegPartInfo(monsterName);
        rightArmInfo.equippedWeapon = equippedWeapon;

        armAttackDelegate = Attack;
        abilityDelegate = Ability;

        //adding methods to be run in fixed update to the check delegate
        //this allows multiple methods that need to constantly check some status of the Enemy
        //to be run in FixedUpdate without filling it with method calls
        checkDelegate += UpdateCooldowns;
        checkDelegate += CheckLineOfSight;
        checkDelegate += CheckAggro;
        checkDelegate += FollowPlayer;

        monster.InitializeMonster(headInfo, torsoInfo, rightArmInfo, leftArmInfo, legPartInfo);
        //setting the cooldown timers so that the player can use the inputs as soon as the game loads
        attackCooldownTimer = attackCooldown;
        abilityCooldownTimer = abilityCooldown;
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
        healthBar.transform.localScale = new Vector3(facingDirection, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        monster.ChangeDirection(facingDirection);
    }

    public void UpdateCooldowns()
    {
        if(attackCooldownTimer < attackCooldown)
        {
            attackCooldownTimer += Time.deltaTime;
        }
        if (abilityCooldownTimer < attackCooldown)
        {
            abilityCooldownTimer += Time.deltaTime;
        }
    }

    public void SetCooldowns()
    {

    }

    public void CheckAggro()
    {
        if(isAggro && aggroTimer < aggroTime)
        {
            aggroTimer += Time.deltaTime;
        }
        else if(isAggro && aggroTimer >= aggroTime)
        {
            isAggro = false;
            aggroTimer = 0;
        }
        else if (!isAggro)
        {
            aggroTimer = 0;
        }
    }

    public void FollowPlayer()
    {
        if (isAggro)
        {
            Debug.Log((PlayerController.Instance.transform.position - transform.position).normalized.x);
            //having the enemy face towards the player
            SetFacingDirection((PlayerController.Instance.transform.position - transform.position).normalized.x);
            //making the enemy move towards the player
            rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);
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
                default:
                    return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void CheckLineOfSight()
    {
        Ray lineOfSight = new Ray();
        lineOfSight.origin = transform.position;
        lineOfSight.direction = new Vector2(facingDirection, 0);

        Debug.DrawRay(lineOfSight.origin, new Vector3(aggroRange * facingDirection, 0, 0), Color.green);

        RaycastHit2D hit = Physics2D.Raycast(lineOfSight.origin, lineOfSight.direction, aggroRange, 1 << LayerMask.NameToLayer("Player"));
        if (hit)
        {
            isAggro = true;
        }
    }

    public void Attack(string armType = "RightArm")
    {

    }

    public void Ability()
    {

    }

    override public void TakeDamage(int damage, float knockBackDirection)
    {
        if (!inHitStun)
        {
            isAggro = true;
            SetFacingDirection(knockBackDirection);
            health -= damage;
            inHitStun = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool triggerIsHurtbox = false;
        //checking to see if the player ran into the Enemy
        Collider2D[] colliders = new Collider2D[5];
        ContactFilter2D contactFilter = new ContactFilter2D();
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");
        contactFilter.SetLayerMask(enemyLayer);
        contactFilter.useLayerMask = true;
        contactFilter.useTriggers = true;

        collision.OverlapCollider(contactFilter, colliders);

        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i] == hurtBox)
            {
                triggerIsHurtbox = true;
            }
        }

        if (collision == PlayerController.Instance.hurtBox && triggerIsHurtbox)
        {
            if (!inHitStun)
            {
                PlayerController.Instance.TakeDamage(1, Helper.GetKnockBackDirection(transform, collision.transform));
            }
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
            GameObject coin = Instantiate(GameManager.instance.coinPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            coin.GetComponent<Coin>().value = coinValue;
        }

        //6 to 10, 50% chance of getting a monster part
        if(partChance >= 6) {
            //Grab a random number from 1 to 5. This number represents one of the 5 parts (Head, Torso, Left Arm, Right Arm, Legs)
            int partToGet = Random.Range(0, 5) + 1;

            //If the enemy is always supposed to drop a part, set partToGet to the correct value based on what part it's supposed to drop
            if(alwaysDropPart) {
                switch(partToAlwaysDrop) {
                    case Helper.PartType.Head:
                        partToGet = 1;
                        break;
                    case Helper.PartType.Torso:
                        partToGet = 2;
                        break;
                    case Helper.PartType.LeftArm:
                        partToGet = 3;
                        break;
                    case Helper.PartType.RightArm:
                        partToGet = 4;
                        break;
                    case Helper.PartType.Legs:
                        partToGet = 5;
                        break;
                }
            }

            if (monsterName != "")
            {
                if (partToGet == 1)
                {
                    //Head
                    GameObject droppedHead = Instantiate(GameManager.instance.headDropPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    droppedHead.GetComponent<DroppedPart>().partType = Helper.PartType.Head;
                    droppedHead.GetComponent<DroppedPart>().monsterName = monsterName;
                    droppedHead.GetComponent<HeadPart>().InitializePart(PartFactory.GetHeadPartInfo(monsterName));
                }
                else if (partToGet == 2)
                {
                    //Torso
                    GameObject droppedTorso = Instantiate(GameManager.instance.torsoDropPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    droppedTorso.GetComponent<DroppedPart>().partType = Helper.PartType.Torso;
                    droppedTorso.GetComponent<DroppedPart>().monsterName = monsterName;
                    droppedTorso.GetComponent<TorsoPart>().InitializePart(PartFactory.GetTorsoPartInfo(monsterName));
                }
                else if (partToGet == 3)
                {
                    //Left Arm
                    GameObject droppedLeftArm = Instantiate(GameManager.instance.leftArmDropPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    droppedLeftArm.GetComponent<DroppedPart>().partType = Helper.PartType.LeftArm;
                    droppedLeftArm.GetComponent<DroppedPart>().monsterName = monsterName;
                    droppedLeftArm.GetComponent<ArmPart>().InitializePart(PartFactory.GetArmPartInfo(monsterName, "LeftArm"));
                }
                else if (partToGet == 4)
                {
                    //Right Arm
                    GameObject droppedRightArm = Instantiate(GameManager.instance.rightArmDropPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    droppedRightArm.GetComponent<DroppedPart>().partType = Helper.PartType.RightArm;
                    droppedRightArm.GetComponent<DroppedPart>().monsterName = monsterName;
                    droppedRightArm.GetComponent<ArmPart>().InitializePart(PartFactory.GetArmPartInfo(monsterName, "RightArm"));
                }
                else if (partToGet == 5)
                {
                    //Legs
                    GameObject droppedLegs = Instantiate(GameManager.instance.legsDropPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    droppedLegs.GetComponent<DroppedPart>().partType = Helper.PartType.Legs;
                    droppedLegs.GetComponent<DroppedPart>().monsterName = monsterName;
                    droppedLegs.GetComponent<LegPart>().InitializePart(PartFactory.GetLegPartInfo(monsterName));
                }
            }
        }

        Destroy(gameObject);
    }
}
