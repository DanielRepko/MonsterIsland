using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueFlick : MonoBehaviour {

    public int damage = 1;
    public bool destroy;

    public string target;

    private void FixedUpdate()
    {
        transform.position = new Vector2(PlayerController.Instance.monster.headPart.transform.position.x + 0.3f * PlayerController.Instance.facingDirection, PlayerController.Instance.monster.headPart.transform.position.y + 0.05f); ;

        if (destroy)
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
                    enemy.TakeDamage(damage, Helper.GetKnockBackDirection(transform, collision.transform));
                    Destroy(gameObject);
                }
            }
        }
        else if(target == "Player")
        {
            if (collision.tag == "Player")
            {
                if (collision == PlayerController.Instance.hurtBox)
                {
                    PlayerController.Instance.TakeDamage(damage, Helper.GetKnockBackDirection(transform, collision.transform));
                    Destroy(gameObject);
                }
            }
        }
    }
}
