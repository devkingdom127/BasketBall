using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FacebookRankItem : MonoBehaviour {

    public Image headImage;
    public Text nameText;
    public Text scoreText;
    public Text rankingText;
    public Image rankImage;
    public Sprite[] rankSprites;

    public void SetRanking(int index)
    {
        if(index <= 2)
        {
            rankImage.gameObject.SetActive(true);
            rankImage.sprite = rankSprites[index];
        }
        else
        {
            rankImage.gameObject.SetActive(false);
        }

        int ranking = index + 1;

        rankingText.text = ranking.ToString();
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }
	
    //下载图片   
    public void downloadPicture(string url)  
    {   
        StartCoroutine(GETTexture(url));  
    } 

    IEnumerator GETTexture(string picURL)  
    {  
        WWW wwwTexture = new WWW(picURL);  

        yield return wwwTexture;  

        if (wwwTexture.error != null)  
        {  
            //GET请求失败  
            Debug.Log("error :" + wwwTexture.error);  
        }  
        else  
        {  
            //GET请求成功  
            Texture2D tex = wwwTexture.texture;
            headImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }  
    } 
}
