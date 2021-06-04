using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobBanner : MonoBehaviour {

// Unity Test Ads Created on Admob
	string BannerAdID = "ca-app-pub-1046159889962346/1938796613";
    BannerView MyBannerView;

	// Use this for initialization
	void Start () {
        /* ----- LOADING BANNER AD ------ */
		MyBannerView = new BannerView(BannerAdID, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.MyBannerView.LoadAd(request);

        /* ----- Displaying BANNER AD ------ */
        this.MyBannerView.Show();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadBannerAd()
    {
		MyBannerView = new BannerView(BannerAdID, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.MyBannerView.LoadAd(request);
    }

    public void ShowBannerAd()
    {
        this.MyBannerView.Show();
    }
    public void HideBannerAd()
    {
        this.MyBannerView.Hide();
    }
    public void DistroyBannerAd()
    {
        this.MyBannerView.Destroy();
    }
}
