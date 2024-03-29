﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Enemy {

    private float doubleJumpTimer;
    private float doubleJumpTime = 0.5f;
    private bool runningTimer;

    public override void InitializeEnemy()
    {
        base.InitializeEnemy();
        checkDelegate += UseAbility;
    }

    public override void Jump()
    {
        if (IsOnGround() && CheckCooldown("jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.Play("Jump" + Helper.GetAnimDirection(facingDirection) + "Anim");
            runningTimer = true;
        }
    }

    public override void Ability()
    {
        animator.Play("Jump" + Helper.GetAnimDirection(facingDirection) + "Anim");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void UseAbility()
    {
        if (runningTimer && doubleJumpTimer < doubleJumpTime)
        {
            doubleJumpTimer += Time.deltaTime;
        }
        else if (runningTimer && doubleJumpTimer >= doubleJumpTime)
        {                       
            runningTimer = false;
            doubleJumpTimer = 0;
            abilityDelegate();
        }
    }
}
