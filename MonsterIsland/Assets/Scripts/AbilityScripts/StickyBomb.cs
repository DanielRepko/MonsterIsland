using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : MonoBehaviour {

    public bool exploded;
    public bool destroy;
    public Animator animator;
    private Enemy connectedEnemy;

    private void FixedUpdate()
    {
        if (destroy)
        {
            if (connectedEnemy)
            {
                connectedEnemy.TakeDamage(5, Helper.GetKnockBackDirection(transform, connectedEnemy.transform));
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.GetComponent<FixedJoint2D>())
        {
            if (collision.tag == "Enemy" && collision.GetComponent<Enemy>().hurtBox == collision)
            {
                gameObject.AddComponent<FixedJoint2D>().connectedBody = collision.attachedRigidbody;
                connectedEnemy = collision.GetComponent<Enemy>();
                animator.Play("StickyBombAnim");
            }
            else if (collision.tag == "Ground")
            {
                gameObject.AddComponent<FixedJoint2D>();
                animator.Play("StickyBombAnim");
            }
        }
        if (exploded)
        {
            if (collision.tag == "Enemy" && collision.GetComponent<Enemy>().hurtBox == collision)
            {
                collision.GetComponent<Enemy>().TakeDamage(5, Helper.GetKnockBackDirection(transform, collision.transform));
            }
        }
    }
}
