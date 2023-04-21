using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject exitButton;
    [SerializeField] GameObject restartButton;
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
        restartButton.SetActive(false);
        crosshair.SetActive(false);
    }

    public void startGame()
    {
        started = true;
        PlayerController.Instance.setGameStarted(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerController.Instance.mouseSensitivity = 3f;
        exitButton.SetActive(false);
        startButton.SetActive(false);
        TargetShooter.Instance.setCrosshair(true);
    }


    public void endGame()
    {
        started = false;
        PlayerController.Instance.setGameStarted(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        PlayerController.Instance.mouseSensitivity = 0;
        exitButton.SetActive(true);
    }

    public void restartGame()
    {
        exitButton.SetActive(false);
        restartButton.SetActive(false);
        PlayerController.Instance.mouseSensitivity = 3f;
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
        restartButton.SetActive(false);
        crosshair.SetActive(true);
        restartGame();
    }

    public void exitGame()
    {
        Application.Quit();
    }

}