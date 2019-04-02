using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [Range(0, 3)]
    public int FileNumber;

    public PlayerController player;
    public GameObject coinPrefab;
    public GameObject headDropPrefab;
    public GameObject leftArmDropPrefab;
    public GameObject rightArmDropPrefab;
    public GameObject torsoDropPrefab;
    public GameObject legsDropPrefab;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SaveGame() {

    }
}
