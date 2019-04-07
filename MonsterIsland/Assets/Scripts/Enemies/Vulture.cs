using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulture : Enemy {

    override public void InitializeEnemy()
    {
        base.InitializeEnemy();
        checkDelegate += Ability;
    }

    override public void Ability()
    {
        if (!isUnderwater)
        {
            
            if (rb.velocity.y < 0)
            {
                rb.gravityScale -= 16f;
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
