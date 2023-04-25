using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class TargetShooter : MonoBehaviour
{
    [SerializeField] public TMP_Text hitCountText;
    [SerializeField] public TMP_Text accurancyText;
    [SerializeField] Camera cam;
    [SerializeField] public TMP_Text timerText;
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] GameObject crosshair;

    public static TargetShooter Instance;

    public bool countdownStarted = false;
    public float countdownTimer = 30f;
    public int hitCount = 0;
    public int shootCount = 0;
    private double accurancy;
    private bool isPaused = false;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }



    void Update()
    {
        if (!PlayerController.Instance.gameStarted) return;

        if (Input.GetMouseButtonDown(0))
        {
            shootCount++;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Target target = hit.collider.gameObject.GetComponent<Target>();
                if (target != null)
                {
                    target.Hit();
                    hitCount++;
                    hitCountText.SetText("Hits: " + hitCount.ToString());
                    if (!countdownStarted)
                    {
                        countdownStarted = true;
                        StartCoroutine(Countdown());
                    }
                }
            }
            Debug.Log("sC: " + shootCount);
            Debug.Log("hC: " + hitCount);
            accurancy = (double)hitCount / shootCount * 100;
            accurancyText.SetText("Accurancy: " + System.Math.Round(accurancy, 1) + "%");
            Debug.Log("acc: " + accurancy);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

    }

    public void setCrosshair(bool active)
    {
        crosshair.SetActive(active);
    }

    public void start()
    {
        hitCount = 0;
        shootCount = 0;
        countdownTimer = 30f;
        hitCountText.SetText("Hits: 0");
        accurancyText.SetText("Accurancy: 0%");
        Time.timeScale = 1f;
        scoreText.SetText("");
        crosshair.SetActive(true);
        Cursor.visible = false;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            crosshair.SetActive(false);
            ButtonControl.Instance.exitButton.SetActive(true);
            ButtonControl.Instance.restartButton.SetActive(true);
            PlayerController.Instance.setGameStarted(false);
            ButtonControl.started = false;
        }
        else
        {
            Time.timeScale = 1f;
            crosshair.SetActive(true);
            ButtonControl.Instance.exitButton.SetActive(false);
            ButtonControl.Instance.restartButton.SetActive(false);
            PlayerController.Instance.setGameStarted(true);
            ButtonControl.started = true;
        }
    }

    IEnumerator Countdown()
    {
        while (countdownTimer > 0)
        {
            yield return new WaitForSeconds(0.1f);
            countdownTimer -= 0.1f;
            timerText.SetText("Time remaining: " + countdownTimer.ToString("0.0"));
        }
        Debug.Log("Game over!");
        ButtonControl.Instance.endGame();
        Time.timeScale = 0f;
        scoreText.SetText("Final score: " + hitCount.ToString());
        hitCountText.SetText("");
        crosshair.SetActive(false);
        Cursor.visible = true;
        countdownStarted = false;
    }
}