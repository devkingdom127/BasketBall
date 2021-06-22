using UnityEngine;
using System.Collections;

public class BSKPlayerController : MonoBehaviour
{
    //是否已选中球
    private bool isPressed;

    //当前选中的球
    public GameObject currentBall;

    //鼠标点击检测深度
    public float depth;

    //冲量方向
    public Vector3 force;

    //力度大小
    public float power;

    //固定z方向上的力度
    public float speedZ;

    //4个挡板
    private GameObject leftEdge;
    private GameObject rightEdge;
    private GameObject bottomEdge;
    private GameObject topEdge;

    //固定选中的球的y坐标
    public float selectBallPosY = 2.25f;

    public float powerArg1 = 7.5f;
    public float powerArg2 = 1.7f;

    void Start()
    {
        isPressed = false;

        leftEdge = GameObject.Find("wall_left");
        rightEdge = GameObject.Find("wall_right");
        bottomEdge = GameObject.Find("wall_front");
        topEdge = GameObject.Find("wall_top");
    }
	
    void FixedUpdate()
    {
        if(BSKGameController.data.gameStatus != GameStatus.Status_Normal)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && !isPressed)
        {
            isPressed = true;

            //以屏幕某点沿摄像机方向发出射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //判断是否发生碰撞
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, 1 << 8))
            {
                if (hit.collider.gameObject.tag == "Basketball" && !hit.collider.gameObject.GetComponent<BSKBall>().IsShooting())
                {
                    currentBall = hit.collider.gameObject;
                    Rigidbody r = currentBall.GetComponent<Rigidbody>();
                    r.isKinematic = true;

                    Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, depth));
                    touchPoint.x = Mathf.Clamp(touchPoint.x, leftEdge.transform.position.x + 0.29f, rightEdge.transform.position.x - 0.29f);
                    touchPoint.y = selectBallPosY;
                    currentBall.transform.position = touchPoint;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isPressed)
        {
            isPressed = false;
            if (currentBall)
            {
                Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, depth)) - currentBall.transform.position;

                power = powerArg1 + powerArg2 * Mathf.Clamp01(Mathf.Abs(offset.y));

                force = new Vector3(offset.x, power, speedZ);

                shoot();
                currentBall = null;
            }
        }

        if (isPressed)
        {
            if (currentBall)
            {
                Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, depth));
                touchPoint.x = Mathf.Clamp(touchPoint.x, leftEdge.transform.position.x + 0.3f, rightEdge.transform.position.x - 0.3f);

                float dis = Vector3.Magnitude(touchPoint - currentBall.transform.position);

                if(touchPoint.y > selectBallPosY - 0.3f && touchPoint.y < selectBallPosY + 0.3f)
                {
                    touchPoint.y = selectBallPosY;
                    //currentBall.transform.position = touchPoint;
                    currentBall.transform.position = Vector3.Lerp(currentBall.transform.position, touchPoint, 8*Time.deltaTime);
                }
                /*if(dis > 0)
                {
                    //touchPoint.y = selectBallPosY;
                    //currentBall.transform.position = touchPoint;
                    currentBall.transform.position = Vector3.Lerp(currentBall.transform.position, touchPoint, 8*Time.deltaTime);
                }*/

            }
        }
    }
        

    void shoot()
    {
        if (currentBall)
        {
            Rigidbody r = currentBall.GetComponent<Rigidbody>();
            r.isKinematic = false;
            r.AddTorque(-60, 0, 0);
            //r.velocity = force;
            r.AddForce(force, ForceMode.Impulse);   

            currentBall.GetComponent<BSKBall>().shoot();
            BSKGameController.data.ShootOnce();
        }
    }


    void OnGUI()
    {
//        GUILayout.TextField("StartPos:" + startPos.ToString());
//        GUILayout.TextField("EndPos:" + endPos.ToString());
//        GUILayout.TextField("W_StartPos:" + w_startPos.ToString());
//        GUILayout.TextField("W_EndPos:" + w_endPos.ToString());
//        GUILayout.TextField("Force:" + force.ToString());
//        GUILayout.TextField("x:" + _x.ToString() + ", y:" + _y.ToString());
//        if(currentBall)
//        {
//            Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, depth)) - currentBall.transform.position;
//            Quaternion rot = Quaternion.LookRotation(offset);
//
//            float moveDis = offset.magnitude;
//            GUILayout.TextField("moveDis:" + offset.y.ToString() + ", rot:" + rot.eulerAngles.ToString());
//        }
    }
}
