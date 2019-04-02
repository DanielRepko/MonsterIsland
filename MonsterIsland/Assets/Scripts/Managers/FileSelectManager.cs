using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FileSelectManager : MonoBehaviour {

    public Button playButton;
    public Button deleteButton;
    public GameObject heartGroup;
    public GameObject[] heartImages;
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
        GameManager.instance.FileNumber = fileNumber;
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
            heartGroup.SetActive(true);

            //Current Hearts
            var i = loadedFile.player.totalHearts;
            foreach(var heart in heartImages) {
                if(i > 0) {
                    heart.SetActive(true);
                    i--;
                } else {
                    heart.SetActive(false);
                }
            }

            //General Information
            monsterName.text = loadedFile.player.name;
            playTime.text = loadedFile.totalPlayTime.ToString();
            areaName.text = loadedFile.saveArea;
            dateSaved.text = loadedFile.saveDate;
            currentBalance.text = "MB$" + loadedFile.player.inventory.monsterBucks;

            //Current Parts and Weapons
            //TODO: When code to save the current parts and weapons is added, update this to load those parts and weapons
            headSlot.SetActive(true);
            torsoSlot.SetActive(true);
            leftArmSlot.SetActive(true);
            rightArmSlot.SetActive(true);
            legSlot.SetActive(true);
            leftWeapon.enabled = true;
            rightWeapon.enabled = true;

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
            heartGroup.SetActive(false);
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
            GameManager.instance.gameFile = null;
        }
    }
}
