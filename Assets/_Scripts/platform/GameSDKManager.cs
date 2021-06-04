using UnityEngine;
using System.Collections;
using SimpleJSON;

public class FacebookUserInfo
{
    public string id;
    public string name;
    public int score;
    public string imageSource;
}

public class GameSDKManager : MonoBehaviour
{
    public static GameSDKManager s_instance;

    private const string ANDROID_CLASS = "com.unity3d.player.UnityPlayer";

    private ArrayList facebookRankList = new ArrayList();

    private bool isFacebookLogin = false;

    private bool isGoogleLogin = false;

    private bool hasVideo = false;

    void Awake()
    {
        s_instance = this;
    }

    public ArrayList GetFacebookRankList()
    {
        return facebookRankList;
    }

    public bool GetGoogleLoginStatus()
    {
        return isGoogleLogin;
    }

    public bool GetFacebookLoginStatus()
    {
        return isFacebookLogin;
    }

    public bool HasVideo()
    {
        return hasVideo;
    }

    //----------------------------------------------------------------------------------------------------------
    //-------------------------------------Unity --> Android----------------------------------------------------
    //----------------------------------------------------------------------------------------------------------
    public void UnityCallAndroid(string method, params object[] args)
    {
        //Debug.Log(Application.platform.ToString());
        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass jc = new AndroidJavaClass(ANDROID_CLASS))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jo.Call(method, args);
                }
            }
        }

    }

    public void ShowBanner(bool isShow)
    {
     //   UnityCallAndroid("showBanner", isShow);
    }

    public void ShowBannerToTop(bool isTop)
    {
      //  UnityCallAndroid("showBannerToTop", isTop);
    }

    public void ShowPauseInterstitial()
    {
     //   UnityCallAndroid("showPauseInterstitial");
    }

    //广告的顶部与中心水平对齐
    public void ShowNativeAds(int w, float posY)
    {
        float a = w / 640.0f;
        float aw = 0.8f * Screen.width;

        int x = (int)(0.5f * Screen.width - 0.5f * aw);
        int y = (int)(Screen.height - posY) - 20;
        int h = (int)(0.834f * aw);

     //   UnityCallAndroid("showNativeAds", x, y, (int)aw, h);
    }

    public void HideNativeAds()
    {
    //    UnityCallAndroid("hideNativeAds");
    }

    public void ShowOffer()
    {
     //   UnityCallAndroid("showOffer");
    }

    /// <summary>
    /// Shows the video.
    /// </summary>
    /// <param name="type">1:get coin 2:relive 3:double up</param>
    public void ShowVideo(int type, int reward)
    {
     //   UnityCallAndroid("showVideo", type, reward);
    }

    public void ShowMore()
    {
    //    UnityCallAndroid("showMore");
    }

    public void ExitGame()
    {
     //   UnityCallAndroid("exitGame");
    }

    public void CheckHasVideo()
    {
      //  UnityCallAndroid("checkHasVideo");
    }

    /// <summary>
    /// Googles Interface.
    /// </summary>
    public void GoogleLogin()
    {
      //  UnityCallAndroid("googleLogin");
    }


    public void GoogleLike()
    {
      //  UnityCallAndroid("googleLike");
    }

    public void GoogleShare()
    {
      //  UnityCallAndroid("googleShare");
    }

    public void GoogleLB()
    {
    //    UnityCallAndroid("googleLB");
    }

    //index: 0 score, 1 star
    public void googleUploadLB(int data, int index)
    {
     //   UnityCallAndroid("googleUploadLB", data, index);
    }

    /// <summary>
    /// Facebooks Interface.
    /// </summary>
    public void FacebookLogin()
    {
    //    UnityCallAndroid("facebookLogin");
    }

    public void FacebookShare()
    {
      //  UnityCallAndroid("facebookShare");
    }

    public void FacebookUpload(int data)
    {
    //    UnityCallAndroid("facebookUpload", data);
    }

    public void FacebookInvite()
    {
     //   UnityCallAndroid("facebookInvite");
    }

    public void FacebookGetFriendsScore()
    {
     //   UnityCallAndroid("facebookGetScore");
    }

    public void FacebookGetUserInfo(string id)
    {
  //      UnityCallAndroid("facebookGetUserInfo", id);
    }

    public void FacebookLike()
    {
    //    UnityCallAndroid("facebookLike");
    }

    //----------------------------------------------------------------------------------------------------------
    //-------------------------------------Android --> Unity----------------------------------------------------
    //----------------------------------------------------------------------------------------------------------
    public void OnMessageGoogleLogin(string str)
    {
        if(str == "true")
        {
            isGoogleLogin = true;
            StartUIController.s_instance.ShowGoogleDialog();
        }
        else
        {
            isGoogleLogin = false;
        }
    }

    public void OnMessageFacebookLogin(string str)
    {
        if(str == "true")
        {
            isFacebookLogin = true;
            StartUIController.s_instance.ShowFacebookDialog();
        }
        else
        {
            isFacebookLogin = false;
        }
    }

    public void OnMessageCheckVideo(string msg)
    {
        if(msg == "true")
        {
            hasVideo = true;
        }
        else
        {
            hasVideo = false;
        }
    }


    public void OnMessageOfferReward(string str)
    {
        BSKSoundController.data.playGetCoins();
        int value = 0;
        int.TryParse(str, out value);
        BSKGameConfig.s_Instance.AddCoin(value);
    }

    public void OnMessageVideoReward(string str)
    {
        BSKSoundController.data.playGetCoins();
        int value = 0;
        int.TryParse(str, out value);
        BSKGameConfig.s_Instance.AddCoin(value);
    }

    public void OnMessageVideoRelive(string str)
    {
        int value = 0;
        int.TryParse(str, out value);
        BSKGameController.data.Relive(value);
    }

    public void OnMessageVideoDoubleUp(string str)
    {
        BSKSoundController.data.playGetCoins();
        int value = 0;
        int.TryParse(str, out value);
        BSKGameConfig.s_Instance.AddCoin(value);
    }


    public void OnMessageFacebookGetScore(string str)
    {
        Debug.Log(str);
        /*{
            "data": [
                    {
                        "score": 100,
                        "user": {
                            "name": "Gang Peng",
                            "id": "114068262373990"
                        }
                    }
            ]
        }*/
        facebookRankList.Clear();
        var root = JSONNode.Parse(str);
        var data = root[0]["data"].AsArray;

        for (int i = 0; i < data.Count; i++)
        {
            FacebookUserInfo con = new FacebookUserInfo();

            con.id = root[0]["data"][i]["user"]["id"];
            con.name = root[0]["data"][i]["user"]["name"];
            con.score = root[0]["data"][i]["score"].AsInt;

            facebookRankList.Add(con);
        }

        foreach(FacebookUserInfo info in facebookRankList)
        {
            FacebookGetUserInfo(info.id);
        }
    }

    public void OnMessageFacebookGetUserInfo(string str)
    {
        Debug.Log(str);
        /*{
            "id": "118932711887545",
            "birthday": "06/09/1951",
            "email": "breakgame@live.com",
            "first_name": "Gang",
            "gender": "male",
            "last_name": "Peng",
            "link": "https://www.facebook.com/app_scoped_user_id/118932711887545/",
            "locale": "zh_CN",
            "name": "Gang Peng",
            "timezone": 8,
            "updated_time": "2016-08-15T03:32:26+0000",
            "verified": true,
            "picture": {
                "data": {
                    "is_silhouette": false,
                    "url": "https://fb-s-c-a.akamaihd.net/h-ak-xpt1/v/t1.0-1/c0.4.50.50/p50x50/13900279_112387039208779_4298101552390542631_n.jpg?oh=f618ca21854b9a67efe2feb19658b70d&oe=58BD461B&__gda__=1487907681_b988c2f3cb1f83a55b046a1fb5767b40"
                }
            }
        }*/
        var root = JSONNode.Parse(str);
        string id = root[0]["id"];
        string imageUrl = root[0]["picture"]["data"]["url"];

        foreach(FacebookUserInfo info in facebookRankList)
        {
            if(id == info.id)
            {
                info.imageSource = imageUrl;
                break;
            }
        }
    }
}
