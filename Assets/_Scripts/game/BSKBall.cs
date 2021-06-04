using UnityEngine;
using System.Collections;

//This script works with its ball. Inter alia it sends an events to any listeners about its actions like thorowed, goaled, failed.
public class BSKBall : MonoBehaviour
{
    //public Material standardMaterial, fadeMaterial;
    [HideInInspector]
    public bool thrown, floored, passed1, passed2, failed, goaled, special, clear;
    private float delayTimer;
    private Color col;
    private float distance;
    [HideInInspector]
    public float maxHeight;
    private GameObject ring;
    [HideInInspector]
    public AudioSource audioSource;
    private Rigidbody thisRigidbody;

    [HideInInspector]
    public bool isReady;

    private bool isShooting = false;

    public delegate void ThrowAction();
    public delegate void GoalAction(float distance, float maxHeight, bool floored, bool clear, bool special);
    public delegate void FailAction();
    public delegate void BallInAction(int score);

    public static event ThrowAction OnThrow;
    public static event GoalAction OnGoal;
    public static event FailAction OnFail;
    public static event BallInAction OnBallIn;

    public float depth;

    void Awake()
    {
        col = GetComponent<Renderer>().material.color;
        ring = GameObject.FindWithTag("ring");
        audioSource = GetComponent<AudioSource>();
        thisRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        gameObject.GetComponent<Renderer>().material = BSKGameConfig.s_Instance.GetBallMaterial();
    }

    void Update()
    {
        if (failed || goaled)
        {
            //FadeOut();
        }

        if (thisRigidbody.IsSleeping() && thrown && !failed)
        {
            SetFailed();
        }
        if (transform.position.y / 2 > maxHeight)
            maxHeight = transform.position.y / 2;

        if (transform.position.y > 10 && thisRigidbody.velocity.y > 10 && Mathf.Abs(thisRigidbody.velocity.x) < 20 && !special && !floored)
            special = true;
    }

    void OnEnable()
    {
        distance = (transform.position - ring.transform.position).magnitude;
        maxHeight = transform.position.y;
    }

    void CheckAudioStatus()
    {
        if(BSKGameConfig.s_Instance.IsAudioOn())
        {
            audioSource.volume = 1;
        }
        else
        {
            audioSource.volume = 0;
        }
    }

    void FadeOut()
    {
        delayTimer += Time.deltaTime;
        if (delayTimer > 1.0f)
        {
            /*if (GetComponent<Renderer>().material != fadeMaterial)
                GetComponent<Renderer>().material = fadeMaterial;*/
            col.a -= 0.01f;
            GetComponent<Renderer>().material.color = col;
            if (col.a <= 0)
            {
                ResetBall();
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetBall()
    {
        thrown = floored = passed1 = passed2 = failed = goaled = special = false;
        delayTimer = 0;
        col.a = 1;
        clear = true;
        //GetComponent<Renderer>().material = standardMaterial;
        //GetComponent<Renderer>().material.color = col;
    }

    public void SetThrown()
    {
        thrown = true;
        if (OnThrow != null)
            OnThrow();
    }

    public void SetGoaled()
    {
        if (!goaled && !failed)
        {
            goaled = true;
            if (OnGoal != null)
                OnGoal(distance, maxHeight, floored, clear, special);
        }
    }

    public void SetBallIn(int score)
    {
        if(OnBallIn != null)
        {
            OnBallIn(score);
        }
    }

    public void SetFailed()
    {
        if (!failed && !goaled)
        {
            failed = true;
            if (OnFail != null)
                OnFail();
        }
    }



    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "PlayZone")
        {
            float Yspeed = Mathf.Abs(thisRigidbody.velocity.y);
            if (!thrown || goaled)
                return;
            if (transform.position.y < ring.transform.position.y - 2 && Yspeed < 3.0f && !passed2)
            {
                SetFailed();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "trigger2")
        {
            if (passed1)
            {
                passed1 = false;
                passed2 = false;

                if(clear)
                {
                    SetBallIn(3);
                }
                else
                {
                    SetBallIn(2);
                    clear = true;
                }

            }
                
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "trigger1":
                if (!passed2)
                    passed1 = true;
                break;
            case "trigger2":
                
                passed2 = true;
                if (passed1)
                {
                    //thisRigidbody.drag = thisRigidbody.velocity.magnitude / 2;
                    PlayRandomClip(BSKSoundController.data.ballImpactNet);
                }

                break;
        }

        if (other.gameObject.tag == "deadZone")
        {
            SetFailed();
        }


    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "ring":
                clear = false;
                PlayRandomClip(BSKSoundController.data.ballImpactRing);
                break;

            case "floor":
                isShooting = false;
                if (!floored)
                {
                    floored = true;
                    special = false;
                }
                else
                    SetFailed();
                PlayRandomClip(BSKSoundController.data.ballImpactFloor);
                break;
            case "board":
                PlayRandomClip(BSKSoundController.data.ballImpactSheet);
                break;
            case "pole":
                PlayRandomClip(BSKSoundController.data.ballImpactPole);
                break;
            case "net":
                PlayRandomClip(BSKSoundController.data.ballImpactNet);
                break;

        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.name == "wall_front")
        {
            isReady = true;
        }
    }

    void PlayRandomClip(AudioClip[] clips)
    {
        CheckAudioStatus();
        float speed = Mathf.Clamp(thisRigidbody.velocity.magnitude, 0, 15);

        audioSource.pitch = 1.15f - speed / 50;
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)], speed / 4);
    }

    public void shoot()
    {
        isShooting = true;
        //PlayRandomClip(BSKSoundController.data.ballWoofs);
        CheckAudioStatus();
        audioSource.PlayOneShot(BSKSoundController.data.ballWoofs[1], 1);
    }

    public bool IsShooting()
    {
        return isShooting;
    }

    void OnMouseDrag()
    {
//        Vector3 mouseScreen = Input.mousePosition;
//        mouseScreen.z = depth;
//
//        Vector3 mousePositon = Camera.main.ScreenToWorldPoint(mouseScreen);
//        this.transform.position = mousePositon;
    }

}

