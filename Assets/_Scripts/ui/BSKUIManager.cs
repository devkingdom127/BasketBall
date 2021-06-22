using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BSKUIManager : MonoBehaviour
{

    public void ReplayBtnCallfun()
    {
        ButtonClick();
        Yodo1AdsTest.instance.ShowInterstitial();

        Time.timeScale = BSKGameConfig.s_Instance.gameTimeScale;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuBtnCallfun()
    {
        ButtonClick();
        Yodo1AdsTest.instance.ShowInterstitial();

        SceneManager.LoadScene("StartScene");
    }

    public void MusicSetting(float val)
    {
        ButtonClick();

        BSKGameConfig config = BSKGameConfig.s_Instance;

        int isOpen = (int)val;
        int saveVal = config.IsBMGOn() ? 1 : 0;

        if (isOpen == saveVal)
        {
            return;
        }

        config.SaveBMGPrefs(isOpen);

        if (isOpen == 1)
        {
            BSKBmgController.s_Instance.Resume();
        }
        else
        {
            BSKBmgController.s_Instance.Pause();
        }
    }

    public void EffectSetting(float val)
    {
        ButtonClick();

        BSKGameConfig config = BSKGameConfig.s_Instance;

        int isOpen = (int)val;
        int saveVal = config.IsAudioOn() ? 1 : 0;

        if (isOpen == saveVal)
        {
            return;
        }

        config.SaveAudioPrefs(isOpen);

        if (isOpen == 1)
        {
            BSKSoundController.data.Resume();
        }
        else
        {
            BSKSoundController.data.Pause();
        }
    }

    public void closeDialog(GameObject obj)
    {
        ButtonClick();
        Yodo1AdsTest.instance.ShowInterstitial();

        obj.SetActive(false);
    }

    public void ButtonClick()
    {
 
        BSKSoundController.data.playButton();
    }

    public void GetMoreCoinButtonClick()
    {
        ButtonClick();
        Yodo1AdsTest.instance.showRewardedAd();
    }
}
