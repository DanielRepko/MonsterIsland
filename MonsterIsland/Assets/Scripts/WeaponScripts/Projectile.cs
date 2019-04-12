using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public SpriteRenderer weaponRenderer;
    public int damage;
    public int speed;
    public string target;

    protected float unsetTriggerTime = 0.05f;
    protected float unsetTriggerTimer = 0;

    protected float offScreenTime = 2;
    protected float offScreenTimer;
    protected bool isOffScreen;

    private void FixedUpdate()
    {
        if(unsetTriggerTimer < unsetTriggerTime)
        {
            unsetTriggerTimer += Time.deltaTime;
        }
        else if(unsetTriggerTimer >= unsetTriggerTime && GetComponent<BoxCollider2D>().isTrigger)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
        
    }

    public void CheckOffScreenStatus()
    {
        if (isOffScreen)
        {
            if (offScreenTimer < offScreenTime)
            {
                offScreenTimer += Time.deltaTime;
            }
            else if (offScreenTimer >= offScreenTime)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            offScreenTimer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if (target == "Enemy")
        {
            if (collision.collider.tag == "Enemy")
            {
                Enemy enemy = collision.collider.GetComponent<Enemy>();
                if (enemy != null && collision.collider == enemy.hurtBox)
                {
                    enemy.TakeDamage(damage, Helper.GetKnockBackDirection(transform, collision.transform));
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
            if (collision.collider.tag == "Player")
            {
                if (collision.collider == PlayerController.Instance.hurtBox)
                {
                    PlayerController.Instance.TakeDamage(damage, Helper.GetKnockBackDirection(transform, collision.transform));
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnBecameVisible()
    {
        isOffScreen = false;
    }

    private void OnBecameInvisible()
    {
        isOffScreen = true;
    }
}
