using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using static AdMob;
using static MyMethod;
using TMPro;

public class MainManager : MonoBehaviour
{
    public static MainManager main;


    [SerializeField] GameObject TitleScreen;
    [SerializeField] GameObject GameOverScreen;

    [SerializeField] TextMeshProUGUI bestScoreText;

    public GameObject moveObject { get; set; }

    public bool isStart { get; set; }
    public bool isOver { get; set; }

    public int bestScore { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        // AdMob.adMob.RequestBanner();

        if (main == null)
        {

            main = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }

        SE.se.PlayBGM(BGM.Home, true);
        LoadBestScore();

    }
    void Test()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }


    public void GameStart()
    {
        isStart = true;
        isOver = false;
        TitleScreen.SetActive(false);

        SE.se.PlayBGM(BGM.Battle, true);
        SE.se.PlayBGM(BGM.Home, false);

        adMob.HideBanner();
        adMob.HideLargeBanner();
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        isOver = true;
        isStart = false;

        // adMob.RequestBanner();
        adMob.RequestLargeBanner();
    }
    public void RestartButton()
    {
        GameStart();
        GameOverScreen.SetActive(false);

        adMob.HideBanner();
        adMob.HideLargeBanner();
        LoadBestScore();
    }
    public void HomeButton()
    {
        GameOverScreen.SetActive(false);
        TitleScreen.SetActive(true);
        SE.se.PlayBGM(BGM.Battle, false);
        SE.se.PlayBGM(BGM.Home, true);
        adMob.RequestBanner();
        adMob.HideLargeBanner();
        LoadBestScore();


    }

    void LoadBestScore()
    {
        bestScore = ES3.Load("best_score", defaultValue: 0);
        bestScoreText.text = TimeDisplay(bestScore);
    }

    public void Banner()
    {
        adMob.RequestBanner();
    }
    public void HideBanner()
    {
        adMob.HideBanner();
    }
    public void ShowBanner()
    {
        adMob.ShowBanner();
    }
    public void Interstitial()
    {
        adMob.ShowInterstitial();
    }
    public void Reward()
    {
        adMob.ShowRewardAd();
    }
}
