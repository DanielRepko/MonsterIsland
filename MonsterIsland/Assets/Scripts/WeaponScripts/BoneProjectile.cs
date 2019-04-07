using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneProjectile : Projectile {

    public float topOfArc;

    // Use this for initialization
    void Start () {
        weaponRenderer.gameObject.SetActive(false);
        topOfArc = transform.position.y + 2.5f;
	}

    private void FixedUpdate()
    {
        if(transform.position.y >= topOfArc && GetComponent<Rigidbody2D>().gravityScale < 0)
        {
            GetComponent<Rigidbody2D>().gravityScale *= -1;
        }

        UpdateCooldown(false);

        CheckOffScreenStatus();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void UpdateCooldown(bool destroyed)
    {
        //Weapon weapon = weaponRenderer.GetComponentInParent<Weapon>();

        if(target == "Enemy")
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
            else if(weaponRenderer.GetComponentInParent<ArmPart>().partType == Helper.PartType.LeftArm)
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
                    weaponRenderer.gameObject.SetActive(true);
                    UpdateCooldown(true);
                    Destroy(gameObject);
                }
                else
                {
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
                    PlayerController.Instance.TakeDamage(damage, Helper.GetKnockBackDirection(transform, collision.transform));
                    weaponRenderer.gameObject.SetActive(true);
                    UpdateCooldown(true);
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
