using UnityEngine;
using System.Collections;

public class Global :MonoBehaviour
{
    public static Global instance;

    static Global()
    {
        GameObject go = new GameObject("Global");
        DontDestroyOnLoad(go);
        instance = go.AddComponent<Global>();
        go.AddComponent<BSKBmgController>();
        go.AddComponent<BSKSoundController>();
        go.AddComponent<BSKGameConfig>();
    }

    public void DoSomeThings()
    {
        Debug.Log("DoSomeThings");
    }

    void Start () 
    {
        Debug.Log("Start");
    }

}
