using UnityEngine;
using System.Collections;

public class FacebookDialog : MonoBehaviour
{
    public GameObject facebookRankingLayer;

    void OnEnable()
    {
        GameSDKManager.s_instance.FacebookGetFriendsScore();
    }

    public void LikeBtnCallback()
    {
        BSKSoundController.data.playButton();
        GameSDKManager.s_instance.FacebookLike();
    }

    public void ShareBtnCallback()
    {
        BSKSoundController.data.playButton();
        GameSDKManager.s_instance.FacebookShare();
    }

    public void RankBtnCallback()
    {
        BSKSoundController.data.playButton();
        facebookRankingLayer.SetActive(true);
        gameObject.SetActive(false);
    }

    public void CloseBtnCallback()
    {
        BSKSoundController.data.playButton();
        gameObject.SetActive(false);
    }
}
