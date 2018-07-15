using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerBG : MonoBehaviour
{
    private UISprite sprite;
    private UISprite Sprite
    {
        get
        {
            if (sprite == null)
                sprite = GetComponent<UISprite>();
            return sprite;
        }
    }

    private TweenAlpha tween;
    private TweenAlpha Tween
    {
        get
        {
            if (tween == null)
                tween = GetComponent<TweenAlpha>();
            return tween;
        }
    }

    private void Start()
    {
        OnNormalMode();
    }

    public void OnCustomMode()
    {
        Sprite.enabled = true;
        Tween.enabled = true;
    }

    public void OnNormalMode()
    {
        Sprite.enabled = false;
        Tween.enabled = false;
    }
}