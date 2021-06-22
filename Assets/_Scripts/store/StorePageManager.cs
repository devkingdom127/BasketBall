using UnityEngine;
using System.Collections;

public class StorePageManager : MonoBehaviour {

    private PageView pageView;
    public GameObject defPage;
    public GameObject gridItem;

    private ArrayList ballList;
    private int currentInuseBall;

    public static StorePageManager s_instance;

    void Awake()
    {
        s_instance = this;
        ballList = new ArrayList();
        pageView = gameObject.GetComponent<PageView>();
        currentInuseBall = BSKGameConfig.s_Instance.GetInUsingBall();
    }

	// Use this for initialization
	void Start () 
    {
        /*pageView.InitPage(defPage, true);
        pageView.LayoutGrid(gridItem, 2, 3);*/
        pageView.InitGridPage(defPage, 6);

        for(int i = 0; i < 20; i++)
        {
            BallConfig data = BSKGameConfig.s_Instance.GetBallDataWithId(i + 1);
            GameObject cGrid = GameObject.Instantiate<GameObject>(gridItem);

            BallGridItem item = cGrid.GetComponent<BallGridItem>();
            item.initGrid(data);
            pageView.AddGrid(cGrid);

            ballList.Add(cGrid);
        }

        SelectBall(currentInuseBall);
	}

    public void SelectBall(int id)
    {
        foreach(GameObject obj in ballList)
        {
            BallGridItem item = obj.GetComponent<BallGridItem>();
            if(id == item.GetId())
            {
                item.SetSelect(true);
            }
            else
            {
                item.SetSelect(false);
            }
        }
    }
	
    public void SetBallInuse(int id)
    {
        if (id == currentInuseBall)
            return;

        currentInuseBall = id;
        
        foreach(GameObject obj in ballList)
        {
            BallGridItem item = obj.GetComponent<BallGridItem>();
            if(currentInuseBall == item.GetId())
            {
                SelectBall(currentInuseBall);
                item.SetInUse(true);
            }
            else
            {
                item.SetInUse(false);
            }
        }
    }
	
}
