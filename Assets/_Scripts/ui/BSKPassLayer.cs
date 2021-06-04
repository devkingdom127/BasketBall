using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;

public class BSKPassLayer : UIBehaviour
{
    public Text scoreText;
    public Text rewardText;
    public Text targetText;

    public Image bg;

    void OnEnable()
    {
        //BSKGameController.data.PauseGame();

        int reward = BSKGameController.data.GetPassReward();
        scoreText.text = BSKGameController.data.GetScore().ToString();
        rewardText.text = reward.ToString();
        targetText.text = BSKGameController.data.GetNextTarget().ToString();

        BSKGameConfig.s_Instance.AddCoin(reward);

        //调用DOmove方法来让图片移动
        Tweener tweener = bg.rectTransform.DOMove(new Vector3(Screen.width/2, Screen.height/2), 0.5f);
        //设置这个Tween不受Time.scale影响
        tweener.SetUpdate(true);
        //设置移动类型
        tweener.SetEase(Ease.InOutBack);
    }

    void OnDisable()
    {
        bg.rectTransform.localPosition = new Vector3(0, 800, 0);
        //BSKGameController.data.ResumeGame();
    }

}
