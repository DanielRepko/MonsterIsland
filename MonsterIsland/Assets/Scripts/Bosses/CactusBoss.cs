using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusBoss : Boss {

    public override void RightAttack(string armType)
    {
        base.RightAttack(armType);
        PlayerController player = PlayerController.Instance;
        player.rb.AddForceAtPosition(new Vector2(5000 * facingDirection, 4000), new Vector2(player.transform.position.x + (player.width / 2 * facingDirection), player.transform.position.y - player.height / 4));
        
    }

    public override void LeftAttack(string armType)
    {
        //loading the prefab
        GameObject needleLoad = Resources.Load<GameObject>("Prefabs/Projectiles/Cactus_Needle");
        int speed = needleLoad.GetComponent<Projectile>().speed;
        needleLoad.GetComponent<Projectile>().target = "Player";
        needleLoad.GetComponent<Projectile>().damage = leftAttackDamage;
        Vector2 needlePosition = new Vector2();

        //determining which hand to spawn the needles at
        if (armType == Helper.PartType.RightArm)
        {
            needlePosition = monster.rightArmPart.hand.transform.position;
        }
        else if (armType == Helper.PartType.LeftArm)
        {
            needlePosition = monster.leftArmPart.hand.transform.position;
        }

        //instatiating each needle with its own rotation
        GameObject upNeedle = Instantiate(needleLoad, needlePosition, Quaternion.Euler(0, 0, 45 * facingDirection));
        GameObject middleNeedle = Instantiate(needleLoad, needlePosition, Quaternion.identity);
        GameObject downNeedle = Instantiate(needleLoad, needlePosition, Quaternion.Euler(0, 0, -45 * facingDirection));

        //turning the needles in the same direction the player is facing
        upNeedle.transform.localScale = new Vector2(upNeedle.transform.localScale.x * facingDirection, upNeedle.transform.localScale.y);
        middleNeedle.transform.localScale = new Vector2(middleNeedle.transform.localScale.x * facingDirection, upNeedle.transform.localScale.y);
        downNeedle.transform.localScale = new Vector2(downNeedle.transform.localScale.x * facingDirection, upNeedle.transform.localScale.y);


        //playing the shoot animation
        animator.Play(armType + Helper.GetAnimDirection(facingDirection, armType) + "ShootAnim");

        upNeedle.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * facingDirection, speed / 2);
        middleNeedle.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * facingDirection, 0);
        downNeedle.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * facingDirection, -speed / 2);

        SetNextAttack();
    }
}
