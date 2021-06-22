using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BSKUIButton : Button {

    private Button shootBtn;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        shootBtn = GetComponent<Button>();
        BSKEventTriggerListener.Get(shootBtn.gameObject).onClick = shootBtnUp;
        BSKEventTriggerListener.Get(shootBtn.gameObject).onDown = shootBtnDown;
        BSKEventTriggerListener.Get(shootBtn.gameObject).onExit = shootBtnCancel;
    }


    void shootBtnDown(GameObject obj)
    {
        Debug.Log("shootBtnDown");
        BSKShooter.shooter.shootBtnClickStart();
    }

    void shootBtnUp(GameObject obj)
    {
        Debug.Log("shootBtnUp");
        BSKShooter.shooter.shootBtnClickEnd();
    }

    void shootBtnCancel(GameObject obj)
    {
        Debug.Log("shootBtnCancel");
        BSKShooter.shooter.shootBtnClickCancel();
    }
}
