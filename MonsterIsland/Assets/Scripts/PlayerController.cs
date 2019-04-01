using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance;

    public Animator animator;

    public float moveSpeed = 15.5f;
    public float jumpForce = 60f;

    //damage dealt by right arm attack
    public int rightAttackPower = 2;
    //damage dealt by left arm attack
    public int leftAttackPower = 2;

    public bool attacksLocked;
    //these fields are used to add delays between when the player can complete certain actions
    private float _rightAttackCooldown = 0.5f;
    public float RightAttackCooldown { get { return _rightAttackCooldown; } set { _rightAttackCooldown = value; } }
    private float rightAttackTimer = 0;

    private float _leftAttackCooldown = 0.5f;
    public float LeftAttackCooldown { get { return _leftAttackCooldown; } set { _leftAttackCooldown = value; } }
    private float leftAttackTimer = 0;

    private float _headAbilityCooldown = 0;
    public float HeadAbilityCooldown { get { return _headAbilityCooldown; } set { _headAbilityCooldown = value; } }
    private float headAbilityTimer = 0;

    private float _torsoAbilityCooldown = 0;
    public float TorsoAbilityCooldown { get { return _torsoAbilityCooldown; } set { _torsoAbilityCooldown = value; } }
    private float torsoAbilityTimer = 0;

    private float _legAbilityCooldown = 0;
    public float LegAbilityCooldown { get { return _legAbilityCooldown; } set { _legAbilityCooldown = value; } }
    private float legAbilityTimer = 0;

    [Space(20, order = 1)]


    public int health;
    public int maxHealth;
    public Collider2D hurtBox;

    private float hitStunCooldown = 0.4f;
    private float hitStunTimer = 0;
    public bool inHitStun = false;
    public bool movementLocked = false;

    public BoxCollider2D hitBox;
    public int hitBoxDamage;
    public int hitCounter;
    public int totalHits;

    public bool hasExtraJump = true;

    public float rayCastLengthCheck = 0.2f;
    public float width;
    public float height;

    private Collider2D nestCheck;

    [Header("Underwater Properties", order = 0)]
    //Values used in the underwater level
    public bool isUnderwater;           //If the user is underwater or not
    [Range(0.00f, 1.00f)]
    public float air;                   //The amount of air the player currently has. Min 0, max 1
    public float timeBetweenAirLoss;    //The amount of time between loss in air percentage, in seconds.
    [Range(0.01f, 1.00f)]
    public float airToLose;            //The amount of air to lose when required. Min 0.01, max 1
    public float timeBetwenAirDamage;  //The amount of time between damage from having no air, in seconds.
    public float drownDamage;          //The amount of damage the player should take from drowning, when required.
    private float timeUnderwater;      //The amount of time the player has spent underwater since they last required air
    public bool hasGills = false;

    [Space(20, order = 1)]

    public Rigidbody2D rb;

    //the Monster gameObject
    public PlayerMonster monster;

    //delegate type used for player actions and abilities
    public delegate void Ability();

    //delegates to be used for most player actions
    public AbilityFactory.Ability moveDelegate = null;
    public AbilityFactory.Ability jumpDelegate = null;
    public AbilityFactory.ArmAbility rightAttackDelegate = null;
    public AbilityFactory.ArmAbility leftAttackDelegate = null;
    public AbilityFactory.Ability torsoAbilityDelegate = null;
    public AbilityFactory.Ability headAbilityDelegate = null;

    //used to check what direction the player is facing
    //-1 = left  1 = right
    public int facingDirection;

    //used to perform miscellaneous checks on the player through fixed update
    public AbilityFactory.Ability playerCheckDelegate = null;

    void Awake() {
        if (Instance == null) {
            rb = GetComponent<Rigidbody2D>();
            width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
            height = GetComponent<Collider2D>().bounds.extents.y + 0.5f;
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        InitializePlayer();
        air = 1;
	}

    // Update is called once per frame
    void Update() {

        if (nestCheck != null && nestCheck.tag == "Nest"
            && Input.GetKeyDown(CustomInputManager.Instance.GetInputKey(InputType.Interact))
            && !UIManager.Instance.nestCanvas.activeInHierarchy) {
            UIManager.Instance.ShowNestCanvas();
            if(nestCheck.gameObject.GetComponent<Nest>().isActive == false) {
                nestCheck.gameObject.GetComponent<Nest>().Activate();
            }
        }

        if(Input.GetKeyDown(CustomInputManager.Instance.GetInputKey(InputType.Pause))) {
            UIManager.Instance.PauseGame();
        }

        //Check if the player is underwater, and if they are, update the underwater timer
        if (isUnderwater) {
            timeUnderwater += Time.deltaTime;

            //If they've been undewater long enough with their air above 0, reduce their air meter.
            if(air > 0 && timeUnderwater >= timeBetweenAirLoss) {
                air -= airToLose;
                timeUnderwater -= timeBetweenAirLoss;
                UIManager.Instance.UpdateAirMeter(air, isUnderwater);
            }

            //If the player's air is 0 or less and enough time has passed, damage them
            if(air <= 0 && timeUnderwater >= timeBetwenAirDamage) {
                Debug.Log("Damage the player for " + drownDamage + " damage");
                timeUnderwater -= timeBetwenAirDamage;
            }
        }
    }

    private void FixedUpdate() {
        //performing status checks on the player using whatever 
        //methods the delegate holds (may hold multiple method implementations)
        playerCheckDelegate();

        //moving the player
        if (!inHitStun && !movementLocked)
        {
            moveDelegate();
        }

        //Handling hitstun
        if(inHitStun && hitStunTimer < hitStunCooldown)
        {
            hitStunTimer += Time.deltaTime;
        }
        else if(inHitStun && hitStunTimer >= hitStunCooldown)
        {
            hitStunTimer = 0;
            inHitStun = false;
        }

        //input rightArm attack
        if (Input.GetKeyDown(CustomInputManager.Instance.GetInputKey(InputType.Primary)) && CheckCooldown("rightAttack")) {
            rightAttackDelegate(Helper.PartType.RightArm);
        }

        //input leftArm attack
        if (Input.GetKeyDown(CustomInputManager.Instance.GetInputKey(InputType.Secondary)) && CheckCooldown("leftAttack")) {
            leftAttackDelegate(Helper.PartType.LeftArm);
        }

        //input torso ability
        if (Input.GetKeyDown(CustomInputManager.Instance.GetInputKey(InputType.Torso)) && CheckCooldown("torsoAbility")) {
            torsoAbilityDelegate();
        }

        //input Head ability
        if (Input.GetKeyDown(CustomInputManager.Instance.GetInputKey(InputType.Head)) && CheckCooldown("headAbility")) {
            headAbilityDelegate();
        }

        //checking to see whether the extra jump should be refreshed
        if (PlayerIsOnGround()) {
            hasExtraJump = true;
        }

        //input jump
        if (Input.GetKeyDown(CustomInputManager.Instance.GetInputKey(InputType.Jump))) {
            jumpDelegate();
        }
    }

    public void Move() {
        //input Left
        if (Input.GetKey(CustomInputManager.Instance.GetInputKey(InputType.Right))) {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        //input Right
        } else if (Input.GetKey(CustomInputManager.Instance.GetInputKey(InputType.Left))) {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        //no input
        } else if (Input.GetKeyUp(CustomInputManager.Instance.GetInputKey(InputType.Left)) || Input.GetKeyUp(CustomInputManager.Instance.GetInputKey(InputType.Right))) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    //makes the player jump
    public void Jump() {
        if (PlayerIsOnGround()) {
            //calling the jump animation
            animator.Play("Jump" + Helper.GetAnimDirection(facingDirection) + "Anim");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    //right arm attack
    public void RightAttack(string armType) {
        Ray attackRay = new Ray();
        attackRay.origin = transform.position;
        attackRay.direction = new Vector2(facingDirection, 0);

        Debug.DrawRay(attackRay.origin, new Vector2(1.7f * facingDirection, 0), Color.green);

        //using the armType and Helper method to call the correct anim
        animator.Play(armType + Helper.GetAnimDirection(facingDirection, armType) + "MeleeAnim");

        RaycastHit2D hit = Physics2D.Raycast(attackRay.origin, attackRay.direction, 1.7f, 1 << LayerMask.NameToLayer("Enemy"));
        if(hit) {
            Enemy enemy = hit.transform.GetComponentInParent<Enemy>();
            if(enemy != null && hit.collider == enemy.hurtBox) {
                enemy.TakeDamage(rightAttackPower, Helper.GetKnockBackDirection(transform, hit.transform));
            }
        }
    }

    //left arm attack
    public void LeftAttack(string armType) {
        Ray attackRay = new Ray();
        attackRay.origin = transform.position;
        attackRay.direction = new Vector2(facingDirection, 0);

        Debug.DrawRay(attackRay.origin, new Vector2(1.7f * facingDirection, 0), Color.green);

        animator.Play(armType + Helper.GetAnimDirection(facingDirection, armType) + "MeleeAnim");

        RaycastHit2D hit = Physics2D.Raycast(attackRay.origin, attackRay.direction, 1.7f, 1 << LayerMask.NameToLayer("Enemy"));
        if (hit) {
            Enemy enemy = hit.transform.GetComponentInParent<Enemy>();
            if (enemy != null && hit.collider == enemy.hurtBox) {
                enemy.TakeDamage(leftAttackPower, Helper.GetKnockBackDirection(transform, hit.transform));
            }
        }
    }

    //the default ability method (default is to have no ability so it is meant to be empty)
    public void AbilityDefault() {}

    public void TakeDamage(int damage, float knockBackDirection)
    {
        if (!inHitStun)
        {
            animator.Play("KnockBack" + Helper.GetAnimDirection(facingDirection) + "Anim");
            rb.velocity = new Vector2(-10 * knockBackDirection, 30);
            health -= damage;
            inHitStun = true;
        }
    }

    [Space(20, order = 1)]
    //these are for easy initialization of the monster and are for testing purposes
    public string head;
    public string torso;
    public string rightArm;
    public string leftArm;
    public string legs;

    public string rightWeapon;
    public string leftWeapon;

    public void InitializePlayer() {
        //creating variables to initialize the player monster
        //this code is for testing purposes, final product will pull this information from the database scripts
        var headInfo = PartFactory.GetHeadPartInfo(head);
        var torsoInfo = PartFactory.GetTorsoPartInfo(torso);
        var rightArmInfo = PartFactory.GetArmPartInfo(rightArm, Helper.PartType.RightArm);
        var leftArmInfo = PartFactory.GetArmPartInfo(leftArm, Helper.PartType.LeftArm);
        var legPartInfo = PartFactory.GetLegPartInfo(legs);
        rightArmInfo.equippedWeapon = rightWeapon;
        leftArmInfo.equippedWeapon = leftWeapon;

        moveDelegate = Move;
        jumpDelegate = Jump;
        rightAttackDelegate = RightAttack;
        leftAttackDelegate = LeftAttack;
        torsoAbilityDelegate = AbilityDefault;
        headAbilityDelegate = AbilityDefault;

        playerCheckDelegate += UpdatePlayerDirection;
        playerCheckDelegate += UpdatePlayerInputCooldowns;
        playerCheckDelegate += CheckHitBox;

        monster.InitializeMonster(headInfo, torsoInfo, rightArmInfo, leftArmInfo, legPartInfo);

        SetPlayerCooldowns();
        //setting the cooldown timers so that the player can use the inputs as soon as the game loads
        rightAttackTimer = RightAttackCooldown;
        leftAttackTimer = LeftAttackCooldown;
        headAbilityTimer = HeadAbilityCooldown;
        torsoAbilityTimer = TorsoAbilityCooldown;
        legAbilityTimer = LegAbilityCooldown;
    }

    public void ShowAttackFace()
    {
        monster.headPart.face.sprite = monster.headPart.attackFaceSprite;
    }

    public void ShowIdleFace()
    {
        monster.headPart.face.sprite = monster.headPart.idleFaceSprite;
    }

    public void ShowHurtFace()
    {
        monster.headPart.face.sprite = monster.headPart.hurtFaceSprite;
    }

    public void CheckHitBox()
    {
        if(hitCounter == totalHits)
        {
            hitBoxDamage = 0;
        }
    }

    //checks to see what direction the player should be facing based on the mouse position
    public void UpdatePlayerDirection() {
        var screenMiddle = Screen.width / 2;
        if (Input.mousePosition.x > screenMiddle) {
            facingDirection = 1;
            monster.ChangeDirection(facingDirection);
        } else if (Input.mousePosition.x < screenMiddle) {
            facingDirection = -1;
            monster.ChangeDirection(facingDirection);
        }
    }

    //used by Initialize player to set all of the input cooldowns
    public void SetPlayerCooldowns()
    {
        if(monster.headPart.partInfo.abilityCooldown != 0)
        {
            HeadAbilityCooldown = monster.headPart.partInfo.abilityCooldown;
        }

        if (monster.torsoPart.partInfo.abilityCooldown != 0)
        {
            TorsoAbilityCooldown = monster.torsoPart.partInfo.abilityCooldown;
        }

        if (monster.legPart.partInfo.abilityCooldown != 0)
        {
            LegAbilityCooldown = monster.legPart.partInfo.abilityCooldown;
        }

        if (monster.rightArmPart.partInfo.abilityCooldown != 0 && monster.rightArmPart.weapon == null)
        {
            RightAttackCooldown = monster.rightArmPart.partInfo.abilityCooldown;
        }

        if (monster.leftArmPart.partInfo.abilityCooldown != 0 && monster.rightArmPart.weapon == null)
        {
            LeftAttackCooldown = monster.leftArmPart.partInfo.abilityCooldown;
        }
    }

    //Updates the player's input cooldowns
    public void UpdatePlayerInputCooldowns()
    {
        //Right Attack Cooldown
        if(rightAttackTimer < RightAttackCooldown)
        {
            rightAttackTimer += Time.deltaTime;
        }

        //Left Attack Cooldown
        if (leftAttackTimer < LeftAttackCooldown)
        {
            leftAttackTimer += Time.deltaTime;
        }

        //Head Ability Cooldown
        if (headAbilityTimer < HeadAbilityCooldown)
        {
            headAbilityTimer += Time.deltaTime;
        }

        //Torso Ability Cooldown
        if (torsoAbilityTimer < TorsoAbilityCooldown)
        {
            torsoAbilityTimer += Time.deltaTime;
        }

        //Leg Ability Cooldown
        if (legAbilityTimer < LegAbilityCooldown)
        {
            legAbilityTimer += Time.deltaTime;
        }
    }

    //Checks the player's input cooldowns
    public bool CheckCooldown(string inputCooldown)
    {
        if (!attacksLocked)
        {
            switch (inputCooldown)
            {
                case "rightAttack":
                    if (rightAttackTimer >= RightAttackCooldown)
                    {
                        //Debug.Log(monster.rightArmPart.partInfo.abilityCooldown);
                        rightAttackTimer = 0;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "leftAttack":
                    if (leftAttackTimer >= LeftAttackCooldown)
                    {
                        leftAttackTimer = 0;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "headAbility":
                    if (headAbilityTimer >= HeadAbilityCooldown)
                    {
                        headAbilityTimer = 0;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "torsoAbility":
                    if (torsoAbilityTimer >= TorsoAbilityCooldown)
                    {
                        torsoAbilityTimer = 0;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "legAbility":
                    if (legAbilityTimer >= LegAbilityCooldown)
                    {
                        legAbilityTimer = 0;
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

    //PlayerIsOnGround function taken from SuperSoyBoy game from Ray Wenderlich
    public bool PlayerIsOnGround() {
        bool groundCheck1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - height), -Vector2.down, rayCastLengthCheck, 1 << LayerMask.NameToLayer("Terrain"));
        bool groundCheck2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck, 1 << LayerMask.NameToLayer("Terrain"));
        bool groundCheck3 = Physics2D.Raycast(new Vector2(transform.position.x - (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck, 1 << LayerMask.NameToLayer("Terrain"));


        bool waterCheck1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - height), -Vector2.down, rayCastLengthCheck, 1 << LayerMask.NameToLayer("Water"));
        bool waterCheck2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck, 1 << LayerMask.NameToLayer("Water"));
        bool waterCheck3 = Physics2D.Raycast(new Vector2(transform.position.x - (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck, 1 << LayerMask.NameToLayer("Water"));
        if (groundCheck1 || groundCheck2 || groundCheck3 || waterCheck1 || waterCheck2 || waterCheck3) {
            return true;
        } else {
            return false;
        }
    }

    //Runs when the object enters the hitbox of another object
    private void OnTriggerEnter2D(Collider2D collision) {
        //If the tag on the object is "Water", the player is underwater.
        if(collision.tag == "Water") {
            isUnderwater = true;
        }

        //If the tag on the object is "Bubble", restore the player's air meter
        if (collision.tag == "Bubble") {
            air = 1;
            timeUnderwater = 0;
            UIManager.Instance.UpdateAirMeter(air, isUnderwater);
        }

        if(collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (hitBox != null)
                {
                    if (hitBox.IsTouching(enemy.hurtBox))
                    {
                        enemy.TakeDamage(hitBoxDamage, Helper.GetKnockBackDirection(transform, collision.transform));
                        hitCounter += 1;
                    }
                }
            }
        }
    }

    //Runs when the object exits the hitbox of another object
    private void OnTriggerExit2D(Collider2D collision) {
        //If the tag on the object is "Water", the player has exited the water. Reset all water related values.
        if(collision.tag == "Water") {
            isUnderwater = false;
            timeUnderwater = 0;
            air = 1;
            UIManager.Instance.UpdateAirMeter(air, isUnderwater);
        }

        //If the tag is "Nest", the player just left the nest's hitbox
        if(collision.tag == "Nest") {
            nestCheck = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.tag == "Nest") {
            nestCheck = collision;
        }
    }
}
