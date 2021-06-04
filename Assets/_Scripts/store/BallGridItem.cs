using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BallGridItem : MonoBehaviour, IPointerClickHandler
{
    public Image ballImage;
    public Image coinImage;
    public Image useImage;
    public Image inuseImage;
    public Image selectImage;
    public Text coinText;
    private bool isInUsing = false;
   
    public Button btn;
    private BallConfig itemData;
    // Use this for initialization
    void Start()
    {
        //BSKEventTriggerListener.Get(gameObject).onClick = OnClick;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        OnClick(gameObject);
    }

    public void initGrid(BallConfig data)
    {
        itemData = data;
        coinText.text = data.price.ToString();
        ballImage.overrideSprite = Resources.Load("Ball/" + data.image, typeof(Sprite)) as Sprite;

        CheckStatus();
    }

    void CheckStatus()
    {
        if(BSKGameConfig.s_Instance.HasBall(itemData.id))
        {
            if(BSKGameConfig.s_Instance.GetInUsingBall() == itemData.id)
            {
                SetInUse(true);
            }
            else
            {
                SetInUse(false);
            }
        }
    }

    public int GetId()
    {
        return itemData.id;
    }

    public void BuyBall()
    {
        BSKSoundController.data.playButton();
        if (BSKGameConfig.s_Instance.HasBall(itemData.id))
            return;
        if (BSKGameConfig.s_Instance.GetCoin() < itemData.price)
        {
            StoreLayerManager.s_instance.ShowMessage();
            return;
        }
            
        BSKGameConfig.s_Instance.AddCoin(-itemData.price);
        BSKGameConfig.s_Instance.AddBall(itemData.id);
        SetInUse(false);
    }

    public void UseBall()
    {
        BSKSoundController.data.playButton();
        if (!BSKGameConfig.s_Instance.HasBall(itemData.id))
            return;
        if (isInUsing)
            return;
        StorePageManager.s_instance.SetBallInuse(itemData.id);
    }

    public void SetInUse(bool isUse)
    {
        isInUsing = isUse;
        if(isUse)
        {
            if (!BSKGameConfig.s_Instance.HasBall(itemData.id))
                return;
            BSKGameConfig.s_Instance.UsingBall(itemData.id);
            inuseImage.gameObject.SetActive(true);
            coinImage.gameObject.SetActive(false);
            useImage.gameObject.SetActive(false);
            coinText.gameObject.SetActive(false);
        }
        else
        {
            if (!BSKGameConfig.s_Instance.HasBall(itemData.id))
                return;
            inuseImage.gameObject.SetActive(false);
            coinImage.gameObject.SetActive(false);
            useImage.gameObject.SetActive(true);
            coinText.gameObject.SetActive(false);
        }
    }

    public void SetSelect(bool isSelect)
    {
        if(isSelect)
        {
            StoreLayerManager.s_instance.UpdateBall(itemData.id);
        }

        selectImage.gameObject.SetActive(isSelect);
    }

    void OnClick(GameObject obj)
    {
        StorePageManager.s_instance.SelectBall(GetId());
        StoreLayerManager.s_instance.UpdateBall(itemData.id);
    }
}
