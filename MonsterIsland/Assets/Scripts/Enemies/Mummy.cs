using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : Enemy {


    public override void InitializeEnemy()
    {
        base.InitializeEnemy();
        checkDelegate += CoughAcid;
    }

    public void CoughAcid()
    {
        if(CheckCooldown("ability") && isAggro && !attacksLocked && !inHitStun)
        {
            abilityDelegate();
        }
    }

    public override void Ability()
    {
        GameObject acidCloudLoad = Resources.Load<GameObject>("Prefabs/Projectiles/AcidCloud");
        acidCloudLoad.GetComponent<AcidBreath>().target = "Player";

        Vector2 acidCloudPosition = new Vector2(transform.position.x + 2 * facingDirection, transform.position.y);

        animator.Play("HeadAbilityAnim");

        Instantiate(acidCloudLoad, acidCloudPosition, Quaternion.identity);
    }
}
