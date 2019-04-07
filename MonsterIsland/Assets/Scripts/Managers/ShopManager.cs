using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {

    public static ShopManager instance;

    public Weapon shopWeapon1;
    public Weapon shopWeapon2;
    public MonsterPartInfo shopPart;

	// Use this for initialization
	void Start () {
		if(instance == null) {
            instance = this;
            SceneManager.sceneLoaded += LoadShopItems;
            LoadShopItems(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        } else if (instance != this) {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void LoadShopItems(Scene scene, LoadSceneMode mode) {
        switch(scene.name) {
            case "Hub":
                shopWeapon1 = WeaponFactory.GetWeapon(Helper.WeaponName.Stick, null, null, null);
                shopWeapon2 = WeaponFactory.GetWeapon(Helper.WeaponName.PeaShooter, null, null, null);
                shopPart = PartFactory.GetHeadPartInfo(Helper.MonsterName.Robot);
                break;
            case "Desert":
                shopWeapon1 = WeaponFactory.GetWeapon(Helper.WeaponName.Scimitar, null, null, null);
                shopWeapon2 = WeaponFactory.GetWeapon(Helper.WeaponName.Boomerang, null, null, null);
                shopPart = PartFactory.GetLegPartInfo(Helper.MonsterName.Robot);
                break;
            case "Underwater":
                shopWeapon1 = WeaponFactory.GetWeapon(Helper.WeaponName.Swordfish, null, null, null);
                shopWeapon2 = WeaponFactory.GetWeapon(Helper.WeaponName.HarpoonGun, null, null, null);
                shopPart = PartFactory.GetArmPartInfo(Helper.MonsterName.Robot, Helper.PartType.RightArm);
                break;
            case "Jungle":
                shopWeapon1 = WeaponFactory.GetWeapon(Helper.WeaponName.BananaGun, null, null, null);
                shopWeapon2 = WeaponFactory.GetWeapon(Helper.WeaponName.Club, null, null, null);
                shopPart = PartFactory.GetArmPartInfo(Helper.MonsterName.Robot, Helper.PartType.LeftArm);
                break;
            case "Skyland":
                shopWeapon1 = WeaponFactory.GetWeapon(Helper.WeaponName.SqueakyHammer, null, null, null);
                shopWeapon2 = WeaponFactory.GetWeapon(Helper.WeaponName.Fan, null, null, null);
                shopPart = PartFactory.GetTorsoPartInfo(Helper.MonsterName.Robot);
                break;
        }

        UIManager.Instance.RefreshShopUI();
    }

    public void PurchaseItem() {
        if(UIManager.Instance.selectedItemName.text == shopPart.abilityName && Inventory.Instance.money >= 75) {
            //Current item is a part. Add it to the inventory and deduct 75 MB
            Inventory.Instance.AddMonsterPart(shopPart.monster, shopPart.partType);
            Inventory.Instance.RemoveMoney(75);
        } else if ((UIManager.Instance.selectedItemName.text == shopWeapon1.WeaponName || UIManager.Instance.selectedItemName.text == shopWeapon2.WeaponName)
            && Inventory.Instance.money >= 50) {
            //Current item is a weapon. Add it to the inventory and deduct 50 MB
            Inventory.Instance.AddWeapon(UIManager.Instance.selectedItemName.text);
            Inventory.Instance.RemoveMoney(50);
        } else {
            //Something went wrong, they shouldn't be able to press the button, keep them from pressing it again
            UIManager.Instance.purchaseButton.interactable = false;
        }
        UIManager.Instance.RefreshShopUI();
    }
}
