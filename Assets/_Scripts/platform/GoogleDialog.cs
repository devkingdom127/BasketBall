using UnityEngine;
using System.Collections;

public class GoogleDialog : MonoBehaviour {

    public void LikeBtnCallback()
    {
        BSKSoundController.data.playButton();
        GameSDKManager.s_instance.GoogleLike();
    }

    public void ShareBtnCallback()
    {
        BSKSoundController.data.playButton();
        GameSDKManager.s_instance.GoogleShare();
    }

    public void RankBtnCallback()
    {
        BSKSoundController.data.playButton();
        GameSDKManager.s_instance.GoogleLB();
    }

    public void CloseBtnCallback()
    {
        BSKSoundController.data.playButton();
        gameObject.SetActive(false);
    }
}
