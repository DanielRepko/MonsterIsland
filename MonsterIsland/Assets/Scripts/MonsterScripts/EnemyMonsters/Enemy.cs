using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    float health = 10;
    public Text text;
    public Collider2D hurtBox;
    private float hitStunCooldown = 0.4f;
    private float hitStunTimer = 0;
    private bool inHitStun = false;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        text.text = health.ToString();

        //Handling Hitstun
        if (inHitStun && hitStunTimer == 0)
        {
            rb.velocity = new Vector2(-10 * transform.localScale.x, 30);
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
    }

    //causes the enemy to take damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        inHitStun = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == PlayerController.Instance.hitBox)
        {
            
            TakeDamage(PlayerController.Instance.hitBoxDamage);

        }
        else if (collision == PlayerController.Instance.hurtBox)
        {
            if (!inHitStun)
            {
                PlayerController.Instance.TakeDamage(1);
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == PlayerController.Instance.hitBox)
        {

            TakeDamage(PlayerController.Instance.hitBoxDamage);

        }
        else if (collision == PlayerController.Instance.hurtBox)
        {
            if (!inHitStun)
            {
                PlayerController.Instance.TakeDamage(1);
            }
        }
    }
}
