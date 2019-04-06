using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hingus : Enemy {

    public BoxCollider2D hitBox;

    public override void Attack(string armType = "RightArm")
    {
        rb.velocity = new Vector2(0, 20);

        animator.Play("TalonFlurry" + Helper.GetAnimDirection(facingDirection) + "Anim");
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "Player")
        {
            if (hitBox != null)
            {
                if (hitBox.IsTouching(PlayerController.Instance.hurtBox) && !hitBox.IsTouching(PlayerController.Instance.shellCollider))
                {
                    PlayerController.Instance.TakeDamage(damage, Helper.GetKnockBackDirection(transform, collision.transform));
                }
            }
        }
    }
}
