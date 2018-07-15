using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shortcut : MonoBehaviour
{
    public int containerID;
    public string skillName;
    private ShortcutDrag drag;
    private UIButton button;
    private UISprite sprite;
    private UISprite mask;

    void Awake()
    {
        button = GetComponent<UIButton>();
        sprite = GetComponent<UISprite>();
        mask = transform.Find("Mask").GetComponent<UISprite>();
        drag = GetComponent<ShortcutDrag>();
        EventDelegate ed = new EventDelegate(this, "OnButtonClick");
        button.onClick.Add(ed);
    }

    // Use this for initialization
    void Start()
    {
        mask.spriteName = skillName;
        sprite.spriteName = skillName;
    }

    // Update is called once per frame
    void Update()
    {
        mask.fillAmount = Skill.cd[skillName[skillName.Length - 1] - '1'] / Skill_Register.GetSkill(skillName).cd;
        //Debug.Log(Skill.cd[skillName[skillName.Length - 1] - '1']);
    }

    void OnButtonClick()
    {
        Skill_Register.DoSkill(skillName);
    }

    //销毁时注销
    private void OnDestroy()
    {
        CustomMode.instance.CustomModeOn -= OnCustomMode;
        CustomMode.instance.CustomModeOff -= OnNormalMode;
        ShortcutManager.instance.Disuse(containerID);
        ShortcutManager.instance.RemoveSkill(skillName);
    }

    public void OnCustomMode()
    {
        drag.enabled = true;
    }

    public void OnNormalMode()
    {
        drag.enabled = false;
    }
}