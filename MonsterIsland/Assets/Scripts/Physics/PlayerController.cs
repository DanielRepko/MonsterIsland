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

    private Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.2f;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        if (xInput > 0f) {
            rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
        } else if (xInput < 0f) {
            rb.velocity = new Vector2(-playerSpeed, rb.velocity.y);
        } else if (xInput == 0f) {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if(PlayerIsOnGround() && yInput > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    //PlayerIsOnGround function taken from SuperSoyBoy game from Ray Wenderlich
    public bool PlayerIsOnGround() {
        bool groundCheck1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        bool groundCheck2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        bool groundCheck3 = Physics2D.Raycast(new Vector2(transform.position.x - (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck);

        if (groundCheck1 || groundCheck2 || groundCheck3) {
            return true;
        } else {
            return false;
        }
    }
}
