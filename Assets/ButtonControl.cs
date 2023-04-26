using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    [SerializeField] public GameObject exitButton;
    [SerializeField] public GameObject restartButton;
    [SerializeField] GameObject crosshair;

    public static ButtonControl Instance;
    public static bool started;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }



    void Start()
    {
        crosshair.SetActive(false);
    }

    public void startGame()
    {
        started = true;
        PlayerController.Instance.setGameStarted(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        exitButton.SetActive(false);
        startButton.SetActive(false);
        restartButton.SetActive(false);
        PlayerController.Instance.mouseSensitivityInput.gameObject.SetActive(false);
        TargetShooter.Instance.start();
    }

    public void endGame()
    {
        restartButton.SetActive(true);
        started = false;
        PlayerController.Instance.setGameStarted(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        exitButton.SetActive(true);
        restartButton.SetActive(true);
        PlayerController.Instance.mouseSensitivityInput.gameObject.SetActive(true);
    }

    public void restartGame()
    {
        exitButton.SetActive(false);
        restartButton.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        TargetShooter.Instance.start();
        startGame();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        TargetShooter.Instance.hitCount = 0;
        TargetShooter.Instance.countdownTimer = 30f;
        TargetShooter.Instance.scoreText.SetText("");
        TargetShooter.Instance.hitCountText.SetText("Hits: 0");
        crosshair.SetActive(true);
        restartGame();
    }

    public void exitGame()
    {
        Application.Quit();
    }

}