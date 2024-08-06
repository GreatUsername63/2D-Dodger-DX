using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    float scoreTime = 0f;
    float startGameTimer = 2f;
    SpawnManager spawnManager;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverMessage;
    public bool isGameStarted;
    public bool isGameActive;
    public bool isGameOver;
    [SerializeField] GameObject gameoverAssets;
    Animator gameoverAnimator;
    public Animator transition;
    [SerializeField] GameObject scoreTextGameobject;
    [SerializeField] GameObject startTextGameobject;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    GameObject playerBlue;
    GameObject playerRed;
    AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        playerBlue = GameObject.Find("Player blue");
        playerRed = GameObject.Find("Player red");
        gameoverAnimator = gameoverAssets.GetComponent<Animator>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameStarted){
            gameStart();
        }
        if(isGameOver){
            if(Input.GetKeyDown(KeyCode.Space)){
                StartCoroutine(ReloadGame());
            }
            return;
        }
        if(!isGameActive){
            return;
        }
        UpdateScore();
    }

    void UpdateScore(){
        scoreTime += Time.deltaTime;
        scoreText.text = "Survival time " + FormatScore(scoreTime);
    }

    //Converts floating point scoreTime to Minute:Second string
    string FormatScore(float scoreToFormat){
        int roundedSeconds = (int)Mathf.Round(scoreToFormat);
        int minutes = (int)Mathf.Floor(roundedSeconds / 60);
        int seconds = roundedSeconds % 60;
        
        string formatedString = string.Format("{0:00}:{1:00}", minutes, seconds);
        return formatedString;
    }

    void gameStart(){
        //Do not run until timer reaches 0
        if(startGameTimer > 0){
            startGameTimer -= Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.Space)){
                startGameTimer = 0;
            }
            return;
        }
        isGameStarted = true;
        isGameActive = true;
        scoreTextGameobject.SetActive(true);
        startTextGameobject.SetActive(false);
    }

    public void GameOver(){
        isGameActive = false;
        isGameOver = true;
        spawnManager.isSpawnerActive = false;
        gameoverAssets.SetActive(true);
        gameoverAnimator.SetBool("gameoverTransition",true);
        audioManager.StopGameMusic();
        scoreTextGameobject.SetActive(false);
        finalScoreText.text = "Your time: " + FormatScore(scoreTime);
        compareScores();
        setGameoverMessage();
    }

    void setGameoverMessage(){
        gameOverMessage.text = "You've lost on purpose didn't you";
        if (scoreTime > 2) gameOverMessage.text = "Yep, this is going to be hard";
        if (scoreTime > 10) gameOverMessage.text = "Let's go you can do it!"; 
        if (scoreTime > 30) gameOverMessage.text = "You've pretty much destroyed my score at this point";
        if (scoreTime > 60) gameOverMessage.text = "You're  real 2d master"; 
        if (scoreTime > 120) gameOverMessage.text = "No way can you keep playing"; 
        if (scoreTime > 180) gameOverMessage.text = "HOW?????????????";
    }

    //Ship: false equals X grabbed the powerup, true equals Y
    public void startInvencibility(bool ship){
        playerBlue.GetComponent<Invecibility>().isActive = true;
        playerRed.GetComponent<Invecibility>().isActive = true;
    }

    IEnumerator ReloadGame(){
        transition.SetBool("Start",true);

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void compareScores(){
        float latestHighScore = PlayerPrefs.GetFloat("HighScore", 0);
        highScoreText.text = "Previous best score:" + FormatScore(latestHighScore);

        if(scoreTime > latestHighScore){
            PlayerPrefs.SetFloat("HighScore",scoreTime);
            highScoreText.color = new Color32(255,236,39,255);
            highScoreText.text = "!!!!!NEW HIGHSCORE!!!!!";
        }
    }
}
