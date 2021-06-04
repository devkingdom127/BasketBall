using UnityEngine;
using System.Collections;

public class BSKSoundController : MonoBehaviour {

    public AudioClip[] ballImpactFloor;
    public AudioClip[] ballImpactNet;
    public AudioClip[] ballImpactRing;
    public AudioClip[] ballImpactSheet;
    public AudioClip[] ballImpactPole;
    public AudioClip[] ballWoofs;
    public AudioClip ballInWind;
    public AudioClip goal;
    public AudioClip goalClear;
    public AudioClip goalClearSpecial;
    public AudioClip bonusOpen;
    public AudioClip newRecord;
    public AudioClip gameOver;
    public AudioClip button;
    public AudioClip countDown;
    public AudioClip readyGo;
    public AudioClip getCoins;
    public static BSKSoundController data;
    private AudioSource thisAudio;
    private bool playedNR;

    void Awake()
    {
        data = this;
        thisAudio = GetComponent<AudioSource>();
    }

    public void Stop()
    {
        if (thisAudio.isPlaying)
        {
            thisAudio.Stop();
            thisAudio.clip = null;
            thisAudio.loop = false;
        }
    }

    public void Pause()
    {
        thisAudio.volume = 0;
    }

    public void Resume()
    {
        thisAudio.volume = 1;
    }

    public void CheckAudioStatus()
    {
        if(BSKGameConfig.s_Instance.IsAudioOn())
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void playBallInWind()
    {
        CheckAudioStatus();
        if (!thisAudio.isPlaying)
        {
            thisAudio.clip = ballInWind;
            thisAudio.loop = true;
            thisAudio.Play();
        }
    }

    public void playGoal()
    {
        CheckAudioStatus();
        thisAudio.PlayOneShot(goal);
    }

    public void playClearGoal()
    {
        CheckAudioStatus();
        thisAudio.PlayOneShot(goalClear);
    }

    public void playClearSpecialGoal()
    {
        CheckAudioStatus();
        thisAudio.PlayOneShot(goalClearSpecial);
    }

    public void playNewRecord()
    {
        CheckAudioStatus();
        thisAudio.PlayOneShot(newRecord);
    }

    public void playGameOver()
    {
        CheckAudioStatus();
        thisAudio.PlayOneShot(gameOver);
    }

    public void playButton()
    {
        CheckAudioStatus();
        thisAudio.PlayOneShot(button);
    }

    public void playCountDown()
    {
        CheckAudioStatus();
        thisAudio.PlayOneShot(countDown);
    }

    public void playReadyGo()
    {
        CheckAudioStatus();
        thisAudio.PlayOneShot(readyGo);
    }

    public void playGetCoins()
    {
        CheckAudioStatus();
        thisAudio.PlayOneShot(getCoins);
    }
}
