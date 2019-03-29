using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonProjectile : Projectile {

    public static HarpoonProjectile lastHarpoon;

    private void Start()
    {
        if(lastHarpoon != null)
        {
            Destroy(lastHarpoon.gameObject);
            lastHarpoon = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            gameObject.AddComponent<FixedJoint2D>();
            GetComponent<BoxCollider2D>().isTrigger = false;
            lastHarpoon = this;
        }

        if (target == "Enemy")
        {
            if (collision.tag == "Enemy")
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null && collision == enemy.hurtBox)
                {
                    enemy.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }
        else if (target == "Player")
        {
            if (collision.tag == "Player")
            {
                if (collision == PlayerController.Instance.hurtBox)
                {
                    PlayerController.Instance.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnBecameInvisible()
    {
        
    }
}
