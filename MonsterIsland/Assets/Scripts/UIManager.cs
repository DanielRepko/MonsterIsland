using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;
    public GameObject airMeter;
    public GameObject airMeterBar;

	// Use this for initialization
	void Start () {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    //When called, updates the air meter based on the provided information
    public void UpdateAirMeter(float air, bool isUnderwater) {
        airMeterBar.GetComponent<RectTransform>().offsetMax = new Vector2(-(122f - (122f * air)), 0f);
        airMeter.SetActive(isUnderwater);
    }
}
