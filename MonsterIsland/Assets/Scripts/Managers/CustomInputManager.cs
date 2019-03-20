using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputTypes {
    Primary, Secondary, Left, Right, Jump, Interact, Torso, Head, Pause
}

public class CustomInputManager : MonoBehaviour {

    public static CustomInputManager Instance;
    private static KeyCode PrimaryInput;
    private static KeyCode SecondaryInput;
    private static KeyCode LeftInput;
    private static KeyCode RightInput;
    private static KeyCode JumpInput;
    private static KeyCode InteractInput;
    private static KeyCode TorsoInput;
    private static KeyCode HeadInput;
    private static KeyCode PauseInput;

	// Use this for initialization
	void Start () {
        if(Instance == null) {
            Instance = this;
            PrimaryInput = KeyCode.Mouse0;
            SecondaryInput = KeyCode.Mouse1;
            LeftInput = KeyCode.A;
            RightInput = KeyCode.D;
            JumpInput = KeyCode.Space;
            InteractInput = KeyCode.W;
            TorsoInput = KeyCode.F;
            HeadInput = KeyCode.E;
            PauseInput = KeyCode.Escape;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
	}
	


	// Update is called once per frame
	void Update () {
		
	}
}
