using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cameraHolder;
    private float mouseSensitivity;
    [SerializeField] public TMP_InputField mouseSensitivityInput;

    public bool gameStarted;
    float verticalLookRotation;

    public static PlayerController Instance;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        mouseSensitivity = 1f;
        mouseSensitivityInput.onValueChanged.AddListener(ValidateInput);
    }

    private void ValidateInput(string value)
    {
        string newValue = "";
        foreach (char c in value)
        {
            if (char.IsDigit(c) || c == ',')
            {
                newValue += c;
            }
        }
        mouseSensitivityInput.text = newValue;
    }

    public void setGameStarted(bool val)
    {
        gameStarted = val;
        Debug.Log("SETGAME STARTED " + val);

        if (gameStarted)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        if (!ButtonControl.started)
        {
            return;
        }
        if (ButtonControl.started)
        {
            if (mouseSensitivityInput.text == null || mouseSensitivityInput.text == "")
            {
                mouseSensitivity = 1f;
            } else
            {
                mouseSensitivity = float.Parse(mouseSensitivityInput.text);
            }
            transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
            verticalLookRotation -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
            cameraHolder.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
        }
    }
}
