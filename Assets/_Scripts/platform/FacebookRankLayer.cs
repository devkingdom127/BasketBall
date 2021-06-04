using UnityEngine;
using System.Collections;
using UnityEngine.UI;

class UserComparer : System.Collections.IComparer
{
    public int Compare(object x, object y)
    { 
        return ((FacebookUserInfo)y).score - ((FacebookUserInfo)x).score;
    }
}

public class FacebookRankLayer : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject content;

    // Use this for initialization
    void OnEnable()
    {
        foreach(Transform obj in content.GetComponentsInChildren<Transform>())
        {
            if(obj != content.transform)
            {
                Destroy(obj.gameObject);
            }
        }

        ArrayList userInfos = GameSDKManager.s_instance.GetFacebookRankList();

        /*FacebookUserInfo info1 = new FacebookUserInfo();
        info1.id = "11111111";
        info1.name = "aaaaaaa";
        info1.score = 100;
        info1.imageSource = "https://fb-s-c-a.akamaihd.net/h-ak-xpt1/v/t1.0-1/c0.4.50.50/p50x50/13900279_112387039208779_4298101552390542631_n.jpg?oh=f618ca21854b9a67efe2feb19658b70d&oe=58BD461B&__gda__=1487907681_b988c2f3cb1f83a55b046a1fb5767b40";
            
        FacebookUserInfo info2 = new FacebookUserInfo();
        info2.id = "222222222";
        info2.name = "bbbbbbb";
        info2.score = 300;
        info2.imageSource = "https://fb-s-c-a.akamaihd.net/h-ak-xpt1/v/t1.0-1/c0.4.50.50/p50x50/13900279_112387039208779_4298101552390542631_n.jpg?oh=f618ca21854b9a67efe2feb19658b70d&oe=58BD461B&__gda__=1487907681_b988c2f3cb1f83a55b046a1fb5767b40";

        FacebookUserInfo info3 = new FacebookUserInfo();
        info3.id = "33333333";
        info3.name = "ccccccccc";
        info3.score = 200;
        info3.imageSource = "https://fb-s-c-a.akamaihd.net/h-ak-xpt1/v/t1.0-1/c0.4.50.50/p50x50/13900279_112387039208779_4298101552390542631_n.jpg?oh=f618ca21854b9a67efe2feb19658b70d&oe=58BD461B&__gda__=1487907681_b988c2f3cb1f83a55b046a1fb5767b40";

        userInfos.Add(info1);
        userInfos.Add(info2);
        userInfos.Add(info3);*/

        UserComparer com = new UserComparer();
        userInfos.Sort(com);

        //Debug.Log("==================User Size:" + userInfos.Count.ToString() + "====================");

        for(int i = 0; i < userInfos.Count; i++)
        {
            FacebookUserInfo info = (FacebookUserInfo)userInfos[i];

            FacebookRankItem item = GameObject.Instantiate(itemPrefab).GetComponent<FacebookRankItem>();
            item.transform.parent = content.transform;
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;
            item.SetRanking(i);
            item.SetName(info.name);
            item.SetScore(info.score);
            item.downloadPicture(info.imageSource);
        }
    }

    public void CloseBtnCallback()
    {
        gameObject.SetActive(false);
    }
}
