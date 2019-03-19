using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour {

    public LevelName levelName;
    public bool fromHub;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            switch(levelName) {
                case LevelName.Plains:
                    if(fromHub) {
                        SceneManager.LoadScene("Plains");
                    } else {
                        SceneManager.LoadScene("Hub");
                    }
                    break;
                case LevelName.Desert:
                    if (fromHub) {
                        SceneManager.LoadScene("Desert");
                    } else {
                        SceneManager.LoadScene("Hub");
                    }
                    break;
                case LevelName.Underwater:
                    if (fromHub) {
                        SceneManager.LoadScene("Underwater");
                    } else {
                        SceneManager.LoadScene("Hub");
                    }
                    break;
                case LevelName.Jungle:
                    if (fromHub) {
                        SceneManager.LoadScene("Jungle");
                    } else {
                        SceneManager.LoadScene("Hub");
                    }
                    break;
                case LevelName.Skyland:
                    if (fromHub) {
                        SceneManager.LoadScene("Skyland");
                    } else {
                        SceneManager.LoadScene("Hub");
                    }
                    break;
                case LevelName.Castle:
                    if (fromHub) {
                        SceneManager.LoadScene("Castle");
                    } else {
                        SceneManager.LoadScene("Hub");
                    }
                    break;
                default:
                    SceneManager.LoadScene("Hub");
                    break;
            }
        }
    }
}
