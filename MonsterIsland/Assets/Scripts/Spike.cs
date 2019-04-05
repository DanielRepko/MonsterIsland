using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    private Collision2D playerCheck;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (playerCheck != null && PlayerController.Instance.canBeHurt) {
            PlayerController.Instance.TakeDamage(1, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            playerCheck = collision;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            playerCheck = null;
        }
    }
}
