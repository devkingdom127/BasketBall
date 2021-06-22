using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartUIController : MonoBehaviour
{
    public GameObject settingLayer;
    public GameObject ballLayer;
    public GameObject googleLayer;
    public GameObject facebookLayer;
    public static StartUIController s_instance;

    void Awake()
    {
        s_instance = this;   
    }

    void Start()
    {
        Time.timeScale = BSKGameConfig.s_Instance.gameTimeScale;
        if (BSKGameConfig.s_Instance.IsBMGOn())
        {
            BSKBmgController.s_Instance.PlayOutGame();
        }
        GameSDKManager.s_instance.ShowBannerToTop(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameSDKManager.s_instance.ExitGame();
        }
    }

    //开始按钮
    public void playBtnCallback()
    {
        BSKSoundController.data.playButton();
        Yodo1AdsTest.instance.showInterstitialAd();
        SceneManager.LoadScene("LoadingScene");
    }

    //购买篮球按钮
    public void ballsBtnCallback()
    {

        BSKSoundController.data.playButton();
        Yodo1AdsTest.instance.ShowInterstitial();

        SceneManager.LoadScene("BallScene");
    }

    //more game
    public void moreGameBtnCallback()
    {
        BSKSoundController.data.playButton();
        GameSDKManager.s_instance.ShowOffer();
    }

    //抽奖
    public void spinBtnCallback()
    {
        BSKSoundController.data.playButton();
    }

    //设置
    public void settingBtnCallback()
    {
        Yodo1AdsTest.instance.ShowInterstitial();

        BSKSoundController.data.playButton();
        settingLayer.SetActive(true);
    }

    //分享
    public void shareBtnCallback()
    {
        BSKSoundController.data.playButton();
        GameSDKManager.s_instance.GoogleShare();
    }

    //点赞
    public void likeBtnCallback()
    {
        BSKSoundController.data.playButton();
        GameSDKManager.s_instance.GoogleLike();
    }

    //排行榜
    public void rankBtnCallback()
    {
        BSKSoundController.data.playButton();
        GameSDKManager.s_instance.GoogleLB();
    }

    public void ShowGoogleDialog()
    {
        googleLayer.SetActive(true);
    }

    public void ShowFacebookDialog()
    {
        facebookLayer.SetActive(true);
    }

    public void GoogleLoginBtnCallback()
    {
        BSKSoundController.data.playButton();

        if(GameSDKManager.s_instance.GetGoogleLoginStatus())
        {
            ShowGoogleDialog();
        }
        else
        {
            GameSDKManager.s_instance.GoogleLogin();
        }
    }

    public void FacebookBtnCallback()
    {
        BSKSoundController.data.playButton();

        if(GameSDKManager.s_instance.GetFacebookLoginStatus())
        {
            ShowFacebookDialog();
        }
        else
        {
            GameSDKManager.s_instance.FacebookLogin();
        }
    }
}
