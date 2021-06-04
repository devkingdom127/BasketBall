using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class intrestitial : MonoBehaviour {

// Unity Test Ads Created on Admob
	string InterstitialAdID = "ca-app-pub-1046159889962346/2308377995";
    InterstitialAd MyInterstitialAd;
    public bool Activate = false;
    bool timer = false;


	// Use this for initialization
    public void Start()
    {
        MyInterstitialAd = new InterstitialAd(InterstitialAdID);
        LoadInterstitialAd();
        timer = true;
	}
	
	// Update is called once per frame
	void Update () {
   
        if(timer)
        {
            Debug.Log("Routine Called");
            StartCoroutine(Timer());
            timer = false;
        }
            
            
        
	
	}
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(15);
        showInterstitialAd();
        timer = false;
    }
    public void activateIntrestitial()
    {
    }

    public void LoadInterstitialAd()
    {
        AdRequest request = new AdRequest.Builder().Build();
        MyInterstitialAd.LoadAd(request);
    }

    public void showInterstitialAd()
    {
        if (MyInterstitialAd.IsLoaded())
        {
            MyInterstitialAd.Show();
        }
        else
        {
            Start();

        }

    }
}
