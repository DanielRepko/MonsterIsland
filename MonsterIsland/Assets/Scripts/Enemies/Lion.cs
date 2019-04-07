using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : Enemy {

    public override void Ability()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.Play("LionsRoar" + Helper.GetAnimDirection(facingDirection) + "Anim");
    }

    public override void TakeDamage(int damage, float knockBackDirection)
    {
        if (!inHitStun)
        {
            isAggro = true;
            SetFacingDirection(knockBackDirection);
            //Enemy was hit from behind
            if (knockBackDirection == facingDirection)
            {
                health -= damage;
                abilityDelegate();
            }
            else
            {
                animator.Play("KnockBack" + Helper.GetAnimDirection(facingDirection) + "Anim");
                health -= damage;
                inHitStun = true;
            }
        }
    }
}
