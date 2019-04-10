using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallout : MonoBehaviour {

    public GameObject resetPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            PlayerController.Instance.TakeDamage(1, 0f);
            PlayerController.Instance.transform.position = resetPoint.transform.position;
        }

        if(collision.tag == "Enemy") {
            Destroy(collision.gameObject);
        }
    }
}
