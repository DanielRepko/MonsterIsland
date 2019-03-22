using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour {

    public LevelName levelName;
    public bool fromHub;
    private bool hasContact;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Time.deltaTime > 0) {
            if (hasContact == true && Input.GetKeyDown(CustomInputManager.Instance.GetInputKey(InputType.Interact))) {
                if (levelName == LevelName.Skyland && fromHub) {
                    UIManager.Instance.ShowLoadingPanel();
                    PlayerController.Instance.transform.position = new Vector2(-0.5f, -1f);
                    SceneManager.LoadScene("Skyland");
                } else if (levelName == LevelName.Skyland && !fromHub) {
                    UIManager.Instance.ShowLoadingPanel();
                    PlayerController.Instance.transform.position = new Vector2(70.5f, 13f);
                    SceneManager.LoadScene("Hub");
                } else if (levelName == LevelName.Castle && fromHub) {
                    UIManager.Instance.ShowLoadingPanel();
                    PlayerController.Instance.transform.position = new Vector2(-20f, 1f);
                    SceneManager.LoadScene("Castle");
                }
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            switch(levelName) {
                case LevelName.Plains:
                    if(fromHub) {
                        UIManager.Instance.ShowLoadingPanel();
                        PlayerController.Instance.transform.position = new Vector2(530f, 25.6f);
                        SceneManager.LoadScene("Plains");
                    } else {
                        UIManager.Instance.ShowLoadingPanel();
                        PlayerController.Instance.transform.position = new Vector2(0f, -1.25f);
                        SceneManager.LoadScene("Hub");
                    }
                    break;
                case LevelName.Desert:
                    if (fromHub) {
                        UIManager.Instance.ShowLoadingPanel();
                        PlayerController.Instance.transform.position = new Vector2(20f, 1f);
                        SceneManager.LoadScene("Desert");
                    } else {
                        UIManager.Instance.ShowLoadingPanel();
                        PlayerController.Instance.transform.position = new Vector2(3f, -32f);
                        SceneManager.LoadScene("Hub");
                    }
                    break;
                case LevelName.Underwater:
                    if (fromHub) {
                        UIManager.Instance.ShowLoadingPanel();
                        PlayerController.Instance.transform.position = new Vector2(-10f, 1f);
                        SceneManager.LoadScene("Underwater");
                    } else {
                        UIManager.Instance.ShowLoadingPanel();
                        PlayerController.Instance.transform.position = new Vector2(120f, -66f);
                        SceneManager.LoadScene("Hub");
                    }
                    break;
                case LevelName.Jungle:
                    if (fromHub) {
                        UIManager.Instance.ShowLoadingPanel();
                        PlayerController.Instance.transform.position = new Vector2(-20f, 1f);
                        SceneManager.LoadScene("Jungle");
                    } else {
                        UIManager.Instance.ShowLoadingPanel();
                        PlayerController.Instance.transform.position = new Vector2(140f, -32f);
                        SceneManager.LoadScene("Hub");
                    }
                    break;
                case LevelName.Castle:
                    if (fromHub) {
                        hasContact = true;
                    } else {
                        UIManager.Instance.ShowLoadingPanel();
                        PlayerController.Instance.transform.position = new Vector2(70.5f, -34f);
                        SceneManager.LoadScene("Hub");
                    }
                    break;
                default:
                    //If default is run, this may be one of the cases where they have to press W to enter. In that case, toggle contact
                    hasContact = true;
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //Contact with the player has been lost
        if (collision.tag == "Player") {
            hasContact = false;
        }
    }
}
