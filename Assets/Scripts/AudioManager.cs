using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip gameTheme;
    [SerializeField] AudioClip invencibilityTheme;
    [SerializeField] AudioClip gameoverSound;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = gameTheme;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayInvencibilityTheme(){
        audioSource.Stop();
        audioSource.clip = invencibilityTheme;
        audioSource.Play();
    }

    public void PlayGameTheme(){
        audioSource.Stop();
        audioSource.clip = gameTheme;
        audioSource.Play();
    }

    public void StopGameMusic(){
        audioSource.Stop();
        audioSource.PlayOneShot(gameoverSound);
    }
}
