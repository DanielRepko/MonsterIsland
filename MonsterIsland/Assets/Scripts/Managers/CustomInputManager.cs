using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InputType {
    Primary, Secondary, Left, Right, Jump, Interact, Torso, Head, Pause
}

public class CustomInputManager : MonoBehaviour {

    public static CustomInputManager Instance;
    public static Dictionary<InputType, KeyCode> InputKeys = new Dictionary<InputType, KeyCode>();

    private GameObject currentKey;

	// Use this for initialization
	void Awake() {
        if(Instance == null) {
            Instance = this;
            InputKeys.Add(InputType.Primary, (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(InputType.Primary.ToString(), "Mouse0")));
            InputKeys.Add(InputType.Secondary, (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(InputType.Secondary.ToString(), "Mouse1")));
            InputKeys.Add(InputType.Left, (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(InputType.Left.ToString(), "A")));
            InputKeys.Add(InputType.Right, (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(InputType.Right.ToString(), "D")));
            InputKeys.Add(InputType.Jump, (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(InputType.Jump.ToString(), "Space")));
            InputKeys.Add(InputType.Interact, (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(InputType.Interact.ToString(), "W")));
            InputKeys.Add(InputType.Torso, (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(InputType.Torso.ToString(), "F")));
            InputKeys.Add(InputType.Head, (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(InputType.Head.ToString(), "E")));
            InputKeys.Add(InputType.Pause, (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(InputType.Pause.ToString(), "Escape")));
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

                //Before doing anything else, check if that key is already assigned. If so, refresh the gui and abort the function
                if(IsKeyAlreadySet(e.keyCode)) {
                    RefreshGUI();
                    return;
                }

                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();

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

    public void RefreshGUI() {
        SettingsManager.Instance.primaryButtonText.GetComponent<Text>().text = InputKeys[InputType.Primary].ToString();
        SettingsManager.Instance.secondaryButtonText.GetComponent<Text>().text = InputKeys[InputType.Secondary].ToString();
        SettingsManager.Instance.leftButtonText.GetComponent<Text>().text = InputKeys[InputType.Left].ToString();
        SettingsManager.Instance.rightButtonText.GetComponent<Text>().text = InputKeys[InputType.Right].ToString();
        SettingsManager.Instance.jumpButtonText.GetComponent<Text>().text = InputKeys[InputType.Jump].ToString();
        SettingsManager.Instance.interactButtonText.GetComponent<Text>().text = InputKeys[InputType.Interact].ToString();
        SettingsManager.Instance.torsoButtonText.GetComponent<Text>().text = InputKeys[InputType.Torso].ToString();
        SettingsManager.Instance.headButtonText.GetComponent<Text>().text = InputKeys[InputType.Head].ToString();
        currentKey = null;
    }

    private bool IsKeyAlreadySet(KeyCode inputKeyCode) {
        foreach(KeyCode existingKeyCode in InputKeys.Values) {
            if(inputKeyCode == existingKeyCode) {
                return true;
            }
        }
        return false;
    }
}
