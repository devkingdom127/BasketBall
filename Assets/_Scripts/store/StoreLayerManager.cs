using UnityEngine;
using System.Collections;

public class StoreLayerManager : MonoBehaviour
{
    public static StoreLayerManager s_instance;

    public GameObject ball;

    public GameObject dialogPrefab;

    void Awake()
    {
        s_instance = this;   
    }

    void Start()
    {
        GameSDKManager.s_instance.ShowBannerToTop(false);
    }

    public void UpdateBall(int id)
    {
        Material currentMat = ball.GetComponent<Renderer>().material;

        if(currentMat == BSKGameConfig.s_Instance.ballMaterials[id - 1])
        {
            return;
        }

        ball.GetComponent<Renderer>().material = BSKGameConfig.s_Instance.ballMaterials[id - 1];
    }
        
	
    // Update is called once per frame
    void Update()
    {
        ball.transform.Rotate(Vector3.right * Time.deltaTime * 30, Space.Self);
        
    }

    public void ShowMessage()
    {
        MessageDialog dialog = GameObject.Instantiate(dialogPrefab).GetComponent<MessageDialog>();
        RectTransform rect = dialog.GetComponent<RectTransform>();

        dialog.transform.parent = transform;
        dialog.transform.localScale = Vector3.one;  
        dialog.transform.localPosition = Vector3.zero;

        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
    }
}
