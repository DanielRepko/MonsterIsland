using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 20f;
    public float jumpForce = 10f;

    //used to add time between when the player can jump
    //prevents the player from holding down jump and skipping
    //through sections of vertical platforming with one-way platforms
    private bool canJump = true;
    public float jumpCooldown = 0.2f;
    private float jumpCooldownTimer = 0;

    private float rayCastLengthCheck = 0.005f;
    private float width;
    private float height;

    private float xInput;
    private float yInput;

    private bool isUnderwater;

    private Rigidbody2D rb;

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

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.5f;
    }

    // Use this for initialization
    void Start () {
        InitializePlayer();
	}

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Jump");
    }

    private void FixedUpdate() {

        if (!canJump && (jumpCooldownTimer < jumpCooldown))
        {
            jumpCooldownTimer += Time.deltaTime;
        } else if(jumpCooldownTimer >= jumpCooldown)
        {
            jumpCooldownTimer = 0;
            canJump = true;
        }

        Move();

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
        if (Input.GetKeyDown("f"))
        {
            torsoAbilityDelegate();
        }

        //input Head ability
        if (Input.GetKeyDown("e"))
        {
            headAbilityDelegate();
        }

        //input jump
        if(yInput >= 1f) {
            jumpDelegate();
        }
    }

    public void Move()
    {
        //input right
        if (xInput > 0f)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        //input left
        }
        else if (xInput < 0f)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        //no input
        }
        else if (xInput == 0f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    //makes the player jump
    public void Jump()
    {
        if (canJump)
        {
            //
            canJump = false;

            if (PlayerIsOnGround())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        
        
    }

    //right arm attack
    public void RightAttack()
    {
        Debug.Log("right arm attack");
    }

    //left arm attack
    public void LeftAttack()
    {
        Debug.Log("left arm attack");
    }

    //the default torso ability (default is to have no ability so it is meant to be empty)
    public void DefaultTorsoAbility()
    {
        Debug.Log("torso ability");
    }

    //the default head ability (default is to have no ability so it is meant to be empty)
    public void DefaultHeadAbility()
    {
        Debug.Log("head ability");
    }

    public void InitializePlayer()
    {
        var headInfo = PartFactory.GetHeadPartInfo(Helper.MonsterName.Mitch);
        var torsoInfo = PartFactory.GetTorsoPartInfo(Helper.MonsterName.Mitch);
        var rightArmInfo = PartFactory.GetArmPartInfo(Helper.MonsterName.Mitch, Helper.PartType.RightArm);
        var leftArmInfo = PartFactory.GetArmPartInfo(Helper.MonsterName.Mitch, Helper.PartType.LeftArm);
        var legPartInfo = PartFactory.GetLegPartInfo(Helper.MonsterName.Mitch);

        moveDelegate = Move;
        jumpDelegate = Jump;
        rightAttackDelegate = RightAttack;
        leftAttackDelegate = LeftAttack;
        torsoAbilityDelegate = DefaultTorsoAbility;

        monster.InitializeMonster(headInfo, torsoInfo, rightArmInfo, leftArmInfo, legPartInfo);
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
}
