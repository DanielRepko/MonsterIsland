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

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.5f;
        isUnderwater = false;
    }

    // Use this for initialization
    void Start () {
        var headInfo = PartFactory.GetHeadPartInfo(Helper.MonsterName.Mitch);
        var torsoInfo = PartFactory.GetTorsoPartInfo(Helper.MonsterName.Mitch);
        var rightArmInfo = PartFactory.GetArmPartInfo(Helper.MonsterName.Mitch, Helper.PartType.RightArm);
        var leftArmInfo = PartFactory.GetArmPartInfo(Helper.MonsterName.Mitch, Helper.PartType.LeftArm);
        var legPartInfo = PartFactory.GetLegPartInfo(Helper.MonsterName.Mitch);

        monster.InitializeMonster(headInfo, torsoInfo, rightArmInfo, leftArmInfo, legPartInfo);
	}
	
	// Update is called once per frame
	void Update () {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Jump");
    }

    private void FixedUpdate() {
        //input left
        if (xInput > 0f) {
            rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
        //input left
        } else if (xInput < 0f) {
            rb.velocity = new Vector2(-playerSpeed, rb.velocity.y);
        //no input
        } else if (xInput == 0f) {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        //input rightArm attack
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("right arm attack");
        }

        //input leftArm attack
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("left arm attack");
        }

        //input torso ability
        if (Input.GetKeyDown("f"))
        {
            Debug.Log("torso ability");
        }

        //input Head ability
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("head ability");
        }

        //input jump
        if (PlayerIsOnGround() && yInput >= 1f) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
}
