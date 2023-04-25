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
    [SerializeField] GameObject restartButton;

    public static TargetShooter Instance;

    public bool countdownStarted = false;
    public float countdownTimer = 30f;
    public int hitCount = 0;
    public int shootCount = 0;
    private double accurancy;

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
        restartButton.SetActive(false);
        crosshair.SetActive(true);
        Cursor.visible = false;
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
        Time.timeScale = 0f; // Pause the game
        scoreText.SetText("Final score: " + hitCount.ToString());
        hitCountText.SetText("");
        restartButton.SetActive(true);
        crosshair.SetActive(false);
        Cursor.visible = true;
        countdownStarted = false;
        ButtonControl.Instance.endGame();
    }
}