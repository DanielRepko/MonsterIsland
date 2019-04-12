using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            switch(SceneManager.GetActiveScene().name) {
                case "Plains":
                    CutsceneManager.Instance.PlayPlainsBossStart();
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}
