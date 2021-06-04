using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class MessageDialog : MonoBehaviour
{
    public Image dialog;
    public Text message;

    void Start()
    {
        Tweener tweener = dialog.rectTransform.DOLocalMove(Vector3.zero, 0.5f);
        tweener.SetUpdate(true);
        tweener.SetEase(Ease.OutQuad);

        tweener.onComplete = delegate()
            {
                StartCoroutine(Timer());
            };
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(Time.timeScale);
        GameObject.Destroy(gameObject);
    }
	
    public void SetContent(string content)
    {
        message.text = content;
    }
}
