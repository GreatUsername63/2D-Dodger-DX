using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] AudioClip gameTheme;
    [SerializeField] AudioClip invencibilityTheme;
    [SerializeField] AudioClip gameoverSound;
    public float bpm = 180.738f;
    float beatTime;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        beatTime = 60 / bpm;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            if (audioSource.clip == gameTheme)
            {
                audioSource.time = beatTime * 4;
            }
        }
    }

    public void PlayInvencibilityTheme()
    {
        audioSource.Stop();
        audioSource.clip = invencibilityTheme;
        audioSource.Play();
    }

    public void PlayGameTheme()
    {
        audioSource.Stop();
        audioSource.clip = gameTheme;
        audioSource.Play();
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
        audioSource.Stop();
        audioSource.PlayOneShot(gameoverSound);
    }
}
