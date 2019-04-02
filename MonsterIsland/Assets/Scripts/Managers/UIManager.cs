using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;
    public GameObject airMeter;
    public GameObject airMeterBar;
    public GameObject nestCanvas;
    public GameObject loadingPanel;
    public GameObject pauseCanvas;
    public GameObject settingsPanel;
    public GameObject quickTravelMenu;

	// Use this for initialization
	void Awake() {
        if(Instance == null) {
            SceneManager.sceneLoaded += OnSceneLoaded;
            Instance = this;
        } else if (Instance != this) {
            Destroy(this);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    //When called, updates the air meter based on the provided information
    public void UpdateAirMeter(float air, bool isUnderwater) {
        if (!PlayerController.Instance.hasGills)
        {
            airMeterBar.GetComponent<RectTransform>().offsetMax = new Vector2(-(122f - (122f * air)), 0f);
            airMeter.SetActive(isUnderwater);
        }
    }

    //When called, displays the Nest Canvas
    public void ShowNestCanvas() {
        nestCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    //When called, hides the Nest Canvas
    public void HideNestCanvas() {
        nestCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        HideLoadingPanel();
    }

    public void ShowLoadingPanel() {
        loadingPanel.SetActive(true);
    }

    public void HideLoadingPanel() {
        loadingPanel.SetActive(false);
    }

    public void PauseGame() {
        if (Time.timeScale != 0) {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void UnpauseGame() {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowSettings() {
        settingsPanel.SetActive(true);
    }

    public void DisableQuickTravelMenu() {
        Debug.Log("'DisableQuickTravelMenu' called!");
        quickTravelMenu.SetActive(false);
    }

    public void EnableQuickTravelMenu() {
        Debug.Log("'EnableQuickTravelMenu' called!");
        quickTravelMenu.SetActive(true);
    }

    public void TravelToStartNest() {
        PlayerController.Instance.transform.position = LocalNestManager.Instance.startNest.transform.position;
        HideNestCanvas();
    }

    public void TravelToShopNest() {
        PlayerController.Instance.transform.position = LocalNestManager.Instance.shopNest.transform.position;
        HideNestCanvas();
    }

    public void TravelToBossNest() {
        PlayerController.Instance.transform.position = LocalNestManager.Instance.bossNest.transform.position;
        HideNestCanvas();
    }

    public void SetStartWarp(bool interactible) {
        quickTravelMenu.transform.Find("QuickStartButton").GetComponent<Button>().interactable = interactible;
    }

    public void SetShopWarp(bool interactible) {
        quickTravelMenu.transform.Find("QuickShopButton").GetComponent<Button>().interactable = interactible;
    }

    public void SetBossWarp(bool interactible) {
        quickTravelMenu.transform.Find("QuickBossButton").GetComponent<Button>().interactable = interactible;
    }
}
