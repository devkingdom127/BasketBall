using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BSKStartController : MonoBehaviour
{
    public GameObject gameController;

	void Start()
    {
        DontDestroyOnLoad(gameController);
      
        StartCoroutine(WaitFun());
    }

    IEnumerator WaitFun()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("StartScene");
    }
}
