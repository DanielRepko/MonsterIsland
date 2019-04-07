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
    public Text moneyText;
    public GameObject[] heartImages;
    
    //Shop related objects
    public GameObject shopPanel;
    public Text shopMoneyText;

    public Button shopWeapon1Button;
    public Text shopWeapon1Text;
    public Image shopWeapon1Image;
    public Button shopWeapon2Button;
    public Text shopWeapon2Text;
    public Image shopWeapon2Image;
    public Button shopPartButton;
    public Text shopPartText;
    public Image shopPartImage;

    public Image selectedItemImage;
    public Text selectedItemName;
    public Text selectedItemCost;
    public Text selectedItemDescription;
    public Button purchaseButton;

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

    public void UpdateHeartCount() {
        int i = PlayerController.Instance.health;
        foreach (GameObject heart in heartImages) {
            if(i > 0) {
                heart.SetActive(true);
            } else {
                heart.SetActive(false);
            }
            i--;
        }
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

    public void ShowShopPanel() {
        shopPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void HideShopPanel() {
        shopPanel.SetActive(false);
        Time.timeScale = 1;
        RefreshShopUI();
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
        quickTravelMenu.SetActive(false);
    }

    public void EnableQuickTravelMenu() {
        quickTravelMenu.SetActive(true);
    }

    public void TravelToStartNest() {
        PlayerController player = FindObjectOfType<PlayerController>();
        LocalObjectManager nestManager = FindObjectOfType<LocalObjectManager>();
        player.transform.position = nestManager.startNest.transform.position;
        HideNestCanvas();
    }

    public void TravelToShopNest() {
        PlayerController player = FindObjectOfType<PlayerController>();
        LocalObjectManager nestManager = FindObjectOfType<LocalObjectManager>();
        player.transform.position = nestManager.shopNest.transform.position;
        HideNestCanvas();
    }

    public void TravelToBossNest() {
        PlayerController player = FindObjectOfType<PlayerController>();
        LocalObjectManager nestManager = FindObjectOfType<LocalObjectManager>();
        player.transform.position = nestManager.bossNest.transform.position;
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

    public void UpdateBalance(int balance) {
        moneyText.text = balance.ToString();
        shopMoneyText.text = balance.ToString();
    }

    public void RefreshShopUI() {
        Inventory inventory = FindObjectOfType<Inventory>();

        shopWeapon1Text.text = ShopManager.instance.shopWeapon1.WeaponName;
        shopWeapon1Image.sprite = ShopManager.instance.shopWeapon1.WeaponSprite;
        if(inventory.collectedWeapons.Contains(ShopManager.instance.shopWeapon1.WeaponName)) {
            shopWeapon1Button.interactable = false;
        } else {
            shopWeapon1Button.interactable = true;
        }

        shopWeapon2Text.text = ShopManager.instance.shopWeapon2.WeaponName;
        shopWeapon2Image.sprite = ShopManager.instance.shopWeapon2.WeaponSprite;
        if (inventory.collectedWeapons.Contains(ShopManager.instance.shopWeapon2.WeaponName)) {
            shopWeapon2Button.interactable = false;
        } else {
            shopWeapon2Button.interactable = true;
        }
        
        shopPartText.text = ShopManager.instance.shopPart.abilityName;
        bool enableButton = true;
        switch(ShopManager.instance.shopPart.partType) {
            case Helper.PartType.Head:
                if(inventory.collectedParts.collectedHeads.Contains(Helper.MonsterName.Robot)) {
                    enableButton = false;
                }
                break;
            case Helper.PartType.Torso:
                if (inventory.collectedParts.collectedTorsos.Contains(Helper.MonsterName.Robot)) {
                    enableButton = false;
                }
                break;
            case Helper.PartType.LeftArm:
                if (Inventory.Instance.collectedParts.collectedLeftArms.Contains(Helper.MonsterName.Robot)) {
                    enableButton = false;
                }
                break;
            case Helper.PartType.RightArm:
                if (Inventory.Instance.collectedParts.collectedRightArms.Contains(Helper.MonsterName.Robot)) {
                    enableButton = false;
                }
                break;
            case Helper.PartType.Legs:
                if (Inventory.Instance.collectedParts.collectedLegs.Contains(Helper.MonsterName.Robot)) {
                    enableButton = false;
                }
                break;
        }
        shopPartButton.interactable = enableButton;

        selectedItemImage.enabled = false;
        selectedItemName.text = "";
        selectedItemCost.text = "";
        selectedItemDescription.text = "";
        purchaseButton.interactable = false;
    }

    public void ShowShopWeapon1Info() {
        selectedItemImage.sprite = ShopManager.instance.shopWeapon1.WeaponSprite;
        selectedItemImage.enabled = true;
        selectedItemName.text = ShopManager.instance.shopWeapon1.WeaponName;
        selectedItemCost.text = "MB$50";
        selectedItemDescription.text = ShopManager.instance.shopWeapon1.WeaponDesc;
        if (Inventory.Instance.money >= 50) {
            purchaseButton.interactable = true;
        } else {
            purchaseButton.interactable = false;
        }
    }

    public void ShowShopWeapon2Info() {
        selectedItemImage.sprite = ShopManager.instance.shopWeapon2.WeaponSprite;
        selectedItemImage.enabled = true;
        selectedItemName.text = ShopManager.instance.shopWeapon2.WeaponName;
        selectedItemCost.text = "MB$50";
        selectedItemDescription.text = ShopManager.instance.shopWeapon2.WeaponDesc;
        if (Inventory.Instance.money >= 50) {
            purchaseButton.interactable = true;
        } else {
            purchaseButton.interactable = false;
        }
    }

    public void ShowShopPartInfo() {
        selectedItemImage.sprite = Resources.Load<Sprite>("Sprites/Monsters/Robot/Head/Monster_Robot_Head_Face_idle");
        selectedItemImage.enabled = true;
        selectedItemName.text = ShopManager.instance.shopPart.abilityName;
        selectedItemCost.text = "MB$75";
        selectedItemDescription.text = ShopManager.instance.shopPart.abilityDesc;
        if (Inventory.Instance.money >= 75) {
            purchaseButton.interactable = true;
        } else {
            purchaseButton.interactable = false;
        }
    }
}
