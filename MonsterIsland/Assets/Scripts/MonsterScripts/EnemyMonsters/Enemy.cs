using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    float health = 10;
    public Text text;
    public EdgeCollider2D hurtBox;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        text.text = health.ToString();
        //Ray attack = new Ray();
        //attack.origin = transform.position;
        //attack.direction = new Vector2(-1, 0);

        //Debug.DrawRay(attack.origin, new Vector3(1.7f * -1f, 0, 0), Color.green);

        //RaycastHit2D hit = Physics2D.Raycast(attack.origin, attack.direction, 1.7f, 1 << LayerMask.NameToLayer("Player"));
        //if (hit)
        //{
        //    if (hit.collider == PlayerController.Instance.hurtBox)
        //    {
        //        Debug.Log("hit the player");
        //    }
        //}
    }

    //causes the enemy to take damage
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
