using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text text;
    public GameObject btnStart;
    public Toggle toggle;

    UserAction UserActionController;
    bool ss = false;
    float S;
    float Now;
    int round = 1;
    // Use this for initialization
    void Start()
    {
        UserActionController = SSDirector.getInstance().currentScenceController as UserAction;
        S = Time.time;
    }

    void Update()
    {
        if (!ss) S = Time.time;
        btnStart.SetActive(!ss);
        text.text = "Score: " + UserActionController.GetScore().ToString() + "  Time:  " + ((int)(Time.time - S)).ToString() + "  Round:  " + round;
        if (ss)
        {
            round = UserActionController.GetRound();
            if (Input.GetButtonDown("Fire1"))
            {

                Vector3 pos = Input.mousePosition;
                UserActionController.Hit(pos);
            }
            if (round > 3)
            {
                round = 3;
                if (UserActionController.RoundStop())
                {
                    ss = false;
                }
            }
        }
    }

    public void OnClickStart()
    {
        S = Time.time;
        ss = true;
        UserActionController.Restart();
    }

    public void OnToggle()
    {
        FirstSceneController.Instance.PhysicManager = toggle.isOn;
    }
}
