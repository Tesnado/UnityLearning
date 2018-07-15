using UnityEngine;
using System.Collections;
using System;

/*public enum Character_Type
{
	Sword,
	Magic
}*/

public class Skill_Attribution
{
    //public Character_Type char_type;
    public float cd;

    public int mp_use;
    public int skill_level;
    public int damage;
    public string name;
    public string des;
    public string damage_des;
    public Action effect;
}

public abstract class Base
{
    protected int a;
}

public class A : Base
{
    private int b;

    void qwe()
    {
        b = a;
    }
}