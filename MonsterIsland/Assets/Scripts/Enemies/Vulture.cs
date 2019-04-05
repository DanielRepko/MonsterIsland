using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulture : Enemy {

    override public void InitializeEnemy()
    {
        base.InitializeEnemy();
        checkDelegate += Ability;
    }

    public override void Ability()
    {
        if (!isUnderwater)
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale -= 2f;
            }
            else
            {
                rb.gravityScale = 20;
            }
        }
        else
        {
            rb.gravityScale = 20;
        }
    }
}
