using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BSKLoadingController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Debug.Log("======Start Loading======");
        StartCoroutine("LoadingScene");
    }

    IEnumerator LoadingScene()
    {
        yield return  new WaitForSeconds(1);
        AsyncOperation async = SceneManager.LoadSceneAsync("GameScene");

        Debug.Log("======End Loading======");
        yield return async;
    }
}
