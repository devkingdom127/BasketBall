using UnityEngine;
//using Yodo1.MAS;
using System.Collections;
using System.Collections.Generic;
public class Yodo1AdsTest : MonoBehaviour
{
    //public bool Activate = false;
    bool timer = false;
    int coins = 0;

    public static Yodo1AdsTest instance;
     void Awake()
    {
        instance = this;
       
            Advertisements.Instance.Initialize();

        ShowBannerAdBottom();

    }
    void Start()
    {
       /* Yodo1U3dMas.SetGDPR(true);
        Yodo1U3dMas.SetCOPPA(false);
        Yodo1U3dMas.SetCCPA(false);
        Yodo1U3dMas.SetInitializeDelegate((bool success, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] InitializeDelegate, success:" + success + ", error: \n" + error.ToString());

            if (success)
            {// Initialize successful
            }
            else
            { // Initialize failure

            }
        });
        Yodo1U3dMas.InitializeSdk();
        Yodo1U3dMas.SetBannerAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] BannerdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Banner ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Banner ad has been shown.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Banner ad error, " + error.ToString());
                    break;
            }
        });

        Yodo1U3dMas.SetInterstitialAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] InterstitialAdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Interstital ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Interstital ad has been shown.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Interstital ad error, " + error.ToString());
                    break;
            }

        });

        Yodo1U3dMas.SetRewardedAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] RewardVideoDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Reward video ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Reward video ad has shown successful.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Reward video ad error, " + error);
                    break;
                case Yodo1U3dAdEvent.AdReward:

                    BSKSoundController.data.playGetCoins();
                    //int value = 0;
                    //int.TryParse(str, out value);
                    BSKGameConfig.s_Instance.AddCoin(100);
                    break;
            }

        });*/


        
    }
    void Update()
    {

        if (timer)
        {
            Debug.Log("Routine Called");
            StartCoroutine(Timer());
            timer = false;
        }




    }


    public void ShawBanner()
    {
        Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
    }

    public void HideBanner()
    {
        Advertisements.Instance.HideBanner();
    }


    /// <summary>
    /// Show Interstitial, assigned from inspector
    /// </summary>
    public void ShowInterstitial()
    {
        Advertisements.Instance.ShowInterstitial();
    }

    /// <summary>
    /// Show rewarded video, assigned from inspector
    /// </summary>
    public void ShowRewardedVideo()
    {
        Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
    }


    /// <summary>
    /// This is for testing purpose
    /// </summary>
   

    private void CompleteMethod(bool completed)
    {
        if (completed)
        {
            BSKSoundController.data.playGetCoins();
            //int value = 0;
            //int.TryParse(str, out value);
            BSKGameConfig.s_Instance.AddCoin(100);
        }

       // coinsText.text = coins.ToString();
    }

    public void ShowBannerAdTop()
    {

        Advertisements.Instance.ShowBanner(BannerPosition.TOP);

        /* if (Yodo1U3dMas.IsBannerAdLoaded())
         {
             int align = Yodo1U3dBannerAlign.BannerTop | Yodo1U3dBannerAlign.BannerHorizontalCenter;
             Yodo1U3dMas.ShowBannerAd(align);
         }
         else
         {
             Debug.Log("[Yodo1 Mas] Banner ad has not been cached.");
         }*/
    }


    public void ShowBannerAdBottom()
    {
        Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);

        /*  if (Yodo1U3dMas.IsBannerAdLoaded())
          {
              int align = Yodo1U3dBannerAlign.BannerBottom | Yodo1U3dBannerAlign.BannerHorizontalCenter;
              Yodo1U3dMas.ShowBannerAd(align);
          }
          else
          {
              Debug.Log("[Yodo1 Mas] Banner ad has not been cached.");
          }*/
    }
    public void HideBannerAd()
    {
        //Yodo1U3dMas.DismissBannerAd();
    }
    public void DistroyBannerAd()
    {
     }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(15);
        showInterstitialAd();
        timer = false;
    }
     

    

    public void showInterstitialAd()
    {

        Advertisements.Instance.ShowInterstitial();

        /*    if (Yodo1U3dMas.IsInterstitialAdLoaded())
            {
                Yodo1U3dMas.ShowInterstitialAd();
            }
            else
            {
                Debug.Log("[Yodo1 Mas] Interstitial ad has not been cached.");
            }*/


    }

    public void showRewardedAd()
    {
        Advertisements.Instance.ShowRewardedVideo(CompleteMethod);

        /*  if (Yodo1U3dMas.IsRewardedAdLoaded())
          {
              Yodo1U3dMas.ShowRewardedAd();
          }
          else
          {
              Debug.Log("[Yodo1 Mas] Reward video ad has not been cached.");
          }*/
    }



}
 