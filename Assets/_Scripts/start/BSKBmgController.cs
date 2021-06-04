using UnityEngine;
using System.Collections;

public class BSKBmgController : MonoBehaviour {

    static public BSKBmgController s_Instance;

    public AudioClip[] bmgClips;

    private AudioSource audioSource;

    void Awake()
    {
        s_Instance = this;
        audioSource = GetComponent<AudioSource>();
        //audioSource.volume = 0.5f;
    }
	
    public void PlayOutGame()
    {
        if (audioSource.isPlaying && audioSource.clip == bmgClips[0])
        {
            return;
        }
        Stop();

        audioSource.clip = bmgClips[0];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayInGame()
    {        
        if (audioSource.isPlaying && audioSource.clip == bmgClips[1])
        {
            return;
        }
        Stop();

        audioSource.clip = bmgClips[1];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void Resume()
    {
        if(!audioSource.isPlaying)
        {
            if(BSKGameController.data != null)
            {
                PlayInGame();
            }
            else
            {
                PlayOutGame();
            }
        }
    }
}
