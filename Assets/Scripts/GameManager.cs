using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GamePlayUI gamePlayUI;
    private int score = 0;
    private static int levelNum;
    private static int targetScore;
    private static int baseTargetScore = 10;
    private static float defaultTimer;
    private float timer;
    private static float baseTimer = 5;
    private int highScore;
    [SerializeField] private float countDown;
    private bool isGameEnded = false;
    private bool isCountDownEnded = false;
    private bool hasWon = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("firstFrame");
    }

    private IEnumerator firstFrame()
    {
        timer = defaultTimer;
        gamePlayUI.setTapCountText(score);
        gamePlayUI.setTimerText(timer);
        gamePlayUI.setCountDownText(countDown);
        gamePlayUI.setTargetScoreText(targetScore);
        loadHighScore();
        gamePlayUI.setHighScoreText(highScore);
        yield return new WaitForSeconds(1);
    }

    // Update is called once per frame
    void Update()
    {
        calculateCountDown();

        if(!isGameEnded && isCountDownEnded && !gamePlayUI.isPaused)
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    score++;
            //    gamePlayUI.setTapCount(score);
            //}
            detectTap();

            //timer -= Time.deltaTime;
            //if(timer<=0)
            //{
            //    timer = 0;
            //    isGameEnded = true;
            //}
            //gamePlayUI.setTimer(timer);
            calculateTimer();

            //if(isGameEnded)
            //{
            //    if (score >= targetScore)
            //        hasWon = true;
            //    else
            //        hasWon = false;
            
            //    gamePlayUI.gameFinished(hasWon);
            //    if(score>highScore)
            //    {
            //        saveHighScore();
            //    }
            //}
            calculateResults();
        }

    }



    private void detectTap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gamePlayUI.tapAudiosPlay();
            score++;
            gamePlayUI.setTapCountText(score);
        }
    }

    private void calculateTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
            isGameEnded = true;
        }
        gamePlayUI.setTimerText(timer);
    }
    
    private void calculateCountDown()
    {
        if(!isCountDownEnded)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                countDown = 0;
                isCountDownEnded = true;
                gamePlayUI.settingSomeTextGameObjects();
            }
            gamePlayUI.setCountDownText(countDown);
        }
    }

    private void calculateResults()
    {
        if (isGameEnded)
        {
            if (score >= targetScore)
                hasWon = true;
            else
                hasWon = false;

            gamePlayUI.gameFinished(hasWon);
            if (score > highScore)
            {
                saveHighScore();
            }
        }
    }

    private void saveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", score);
        loadHighScore();
        gamePlayUI.setHighScoreText(highScore);
    }
    
    private void loadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    public static void setLevelNum(int val)
    {
        levelNum = val;
    }

    public static int getLevelNum()
    {
        return levelNum;
    }

    public static void setTargetScore()
    {
        targetScore = getLevelNum() * baseTargetScore;
    }

    public static void setTimer(float val)
    {
        defaultTimer = baseTimer + val;
    }

}
