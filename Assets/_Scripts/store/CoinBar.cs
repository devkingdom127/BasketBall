using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinBar : MonoBehaviour
{
    public Text coinText;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Timer());
    }
	
    IEnumerator Timer()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f * Time.timeScale);
            int num = BSKGameConfig.s_Instance.GetCoin();
            coinText.text = num.ToString();
        }
    }
}
