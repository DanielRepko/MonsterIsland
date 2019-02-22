﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float playerSpeed = 20f;

    private Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void FixedUpdate() {
        if (Input.GetAxis("Horizontal") > 0) {
            rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
        } else if (Input.GetAxis("Horizontal") < 0) {
            rb.velocity = new Vector2(-playerSpeed, rb.velocity.y);
        } else if (Input.GetAxis("Horizontal") == 0) {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }
}
