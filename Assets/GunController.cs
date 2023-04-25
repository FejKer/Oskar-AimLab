using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public AudioClip gunshotSound;
    private AudioSource audioSource;
    public float recoilAmount = 0.1f;
    public float recoilRecoveryTime = 0.1f;
    private Vector3 originalPosition;
    public ParticleSystem muzzleFlash;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = gunshotSound;
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (!PlayerController.Instance.gameStarted) return;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            StartCoroutine(Recoil());
        }
    }

    void Shoot()
    {
        audioSource.Play();
        muzzleFlash.Play();
    }

    IEnumerator Recoil()
    {
        float elapsedTime = 0;
        Vector3 recoilPosition = transform.localPosition - new Vector3(0, 0, recoilAmount);
        Vector3 startPosition = transform.localPosition;

        while (elapsedTime < recoilRecoveryTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / recoilRecoveryTime;
            transform.localPosition = Vector3.Lerp(startPosition, recoilPosition, t);
            yield return null;
        }

        elapsedTime = 0;
        startPosition = transform.localPosition;

        while (elapsedTime < recoilRecoveryTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / recoilRecoveryTime;
            transform.localPosition = Vector3.Lerp(startPosition, originalPosition, t);
            yield return null;
        }
    }
}
