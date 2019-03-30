using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public AudioClip mainMenuMusic;

	// Use this for initialization
	void Start () {
        AudioManager.Instance.PlayMusic(mainMenuMusic, true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowFileSelect() {
        SceneManager.LoadScene("FileSelectMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
