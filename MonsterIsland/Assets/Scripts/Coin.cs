using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int value = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.transform.tag == "Player") {
            Inventory.Instance.AddMoney(value);
            Destroy(gameObject);
        }
    }

}
