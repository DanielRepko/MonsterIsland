using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFactory : MonoBehaviour {

    public delegate void Ability();

    public static Ability GetPartAbility(string abilityName)
    {
        Ability ability = null;

        switch (abilityName)
        {
            case "Laser Eyes":
                ability = Ability_LaserEyes;
                return ability;
            case "Tongue Flick":
                ability = Ability_TongueFlick;
                return ability;
            case "Lion's Roar":
                ability = Ability_LionsRoar;
                return ability;
            case "Acid Breath":
                ability = Ability_AcidBreath;
                return ability;
            case "Big Beak":
                ability = Ability_BigBeak;
                return ability;
            case "Armored Body":
                ability = Ability_ArmoredBody;
                return ability;
            case "Gills":
                ability = Ability_Gills;
                return ability;
            case "Hard Shell":
                ability = Ability_HardShell;
                return ability;
            case "Ghost Walk":
                ability = Ability_GhostWalk;
                return ability;
            case "Swoop da Woop":
                ability = Ability_SwoopDaWoop;
                return ability;
            case "Sticky Bomb":
                ability = Ability_StickyBomb;
                return ability;
            case "Drill Fist":
                ability = Ability_DrillFist;
                return ability;
            case "Strong Arm":
                ability = Ability_StrongArm;
                return ability;
            case "Weapons Training":
                ability = Ability_WeaponsTraining;
                return ability;
            case "Needle Shot":
                ability = Ability_NeedleShot;
                return ability;
            case "Feather Fall":
                ability = Ability_FeatherFall;
                return ability;
            case "Pincer Pistol":
                ability = Ability_PincerPistol;
                return ability;
            case "Bone Toss":
                ability = Ability_BoneToss;
                return ability;
            case "Spiked Feet":
                ability = Ability_SpikedFeet;
                return ability;
            case "Quick Feet":
                ability = Ability_QuickFeet;
                return ability;
            case "Acrobat":
                ability = Ability_Acrobat;
                return ability;
            case "Joey Jump":
                ability = Ability_JoeyJump;
                return ability;
            case "Talon Flurry":
                ability = Ability_TalonFlurry;
                return ability;
            default:
                return null;
        }
    }

    //Head Ability (Activate): Allows the player to shoot a laser beam
    public static void Ability_LaserEyes()
    {

    }

    //Head Ability (Activate): Allows the player to attack with a tongue flick
    public static void Ability_TongueFlick()
    {

    }

    //Head Ability (Activate): Allows the player to knock back enemies with 
    //an AOE roar attack. Does not deal damage
    public static void Ability_LionsRoar()
    {

    }

    //Head Ability (Activate): Allows the player to spit out a cloud of acid 
    //that lingers for several seconds and deals damage over time
    public static void Ability_AcidBreath()
    {

    }

    //Head Ability (Activate): Allows the player to attack with large beak
    public static void Ability_BigBeak()
    {
       
    }

    //Torso Ability (Passive): Grants the player an extra heart of health
    public static void Ability_ArmoredBody()
    {
        GameManager.instance.player.health += 1;
    }

    //Torso Ability (Passive): Allows the player to breath underwater
    public static void Ability_Gills()
    {

    }

    //Torso Ability (Passive): Prevents the player from being damaged by attacks from behind
    public static void Ability_HardShell()
    {

    }

    //Torso Ability (Activate): Allows the player to teleport short distances
    public static void Ability_GhostWalk()
    {
        PlayerController player = GameManager.instance.player;
        
        Ray topRay = new Ray();
        Ray middleRay = new Ray();
        Ray bottomRay = new Ray();

        topRay.origin = new Vector2(player.transform.position.x, player.transform.position.y + 1.4f);
        topRay.direction = new Vector2(player.facingDirection, 0);

        middleRay.origin = new Vector2(player.transform.position.x, player.transform.position.y);
        middleRay.direction = new Vector2(player.facingDirection, 0);

        bottomRay.origin = new Vector2(player.transform.position.x, player.transform.position.y - 1.8f);
        bottomRay.direction = new Vector2(player.facingDirection, 0);

        Debug.DrawRay(topRay.origin, new Vector2(8 * player.facingDirection, 0), Color.green);
        Debug.DrawRay(middleRay.origin, new Vector2(8 * player.facingDirection, 0), Color.green);
        Debug.DrawRay(bottomRay.origin, new Vector2(8 * player.facingDirection, 0), Color.green);

        //these raycasts check to see if there is terrain in the path of teleport
        RaycastHit2D topRayTerrainHit = Physics2D.Raycast(topRay.origin, topRay.direction, 8f, 1 << LayerMask.NameToLayer("Terrain"));
        RaycastHit2D middleRayTerrainHit = Physics2D.Raycast(middleRay.origin, middleRay.direction, 8f, 1 << LayerMask.NameToLayer("Terrain"));
        RaycastHit2D bottomRayTerrainHit = Physics2D.Raycast(bottomRay.origin, bottomRay.direction, 8f, 1 << LayerMask.NameToLayer("Terrain"));

        //these raycasts check to see if there is an enemy in the path of the teleport
        //need to do enemy and terrain separately because 2D raycast does not allow multiple layer mask selections
        RaycastHit2D topRayEnemyHit = Physics2D.Raycast(topRay.origin, topRay.direction, 8f, 1 << LayerMask.NameToLayer("Enemy"));
        RaycastHit2D middleRayEnemyHit = Physics2D.Raycast(middleRay.origin, middleRay.direction, 8f, 1 << LayerMask.NameToLayer("Enemy"));
        RaycastHit2D bottomRayEnemyHit = Physics2D.Raycast(bottomRay.origin, bottomRay.direction, 8f, 1 << LayerMask.NameToLayer("Enemy"));

        RaycastHit2D[] hitList = { topRayTerrainHit, middleRayTerrainHit, bottomRayTerrainHit, topRayEnemyHit, middleRayEnemyHit, bottomRayEnemyHit };

        float shortestDistance = 8;

        for(int i = 0; i < hitList.Length; i++)
        {
            if (hitList[i])
            {
                if (hitList[i].distance < shortestDistance)
                {
                    shortestDistance = hitList[i].distance;
                }
            }
        }

        //shortestDistance - 0.3 to account for the half of the player that will be over the distance threshold
        float teleportDistance = player.transform.position.x + ((shortestDistance - 0.6f) * player.facingDirection);

        //making the player "disappear"
        player.monster.gameObject.SetActive(false);

        //moving the player to the new
        player.transform.position = new Vector2(teleportDistance, player.transform.position.y);

        //making the player "reappear"
        player.monster.gameObject.SetActive(true);
    }

    //Torso Ability (Activate): Allows the player to fly up forwards while in the air, or 
    //fly up backwards while on the ground
    public static void Ability_SwoopDaWoop()
    {

    }

    //Arm Ability (Activate): Lets the player shoot out a bomb that explodes after 
    //a few seconds. Sticks to walls and enemies
    public static void Ability_StickyBomb()
    {

    }

    //Arm Ability (Activate): Allows the player to attack with a drill, deals multiple hits of 
    //damage, all other actions and movement are locked for the ability's duration
    public static void Ability_DrillFist()
    {

    }

    //Arm Ability (Passive): Increases the player's melee damage, does not affect weapon damage
    public static void Ability_StrongArm()
    {

    }

    //Arm Ability (Passive): Increases the player's melee weapon damage, does not affect projectile weapon damage
    public static void Ability_WeaponsTraining()
    {

    }

    //Arm Ability (Activate): Allow the player to shoot needles in three shot bursts
    public static void Ability_NeedleShot()
    {

    }

    //Arm Ability (Passive): Makes the player fall slower, effect can stack with other arm
    //due to nature of the ability, needs to pass on a separate method (see FeatherFall())
    public static void Ability_FeatherFall()
    {
        GameManager.instance.player.playerCheckDelegate += FeatherFall;
    }

    //Arm Ability (Activate): Allows the player to extend and shoot their arm out to 
    //attack enemies at medium range
    public static void Ability_PincerPistol()
    {

    }

    //Arm Ability (Passive): Equips the player with the bone weapon, 
    //takes up and locks this arm's weapon slot
    public static void Ability_BoneToss()
    {

    }

    //Leg Ability (Passive): Allows the player to damage enemies by jumping on them
    public static void Ability_SpikedFeet()
    {

    }

    //Leg Ability (Passive): Increases the player's move speed
    public static void Ability_QuickFeet()
    {
        GameManager.instance.player.moveSpeed = 19;
    }

    //Leg Ability (Activate): Allows the player to perform a single jump in mid-air
    public static void Ability_Acrobat()
    {
        PlayerController player = GameManager.instance.player;

        if (!player.isUnderwater)
        {
            if (player.PlayerIsOnGround())
            {
                player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
            }
            else if (!player.PlayerIsOnGround() && player.hasExtraJump)
            {
                player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
                player.hasExtraJump = false;
            }
        }
    }

    //Leg Ability (Passive): Increases the player's jump height
    public static void Ability_JoeyJump()
    {
        if (!GameManager.instance.player.isUnderwater)
        {
            GameManager.instance.player.jumpForce = 70;
        }
    }

    //Leg Ability (Activate): Allows the player to attack with taloned feet
    public static void Ability_TalonFlurry()
    {

    }    


    //The below methods are for miscellaneous use by the actual ability methods

    public static void FeatherFall()
    {
        PlayerController player = GameManager.instance.player;

        if (!player.isUnderwater)
        {
            if (player.rb.velocity.y < 0)
            {
                player.rb.gravityScale -= 2f;
            }
            else
            {
                player.rb.gravityScale = 20;
            }
        }
        else 
        {
            GameManager.instance.player.rb.gravityScale = 20;
        }
    }
}
