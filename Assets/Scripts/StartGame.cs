using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class StartGame : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip jingle;
    [SerializeField] AudioClip secretJingle;

    public Animator transition;

    public GameObject secret;
    public string secretString = "miprimerachamba";
    public int secretIndex = 0;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(LoadGame());
        }
        DetectSecretString();
    }

    IEnumerator LoadGame()
    {
        transition.SetBool("Start", true);

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Mygame");
    }

    public void PlayJingle()
    {
        audioSource.PlayOneShot(jingle);
    }

    void DetectSecretString()
    {
        string currentKey = secretString[secretIndex].ToString();
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(currentKey))
            {
                secretIndex++;
                if (secretIndex >= secretString.Length)
                {
                    audioSource.PlayOneShot(secretJingle);
                    secret.SetActive(true);
                    secretIndex = 0;
                }
            }
            else
            {
                secretIndex = 0;
            }
        }
    }
}
