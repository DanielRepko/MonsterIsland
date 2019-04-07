using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dingus : Enemy {


    public override void Attack(string armType = "RightArm")
    {
        Ray beakRay = new Ray();
        beakRay.origin = new Vector2(transform.position.x, transform.position.y + 1f);
        beakRay.direction = new Vector3(facingDirection, 0, 0);

        Debug.DrawRay(beakRay.origin, new Vector2(1.7f * facingDirection, 0), Color.green);

        //playing the animation
        animator.Play("HeadAbilityAnim");

        RaycastHit2D hit = Physics2D.Raycast(beakRay.origin, beakRay.direction, 1.7f, 1 << LayerMask.NameToLayer("Player"));
        if (hit)
        {
            if (hit.collider == PlayerController.Instance.hurtBox)
            {
                PlayerController.Instance.TakeDamage(damage, Helper.GetKnockBackDirection(transform, hit.transform));
            }
        }
    }
}
