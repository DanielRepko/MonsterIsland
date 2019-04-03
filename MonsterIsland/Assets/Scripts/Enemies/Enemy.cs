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

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        InitializeEnemy();
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

        checkDelegate += UpdateCooldowns;

        monster.InitializeMonster(headInfo, torsoInfo, rightArmInfo, leftArmInfo, legPartInfo);
        //setting the cooldown timers so that the player can use the inputs as soon as the game loads
        attackCooldownTimer = attackCooldown;
        abilityCooldownTimer = abilityCooldown;
    }

    public void SetFacingDirection(float scaleX)
    {
        facingDirection = scaleX;
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
            SetFacingDirection(knockBackDirection);
            health -= damage;
            inHitStun = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == PlayerController.Instance.hurtBox)
        {
            if (!inHitStun)
            {
                PlayerController.Instance.TakeDamage(1, Helper.GetKnockBackDirection(transform, collision.transform));
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == PlayerController.Instance.hurtBox)
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
