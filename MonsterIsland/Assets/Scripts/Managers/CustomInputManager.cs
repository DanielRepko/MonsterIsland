using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InputType {
    Primary, Secondary, Left, Right, Jump, Interact, Torso, Head, Pause
}

public class CustomInputManager : MonoBehaviour {

    public static CustomInputManager Instance;
    private static Dictionary<InputType, KeyCode> InputKeys = new Dictionary<InputType, KeyCode>();

    private GameObject currentKey;

	// Use this for initialization
	void Start () {
        if(Instance == null) {
            Instance = this;
            InputKeys.Add(InputType.Primary, KeyCode.Mouse0);
            InputKeys.Add(InputType.Secondary, KeyCode.Mouse1);
            InputKeys.Add(InputType.Left, KeyCode.A);
            InputKeys.Add(InputType.Right, KeyCode.D);
            InputKeys.Add(InputType.Jump, KeyCode.Space);
            InputKeys.Add(InputType.Interact, KeyCode.W);
            InputKeys.Add(InputType.Torso, KeyCode.F);
            InputKeys.Add(InputType.Head, KeyCode.E);
            InputKeys.Add(InputType.Pause, KeyCode.Escape);
        } else if (Instance != this) {
            Destroy(gameObject);
        }
	}
	
    public KeyCode GetInputKey(InputType inputType) {
        return InputKeys[inputType];
    }

    private void SetInputKey(InputType inputType, KeyCode key) {
        if(inputType != InputType.Pause) {
            InputKeys[inputType] = key;
        } else {
            Debug.LogError("Pause cannot be rebound");
            return;
        }
    }

    public void ModifyKey(GameObject buttonClicked) {
        if (currentKey == null) {
            buttonClicked.transform.GetChild(0).GetComponent<Text>().text = "???";
            currentKey = buttonClicked;
        }
    }

    private void OnGUI() {
        if (currentKey != null) {
            Event e = Event.current;
            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                e.keyCode = KeyCode.Mouse0;
            }

            if(Input.GetKeyDown(KeyCode.Mouse1)) {
                e.keyCode = KeyCode.Mouse1;
            }

            if (e.isKey || e.keyCode == KeyCode.Mouse0 || e.keyCode == KeyCode.Mouse1) {

                if (e.keyCode == KeyCode.Mouse0) {
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = "Left Mouse";
                } else if (e.keyCode == KeyCode.Mouse1) {
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = "Right Mouse";
                } else {
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                }

                switch (currentKey.name) {
                    case "PrimaryButton":
                        SetInputKey(InputType.Primary, e.keyCode);
                        currentKey = null;
                        return;
                    case "SecondaryButton":
                        SetInputKey(InputType.Secondary, e.keyCode);
                        currentKey = null;
                        return;
                    case "LeftButton":
                        SetInputKey(InputType.Left, e.keyCode);
                        currentKey = null;
                        return;
                    case "RightButton":
                        SetInputKey(InputType.Right, e.keyCode);
                        currentKey = null;
                        return;
                    case "JumpButton":
                        SetInputKey(InputType.Jump, e.keyCode);
                        currentKey = null;
                        return;
                    case "InteractButton":
                        SetInputKey(InputType.Interact, e.keyCode);
                        currentKey = null;
                        return;
                    case "TorsoButton":
                        SetInputKey(InputType.Torso, e.keyCode);
                        currentKey = null;
                        return;
                    case "HeadButton":
                        SetInputKey(InputType.Head, e.keyCode);
                        currentKey = null;
                        return;
                    default:
                        Debug.LogError("Invalid button name");
                        currentKey = null;
                        return;
                }
            }
        }
    }
}
