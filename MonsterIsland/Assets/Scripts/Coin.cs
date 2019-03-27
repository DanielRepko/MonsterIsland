﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int value = 0;
    public AudioClip coinClip;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.transform.tag == "Player") {
            AudioManager.Instance.PlaySound(coinClip);
            Inventory.Instance.AddMoney(value);
            Destroy(gameObject);
        }
    }

}