using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject tapCountText;
    [SerializeField] private Text countDownText;
    [SerializeField] private Text targetScoreText;
    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private GameObject gamePlayPnl;
    [SerializeField] private GameObject pausePnl;
    [SerializeField] private GameObject gameOverPnl;
    [SerializeField] private GameObject winPnl;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip btnClickAudio;
    [SerializeField] private AudioClip[] tapAudios;
    [SerializeField] private AudioClip[] countDownAudios;
    [SerializeField] private Animator fadeAnimator;
    private int countDownAudioIndex = 2;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        fadeAnimator.SetTrigger("FadeOut");
        StartCoroutine("firstFrame");
    }

    private IEnumerator firstFrame()
    {
        gamePlayPnl.SetActive(true);
        pausePnl.SetActive(false);
        gameOverPnl.SetActive(false);
        winPnl.SetActive(false);
        //tapCountText.SetActive(false);
        //timerText.gameObject.SetActive(false);
        //countDownText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        countDownAudioPlay();
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void setTapCountText(int score)
    {
        tapCountText.GetComponent<Text>().text = "Tap Count:\n" + score;
    }

    public void setTimerText(float timer)
    {
        timerText.text = "Timer: " + Math.Ceiling(timer);
    }
    
    public void setCountDownText(float countDown)
    {
        countDownText.text = "Count Down:\n" + Math.Ceiling(countDown);
    }

    public void setTargetScoreText(int highScore)
    {
        targetScoreText.text = "Target Score: " + highScore;
    }
    
    public void setHighScoreText(int highScore)
    {
        highScoreText.text = "High Score: " + highScore;
    }


    //Setting Active Property of
    //countDownText, timerText, tapCountText, targetScoreText;
    public void settingSomeTextGameObjects()
    {
        countDownText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);
        tapCountText.SetActive(true);
        targetScoreText.gameObject.SetActive(true);
    }

    public void gameFinished(bool hasWon)
    {
        if(hasWon)
        {
            pauseBtn.SetActive(false);
            winPnl.SetActive(true);
        }
        else
        {
            pauseBtn.SetActive(false);
            gameOverPnl.SetActive(true);
        }
    }

    public void pauseBtnClicked()
    {
        Time.timeScale = 0;
        pauseBtn.SetActive(false);
        pausePnl.SetActive(true);
        isPaused = true;
    }
    public void resumeBtnClicked()
    {
        pausePnl.SetActive(false);
        pauseBtn.SetActive(true);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void retryBtnClicked()
    {
        fadeAnimator.SetTrigger("FadeIn");
        StartCoroutine(loadLevelWithDelay());
    }

    public void nextLevelBtnClicked()
    {
        fadeAnimator.SetTrigger("FadeIn");
        GameManager.setLevelNum(GameManager.getLevelNum() + 1);
        GameManager.setTargetScore();
        GameManager.setTimer(GameManager.getLevelNum());
        StartCoroutine(loadLevelWithDelay());
    }

    private IEnumerator loadLevelWithDelay()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    public void mainMenuBtnClicked()
    {
        fadeAnimator.SetTrigger("FadeIn");
        StartCoroutine(loadMainMenuWithDelay());
    }

    private IEnumerator loadMainMenuWithDelay()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    public void btnClickAudioPlay()
    {
        audioSource.PlayOneShot(btnClickAudio);
    }
    public void tapAudiosPlay()
    {
        int i = UnityEngine.Random.Range(0, tapAudios.Length);
        audioSource.PlayOneShot(tapAudios[i]);
    }

    public void countDownAudioPlay()
    {
        if (countDownAudioIndex == -1) {}
        else
        {
            //float temp = audioSource.volume;
            //audioSource.volume = 1;
            audioSource.PlayOneShot(countDownAudios[countDownAudioIndex]);
            //audioSource.volume = temp;
            countDownAudioIndex--;
            Invoke("countDownAudioPlay", 1);
        }
    }

}
