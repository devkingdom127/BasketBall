using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BSKSettingController : MonoBehaviour
{
    public Slider musicBar;
    public Slider audioBar;
    public RectTransform rect;

    void OnEnable()
    {
        float y = rect.position.y;
        GameSDKManager.s_instance.ShowNativeAds(452, y);
    }

    void OnDisable()
    {
        GameSDKManager.s_instance.HideNativeAds();
    }

    void Start()
    {
//        int val_music = PlayerPrefs.GetInt("Music");
//        int val_audio = PlayerPrefs.GetInt("Audio");
//
//        musicBar.value = val_music;
//        audioBar.value = val_audio;

        BSKGameConfig config = BSKGameConfig.s_Instance;

        musicBar.value = config.IsBMGOn() ? 1: 0;
        audioBar.value = config.IsAudioOn() ? 1: 0;

    }
	
}
