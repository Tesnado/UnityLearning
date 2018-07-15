using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestAndDevil;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController Instance;
    public Text hintText;
    public GameObject btnRestart;

    private Action hintAction;
    private Action restartAction;
    private IUserController ctrl;

    void Awake()
    {
        Instance = this;
        //hintEvent = () => { Debug.LogError("asdsadas!"); };
    }
    //<== 1 Devil
    void Start()
    {
        ctrl = SSDirector.getInstance().currentSceneController as IUserController;
    }

    public void OnClickHint()
    {
        //hintAction();
        ctrl.moveAI();
    }

    public void OnClickRestart()
    {
        //restartAction();
        ctrl.restart();
        btnRestart.SetActive(false);
        hintText.text = "";
    }

    public void ShowTxtHint(string hint)
    {
        //hintText.text = ctrl.getMinPlan();
        hintText.text = hint;
    }

    public void SetHintEvent(Action action)
    {
        hintAction = action;
    }

    public void SetRestartEvent(Action action)
    {
        restartAction = action;
    }

    public void Refresh(int status)
    {
        if (status == 1)
        {
            hintText.text = "You Lose!";
            btnRestart.SetActive(true);
        }
        else if (status == 2)
        {
            hintText.text = "You Win!";
            btnRestart.SetActive(true);
        }
    }
}
