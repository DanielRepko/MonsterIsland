using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wingus : Enemy {

    private float swoopDaWoopTimer;
    private float swoopDaWoopTime = 0.5f;
    private bool runningTimer;

    public override void InitializeEnemy()
    {
        base.InitializeEnemy();
        checkDelegate += UseAbility;
    }

    public override void Jump()
    {
        base.Jump();
        runningTimer = true;
    }

    public override void Ability()
    {
        rb.velocity = new Vector2(17f * facingDirection, 20);
        animator.Play("SwoopDaWoop" + Helper.GetAnimDirection(facingDirection) + "Anim_Aerial");
    }

    public void UseAbility()
    {
        if(runningTimer && swoopDaWoopTimer < swoopDaWoopTime)
        {
            swoopDaWoopTimer += Time.deltaTime;
        }
        else if(runningTimer && swoopDaWoopTimer >= swoopDaWoopTime)
        {
            abilityDelegate();
            runningTimer = false;
            swoopDaWoopTimer = 0;
        }
    }
}
