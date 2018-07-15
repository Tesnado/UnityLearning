using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutManager : MonoBehaviour
{
    public static ShortcutManager instance;

    private List<string> shortcutSkillList = new List<string>();
    private GameObject[] container = new GameObject[4];
    private bool[] containerUsed = new bool[4];
    private GameObject shortcutPrefab;
    private GameObject transhCanContainer;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < containerUsed.Length; i++)
        {
            container[i] = transform.Find(i + "ShortcutContainer").gameObject;
            ContainerBG bg = container[i].GetComponentInChildren<ContainerBG>();
            CustomMode.instance.CustomModeOn += bg.OnCustomMode;
            CustomMode.instance.CustomModeOff += bg.OnNormalMode;
            containerUsed[i] = false;
        }
        shortcutPrefab = Resources.Load("jianhao/Shortcut/Shortcut") as GameObject;
        transhCanContainer = transform.Find("ShortcutTrashCan").gameObject;
        CustomMode.instance.CustomModeOn += TranshCanOn;
        CustomMode.instance.CustomModeOff += TranshCanOff;
        TranshCanOff();
        /*AddSkill("SwordSkill1");
       AddSkill("SwordSkill2");
       AddSkill("SwordSkill3");
       AddSkill("SwordSkill4");*/
    }

    public void Use(int id)
    {
        containerUsed[id] = true;
    }

    public void Disuse(int id)
    {
        containerUsed[id] = false;
    }

    //为Shortcut加入新技能
    public void AddSkill(string skillName)
    {
        if (shortcutSkillList.Contains(skillName))
            return;
        for (int i = 0; i < containerUsed.Length; i++)
            if (containerUsed[i] == false)
            {
                shortcutSkillList.Add(skillName);
                GameObject go = NGUITools.AddChild(container[i], shortcutPrefab);
                Shortcut sc = go.GetComponent<Shortcut>();
                sc.containerID = i;
                sc.skillName = skillName;
                CustomMode.instance.CustomModeOn += sc.OnCustomMode;
                CustomMode.instance.CustomModeOff += sc.OnNormalMode;
                containerUsed[i] = true;
                break;
            }
    }

    public void RemoveSkill(string skillName)
    {
        shortcutSkillList.Remove(skillName);
    }

    private void TranshCanOn()
    {
        transhCanContainer.SetActive(true);
    }

    private void TranshCanOff()
    {
        transhCanContainer.SetActive(false);
    }
}