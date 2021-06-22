using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class BSKReliveLayer : MonoBehaviour
{
    public Image bg;
    public Image progressBar;

    public float delayTime;
    private float currentTime;
    private bool countDown;

    void OnEnable()
    {
//        if(BSKGameController.data != null)
//        {
//            BSKGameController.data.PauseGame();
//        }
        BSKGameController.data.PauseGame();

        currentTime = 0;
        countDown = false;
        progressBar.fillAmount = 1.0f;

        //调用DOmove方法来让图片移动
        Tweener tweener = bg.rectTransform.DOMove(new Vector3(Screen.width/2, Screen.height/2), 0.5f);
        //设置这个Tween不受Time.scale影响
        tweener.SetUpdate(true);
        //设置移动类型
        tweener.SetEase(Ease.InOutBack);
        tweener.onComplete = delegate()
            {
                countDown = true;
            };
    }

    void OnDisable()
    {
        BSKGameController.data.ResumeGame();
        bg.rectTransform.localPosition = new Vector3(0, 640, 0);
    }
        
	
    // Update is called once per frame
    void Update()
    {
        if(!countDown)
        {
            return;
        }

        if(currentTime >= delayTime)
        {
            currentTime = 0;
            GameOver();
            return;
        }

        currentTime += Time.unscaledDeltaTime;
        progressBar.fillAmount = 1.0f - currentTime / delayTime;
    }

    public void GameOver()
    {
        gameObject.SetActive(false);
        BSKGameController.data.GameOver();
    }

    public void ReliveBtnCallback()
    {
        BSKSoundController.data.playButton();
        GameSDKManager.s_instance.ShowVideo(2, 10);
        //GameSDKManager.s_instance.OnMessageVideoRelive("10");
    }

    public void CloseBtnCallback()
    {
        BSKSoundController.data.playButton();
        GameOver();
    }
}
