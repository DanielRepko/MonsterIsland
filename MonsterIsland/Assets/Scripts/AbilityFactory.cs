using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFactory : MonoBehaviour {

    public delegate void Ability();
    public delegate void ArmAbility(string armType);

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

    public static ArmAbility GetArmPartAbility(string abilityName)
    {
        ArmAbility armAbility = null;

        switch (abilityName)
        {
            case "Sticky Bomb":
                armAbility = Ability_StickyBomb;
                return armAbility;
            case "Drill Fist":
                armAbility = Ability_DrillFist;
                return armAbility;
            case "Strong Arm":
                armAbility = Ability_StrongArm;
                return armAbility;
            case "Weapons Training":
                armAbility = Ability_WeaponsTraining;
                return armAbility;
            case "Needle Shot":
                armAbility = Ability_NeedleShot;
                return armAbility;
            case "Feather Fall":
                armAbility = Ability_FeatherFall;
                return armAbility;
            case "Pincer Pistol":
                armAbility = Ability_PincerPistol;
                return armAbility;
            case "Bone Toss":
                armAbility = Ability_BoneToss;
                return armAbility;
            default:
                return null;
        }
    }
    //Head Ability (Activate): Allows the player to shoot a laser beam
    public static void Ability_LaserEyes()
    {
        PlayerController player = PlayerController.Instance;
        GameObject laserLoad = Resources.Load<GameObject>("Prefabs/Projectiles/Robot_Laser");

        //playing the animation
        player.animator.Play("HeadAbilityAnim");

        Vector2 laserPosition = new Vector2(player.monster.headPart.transform.position.x + 0.3f * player.facingDirection, player.monster.headPart.transform.position.y + 0.3f);
        GameObject laser = Instantiate(laserLoad, laserPosition, Quaternion.identity);
        laser.GetComponent<Projectile>().target = "Enemy";

        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(laser.GetComponent<Projectile>().speed * player.facingDirection, 0);
    }

    //Head Ability (Activate): Allows the player to attack with a tongue flick
    public static void Ability_TongueFlick()
    {
        PlayerController player = PlayerController.Instance;
        GameObject tongueLoad = Resources.Load<GameObject>("Prefabs/Projectiles/Frog_Tongue");
        tongueLoad.GetComponent<TongueFlick>().target = "Enemy";

        Vector2 tonguePosition = new Vector2(player.monster.headPart.transform.position.x + 0.3f * player.facingDirection, player.monster.headPart.transform.position.y + 0.05f);
        //Debug.Log(tongueLoad.transform.localScale);
        //tongueLoad.transform.localScale *= player.facingDirection;
        //Debug.Log(tongueLoad.transform.localScale);
        GameObject tongue = Instantiate(tongueLoad, tonguePosition, Quaternion.identity);
        tongue.transform.localScale *= player.facingDirection;

        player.animator.Play("HeadAbilityAnim");
        tongue.GetComponent<Animator>().Play("TongueFlickAnim");
    }

    //Head Ability (Activate): Allows the player to knock back enemies with 
    //an AOE roar attack. Does not deal damage
    public static void Ability_LionsRoar()
    {
        
        PlayerController.Instance.animator.Play("LionsRoar" + Helper.GetAnimDirection(PlayerController.Instance.facingDirection) + "Anim");
    }

    //Head Ability (Activate): Allows the player to spit out a cloud of acid 
    //that lingers for several seconds and deals damage over time
    public static void Ability_AcidBreath()
    {
        PlayerController player = PlayerController.Instance;
        GameObject acidCloudLoad = Resources.Load<GameObject>("Prefabs/Projectiles/AcidCloud");
        acidCloudLoad.GetComponent<AcidBreath>().target = "Enemy";

        Vector2 acidCloudPosition = new Vector2(player.transform.position.x + 2 * player.facingDirection, player.transform.position.y);

        Instantiate(acidCloudLoad, acidCloudPosition, Quaternion.identity);
    }

    //Head Ability (Activate): Allows the player to attack with large beak
    public static void Ability_BigBeak()
    {
        PlayerController player = PlayerController.Instance;
        Ray beakRay = new Ray();
        beakRay.origin = new Vector2(player.transform.position.x, player.transform.position.y + 1f);
        beakRay.direction = new Vector3(player.facingDirection, 0, 0);

        Debug.DrawRay(beakRay.origin, new Vector2(1.7f * player.facingDirection, 0), Color.green);

        //playing the animation
        player.animator.Play("HeadAbilityAnim");

        RaycastHit2D hit = Physics2D.Raycast(beakRay.origin, beakRay.direction, 1.7f, 1 << LayerMask.NameToLayer("Enemy"));
        if (hit)
        {
            Enemy enemy = hit.transform.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(3, Helper.GetKnockBackDirection(player.transform, hit.transform));
            }
        }
    }

    //Torso Ability (Passive): Grants the player an extra heart of health
    public static void Ability_ArmoredBody()
    {
        PlayerController.Instance.maxHealth += 1;
    }

    //Torso Ability (Passive): Allows the player to breath underwater
    public static void Ability_Gills()
    {
        PlayerController.Instance.hasGills = true;
    }

    //Torso Ability (Passive): Prevents the player from being damaged by attacks from behind
    public static void Ability_HardShell()
    {
        //creating the collider to act as the shell
        BoxCollider2D shellCollider = PlayerController.Instance.gameObject.AddComponent<BoxCollider2D>();
        shellCollider.isTrigger = true;
        shellCollider.offset = new Vector2(-0.78f, -0.2326667f);
        shellCollider.size = new Vector2(0.2f, 3.214667f);
    }

    //Torso Ability (Activate): Allows the player to teleport short distances
    public static void Ability_GhostWalk()
    {
        PlayerController player = PlayerController.Instance;
        
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

        RaycastHit2D[] hitList = { topRayTerrainHit, middleRayTerrainHit, bottomRayTerrainHit };

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
        PlayerController player = PlayerController.Instance;

        if (!player.isUnderwater)
        {
            if (player.IsOnGround())
            {
                player.animator.Play("SwoopDaWoop" + Helper.GetAnimDirection(player.facingDirection) + "Anim_Grounded");
                player.rb.velocity = new Vector2(-13f * player.facingDirection, 20);
            }
            else if (!player.IsOnGround() && player.hasExtraJump)
            {
                player.rb.velocity = new Vector2(17f * player.facingDirection, 20);
                player.animator.Play("SwoopDaWoop" + Helper.GetAnimDirection(player.facingDirection) + "Anim_Aerial");
                player.hasExtraJump = false;
            }
        }
    }

    //Arm Ability (Activate): Lets the player shoot out a bomb that explodes after 
    //a few seconds. Sticks to walls and enemies
    public static void Ability_StickyBomb(string armType)
    {
        PlayerController player = PlayerController.Instance;
        //play attack animation
        player.animator.Play(armType + Helper.GetAnimDirection(player.facingDirection, armType) + "MeleeAnim");

        GameObject bombPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/StickyBomb");
        GameObject bomb = Instantiate(bombPrefab, player.monster.rightArmPart.hand.transform.position, Quaternion.identity);
        bomb.GetComponent<Rigidbody2D>().velocity = new Vector2(bomb.GetComponent<Rigidbody2D>().velocity.x + 40 * player.facingDirection, bomb.GetComponent<Rigidbody2D>().velocity.y+10);
    }

    //Arm Ability (Activate): Allows the player to attack with a drill, deals multiple hits of 
    //damage, all other actions and movement are locked for the ability's duration
    public static void Ability_DrillFist(string armType)
    {
        PlayerController player = PlayerController.Instance;

        GameObject drillPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/Projectile_Drill");

        GameObject drill = Instantiate(drillPrefab, player.monster.leftArmPart.hand.transform.position, drillPrefab.transform.rotation);

        drill.transform.localScale = new Vector3(drill.transform.localScale.x, drill.transform.localScale.y * player.facingDirection, drill.transform.localScale.z);

        //playing the shoot animation
        player.animator.Play(armType + Helper.GetAnimDirection(player.facingDirection, armType) + "ShootAnim");

        drill.GetComponent<Rigidbody2D>().velocity = new Vector2(drill.GetComponent<Projectile>().speed * player.facingDirection, 0);
    }

    //Arm Ability (Passive): Increases the player's melee damage, does not affect weapon damage
    public static void Ability_StrongArm(string armType)
    {
        if (armType == Helper.PartType.RightArm)
        {
            PlayerController.Instance.rightAttackPower = 5;
        }
        else if (armType == Helper.PartType.LeftArm)
        {
            PlayerController.Instance.leftAttackPower = 5;
        }
    }

    //Arm Ability (Passive): Increases the player's melee weapon damage, does not affect projectile weapon damage
    public static void Ability_WeaponsTraining(string armType)
    {
        PlayerController player = PlayerController.Instance;
        if(armType == Helper.PartType.RightArm)
        {
            if(player.monster.rightArmPart.weapon != null && player.monster.rightArmPart.weapon.WeaponType == Helper.WeaponType.Melee)
            {
                player.monster.rightArmPart.weapon.Damage += 1;
            }
        }
        else if (armType == Helper.PartType.LeftArm)
        {
            if (player.monster.leftArmPart.weapon != null && player.monster.leftArmPart.weapon.WeaponType == Helper.WeaponType.Melee)
            {
                player.monster.leftArmPart.weapon.Damage += 1;
            }
        }
    }

    //Arm Ability (Activate): Allow the player to shoot three needles in a spread formation
    public static void Ability_NeedleShot(string armType)
    {
        PlayerController player = PlayerController.Instance;

        //loading the prefab
        GameObject needleLoad = Resources.Load<GameObject>("Prefabs/Projectiles/Cactus_Needle");
        int speed = needleLoad.GetComponent<Projectile>().speed;
        needleLoad.GetComponent<Projectile>().target = "Enemy";
        Vector2 needlePosition = new Vector2();

        //determining which hand to spawn the needles at
        if (armType == Helper.PartType.RightArm)
        {
            needlePosition = player.monster.rightArmPart.hand.transform.position;
        }
        else if (armType == Helper.PartType.LeftArm)
        {
            needlePosition = player.monster.leftArmPart.hand.transform.position;
        }

        //instatiating each needle with its own rotation
        GameObject upNeedle = Instantiate(needleLoad, needlePosition, Quaternion.Euler(0, 0, 45));
        GameObject middleNeedle = Instantiate(needleLoad, needlePosition, Quaternion.identity);
        GameObject downNeedle = Instantiate(needleLoad, needlePosition, Quaternion.Euler(0, 0, -45));

        //turning the needles in the same direction the player is facing
        upNeedle.transform.localScale = new Vector2(upNeedle.transform.localScale.x * player.facingDirection, upNeedle.transform.localScale.y);
        middleNeedle.transform.localScale = new Vector2(upNeedle.transform.localScale.x * player.facingDirection, upNeedle.transform.localScale.y);
        downNeedle.transform.localScale = new Vector2(upNeedle.transform.localScale.x * player.facingDirection, upNeedle.transform.localScale.y);


        //playing the shoot animation
        player.animator.Play(armType + Helper.GetAnimDirection(player.facingDirection, armType) + "ShootAnim");

        upNeedle.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * player.facingDirection, speed / 2);
        middleNeedle.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * player.facingDirection, 0);
        downNeedle.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * player.facingDirection, -speed / 2);
    }

    //Arm Ability (Passive): Makes the player fall slower, effect can stack with other arm
    //due to nature of the ability, needs to pass on a separate method (see FeatherFall())
    public static void Ability_FeatherFall(string armType)
    {
        PlayerController.Instance.playerCheckDelegate += FeatherFall;
    }

    //Arm Ability (Activate): Allows the player to extend and shoot their arm out to 
    //attack enemies at medium range
    public static void Ability_PincerPistol(string armType)
    {
        PlayerController player = PlayerController.Instance;

        player.animator.Play("PincerPistol_" + armType + "_" + Helper.GetAnimDirection(player.facingDirection, armType) + "_Anim");

        Debug.DrawRay(player.monster.rightArmPart.bicep.transform.position, new Vector2(5f, 0), Color.green);

        Ray pincerRay = new Ray();
        if(armType == Helper.PartType.RightArm)
        {
            pincerRay.origin = player.monster.rightArmPart.bicep.transform.position;
        }
        else if(armType == Helper.PartType.LeftArm)
        {
            pincerRay.origin = player.monster.leftArmPart.bicep.transform.position;
        }
        pincerRay.direction = new Vector2(player.facingDirection, 0);
        
        RaycastHit2D hit = Physics2D.Raycast(pincerRay.origin, pincerRay.direction, 5f, 1 << LayerMask.NameToLayer("Enemy"));
        if (hit)
        {
            Enemy enemy = hit.transform.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(3, Helper.GetKnockBackDirection(player.transform, hit.transform));
            }
        }
    }

    //Arm Ability (Passive): Equips the player with the bone weapon, 
    //takes up and locks this arm's weapon slot
    public static void Ability_BoneToss(string armType)
    {
        if(armType == Helper.PartType.RightArm)
        {
            PlayerController.Instance.monster.rightArmPart.weapon = WeaponFactory.GetWeapon(Helper.WeaponName.Bone, armType, "Player", PlayerController.Instance.monster.rightArmPart.weaponRenderer);
        }
        else if (armType == Helper.PartType.LeftArm)
        {
            PlayerController.Instance.monster.leftArmPart.weapon = WeaponFactory.GetWeapon(Helper.WeaponName.Bone, armType, "Player", PlayerController.Instance.monster.leftArmPart.weaponRenderer);
        }
    }

    //Leg Ability (Passive): Allows the player to damage enemies by jumping on them
    public static void Ability_SpikedFeet()
    {
        PlayerController.Instance.playerCheckDelegate += SpikedFeet;
    }

    //Leg Ability (Passive): Increases the player's move speed
    public static void Ability_QuickFeet()
    {
        PlayerController.Instance.moveSpeed = 19;
    }

    //Leg Ability (Activate): Allows the player to perform a single jump in mid-air
    public static void Ability_Acrobat()
    {
        PlayerController player = PlayerController.Instance;

        if (!player.isUnderwater)
        {
            if (player.IsOnGround())
            {
                //calling the jump animation
                player.animator.Play("Jump" + Helper.GetAnimDirection(player.facingDirection) + "Anim");
                player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
            }
            else if (!player.IsOnGround() && player.hasExtraJump)
            {
                //calling the jump animation
                player.animator.Play("Jump" + Helper.GetAnimDirection(player.facingDirection) + "Anim");
                player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
                player.hasExtraJump = false;
            }
        }
    }

    //Leg Ability (Passive): Increases the player's jump height
    public static void Ability_JoeyJump()
    {
        PlayerController.Instance.jumpForce = 70;
    }

    //Leg Ability (Activate): Allows the player to attack with taloned feet
    public static void Ability_TalonFlurry()
    {
        PlayerController player = PlayerController.Instance;

        if (!player.isUnderwater)
        {
            if (player.IsOnGround())
            {
                //calling the jump animation
                player.animator.Play("Jump" + Helper.GetAnimDirection(player.facingDirection) + "Anim");
                player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
            }
            else if (!player.IsOnGround() && player.hasExtraJump)
            {
                player.hasExtraJump = false;

                //setting the size and offset of the hitbox
                player.hitBox.offset = new Vector2(0.3871388f, -1.349547f);
                player.hitBox.size = new Vector2(1.37546f, 1.591879f);

                player.hitBoxDamage = 1;
                player.hitCounter = 0;
                player.totalHits = 3;

                player.rb.velocity = new Vector2(player.rb.velocity.x, 5);

                player.animator.Play("TalonFlurry" + Helper.GetAnimDirection(player.facingDirection) + "Anim");
                
            }
        }
    }    


    //The bellow methods are for miscellaneous use by the actual ability methods

    public static void FeatherFall()
    {
        PlayerController player = PlayerController.Instance;

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
            player.rb.gravityScale = 20;
        }
    }

    public static void SpikedFeet()
    {
        PlayerController player = PlayerController.Instance;

        if (!player.inHitStun)
        {

            var stompCheck1 = Physics2D.Raycast(new Vector2(player.transform.position.x, player.transform.position.y - player.height), -Vector2.down, player.rayCastLengthCheck, 1 << LayerMask.NameToLayer("Enemy"));
            var stompCheck2 = Physics2D.Raycast(new Vector2(player.transform.position.x + (player.width - 0.2f), player.transform.position.y - player.height), -Vector2.up, player.rayCastLengthCheck, 1 << LayerMask.NameToLayer("Enemy"));
            var stompCheck3 = Physics2D.Raycast(new Vector2(player.transform.position.x - (player.width - 0.2f), player.transform.position.y - player.height), -Vector2.up, player.rayCastLengthCheck, 1 << LayerMask.NameToLayer("Enemy"));

            Enemy enemyHit = null;
            if (stompCheck1)
            {
                enemyHit = stompCheck1.transform.GetComponentInParent<Enemy>();
            }
            else if (stompCheck2)
            {
                enemyHit = stompCheck2.transform.GetComponentInParent<Enemy>();
            }
            else if (stompCheck3)
            {
                enemyHit = stompCheck3.transform.GetComponentInParent<Enemy>();
            }
            if (enemyHit != null)
            {
                player.animator.Play("Jump" + Helper.GetAnimDirection(player.facingDirection) + "Anim");
                player.rb.velocity = new Vector2(player.rb.velocity.x, 40);
                enemyHit.TakeDamage(2, Helper.GetKnockBackDirection(player.transform, enemyHit.transform));
            }
        }
    }
}
