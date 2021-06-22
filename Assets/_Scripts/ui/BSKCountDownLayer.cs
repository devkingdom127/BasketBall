using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class BSKCountDownLayer : UIBehaviour
{
    public Text targetText;
    public Image readyImg;
    public Image goImg;

    void OnEnable()
    {
        BSKGameController.data.gameStatus = GameStatus.Status_CountDown;


        Color col1 = readyImg.material.color;
        col1.a = 1f;

        Color col2 = goImg.material.color;
        col2.a = 1f;

        readyImg.material.color = col1;
        goImg.material.color = col2;

        readyImg.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        goImg.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        readyImg.gameObject.SetActive(false);
        goImg.gameObject.SetActive(false);

//        readyImg.gameObject.SetActive(true);
//        Tweener tweener1 = readyImg.material.DOFade(0, 1f);
//        tweener1.SetUpdate(true);
//        Tweener tweener2 = readyImg.transform.DOScale(new Vector3(2.0f, 2.0f, 2.0f), 1f);
//        tweener2.SetUpdate(true);
//
//        tweener2.onComplete = delegate()
//            {
//                goImg.gameObject.SetActive(true);
//                Tweener tweener3 = goImg.material.DOFade(0, 1f);
//                tweener3.SetUpdate(true);
//                Tweener tweener4 = goImg.transform.DOScale(new Vector3(2.0f, 2.0f, 2.0f), 1f);
//                tweener4.SetUpdate(true);
//
//                tweener4.onComplete = delegate()
//                    {
//                        BSKGameController.data.gameStatus = GameStatus.Status_Normal;
//                        BSKGameController.data.StartGame();
//                        gameObject.SetActive(false);
//                    };
//            };
        BSKSoundController.data.playReadyGo();
        StartCoroutine(CountDown1());
    }

    void OnDisable()
    {
        //BSKGameController.data.ResumeGame();
    }

    IEnumerator CountDown()
    {
        targetText.text = "3";
        yield return new WaitForSeconds(Time.timeScale);
        targetText.text = "2";
        yield return new WaitForSeconds(Time.timeScale);
        targetText.text = "1";
        yield return new WaitForSeconds(Time.timeScale);
        targetText.text = "GO";
        yield return new WaitForSeconds(Time.timeScale);

        BSKGameController.data.gameStatus = GameStatus.Status_Normal;
        BSKGameController.data.StartGame();
        gameObject.SetActive(false);
    }

    IEnumerator CountDown1()
    {
        yield return new WaitForSeconds(0.5f * Time.timeScale);

        readyImg.gameObject.SetActive(true);
        Tweener tweener1 = readyImg.material.DOFade(0, 1f);
        Tweener tweener2 = readyImg.transform.DOScale(new Vector3(2.0f, 2.0f, 2.0f), 1f);

        yield return new WaitForSeconds(0.8f * Time.timeScale);

        goImg.gameObject.SetActive(true);
        Tweener tweener3 = goImg.material.DOFade(0, 1f);
        Tweener tweener4 = goImg.transform.DOScale(new Vector3(2.0f, 2.0f, 2.0f), 1f);

        yield return new WaitForSeconds(Time.timeScale);

        gameObject.SetActive(false);
        BSKGameController.data.gameStatus = GameStatus.Status_Normal;
        BSKGameController.data.StartGame();
    }
}
