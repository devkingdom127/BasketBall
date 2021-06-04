using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;

public class BSKOverLayer : UIBehaviour
{
    public Text scoreText;
    public Text bestText;
    public Text stageText;
    public Text totalText;
    public Text fieldGoalText;
    public Text cleanShotsText;
    public Text percentText;
    public Text rewarText;
    public Image recoredImage;
    public Button doubleBtn;
    public GameObject dialogPrefab;

    public Image bg;


    void OnEnable()
    {
        BSKGameController.data.PauseGame();

        if(GameSDKManager.s_instance.HasVideo())
        {
            doubleBtn.enabled = true;
        }

        int score = BSKGameController.data.GetScore();
        int best = BSKGameConfig.s_Instance.GetBestScore();
        if(score >= best)
        {
            recoredImage.gameObject.SetActive(true);
            BSKGameConfig.s_Instance.SetBestScore(score);
            GameSDKManager.s_instance.googleUploadLB(score, 0);
            GameSDKManager.s_instance.FacebookUpload(score);
        }

        scoreText.text = score.ToString();
        bestText.text = BSKGameConfig.s_Instance.GetBestScore().ToString();

        stageText.text = BSKGameController.data.GetStage().ToString();
        totalText.text = BSKGameController.data.GetShootTotal().ToString();
        fieldGoalText.text = BSKGameController.data.GetFieldGoalCount().ToString();
        cleanShotsText.text = BSKGameController.data.GetCleanShotsCount().ToString();
        percentText.text = BSKGameController.data.GetGoalPercent().ToString();
        rewarText.text = BSKGameController.data.GetTotalReward().ToString();

        //调用DOmove方法来让图片移动
        Tweener tweener = bg.rectTransform.DOMove(new Vector3(Screen.width/2, Screen.height/2), 0.5f);
        //设置这个Tween不受Time.scale影响
        tweener.SetUpdate(true);
        //设置移动类型
        tweener.SetEase(Ease.InOutBack);
        tweener.onComplete = delegate()
            {
                GameSDKManager.s_instance.ShowPauseInterstitial();
            };

    }

    void OnDisable()
    {
        BSKGameController.data.ResumeGame();
        bg.rectTransform.localPosition = new Vector3(0, 834, 0);
    }

    public void DoubleUpBtnCallback()
    {
        BSKSoundController.data.playButton();
        if(GameSDKManager.s_instance.HasVideo())
        {
            doubleBtn.enabled = false;
            GameSDKManager.s_instance.ShowVideo(3, BSKGameController.data.GetTotalReward());
        }
        else
        {
            ShowMessage();
        }
    }

    public void ShowMessage()
    {
        MessageDialog dialog = GameObject.Instantiate(dialogPrefab).GetComponent<MessageDialog>();
        RectTransform rect = dialog.GetComponent<RectTransform>();

        dialog.transform.parent = transform;
        dialog.transform.localScale = Vector3.one;  
        dialog.transform.localPosition = Vector3.zero;
        dialog.SetContent("Videos not available!");

        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
    }

}
