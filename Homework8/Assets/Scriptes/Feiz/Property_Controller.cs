using UnityEngine;
using System.Collections;

public class Property_Controller : MonoBehaviour
{
    static Property_Controller instance;
    public static bool dead = false;
    //获取单实例
    public static Property_Controller Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Property_Controller();
            }
            return instance;
        }
    }

    public static Character_Property my_Property;


    /// <summary>
    /// 使用属性点
    /// </summary>
    public void Add_Point()
    {
        if(my_Property.remain_point >=1)
            my_Property.remain_point -= 1;
        GameObject a = Instantiate(Resources.Load("Feiz/Voice/AddPoint")) as GameObject;
        a.transform.position = Character_Controller.character.transform.position;
        Destroy(a, 1);
    }

    /// <summary>
    /// 设置剩余属性点
    /// </summary>
    /// <param name="value"></param>
    public void Set_Point(int value)
    {
        if (value < 0)
            value = 0;
        my_Property.remain_point = value;
    }

    //获取剩下技能点
    public int Get_Skill_Point()
    {
        return my_Property.remain_skill_point;
    }

    //改变剩下技能点
    //正数为加，负数为减
    public void Change_Skill_Point(int value)
    {
        my_Property.remain_skill_point += value;
        if (my_Property.remain_skill_point < 0)
        {
            my_Property.remain_skill_point = 0;
        }

    }

    /// <summary>
    /// 设置当前技能点
    /// </summary>
    /// <param name="value">数值，小于0设置成0</param>
    public void Set_Skill_Point(int value)
    {
        if (value < 0)
        {
            value = 0;
        }
        my_Property.remain_skill_point = value;
    }


    public void Change_Def(int value)
    {
        my_Property.defensive += value;
        if (my_Property.defensive < 0)
            my_Property.defensive = 0;
    }




    /// <summary>
    /// 获取背包
    /// </summary>
    /// <returns></returns>
    public Container Get_Bag()
    {
        return my_Property.bag;
    }

    /// <summary>
    /// 判断是否死亡（Property_Controller.dead代替）
    /// </summary>
    /// <returns></returns>
    public bool isDead()
    {
        return dead;
    }

    /// <summary>
    /// 获取当前物品数量
    /// </summary>
    /// <returns>当前物品数量</returns>
    public int GetCurrentCreatures()
    {
        return my_Property.currentCreatures;
    }

    /// <summary>
    /// 当前获取物品数量加一
    /// </summary>
    public void CurrentCreaturesIncrease()
    {
        my_Property.currentCreatures += 1;
    }

    /// <summary>
    /// 设置当前物品数量
    /// </summary>
    /// <param name="currentCreatures"></param>
    public void SetCurrentCreatures(int currentCreatures)
    {
        my_Property.currentCreatures = currentCreatures;
    }

    /// <summary>
    /// 当前任务物品减一
    /// </summary>
    /// <param name="currentCreatures"></param>
     public void CurrentCreaturesDecrease()
    {
        my_Property.currentCreatures -= 1;
    }

    /// <summary>
    /// 获取当前任务ID
    /// </summary>
    /// <returns></returns>
    public int GetCurrentTask()
    {
        return my_Property.currentTask;
    }

    /// <summary>
    /// 当前任务ID加一
    /// </summary>
    public void CurrentTaskIncrease()
    {
        my_Property.currentTask += 1;
    }

    /// <summary>
    /// 设置当前任务ID
    /// </summary>
    /// <param name="id"></param>
    public void SetCurrentTask(int id)
    {
        my_Property.currentTask = id;
    }

    /// <summary>
    /// 获取当前杀怪数目
    /// </summary>
    /// <returns></returns>
    public int GetKillMonster()
    {
        return my_Property.killMonster;
    }

    /// <summary>
    /// 当前杀怪数目加一
    /// </summary>
    public void KillMonsterIncrease()
    {
        my_Property.killMonster += 1;
    }

    /// <summary>
    /// 设置当前杀怪数目
    /// </summary>
    /// <param name="num"></param>
    public void SetKillMonster(int num)
    {
        my_Property.killMonster = num;
    }


    //选择职业
    //type == 0 :选择swordman
    //type == 1 :选择magicman
    public void ChooseCareer(int type)
    {
        my_Property = new Character_Property();
        GameObject man = GameObject.FindWithTag("Player");
        if (man != null)
        {
            Destroy(man.GetComponent<Character_Controller>());
            Destroy(man);
        }
        if (type == 1)
        {
            my_Property.career = Career.Magician;
            GameObject a = Instantiate(Resources.Load("Feiz/Magician")) as GameObject;
            Character_Controller.Instance.setCharacter(a);
        }
        else
        {
            my_Property.career = Career.Swordman;
            GameObject a = Instantiate(Resources.Load("Feiz/Swordman")) as GameObject;
            Character_Controller.Instance.setCharacter(a);
        }
        Init();
    }


    /// <summary>
    /// 改变
    /// </summary>
    /// <param name="Value"></param>
    public void Change_Speed(int Value)
    {
        my_Property.speed += Value;
        if (my_Property.speed < 0)
        {
            my_Property.speed = 0;
        }
    }

    /// <summary>
    /// 改变最大血量
    /// </summary>
    /// <param name="value"></param>
    public void Change_Max_HP(int value)
    {
        my_Property.Character_MAX_HP += value;
        if (my_Property.Character_MAX_HP <= 0)
            my_Property.Character_MAX_HP = 0;
        if (my_Property.Character_Current_HP > my_Property.Character_MAX_HP)
            my_Property.Character_Current_HP = my_Property.Character_MAX_HP;
    }

    /// <summary>
    /// 改变最大蓝量
    /// </summary>
    /// <param name="value"></param>
    public void Change_Max_MP(int value)
    {
        my_Property.Character_MAX_MP += value;
        if (my_Property.Character_MAX_MP <= 0)
            my_Property.Character_MAX_MP = 0;
        if (my_Property.Character_Current_MP > my_Property.Character_MAX_MP)
            my_Property.Character_Current_MP = my_Property.Character_MAX_MP;
    }

    //获取当前血量
    public int Get_CurrentMP()
    {
        return my_Property.Character_Current_MP;
    }

    //设置当前蓝量
    public void Set_CurrentMP(int value)
    {
        if (value < 0)
        {
            my_Property.Character_Current_MP = 0;
        }
        else if (value > my_Property.Character_MAX_MP)
        {
            my_Property.Character_Current_MP = my_Property.Character_MAX_MP;
        }
        else
        {
            my_Property.Character_Current_MP = value;
        }

    }

    //改变蓝量（消耗或恢复）
    public void Change_CurrentMP(int value)
    {
        my_Property.Character_Current_MP += value;
        if (my_Property.Character_Current_MP < 0)
        {
            my_Property.Character_Current_MP = 0;
        }
        if (my_Property.Character_Current_MP > my_Property.Character_MAX_MP)
        {
            my_Property.Character_Current_MP = my_Property.Character_MAX_MP;
        }

    }

    //获取当前血量
    public int Get_CurrentHP()
    {
        return my_Property.Character_Current_HP;
    }

    //设置当前血量
    public void Set_CurrentHP(int value)
    {
        if (value < 0)
        {
            my_Property.Character_Current_HP = 0;
            dead = true;
        }
        else if (value > my_Property.Character_MAX_HP)
        {
            my_Property.Character_Current_HP = my_Property.Character_MAX_HP;
        }
        else
        {
            my_Property.Character_Current_HP = value;
        }

    }

    //改变血量（消耗或恢复）
    public void Change_CurrentHP(int value)
    {
        my_Property.Character_Current_HP += value;
        if (my_Property.Character_Current_HP < 0)
        {
            my_Property.Character_Current_HP = 0;
            dead = true;
        }
        if (my_Property.Character_Current_HP > my_Property.Character_MAX_HP)
        {
            my_Property.Character_Current_HP = my_Property.Character_MAX_HP;
        }

    }

    //获取当前AD
    public int Get_AD()
    {
        return my_Property.AD;
    }
    
    //设置当前AD
    public void Set_AD(int value)
    {
        my_Property.AD = value;
    }

    //改变当前AD(增加或减少)
    public void Change_AD(int value)
    {
        my_Property.AD += value;
        if (my_Property.AD < 0)
        {
            my_Property.AD = 0;
        }
    }


    //获取当前AP
    public int Get_AP()
    {
        return my_Property.AP;
    }

    //设置当前AP
    public void Set_AP(int value)
    {
        my_Property.AP = value;
    }

    //改变当前AP(增加或减少)
    public void Change_AP(int value)
    {
        my_Property.AP += value;
        if (my_Property.AP < 0)
        {
            my_Property.AP = 0;
        }
    }


    //初始化角色
    //不同职业不同属性
    public void Init()
    {
        if (my_Property.career == Career.Magician)
        {
            my_Property.Character_MAX_HP = my_Property.Character_Current_HP = 100;
            my_Property.Character_MAX_MP = my_Property.Character_Current_MP = 200;
            my_Property.defensive = 2;
            my_Property.AP = 8;
            my_Property.AD = 100;
        }
        else
        {
            my_Property.Character_MAX_HP = my_Property.Character_Current_HP = 200;
            my_Property.Character_MAX_MP = my_Property.Character_Current_MP = 100;
            my_Property.defensive = 5;
            my_Property.AD = 100;
            my_Property.AP = 3;
        }
        my_Property.speed = 100;
        my_Property.bag = new Container();
        my_Property.level = 1;
        my_Property.remain_point = 5;
        my_Property.remain_skill_point = 1;
        my_Property.Character_MAX_EXP = 100;
        my_Property.Character_Current_EXP = 0;
        my_Property.money = 10000000;
        my_Property.currentTask = 0;
        my_Property.bag.addGoods("MpPotion", 0);
        my_Property.bag.addGoods("MpPotion", 0);
        my_Property.bag.addGoods("MpPotion", 0);
        my_Property.bag.addGoods("MpPotion", 0);
        my_Property.bag.addGoods("MpPotion", 0);
        my_Property.bag.addGoods("MpPotion", 0);
        my_Property.bag.addGoods("RustySword", 0);
        my_Property.bag.addGoods("HpPotion", 0);
        my_Property.bag.addGoods("Long Sword", 0);
        my_Property.bag.addGoods("Sword Of Knight", 0);
        my_Property.bag.addGoods("Rod Of Learner", 0);
        my_Property.bag.addGoods("HpPotion", 0);
    }


    /// <summary>
    /// 获得经验
    /// </summary>
    /// <param name="exp"></param>
    public void GetEXP(int exp)
    {
        if (exp < 0)
        {
            return;
        }
        my_Property.Character_Current_EXP += exp;
        while (my_Property.Character_Current_EXP >= my_Property.Character_MAX_EXP)
        {
            my_Property.Character_Current_EXP -= my_Property.Character_MAX_EXP;
            Upgrade();
        }
    }

    /// <summary>
    /// 获取当前金钱
    /// </summary>
    /// <returns></returns>
    public int GetMoney()
    {
        return my_Property.money;
    }

    /// <summary>
    /// 改变金钱数量
    /// </summary>
    /// <param name="money">正数为加，负数为减</param>
    public void ChangeMoney(int money)
    {
        my_Property.money += money;
        if (my_Property.money < 0)
        {
            my_Property.money = 0;
        }
    }

    //升级角色
    public void Upgrade()
    {
        GameObject a = Instantiate(Resources.Load("Feiz/Effect/LvUp")) as GameObject;
        a.transform.position = Character_Controller.character.transform.position;
        if (my_Property.career == Career.Magician)
        {
            my_Property.Character_MAX_HP = my_Property.Character_Current_HP = my_Property.Character_MAX_HP+10;
            my_Property.Character_MAX_MP = my_Property.Character_Current_MP = my_Property.Character_Current_MP+20;
            my_Property.defensive += 1;
            my_Property.AD += 1;
            my_Property.AP += 3;
        }
        else
        {
            my_Property.Character_MAX_HP = my_Property.Character_Current_HP = my_Property.Character_MAX_HP+20;
            my_Property.Character_MAX_MP = my_Property.Character_Current_MP = my_Property.Character_Current_MP + 10;
            my_Property.defensive += 2;
            my_Property.AD += 2;
            my_Property.AP += 1;
        }
        my_Property.level += 1;
        my_Property.Character_MAX_EXP += 10;
        my_Property.remain_point += 5;
        my_Property.remain_skill_point += 1;
        if (UI_manager.m_SkillBookUI != null && UI_manager.m_SkillBookUI.active)
        {
            UI_manager.m_SkillBookUI.BroadcastMessage("sp");
        }
        Destroy(a, 2f);
    }

    //传上装备
    public void Equip(int _item_id)
    {

    }

    /// <summary>
    /// 设置角色名称
    /// </summary>
    /// <param name="name"></param>
    public void SetName(string name)
    {
        my_Property.name = name;
    }

    /// <summary>
    /// 获取角色名称
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return my_Property.name;
    }
}
