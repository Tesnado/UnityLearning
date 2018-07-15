using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillListener : MonoBehaviour
{
    /*private GameObject button;
    private GameObject Button
    {
        get
        {
            if (button == null)
                button = gameObject;
            return button;
        }
    }*/
    public string skillName;
    private float timer;
    private float pressTime = 0.5f;
    private bool isPressed = false;

    void Awake()
    {
        /*UIEventListener.Get(Button).onPress = OnPress;
        UIEventListener.Get(Button).o = OnPress;*/
        if (Property_Controller.my_Property.career == Career.Swordman)
            skillName = "Sword" + skillName;
        else if (Property_Controller.my_Property.career == Career.Magician)
            skillName = "Magic" + skillName;
    }

    void Update()
    {
        if (isPressed)
            timer += Time.deltaTime;
    }

    public void OnPress()
    {
        isPressed = true;
    }

    public void OnRelease()
    {
        if (timer > pressTime)
            ShortcutManager.instance.AddSkill(skillName);
        else
        {
            if (DescriptionShow.instance.gameObject.activeInHierarchy == false)
                DescriptionShow.instance.gameObject.SetActive(true);
            DescriptionShow.instance.OnClickShowButton(skillName);
            DescriptionShow.instance.Refresh();
        }

        isPressed = false;
        timer = 0;
    }

    /*
            IEnumerator CanClick()
            {
                yield return new WaitForSeconds(0.15f);
                DesciptionShow.instance.canClick = true;
            }*/
}