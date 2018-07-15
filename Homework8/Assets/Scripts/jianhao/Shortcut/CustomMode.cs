using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMode : MonoBehaviour
{
    public static CustomMode instance;

    public delegate void CustomModeManager();

    public event CustomModeManager CustomModeOn;

    public event CustomModeManager CustomModeOff;

    private EventDelegate buttonEvent;

    private UIButton button;
    private TweenRotation tweenRot;

    void Awake()
    {
        instance = this;
        tweenRot = GetComponent<TweenRotation>();
        button = GetComponent<UIButton>();
        buttonEvent = new EventDelegate(this, "OnCustomModeOn");
        button.onClick.Add(buttonEvent);
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCustomModeOn()
    {
        //Debug.Log(1);
        tweenRot.PlayForward();
        CustomModeOn();
        buttonEvent.methodName = "OnCustomModeOff";
    }

    void OnCustomModeOff()
    {
        //Debug.Log(2);
        tweenRot.PlayReverse();
        CustomModeOff();
        buttonEvent.methodName = "OnCustomModeOn";
    }
}