using UnityEngine;
using System.Collections;

public class GameUIController : MonoBehaviour
{
    public GameObject pauseLayer;

    public void PauseBtnCallback()
    {
        pauseLayer.SetActive(true);

        BSKGameController.data.gameStatus = GameStatus.Status_Pause;
    }

    public void resume()
    {
        Time.timeScale = BSKGameConfig.s_Instance.gameTimeScale;
        pauseLayer.SetActive(false);
    }
        
}

