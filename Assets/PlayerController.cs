using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cameraHolder;
    [SerializeField] public float mouseSensitivity = 3f;

    public bool gameStarted;
    float verticalLookRotation;

    public static PlayerController Instance;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
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
            transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
            verticalLookRotation -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
            cameraHolder.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
        }
    }
}
