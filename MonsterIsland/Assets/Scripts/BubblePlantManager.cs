using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePlantManager : MonoBehaviour {

    public List<GameObject> bubblePlants;
    public GameObject bubblePrefab;
    public float timeBetweenBubbles = 5f;
    private float timeSinceLastBubble;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceLastBubble += Time.deltaTime;
        if(timeSinceLastBubble >= timeBetweenBubbles) {
            foreach(GameObject bubblePlant in bubblePlants) {
                GameObject bubble = (GameObject)Instantiate(bubblePrefab,new Vector3(bubblePlant.transform.position.x, bubblePlant.transform.position.y), Quaternion.identity);
            }
            timeSinceLastBubble -= timeBetweenBubbles;
        }
	}
}
