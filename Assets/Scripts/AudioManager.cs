using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    bool isGameOver;
    public AudioSource audioSource;
    [SerializeField] AudioClip gameTheme;
    [SerializeField] AudioClip invencibilityTheme;
    [SerializeField] AudioClip gameoverSound;
    [SerializeField] AudioClip smallExplosionSound;
    public float bpm = 180.738f;
    float beatTime;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        beatTime = 60 / bpm;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && !isGameOver)
        {
            audioSource.Play();
            if (audioSource.clip == gameTheme)
            {
                audioSource.time = beatTime * 4;
            }
            if (audioSource.clip == invencibilityTheme)
            {
                audioSource.time = 0;
            }
        }
    }

    public void PlayInvencibilityTheme()
    {
        audioSource.Stop();
        audioSource.clip = invencibilityTheme;
        audioSource.Play();
        audioSource.time = 0;
    }

    public void PlayGameTheme()
    {
        audioSource.Stop();
        audioSource.clip = gameTheme;
        audioSource.Play();
        audioSource.time = 0;
    }

    public void PlayGameThemeSkipIntro()
    {
        audioSource.Stop();
        audioSource.clip = gameTheme;
        audioSource.Play();
        audioSource.time = beatTime * 4;
    }

    public void StopGameMusic()
    {
        isGameOver = true;
        audioSource.Stop();
        audioSource.PlayOneShot(gameoverSound);
    }

    public void PlaySmallExplosionSound()
    {
        audioSource.PlayOneShot(smallExplosionSound);
    }
}
