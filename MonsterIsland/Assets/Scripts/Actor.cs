using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Actor : MonoBehaviour {

    public float moveSpeed = 15.5f;
    public float jumpForce = 60f;

    public bool attacksLocked;
    public bool movementLocked;

    protected float hitStunCooldown = 0.4f;
    protected float hitStunTimer = 0;
    public bool inHitStun = false;

    public int health;
    public int maxHealth;
    public Collider2D hurtBox;

    public float rayCastLengthCheck = 0.2f;
    public float width;
    public float height;

    public Monster monster;

    public Animator animator;

    //used to check what direction the actor is facing
    //-1 = left  1 = right
    public float facingDirection;

    public Rigidbody2D rb;

    abstract public void TakeDamage(int damage, float knockBackDirection);

    public void ShowAttackFace()
    {
        monster.headPart.face.sprite = monster.headPart.attackFaceSprite;
    }

    public void ShowIdleFace()
    {
        monster.headPart.face.sprite = monster.headPart.idleFaceSprite;
    }

    public void ShowHurtFace()
    {
        monster.headPart.face.sprite = monster.headPart.hurtFaceSprite;
    }

    //PlayerIsOnGround function taken from SuperSoyBoy game from Ray Wenderlich
    //renamed to IsOnGround to better fit purposes
    public bool IsOnGround()
    {
        bool groundCheck1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - height), -Vector2.down, rayCastLengthCheck, 1 << LayerMask.NameToLayer("Terrain"));
        bool groundCheck2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck, 1 << LayerMask.NameToLayer("Terrain"));
        bool groundCheck3 = Physics2D.Raycast(new Vector2(transform.position.x - (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck, 1 << LayerMask.NameToLayer("Terrain"));


        bool waterCheck1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - height), -Vector2.down, rayCastLengthCheck, 1 << LayerMask.NameToLayer("Water"));
        bool waterCheck2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck, 1 << LayerMask.NameToLayer("Water"));
        bool waterCheck3 = Physics2D.Raycast(new Vector2(transform.position.x - (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck, 1 << LayerMask.NameToLayer("Water"));
        if (groundCheck1 || groundCheck2 || groundCheck3 || waterCheck1 || waterCheck2 || waterCheck3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
