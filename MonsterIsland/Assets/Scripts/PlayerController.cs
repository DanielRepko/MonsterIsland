using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 15.5f;
    public float jumpForce = 10f;

    public float health;

    public bool hasExtraJump = true;

    private float rayCastLengthCheck = 0.005f;
    private float width;
    private float height;

    [Header("Underwater Properties", order = 0)]
    //Values used in the underwater level
    public bool isUnderwater;           //If the user is underwater or not
    [Range(0.00f, 1.00f)]
    public float air;                   //The amount of air the player current has. Min 0, max 1
    public float timeBetweenAirLoss;    //The amount of time between loss in air percentage, in seconds.
    [Range(0.01f, 1.00f)]
    public float airToLose;            //The amount of air to lose when required. Min 0.01, max 1
    public float timeBetwenAirDamage;  //The amount of time between damage from having no air, in seconds.
    public float drownDamage;          //The amount of damage the player should take from drowning, when required.
    private float timeUnderwater;      //The amount of time the player has spent underwater since they last required air

    [Space(20, order = 1)]

    public Rigidbody2D rb;

    //the Monster gameObject
    public PlayerMonster monster;

    //delegate type used for all of the player actions and abilities
    public delegate void Ability();

    public AbilityFactory.Ability moveDelegate = null;
    public AbilityFactory.Ability jumpDelegate = null;
    public AbilityFactory.Ability rightAttackDelegate = null;
    public AbilityFactory.Ability leftAttackDelegate = null;
    public AbilityFactory.Ability torsoAbilityDelegate = null;
    public AbilityFactory.Ability headAbilityDelegate = null;

    //used to check what direction the player is facing
    //-1 = left  1 = right
    public int playerDirection;

    //used to perform miscellaneous checks on the player through fixed update
    public AbilityFactory.Ability playerCheckDelegate = null;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.5f;
    }

    // Use this for initialization
    void Start () {
        InitializePlayer();
        air = 1;
	}

    // Update is called once per frame
    void Update() {
        //Check if the player is underwater, and if they are, update the underwater timer
        if(isUnderwater) {
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

    private void FixedUpdate()
    {
        //performing status checks on the player using whatever 
        //methods the delegate holds (may hold multiple method implementations)
        playerCheckDelegate();

        //moving the player
        moveDelegate();

        //input rightArm attack
        if (Input.GetMouseButtonDown(0))
        {
            rightAttackDelegate();
        }

        //input leftArm attack
        if (Input.GetMouseButtonDown(1))
        {
            leftAttackDelegate();
        }

        //input torso ability
        if (Input.GetKeyDown(KeyCode.F))
        {
            torsoAbilityDelegate();
        }

        //input Head ability
        if (Input.GetKeyDown(KeyCode.E))
        {
            headAbilityDelegate();
        }

        //checking to see whether the extra jump should be refreshed
        if (PlayerIsOnGround())
        {
            hasExtraJump = true;
        }

        //input jump
        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpDelegate();
        }
    }

    public void Move()
    {
        //input Left
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        //input Right
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        //no input
        }
        else if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    //makes the player jump
    public void Jump()
    {
        if (PlayerIsOnGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //rb.AddForce(new Vector2(rb.velocity.x, jumpForce * 10000));
        }
    }

    //right arm attack
    public void RightAttack()
    {
        var playerPosition = transform.position;

        Debug.DrawRay(playerPosition, new Vector3(1.65f * playerDirection, 0, 0), Color.green);
    }

    //left arm attack
    public void LeftAttack()
    {
        var playerPosition = transform.position;

        Debug.DrawRay(playerPosition, new Vector3(1.65f * playerDirection, 0, 0), Color.green);
    }

    //the default ability method (default is to have no ability so it is meant to be empty)
    public void AbilityDefault()
    {

    }

    public void InitializePlayer()
    {
        //creating variables to initialize the player monster
        //this code is for testing purposes, final product will pull this information from the database scripts
        var headInfo = PartFactory.GetHeadPartInfo(Helper.MonsterName.Mitch);
        var torsoInfo = PartFactory.GetTorsoPartInfo(Helper.MonsterName.Mitch);
        var rightArmInfo = PartFactory.GetArmPartInfo(Helper.MonsterName.Mitch, Helper.PartType.RightArm);
        var leftArmInfo = PartFactory.GetArmPartInfo(Helper.MonsterName.Mitch, Helper.PartType.LeftArm);
        var legPartInfo = PartFactory.GetLegPartInfo(Helper.MonsterName.Mitch);

        moveDelegate = Move;
        jumpDelegate = Jump;
        rightAttackDelegate = RightAttack;
        leftAttackDelegate = LeftAttack;
        torsoAbilityDelegate = AbilityDefault;
        headAbilityDelegate = AbilityDefault;
        playerCheckDelegate+=UpdatePlayerDirection;

        monster.InitializeMonster(headInfo, torsoInfo, rightArmInfo, leftArmInfo, legPartInfo);
    }

    //checks to see what direction the player should be facing based on the mouse position
    public void UpdatePlayerDirection()
    {
        var screenMiddle = Screen.width / 2;
        if (Input.mousePosition.x > screenMiddle)
        {
            playerDirection = 1;
            monster.ChangeDirection(playerDirection);
        }
        else if (Input.mousePosition.x < screenMiddle)
        {
            playerDirection = -1;
            monster.ChangeDirection(playerDirection);
        }
    }

    //PlayerIsOnGround function taken from SuperSoyBoy game from Ray Wenderlich
    public bool PlayerIsOnGround() {
        bool groundCheck1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - height), -Vector2.down, rayCastLengthCheck);
        bool groundCheck2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        bool groundCheck3 = Physics2D.Raycast(new Vector2(transform.position.x - (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        if (groundCheck1 || groundCheck2 || groundCheck3) {
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
    }
}
