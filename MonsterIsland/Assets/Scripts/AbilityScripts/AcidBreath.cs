using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBreath : MonoBehaviour {

    public int damage = 1;

    public float lifeTime = 3.5f;
    private float lifeTimer = 0;

    public float timeTillDamage = 0.6f;
    private float damageTimer = 0;
    private bool readyToDamage = true;

    public string target;

    private void FixedUpdate()
    {
        if(!readyToDamage && damageTimer < timeTillDamage)
        {
            damageTimer += Time.deltaTime;
        }
        else if(!readyToDamage && damageTimer >= timeTillDamage)
        {
            damageTimer = 0;
            readyToDamage = true;
        }

        if (lifeTimer < lifeTime)
        {
            lifeTimer += Time.deltaTime;
        }
        else if (lifeTimer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target == "Enemy")
        {
            if (collision.tag == "Enemy")
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null && collision == enemy.hurtBox)
                {
                    if (readyToDamage)
                    {
                        enemy.TakeDamage(damage);
                        readyToDamage = false;
                    }
                }
            }
        }
        else if (target == "Player")
        {
            if (collision.tag == "Player")
            {
                if (collision == PlayerController.Instance.hurtBox)
                {
                    if (readyToDamage)
                    {
                        PlayerController.Instance.TakeDamage(damage);
                        readyToDamage = false;
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (target == "Enemy")
        {
            if (collision.tag == "Enemy")
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null && collision == enemy.hurtBox)
                {
                    if (readyToDamage)
                    {
                        enemy.TakeDamage(damage);
                        readyToDamage = false;
                    }
                }
            }
        }
        else if (target == "Player")
        {
            if (collision.tag == "Player")
            {
                if (collision == PlayerController.Instance.hurtBox)
                {
                    if (readyToDamage)
                    {
                        PlayerController.Instance.TakeDamage(damage);
                        readyToDamage = false;
                    }
                }
            }
        }
    }
}
