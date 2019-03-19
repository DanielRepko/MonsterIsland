using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : MonoBehaviour {

    Enemy connectedEnemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && collision.GetComponent<Enemy>().hurtBox == collision)
        {
            FixedJoint2D stick = gameObject.AddComponent<FixedJoint2D>();
            stick.connectedBody = collision.attachedRigidbody;
            connectedEnemy = collision.GetComponent<Enemy>();
        } else if (collision.tag == "Ground")
        {
            FixedJoint2D stick = gameObject.AddComponent<FixedJoint2D>();
        }
    }
}
