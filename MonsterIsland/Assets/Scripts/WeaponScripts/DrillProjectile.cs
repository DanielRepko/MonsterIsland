using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillProjectile : Projectile {

    private bool hasGoneThroughWall;
    private bool hasBeenSlowed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            if (hasGoneThroughWall)
            {
                Destroy(gameObject);
            }
            else if(!hasBeenSlowed)
            {
                GetComponent<Rigidbody2D>().velocity /= 3;
                hasBeenSlowed = true;
            }
        }
        if (target == "Enemy")
        {
            if (collision.tag == "Enemy")
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null && collision == enemy.hurtBox)
                {
                    enemy.TakeDamage(damage, Helper.GetKnockBackDirection(transform, collision.transform));
                }
            }
        }
        else if (target == "Player")
        {
            if (collision.tag == "Player")
            {
                if (collision == PlayerController.Instance.hurtBox)
                {
                    PlayerController.Instance.TakeDamage(damage, Helper.GetKnockBackDirection(transform, collision.transform));
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            hasGoneThroughWall = true;
            GetComponent<Rigidbody2D>().velocity *= 3;
        }
    }

    private void FixedUpdate()
    {
        CheckOffScreenStatus();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
