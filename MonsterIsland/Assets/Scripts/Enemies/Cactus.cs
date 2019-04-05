using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Enemy {

    public override void Attack(string armType = "RightArm")
    {
        //loading the prefab
        GameObject needleLoad = Resources.Load<GameObject>("Prefabs/Projectiles/Cactus_Needle");
        int speed = needleLoad.GetComponent<Projectile>().speed;
        needleLoad.GetComponent<Projectile>().target = "Player";
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
        GameObject upNeedle = Instantiate(needleLoad, needlePosition, Quaternion.Euler(0, 0, 45));
        GameObject middleNeedle = Instantiate(needleLoad, needlePosition, Quaternion.identity);
        GameObject downNeedle = Instantiate(needleLoad, needlePosition, Quaternion.Euler(0, 0, -45));

        //turning the needles in the same direction the player is facing
        upNeedle.transform.localScale = new Vector2(upNeedle.transform.localScale.x * facingDirection, upNeedle.transform.localScale.y);
        middleNeedle.transform.localScale = new Vector2(middleNeedle.transform.localScale.x * facingDirection, upNeedle.transform.localScale.y);
        downNeedle.transform.localScale = new Vector2(downNeedle.transform.localScale.x * facingDirection, upNeedle.transform.localScale.y);


        //playing the shoot animation
        animator.Play(armType + Helper.GetAnimDirection(facingDirection, armType) + "ShootAnim");

        upNeedle.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * facingDirection, speed / 2);
        middleNeedle.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * facingDirection, 0);
        downNeedle.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * facingDirection, -speed / 2);
    }
}
