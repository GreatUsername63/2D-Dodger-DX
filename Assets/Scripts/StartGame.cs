using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    public Animator transition;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
            StartCoroutine(LoadGame());
        }
    }

    IEnumerator LoadGame(){
        transition.SetBool("Start",true);

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Mygame");
    }
}
