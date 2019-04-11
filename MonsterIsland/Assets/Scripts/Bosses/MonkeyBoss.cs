using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyBoss : Boss {

    private float jumpTimer;
    private float jumpTime;
    private float doubleJumpTimer;
    private float doubleJumpTime = 0.5f;
    private bool runningTimer;

    public override void InitializeEnemy()
    {
        base.InitializeEnemy();
        checkDelegate += UseAbility;
        jumpTime = Random.Range(0, 3) + 1;
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

    public void UseAbility()
    {

        if (IsOnGround() && jumpTimer < jumpTime)
        {
            jumpTimer += Time.deltaTime;
        }
        else if (jumpTimer >= jumpTime && IsOnGround())
        {
            jumpTimer = 0;
            jumpTime = Random.Range(0, 3) + 1;
            Jump();
        }

        if (runningTimer && doubleJumpTimer < doubleJumpTime)
        {
            doubleJumpTimer += Time.deltaTime;
        }
        else if (runningTimer && doubleJumpTimer >= doubleJumpTime)
        {
            runningTimer = false;
            doubleJumpTimer = 0;
            animator.Play("Jump" + Helper.GetAnimDirection(facingDirection) + "Anim");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
