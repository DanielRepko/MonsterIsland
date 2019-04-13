using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FileSelectManager : MonoBehaviour {

    public Button playButton;
    public Button deleteButton;
    public Text monsterName;
    public Text playTime;
    public Text areaName;
    public Text dateSaved;
    public Text currentBalance;
    public GameObject headSlot;
    public GameObject torsoSlot;
    public GameObject leftArmSlot;
    public GameObject rightArmSlot;
    public GameObject legSlot;
    public Image leftWeapon;
    public Image rightWeapon;
    public GameObject legendaryHead;
    public GameObject legendaryTorso;
    public GameObject legendaryLeftArm;
    public GameObject legendaryRightArm;
    public GameObject legendaryLegs;
    public GameObject loadingPanel;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReturnToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowFile(int fileNumber) {
        GameManager.instance.fileNumber = fileNumber;
        var savePath = Path.Combine(Application.persistentDataPath, "file" + fileNumber + ".json");
        string loadedFileJson;
        try {
            loadedFileJson = File.ReadAllText(savePath);
        } catch(FileNotFoundException) {
            loadedFileJson = null;
        }
        if(loadedFileJson != null) {
            var loadedFile = JsonUtility.FromJson<GameFile>(loadedFileJson);
            playButton.interactable = true;
            deleteButton.interactable = true;

            //General Information
            monsterName.text = loadedFile.player.name;
            playTime.text = TimeSpan.FromSeconds((int) loadedFile.totalPlayTime).ToString();
            areaName.text = loadedFile.saveArea;
            dateSaved.text = loadedFile.saveDate;
            currentBalance.text = "MB$" + loadedFile.player.inventory.monsterBucks;

            //Current Parts and Weapons
            //TODO: When code to save the current parts and weapons is added, update this to load those parts and weapons
            headSlot.SetActive(true);
            headSlot.GetComponent<HeadSlot>().faceImage.sprite = Helper.CreateSprite(loadedFile.player.headPart.mainSprite, Helper.HeadImporter);
            headSlot.GetComponent<HeadSlot>().neckImage.sprite = Helper.CreateSprite(loadedFile.player.headPart.neckSprite, Helper.HeadImporter);

            torsoSlot.SetActive(true);
            torsoSlot.GetComponent<TorsoSlot>().torsoImage.sprite = Helper.CreateSprite(loadedFile.player.torsoPart.mainSprite, Helper.TorsoImporter);

            leftArmSlot.SetActive(true);
            leftArmSlot.GetComponent<ArmSlot>().bicepImage.sprite = Helper.CreateSprite(loadedFile.player.leftArmPart.bicepSprite, Helper.BicepImporter);
            leftArmSlot.GetComponent<ArmSlot>().forearmImage.sprite = Helper.CreateSprite(loadedFile.player.leftArmPart.forearmSprite, Helper.ForearmImporter);
            leftArmSlot.GetComponent<ArmSlot>().handImage.sprite = Helper.CreateSprite(loadedFile.player.leftArmPart.handFrontSprite, Helper.HandImporter);
            leftArmSlot.GetComponent<ArmSlot>().fingersImage.sprite = Helper.CreateSprite(loadedFile.player.leftArmPart.fingersOpenFrontSprite, Helper.HandImporter);

            rightArmSlot.SetActive(true);
            rightArmSlot.GetComponent<ArmSlot>().bicepImage.sprite = Helper.CreateSprite(loadedFile.player.rightArmPart.bicepSprite, Helper.BicepImporter);
            rightArmSlot.GetComponent<ArmSlot>().forearmImage.sprite = Helper.CreateSprite(loadedFile.player.rightArmPart.forearmSprite, Helper.ForearmImporter);
            rightArmSlot.GetComponent<ArmSlot>().handImage.sprite = Helper.CreateSprite(loadedFile.player.rightArmPart.handFrontSprite, Helper.HandImporter);
            rightArmSlot.GetComponent<ArmSlot>().fingersImage.sprite = Helper.CreateSprite(loadedFile.player.rightArmPart.fingersOpenFrontSprite, Helper.HandImporter);

            legSlot.SetActive(true);
            legSlot.GetComponent<LegsSlot>().pelvisImage.sprite = Helper.CreateSprite(loadedFile.player.legsPart.pelvisSprite, Helper.PelvisImporter);
            legSlot.GetComponent<LegsSlot>().leftFootImage.sprite = legSlot.GetComponent<LegsSlot>().rightFootImage.sprite = Helper.CreateSprite(loadedFile.player.legsPart.footSprite, Helper.FootImporter);
            legSlot.GetComponent<LegsSlot>().leftShinImage.sprite = legSlot.GetComponent<LegsSlot>().rightShinImage.sprite = Helper.CreateSprite(loadedFile.player.legsPart.shinSprite, Helper.ShinImporter);
            legSlot.GetComponent<LegsSlot>().leftThighImage.sprite = legSlot.GetComponent<LegsSlot>().rightThighImage.sprite = Helper.CreateSprite(loadedFile.player.legsPart.thighSprite, Helper.ThighImporter);

            leftWeapon.enabled = true;
            leftWeapon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Weapons/" + loadedFile.player.leftArmPart.equippedWeapon);

            rightWeapon.enabled = true;
            rightWeapon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Weapons/" + loadedFile.player.rightArmPart.equippedWeapon);

            //Legendary Parts
            legendaryHead.SetActive(loadedFile.gameProgression.collectedLegendaryParts.headCollected);
            legendaryTorso.SetActive(loadedFile.gameProgression.collectedLegendaryParts.torsoCollected);
            legendaryLeftArm.SetActive(loadedFile.gameProgression.collectedLegendaryParts.leftArmCollected);
            legendaryRightArm.SetActive(loadedFile.gameProgression.collectedLegendaryParts.rightArmCollected);
            legendaryLegs.SetActive(loadedFile.gameProgression.collectedLegendaryParts.legsCollected);
            GameManager.instance.gameFile = loadedFile;
        } else {
            playButton.interactable = true;
            deleteButton.interactable = false;
            monsterName.text = "-----";
            playTime.text = "-----";
            areaName.text = "-----";
            dateSaved.text = "-----";
            currentBalance.text = "MB$0";
            headSlot.SetActive(false);
            torsoSlot.SetActive(false);
            leftArmSlot.SetActive(false);
            rightArmSlot.SetActive(false);
            legSlot.SetActive(false);
            leftWeapon.enabled = false;
            rightWeapon.enabled = false;
            legendaryHead.SetActive(false);
            legendaryTorso.SetActive(false);
            legendaryLeftArm.SetActive(false);
            legendaryRightArm.SetActive(false);
            legendaryLegs.SetActive(false);
            GameManager.instance.gameFile.fileID = -1;
        }
    }

    public void PlayPressed() {
        loadingPanel.SetActive(true);
        if (GameManager.instance.gameFile.fileID == -1) {
            //Create a new save, and head to the monster maker so the player can make their first monster!
            GameManager.instance.CreateSave();
            SceneManager.LoadScene("MonsterMaker");
        } else if(GameManager.instance.gameFile.fileID > 0 && (
            GameManager.instance.gameFile.player.headPart.monster == ""
            || GameManager.instance.gameFile.player.torsoPart.monster == ""
            || GameManager.instance.gameFile.player.leftArmPart.monster == ""
            || GameManager.instance.gameFile.player.rightArmPart.monster == ""
            || GameManager.instance.gameFile.player.legsPart.monster == ""
            || GameManager.instance.gameFile.player.name == "")) {
            SceneManager.LoadScene("MonsterMaker");
        } else {
            GameManager.instance.LoadToLastNestUsed();
        }
    }

    public void DeletePressed() {
        if(GameManager.instance.gameFile.fileID != -1) {
            GameManager.instance.DeleteSave();
            ShowFile(GameManager.instance.fileNumber);
        }
    }
}
