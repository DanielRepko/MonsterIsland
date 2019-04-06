using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy {

    public override void Attack(string armType = "RightArm")
    {
        GameObject tongueLoad = Resources.Load<GameObject>("Prefabs/Projectiles/Frog_Tongue");
        tongueLoad.GetComponent<TongueFlick>().target = "Player";

        Vector2 tonguePosition = new Vector2(monster.headPart.transform.position.x + 0.3f * facingDirection, monster.headPart.transform.position.y + 0.05f);
        //Debug.Log(tongueLoad.transform.localScale);
        //tongueLoad.transform.localScale *= player.facingDirection;
        //Debug.Log(tongueLoad.transform.localScale);
        GameObject tongue = Instantiate(tongueLoad, tonguePosition, Quaternion.identity);
        tongue.transform.localScale *= facingDirection;

        animator.Play("HeadAbilityAnim");
        tongue.GetComponent<Animator>().Play("TongueFlickAnim");
    }
}
