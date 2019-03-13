using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float playerSpeed = 20f;
    public float jumpForce = 10f;

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

    public Ability moveRightDelegate = null;
    public Ability moveLeftDelegate = null;
    public Ability jumpDelegate = null;
    public Ability rightAttackDelegate = null;
    public Ability leftAttackDelegate = null;
    public Ability torsoAbilityDelegate = null;
    public Ability headAbilityDelegate = null;

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
        //input right
        if (xInput > 0f) {
            moveRightDelegate();
        //input left
        } else if (xInput < 0f) {
            moveLeftDelegate();
        //no input
        } else if (xInput == 0f) {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

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

    //moves the player right
    public void MoveRight()
    {
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
    }

    //moves the player left
    public void MoveLeft()
    {
        rb.velocity = new Vector2(-playerSpeed, rb.velocity.y);
    }

    //makes the player jump
    public void Jump()
    {
        if (PlayerIsOnGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
    public void TorsoAbility()
    {
        Debug.Log("torso ability");
    }

    //the default head ability (default is to have no ability so it is meant to be empty)
    public void HeadAbility()
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

    moveRightDelegate = MoveRight;
    moveLeftDelegate = MoveLeft;
    jumpDelegate = Jump;
    rightAttackDelegate = RightAttack;
    leftAttackDelegate = LeftAttack;
    torsoAbilityDelegate = TorsoAbility;
    headAbilityDelegate = HeadAbility;

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
