using TMPro;
using UnityEngine;

public class KeyBindingUIManager : MonoBehaviour
{
    public TMP_InputField jumpKeyInputField;
    public TMP_InputField slideKeyInputField;
    public TMP_InputField runKeyInputField;
    public TMP_InputField leftKeyInputField;
    public TMP_InputField rightKeyInputField;

    private KeyBindingManager keyBindingManager;

    private void Start()
    {
        keyBindingManager = KeyBindingManager.Instance;

        // 초기값 설정
        jumpKeyInputField.text = keyBindingManager.jumpKeyCode.ToString();
        slideKeyInputField.text = keyBindingManager.slideKeyCode.ToString();
        runKeyInputField.text = keyBindingManager.runKeyCode.ToString();
        leftKeyInputField.text = keyBindingManager.leftKeyCode.ToString();
        rightKeyInputField.text = keyBindingManager.rightKeyCode.ToString();

        jumpKeyInputField.onEndEdit.AddListener(SetJumpKey);
        slideKeyInputField.onEndEdit.AddListener(SetSlideKey);
        //runKeyInputField.onEndEdit.AddListener(SetRunKey);
        leftKeyInputField.onEndEdit.AddListener(SetLeftKey);
        rightKeyInputField.onEndEdit.AddListener(SetRightKey);
    }

    private KeyCode GetKeyCodeFromName(string keyName)
    {
        KeyCode keyCode;
        string keyNameUpper = keyName.ToUpper();
        if (System.Enum.TryParse(keyNameUpper, out keyCode) && System.Enum.IsDefined(typeof(KeyCode), keyCode))
        {
            return keyCode;
        }
        Debug.LogWarning($"Invalid key name: {keyName}. Defaulting to KeyCode.None.");
        return KeyCode.None;
    }


    private void SetJumpKey(string key)
    {
        KeyCode newKey = GetKeyCodeFromName(key);
        if (newKey != KeyCode.None)
        {
            keyBindingManager.jumpKeyCode = newKey;
            keyBindingManager.SaveKeyBindings();
        }
        else
        {
            jumpKeyInputField.text = keyBindingManager.jumpKeyCode.ToString();
        }
    }

    private void SetSlideKey(string key)
    {
        KeyCode newKey = GetKeyCodeFromName(key);
        if (newKey != KeyCode.None)
        {
            keyBindingManager.slideKeyCode = newKey;
            keyBindingManager.SaveKeyBindings();
        }
        else
        {
            slideKeyInputField.text = keyBindingManager.slideKeyCode.ToString();
        }
    }

/*    private void SetRunKey(string key)
    {
        KeyCode newKey = GetKeyCodeFromName(key);
        if (newKey != KeyCode.None)
        {
            keyBindingManager.runKeyCode = newKey;
            keyBindingManager.SaveKeyBindings();
        }
        else
        {
            runKeyInputField.text = keyBindingManager.runKeyCode.ToString();
        }
    }*/

    private void SetLeftKey(string key)
    {
        KeyCode newKey = GetKeyCodeFromName(key);
        if (newKey != KeyCode.None)
        {
            keyBindingManager.leftKeyCode = newKey;
            keyBindingManager.SaveKeyBindings();
        }
        else
        {
            leftKeyInputField.text = keyBindingManager.leftKeyCode.ToString();
        }
    }

    private void SetRightKey(string key)
    {
        KeyCode newKey = GetKeyCodeFromName(key);
        if (newKey != KeyCode.None)
        {
            keyBindingManager.rightKeyCode = newKey;
            keyBindingManager.SaveKeyBindings();
        }
        else
        {
            rightKeyInputField.text = keyBindingManager.rightKeyCode.ToString();
        }
    }
}
