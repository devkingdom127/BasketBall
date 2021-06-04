using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum GameStatus
{
    Status_Normal,
    Status_CountDown,
    Status_Pause,
    Status_LevelUp,
    Status_Over
}

public enum MoveDirection
{
    Move_Left,
    Move_Right
}

public class BSKGameController : MonoBehaviour
{
    private BSKGameConfig gameConfig;
    public static BSKGameController data;

    //篮网
    public Cloth net;
    //篮网球形碰撞器
    private ClothSphereColliderPair[] colPair;
    const int clothSphereColliderPairNum = 5;

    //篮球
    public GameObject BallPrefab;
    private List<GameObject> balls;

    public GameObject basket;
    public float basketSpeed;
    public bool basketMove = true;
    private MoveDirection direction = MoveDirection.Move_Left;


    public Text targetText;
    public Text scoreText;
    public Text timeText;
    public Text stageText;
    
    private int currentStage = 1;
    private int timeLeft = 0;
    private int targetScore = 0;
    private int score = 0;
    private int levelBallCount = 0;
    private int shootCount = 0;
    private int fieldGoalCount = 0;
    private int cleanShotsCount = 0;
    private int rewardCount = 0;
    private bool hasRelive = false;

    public GameObject overLayer;
    public GameObject passLayer;
    public GameObject countDownLayer;
    public GameObject reliveLayer;

    public GameStatus gameStatus;

    void Awake()
    {
        data = this;
    }

    void OnEnable()
    {
        BSKBall.OnBallIn += BallIn;
    }

    void OnDisable()
    {
        BSKBall.OnBallIn -= BallIn;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = BSKGameConfig.s_Instance.gameTimeScale;
    }

    void Start()
    {
      //  GameSDKManager.s_instance.ShowBannerToTop(true);

        gameConfig = BSKGameConfig.s_Instance;
        colPair = new ClothSphereColliderPair[clothSphereColliderPairNum];
        balls = new List<GameObject>();
        resetBalls();

        timeLeft = gameConfig.GetStageConfigById(currentStage).time;
        targetScore = gameConfig.GetStageConfigById(currentStage).target;

        RefreshStage();
        RefreshTime();
        RefreshScore();
        RefreshTarget();

        //StartGame();
        CountDown();

        //gameStatus = GameStatus.Status_Normal;

        if (gameConfig.IsBMGOn())
        {
            BSKBmgController.s_Instance.PlayInGame();
        }
    }

    void FixedUpdate()
    {
        if(basketMove)
        {
            Vector3 pos = basket.transform.localPosition;
            if(direction == MoveDirection.Move_Left)
            {
                if(pos.x >= 0.8f)
                {
                    direction = MoveDirection.Move_Right;
                }
                else
                {
                    basket.transform.localPosition = new Vector3(pos.x + basketSpeed * Time.deltaTime, pos.y, pos.z);
                }
            }

            if(direction == MoveDirection.Move_Right)
            {
                if(pos.x <= -0.8f)
                {
                    direction = MoveDirection.Move_Left;
                }
                else
                {
                    basket.transform.localPosition = new Vector3(pos.x - basketSpeed * Time.deltaTime, pos.y, pos.z);
                }
            }
        }
    }

    void StartMove()
    {
        float dt = 10000;
        if (currentStage == 1)
            return;
        if (currentStage == 2 || currentStage == 3)
            dt = 10;
        if (currentStage > 3)
            dt = 5;
        basketSpeed = (currentStage - 2) * 0.1f + 0.4f;
        basketSpeed = Mathf.Clamp(basketSpeed, 0.4f, 1.0f);
        StartCoroutine(Timer(dt));
    }

    IEnumerator Timer(float t)
    {
        yield return new WaitForSeconds(t * Time.timeScale);
        basketMove = true;
    }

    void ResetBasketPos()
    {
        Vector3 cur = basket.transform.localPosition;

        basket.transform.localPosition = new Vector3(0, cur.y, cur.z);
    }

    void resetBalls()
    {
        foreach (GameObject t in balls)
        {
            Destroy(t);
        }
        balls.Clear();

        for (int i = 0; i < 6; i++)
        {
            GameObject obj = (GameObject)Instantiate(BallPrefab);
            obj.transform.parent = null;

            if(i < 3)
            {
                obj.transform.position = new Vector3(-0.6f + 0.6f * i, 3.0f, 0f);
            }
            else if(i >= 3 && i < 5)
            {
                obj.transform.position = new Vector3(-0.3f + 0.6f * (i-3), 3.0f, 0.6f);
            }
            else
            {
                obj.transform.position = new Vector3(0, 3.0f, 1.2f);
            }

            obj.layer = 8;

            obj.transform.rotation = GetRandomRotation();
            balls.Add(obj);

            addBallCollider2Net(obj.GetComponent<SphereCollider>());
        }

         
    }

    public Quaternion GetRandomRotation()
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

    void CountDown()
    {
        gameStatus = GameStatus.Status_CountDown;
        countDownLayer.SetActive(true);
    }

    public void StartGame()
    {
        StartCoroutine(ProgressTimer(1));
    }

    IEnumerator ProgressTimer(int dt)
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(dt * Time.timeScale);

            timeLeft -= dt;
            RefreshTime();

            if (timeLeft == 0)
            {
                TimeOver();
            }
        }
    }

    void RefreshTime()
    {
        if(timeLeft <= 5)
        {
            BSKSoundController.data.playCountDown();
        }
        timeText.text = string.Format("{0:D3}", timeLeft);
    }

    void RefreshScore()
    {
        scoreText.text = string.Format("{0:D3}", score);
    }

    void RefreshTarget()
    {
        targetText.text = string.Format("{0:D3}", targetScore);
    }

    void RefreshStage()
    {
        stageText.text = currentStage.ToString();
    }

    void TimeOver()
    {
        if (score >= targetScore)
        {
            gameStatus = GameStatus.Status_LevelUp;
            passLayer.SetActive(true);
            //LevelUp();
            BSKSoundController.data.playGoal();
        }
        else
        {
            if(!hasRelive && GameSDKManager.s_instance.HasVideo())
            {
                reliveLayer.SetActive(true);
            }
            else
            {
                GameOver();
            }
        }
    }

    public void LevelUp()
    {
        Debug.Log("Level Up!!!");
        GameSDKManager.s_instance.CheckHasVideo();
        currentStage += 1;
        levelBallCount = 0;
        timeLeft = gameConfig.GetStageConfigById(currentStage).time;
        targetScore = gameConfig.GetStageConfigById(currentStage).target;

        RefreshStage();
        RefreshTime();
        RefreshScore();
        RefreshTarget();
        ResetBasketPos();
        basketMove = false;

        passLayer.SetActive(false);
        gameStatus = GameStatus.Status_Normal;
        StartMove();
        //StartGame();
        CountDown();
    }

    public void GameOver()
    {
        Debug.Log("Game Over!!!");
        BSKSoundController.data.playGameOver();
        gameStatus = GameStatus.Status_Over;
        overLayer.SetActive(true);
    }

    public void Relive(int time)
    {
        GameSDKManager.s_instance.CheckHasVideo();
        reliveLayer.SetActive(false);

        timeLeft = time;
        RefreshTime();

        StartGame();

        hasRelive = true;
    }

    void BallIn(int s)
    {
        if (s == 3)
        {
            cleanShotsCount++;
        }

        score += s;
        levelBallCount++;
        fieldGoalCount++;
        RefreshScore();
    }

    public int GetScore()
    {
        return score;
    }

    public int GetStage()
    {
        return currentStage;
    }

    public int GetPassReward()
    {
        int reward = gameConfig.GetStageConfigById(currentStage).passReward + levelBallCount;
        rewardCount += reward;

        return reward;
    }

    public int GetNextTarget()
    {
        return gameConfig.GetStageConfigById(currentStage + 1).target;
    }

    public void ShootOnce()
    {
        shootCount++;
    }

    public int GetShootTotal()
    {
        return shootCount;
    }

    public int GetFieldGoalCount()
    {
        return fieldGoalCount;
    }

    public int GetCleanShotsCount()
    {
        return cleanShotsCount;
    }

    public int GetGoalPercent()
    {
        int per = 0;
        if(shootCount > 0)
            per = fieldGoalCount * 100 / shootCount;
        return per;
    }

    public int GetTotalReward()
    {
        return rewardCount;
    }
    //    void OnGUI()
    //    {
    //        GUILayout.TextArea("TimeScal:" + Time.timeScale.ToString());
    //    }
}
