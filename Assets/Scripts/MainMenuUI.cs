using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPnl;
    [SerializeField] private GameObject howToPlayPnl;
    [SerializeField] private GameObject creditsPnl;
    [SerializeField] private GameObject settingsPnl;
    [SerializeField] private Button playBtn;
    [SerializeField] private Button howToPlayBtn;
    [SerializeField] private Button creditsBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button howToPlayBackBtn;
    [SerializeField] private Button creditsBackBtn;
    [SerializeField] private Button settingsBackBtn;
    [SerializeField] private Button resetHighScoreBtn;
    [SerializeField] private Animator mainMenuAnimator;
    [SerializeField] private Animator howToPlayAnimator;
    [SerializeField] private Animator creditsAnimator;
    [SerializeField] private Animator settingsAnimator;
    [SerializeField] private GameObject fadePnl;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip btnClickAudio;

    // Start is called before the first frame update
    void Start()
    {
        fadePnl.GetComponent<Animator>().SetTrigger("FadeOut");
        mainMenuPnl.SetActive(true);
        howToPlayPnl.SetActive(false);
        creditsPnl.SetActive(false);
        settingsPnl.SetActive(false);
        settingsBackBtn.interactable = false;
        resetHighScoreBtn.interactable = false;
    }

    public void playBtnClicked()
    {
        fadePnl.GetComponent<Animator>().SetTrigger("FadeIn");
        GameManager.setLevelNum(1);
        GameManager.setTargetScore();
        GameManager.setTimer(0);
        //SceneManager.LoadScene("GamePlayScene");
        StartCoroutine("loadLevelWithDelay");
    }

    private IEnumerator loadLevelWithDelay()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GamePlayScene");
    }

    public void howToPlayBtnClicked()
    {
        mainMenuPnl.SetActive(false);
        howToPlayPnl.SetActive(true);
    }
    public void creditsBtnClicked()
    {
        mainMenuPnl.SetActive(false);
        creditsPnl.SetActive(true);
    }

    public void settingsBtnClicked()
    {
        StartCoroutine(disablingMainMenuPnl());
        StartCoroutine(settingsBtnActions());
    }
    public void settingsBackBtnClicked()
    {
        StartCoroutine(settingsBackBtnActions());
        StartCoroutine(enablingMainMenuPnl());
    }

    public IEnumerator settingsBtnActions()
    {
        //mainMenuPnl.SetActive(false);
        //settingsBtn.interactable = false;
        settingsPnl.SetActive(true);
        settingsAnimator.SetTrigger("SlideIn");
        yield return new WaitForSeconds(1);
        settingsBackBtn.interactable = true;
        resetHighScoreBtn.interactable = true;
    }
    
    public IEnumerator settingsBackBtnActions()
    {
        settingsBackBtn.interactable = false;
        resetHighScoreBtn.interactable = false;
        settingsAnimator.SetTrigger("SlideOut");
        //mainMenuPnl.SetActive(true);
        yield return new WaitForSeconds(1);
        //settingsBtn.interactable = true;
        settingsPnl.SetActive(false);
    }

    public IEnumerator disablingMainMenuPnl()
    {
        playBtn.interactable = false;
        howToPlayBtn.interactable = false;
        creditsBtn.interactable = false;
        settingsBtn.interactable = false;
        mainMenuAnimator.SetTrigger("SlideDown");
        yield return new WaitForSeconds(1);
        mainMenuPnl.SetActive(false);
    }
    public IEnumerator enablingMainMenuPnl()
    {
        mainMenuPnl.SetActive(true);
        mainMenuAnimator.SetTrigger("SlideUp");
        yield return new WaitForSeconds(1);
        playBtn.interactable = true;
        howToPlayBtn.interactable = true;
        creditsBtn.interactable = true;
        settingsBtn.interactable = true;
    }

    public void backBtnClicked()
    {
        howToPlayPnl.SetActive(false);
        creditsPnl.SetActive(false);
        settingsPnl.SetActive(false);
        mainMenuPnl.SetActive(true);
    }


    public void resetHighScoreBtnClicked()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }

    public void btnClickAudioPlay()
    {
        audioSource.PlayOneShot(btnClickAudio);
    }



    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
