using UnityEngine;
using System.Collections;

public class BSKPauseLayer : MonoBehaviour
{

    void OnEnable()
    {
        BSKGameController.data.PauseGame();
    }

    void OnDisable()
    {
        BSKGameController.data.ResumeGame();
    }

    public void ContinueGame()
    {
        //BSKGameController.data.ResumeGame();
        BSKGameController.data.gameStatus = GameStatus.Status_Normal;
        gameObject.SetActive(false);
    }
}
