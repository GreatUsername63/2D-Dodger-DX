using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public float scoreTime = 0f;
    SpawnManager spawnManager;
    public bool isGameStarted;
    public bool isGameActive;
    public bool isGameOver;
    [SerializeField] GameObject gameoverAssets;
    Animator gameoverAnimator;

    //Transition
    [Header("Transition")]
    public Animator transition;
    //Text
    [Header("Text")]
    [SerializeField] GameObject scoreTextGameobject;
    [SerializeField] GameObject startTextGameobject;
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverMessage;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    //Player references
    [Header("Player references")]
    [SerializeField] GameObject playerBlue;
    [SerializeField] GameObject playerRed;

    //Audio
    [Header("Audio and rhythm")]
    AudioManager audioManager;
    public float bpm = 180.738f;
    float beatTime;
    public float offset = 0;
    float songPosition = 0;
    float dspTimeSong;
    public float timeBeforeSongStart = 2f;
    bool countDownOver = false;
    bool rhythmInitialized = false;

    //Singleton
    public static GameManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = SpawnManager.Instance;
        gameoverAnimator = gameoverAssets.GetComponent<Animator>();
        audioManager = AudioManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted)
        {
            gameStart();
        }
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(ReloadGame());
            }
            return;
        }
        if (!isGameActive)
        {
            return;
        }
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreTime += Time.deltaTime;
        scoreText.text = "Survival time " + FormatScore(scoreTime);
    }

    //Converts floating point scoreTime to Minute:Second string
    string FormatScore(float scoreToFormat)
    {
        int roundedSeconds = (int)Mathf.Round(scoreToFormat);
        int minutes = (int)Mathf.Floor(roundedSeconds / 60);
        int seconds = roundedSeconds % 60;

        string formatedString = string.Format("{0:00}:{1:00}", minutes, seconds);
        return formatedString;
    }

    void gameStart()
    {
        //rhythmic startup
        if (!countDownOver)
        {
            timeBeforeSongStart -= Time.deltaTime;
            startText.text = "Get Ready!";
            //rhythm
            if (timeBeforeSongStart <= 0 && !rhythmInitialized)
            {
                audioManager.PlayGameTheme();
                dspTimeSong = (float)AudioSettings.dspTime;
                beatTime = 60 / bpm;
                rhythmInitialized = true;
            }
            //Each beat
            if (timeBeforeSongStart < 0)
            {
                songPosition = (float)(AudioSettings.dspTime - dspTimeSong) - offset;
            }
            if (songPosition < beatTime * 1)
            {
                startText.text = "3";
            }
            if (songPosition > beatTime * 1 && songPosition < beatTime * 2)
            {
                startText.text = "2";
            }
            if (songPosition > beatTime * 2 && songPosition < beatTime * 3)
            {
                startText.text = "1";
            }
            if (songPosition > beatTime * 3)
            {
                startText.text = "Go!";
            }
            if (songPosition > beatTime * 4)
            {
                countDownOver = true;
            }
            //Skip
            if (Input.GetKeyDown(KeyCode.Space))
            {
                audioManager.PlayGameThemeSkipIntro();
                countDownOver = true;
            }
            return;
        }
        isGameStarted = true;
        isGameActive = true;
        scoreTextGameobject.SetActive(true);
        startTextGameobject.SetActive(false);
    }

    public void GameOver()
    {
        isGameActive = false;
        isGameOver = true;
        spawnManager.isSpawnerActive = false;
        gameoverAssets.SetActive(true);
        gameoverAnimator.SetBool("gameoverTransition", true);
        audioManager.StopGameMusic();
        scoreTextGameobject.SetActive(false);
        finalScoreText.text = "Your time: " + FormatScore(scoreTime);
        compareScores();
        setGameoverMessage();
    }

    void setGameoverMessage()
    {
        gameOverMessage.text = "You've lost on purpose didn't you";
        if (scoreTime > 1) gameOverMessage.text = "Yep, this is going to be hard";
        if (scoreTime > 10) gameOverMessage.text = "Let's go you can do it!";
        if (scoreTime > 70) gameOverMessage.text = "You've pretty much destroyed my score at this point";
        if (scoreTime > 180) gameOverMessage.text = "You're  real 2d master";
        if (scoreTime > 240) gameOverMessage.text = "No way can you keep playing";
        if (scoreTime > 360) gameOverMessage.text = "HOW?????????????";
    }

    //Ship: false equals X grabbed the powerup, true equals Y
    public void startInvencibility(bool ship)
    {
        playerBlue.GetComponent<Invecibility>().isActive = true;
        playerRed.GetComponent<Invecibility>().isActive = true;
    }

    IEnumerator ReloadGame()
    {
        transition.SetBool("Start", true);

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void compareScores()
    {
        float latestHighScore = PlayerPrefs.GetFloat("HighScore", 0);
        highScoreText.text = "Previous best score:" + FormatScore(latestHighScore);

        if (scoreTime > latestHighScore)
        {
            PlayerPrefs.SetFloat("HighScore", scoreTime);
            highScoreText.color = new Color32(255, 236, 39, 255);
            highScoreText.text = "!!!!!NEW HIGHSCORE!!!!!";
        }
    }
}
