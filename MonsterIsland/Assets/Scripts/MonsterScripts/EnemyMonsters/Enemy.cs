using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    float health = 10;
    public Text text;
    public Collider2D hurtBox;
    public string monsterName;
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
		if(health <= 0) {
            KillEnemy();
        }
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
        if (collision == PlayerController.Instance.hurtBox)
        {
            if (!inHitStun)
            {
                PlayerController.Instance.TakeDamage(1);
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == PlayerController.Instance.hurtBox)
        {
            if (!inHitStun)
            {
                PlayerController.Instance.TakeDamage(1);
            }
        }
    }

    private void KillEnemy() {
        int coinChance = Random.Range(0, 10) + 1;
        int partChance = Random.Range(0, 10) + 1;

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
            if(partToGet == 1) {
                //Head
            } else if (partToGet == 2) {
                //Torso
            } else if (partToGet == 3) {
                //Left Arm
            } else if (partToGet == 4) {
                //Right Arm
            } else if (partToGet == 5) {
                //Legs
            }
        }

        Destroy(gameObject);
    }
}
