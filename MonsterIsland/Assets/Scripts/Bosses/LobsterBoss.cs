using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterBoss : Boss {

    public override void RightAttack(string armType)
    {
        animator.Play("PincerPistol_" + armType + "_" + Helper.GetAnimDirection(facingDirection, armType) + "_Anim");

        Debug.DrawRay(monster.rightArmPart.bicep.transform.position, new Vector2(5f, 0), Color.green);

        Ray pincerRay = new Ray();
        if (armType == Helper.PartType.RightArm)
        {
            pincerRay.origin = monster.rightArmPart.bicep.transform.position;
        }
        else if (armType == Helper.PartType.LeftArm)
        {
            pincerRay.origin = monster.leftArmPart.bicep.transform.position;
        }
        pincerRay.direction = new Vector2(facingDirection, 0);

        RaycastHit2D hit = Physics2D.Raycast(pincerRay.origin, pincerRay.direction, 5f, 1 << LayerMask.NameToLayer("Player"));
        if (hit)
        {
            if (hit.collider == PlayerController.Instance.hurtBox)
            {
                PlayerController.Instance.TakeDamage(damage, Helper.GetKnockBackDirection(transform, hit.transform));
            }
        }
    }

    public override void LeftAttack(string armType)
    {
        animator.Play("PincerPistol_" + armType + "_" + Helper.GetAnimDirection(facingDirection, armType) + "_Anim");

        Debug.DrawRay(monster.rightArmPart.bicep.transform.position, new Vector2(5f, 0), Color.green);

        Ray pincerRay = new Ray();
        if (armType == Helper.PartType.RightArm)
        {
            pincerRay.origin = monster.rightArmPart.bicep.transform.position;
        }
        else if (armType == Helper.PartType.LeftArm)
        {
            pincerRay.origin = monster.leftArmPart.bicep.transform.position;
        }
        pincerRay.direction = new Vector2(facingDirection, 0);

        RaycastHit2D hit = Physics2D.Raycast(pincerRay.origin, pincerRay.direction, 5f, 1 << LayerMask.NameToLayer("Player"));
        if (hit)
        {
            if (hit.collider == PlayerController.Instance.hurtBox)
            {
                PlayerController.Instance.TakeDamage(damage, Helper.GetKnockBackDirection(transform, hit.transform));
            }
        }
    }
}
