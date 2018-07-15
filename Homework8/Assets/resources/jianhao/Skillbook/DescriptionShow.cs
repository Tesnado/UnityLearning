using UnityEngine;
using System.Collections;

public class DescriptionShow : MonoBehaviour
{
    public static DescriptionShow instance;

    public static int current_click = 0;
    private int last_click = 0;
    private bool[] is_show = new bool[4];

    void Awake()
    {
        instance = this;
        current_click = 0;
        last_click = 0;
        for (int i = 0; i < 4; i++)
        {
            is_show[i] = false;
        }
    }

    public void Refresh()
    {
        if (!is_show[current_click])
        {
            if (!is_show[last_click])
            {
                gameObject.SetActive(true);
            }
            is_show[last_click] = false;
            is_show[current_click] = true;
            //SendMessage("ReadSkill");
            BroadcastMessage("detail");
            //DetailLoad.Instance.ReadSkill();
        }
        else
        {
            gameObject.SetActive(false);
            is_show[current_click] = false;
        }
        last_click = current_click;
    }

    public void OnClickShowButton(string skillName)
    {
        current_click = skillName[skillName.Length - 1] - '1';
    }

    /*

        public void Click1()
        {
            current_click = 0;
        }

        public void Click2()
        {
            current_click = 1;
        }

        public void Click3()
        {
            current_click = 2;
        }

        public void Click4()
        {
            current_click = 3;
        }*/
}