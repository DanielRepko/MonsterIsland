using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int damage;
    public int speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
        else if(collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if(enemy != null && collision == enemy.hurtBox)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
