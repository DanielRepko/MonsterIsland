using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : Projectile {

    float timeTillReturn = 0.8f;
    float throwTimer = 0;

    bool returning;

    // Use this for initialization
    void Start()
    {
        weaponRenderer.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        //timer for when to start returning
        if(!returning && throwTimer < timeTillReturn)
        {
            throwTimer += Time.deltaTime;
        }
        else if(!returning && throwTimer >= timeTillReturn)
        {
            returning = true;
        }

        //boomerang is returning to player
        if (returning)
        {
            Vector2 destination = (PlayerController.Instance.transform.position - transform.position).normalized * speed;
            GetComponent<Rigidbody2D>().velocity = destination;
        }

        UpdateCooldown(false);

    }

    private void UpdateCooldown(bool destroyed)
    {
        //Weapon weapon = weaponRenderer.GetComponentInParent<Weapon>();

        if (target == "Enemy")
        {
            if (weaponRenderer.GetComponentInParent<ArmPart>().partType == Helper.PartType.RightArm)
            {
                if (!destroyed)
                {
                    PlayerController.Instance.RightAttackCooldown += 1;
                }
                else
                {
                    PlayerController.Instance.RightAttackCooldown = 1.5f;
                }
            }
            else if (weaponRenderer.GetComponentInParent<ArmPart>().partType == Helper.PartType.LeftArm)
            {
                if (!destroyed)
                {
                    PlayerController.Instance.LeftAttackCooldown += 1;
                }
                else
                {
                    PlayerController.Instance.LeftAttackCooldown = 1.5f;
                }
            }
        }
        else if (target == "Player")
        {
            //add code to adjust enemy cooldowns
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            weaponRenderer.gameObject.SetActive(true);
            UpdateCooldown(true);
            Destroy(gameObject);
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
            else if (collision.tag == "Player" && returning)
            {
                weaponRenderer.gameObject.SetActive(true);
                UpdateCooldown(true);
                Destroy(gameObject);
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
    private void OnBecameInvisible()
    {

    }
}
