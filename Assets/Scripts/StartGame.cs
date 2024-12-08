using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    public Animator transition;

    public GameObject secret;
    public string secretString = "miprimerachamba";
    public int secretIndex = 0;

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

    void DetectSecretString()
    {
        string currentKey = secretString[secretIndex].ToString();
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(currentKey))
            {
                secretIndex++;
                Debug.Log(currentKey);
                if (secretIndex >= secretString.Length)
                {
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
