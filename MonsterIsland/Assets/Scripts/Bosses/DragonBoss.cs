using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBoss : Boss {

    public Animator wingsAnimator;
    public bool dontTurn;

    // Use this for initialization
    override public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.5f;

        InitializeEnemy();
    }

    // Update is called once per frame
    override public void Update()
    {
        if (health <= 0)
        {
            KillBoss();
        }
    }

    public void StartFlapping()
    {
        wingsAnimator.enabled = true;
    }

    public void StopFlapping()
    {
        wingsAnimator.enabled = false;
    }
    

    override public void FixedUpdate()
    {
        if (!PlayerController.Instance.isAlive)
        {
            target = null;
        }

        float lookTarget = (PlayerController.Instance.transform.position - transform.position).normalized.x;
        SetFacingDirection(lookTarget);

        //running any necessary checks on the Enemy
        //checkDelegate();
        
    }

    override public void InitializeEnemy()
    {
        //creating variables to initialize the monster
        //this code is for testing purposes, final product will pull this information from the database scripts
        var headInfo = PartFactory.GetHeadPartInfo(monsterName);
        var torsoInfo = PartFactory.GetTorsoPartInfo(monsterName);
        var rightArmInfo = PartFactory.GetArmPartInfo(monsterName, Helper.PartType.RightArm);
        var leftArmInfo = PartFactory.GetArmPartInfo(monsterName, Helper.PartType.LeftArm);
        var legPartInfo = PartFactory.GetLegPartInfo(monsterName);
        rightArmInfo.equippedWeapon = equippedRightWeapon;
        leftArmInfo.equippedWeapon = equippedLeftWeapon;

        monster.InitializeMonster(headInfo, torsoInfo, rightArmInfo, leftArmInfo, legPartInfo);

        SetFacingDirection(transform.localScale.x);
    }

    override public void SetFacingDirection(float scaleX)
    {
        if (dontTurn)
        {
            //checking to see whether scaleX is indicating left or right (may not always be passed as -1 or 1)
            if (scaleX < 0)
            {
                facingDirection = -1;
            }
            else
            {
                facingDirection = 1;
            }

            transform.localScale = new Vector3(facingDirection, 1, 1);
            monster.ChangeDirection(facingDirection);
        }
    }

    override public void TakeDamage(int damage, float knockBackDirection)
    {
        if (!inHitStun)
        {
            //animator.Play(); insert dragon specific hurt animation
            health -= damage;
        }
    }

    public void UseAttack(string attackName)
    {
        switch (attackName)
        {
            case "Slow Burn":
                GameObject slowFireBall = Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles/SlowFireBall"), transform.position, Quaternion.identity);
                ShootAtPlayer(slowFireBall);
                break;
            case "Fire Bolt":
                GameObject fireBolt = Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles/FireBolt"), transform.position, Quaternion.identity);
                fireBolt.transform.right = (PlayerController.Instance.transform.position - fireBolt.transform.position).normalized;
                ShootAtPlayer(fireBolt);
                break;
            case "Multi Flame":
                GameObject fireBolt1 = Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles/FireBolt"), transform.position, Quaternion.Euler(0,0,-140));
                GameObject fireBolt2 = Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles/FireBolt"), transform.position, Quaternion.Euler(0, 0, -120));
                GameObject fireBolt3 = Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles/FireBolt"), transform.position, Quaternion.Euler(0, 0, -90));
                GameObject fireBolt4 = Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles/FireBolt"), transform.position, Quaternion.Euler(0, 0, -60));
                GameObject fireBolt5 = Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles/FireBolt"), transform.position, Quaternion.Euler(0, 0, -40));

                fireBolt1.GetComponent<Rigidbody2D>().velocity = fireBolt1.transform.right * fireBolt1.GetComponent<Projectile>().speed;
                fireBolt2.GetComponent<Rigidbody2D>().velocity = fireBolt2.transform.right * fireBolt2.GetComponent<Projectile>().speed;
                fireBolt3.GetComponent<Rigidbody2D>().velocity = fireBolt3.transform.right * fireBolt3.GetComponent<Projectile>().speed;
                fireBolt4.GetComponent<Rigidbody2D>().velocity = fireBolt4.transform.right * fireBolt4.GetComponent<Projectile>().speed;
                fireBolt5.GetComponent<Rigidbody2D>().velocity = fireBolt5.transform.right * fireBolt5.GetComponent<Projectile>().speed;
                break;
            default:
                break;            
        }
    }

    //used to more easily aim and shoot fireballs at the player
    public void ShootAtPlayer(GameObject projectile)
    {
        Vector2 destination = (PlayerController.Instance.transform.position - transform.position).normalized;
        projectile.GetComponent<Rigidbody2D>().velocity = destination * projectile.GetComponent<Projectile>().speed;
    }

    override public void KillBoss()
    {
        Destroy(gameObject);
    }

    /*
     * all methods below this point are overridden to be empty because they are not needed for this boss as
     * most interactions and operations for this boss will be scripted (predetermined) in animations and timelines
     * the main reason for this is that the Bosses need to extend the enemy script to be properly damaged by the player
     * without major refactor of how damage is taken and dealt, which there is not time for
    */
    override public void RightAttack(string armType)
    {
        
    }

    override public void LeftAttack(string armType)
    {
        
    }

    //Used to alternate between the attacks of each arm
    override public void SetNextAttack()
    {
        
    }   

    override public bool PlayerIsInAttackRange()
    {
        return false;
    }    

    override public void UpdateCooldowns()
    {
        
    }

    override public bool CheckCooldown(string inputCooldown)
    {
        return false;
    }

    override public void SetCooldowns()
    {
        
    }

    override public void OnTriggerEnter2D(Collider2D collision)
    {

    }

    override public void OnTriggerExit2D(Collider2D collision)
    {

    }
    
}
