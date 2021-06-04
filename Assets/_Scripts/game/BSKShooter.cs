using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BSKShooter : MonoBehaviour
{

    public static BSKShooter shooter;

    //篮网
    public Cloth net;
    //篮网球形碰撞器
    private ClothSphereColliderPair[] colPair;
    private int clothSphereColliderPairNum = 10;

    //篮球
    public GameObject BallPrefab;
    private List<GameObject> balls;

    //当前人物所持篮球
    public GameObject currentBall;

    //人物
    public GameObject player;

    //投篮力量
    public float power = 2.5f;
    public float minPower = 2.0f;
    public float maxPower = 2.8f;

    //从点击投篮按钮开始到球投出去的时间
    public float poseTime = 0.36f;

    //投球矢量
    public Vector3 throwForce;

    [HideInInspector]
    public bool shootStart = false;

    //是否点击
    //private bool isPressed = false;

    public float depth = 0;

    void Start()
    {
        shooter = this;
        depth = GameObject.Find("wall_front").transform.position.z - Camera.main.transform.position.z;
        depth = Mathf.Abs(depth) + 0.3f;
        colPair = new ClothSphereColliderPair[clothSphereColliderPairNum];
        balls = new List<GameObject>();
        resetBalls();
    }

	
    void Update()
    {
        if (shootStart)
        {
            float times = poseTime / Time.deltaTime;
            float deltaPower = (maxPower - minPower) / times;
            power += deltaPower;
            player.GetComponent<Player>().setForce(power * throwForce);
        }

        /*if (Input.GetMouseButtonDown(0) && !isPressed)
        {
            isPressed = true;

            //以屏幕某点沿摄像机方向发出射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //判断是否发生碰撞
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, 1 << 8))
            {
                if (hit.collider.gameObject.tag == "Basketball")
                {
                    currentBall = hit.collider.gameObject;
                    Rigidbody r = currentBall.GetComponent<Rigidbody>();
                    r.isKinematic = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isPressed)
        {
            isPressed = false;
            if (currentBall)
            {
                addBallCollider2Net(currentBall.GetComponent<SphereCollider>());

                shoot();
                currentBall = null;
            }
        }

        if(isPressed)
        {
            if (currentBall)
            {
                //将物体由世界坐标系转换为屏幕坐标系
                Vector3 screenSpace = Camera.main.WorldToScreenPoint(currentBall.transform.position);
                Vector3 offset = currentBall.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentBall.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, depth));//Input.mousePosition;
            }
        }*/

        if (!currentBall)
        {
            foreach (GameObject b in balls)
            {
                if (b.GetComponent<BSKBall>().isReady)
                {
                    currentBall = b;
                    Rigidbody r = currentBall.GetComponent<Rigidbody>();
                    r.isKinematic = true;
                    break;
                }
            }
        }

    }

    //投球
    public void shoot()
    {
        if (currentBall)
        {
            Rigidbody r = currentBall.GetComponent<Rigidbody>();
            r.isKinematic = false;
            r.AddTorque(-30, 0, 0);
            r.constraints = RigidbodyConstraints.None;
            r.AddForce(power * throwForce, ForceMode.Impulse);    

            BSKGameController.data.ShootOnce();
        }
    }

    public void shootBtnClickStart()
    {
        if (!currentBall || player.GetComponent<Player>().isShoot())
            return;
        power = minPower;
        shootStart = true;
        //Rigidbody r = currentBall.GetComponent<Rigidbody>();
        //r.AddTorque(-30, 0, 0);
        //r.AddForce(power * throwForce, ForceMode.Impulse);
        addBallCollider2Net(currentBall.GetComponent<SphereCollider>());
        player.GetComponent<Player>().setCurrentBall(currentBall);
        //player.GetComponent<Player>().setForce(power * throwForce);
        player.GetComponent<Player>().shootStart();
        
    }

    public void shootBtnClickEnd()
    {
        if (!currentBall)
            return;

        shootStart = false;
        //currentBall = null;
        //Debug.Log("shoot End");
    }

    public void shootBtnClickCancel()
    {
        if (!currentBall)
            return;

        //Debug.Log("shoot Cancel");
    }

    private void resetBalls()
    {
        foreach (GameObject t in balls)
        {
            Destroy(t);
        }
        balls.Clear();

        for (int i = 0; i < 4; i++)
        {
            GameObject obj = (GameObject)Instantiate(BallPrefab);
            obj.transform.parent = null;
            obj.layer = 8;
            obj.transform.position = new Vector3(-0.47f + 0.44f * i, 2.4f, 1.22f);
            obj.transform.rotation = GetRandomRot();
            balls.Add(obj);
        }      
    }

    public Quaternion GetRandomRot()
    {
        Quaternion randRot = new Quaternion();
        randRot.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        return randRot;
    }

    //给篮网加入碰撞器
    void addBallCollider2Net(SphereCollider collider)
    {
        for (int i = 0; i < clothSphereColliderPairNum; i++)
        {
            if ((colPair[i].first == null || !colPair[i].first.gameObject.activeInHierarchy))
            {
                colPair[i].first = collider;
                net.sphereColliders = colPair;
                return;
            }
        }
    }
}
