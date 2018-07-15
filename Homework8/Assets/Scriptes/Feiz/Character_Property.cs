using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Career
{
    Swordman,
    Magician
}

public class Character_Property {
  
    public int Character_MAX_HP, Character_MAX_MP, Character_MAX_EXP;
    public int Character_Current_HP, Character_Current_MP, Character_Current_EXP;
    public string name;
    public Career career;
    public float criticalRate;
    public int level, money;
    public int AD,AP, defensive;
    public int currentTask;
    public int speed;
    public int killMonster;
    public int[] SubTask_Monster = new int[100];
    public int currentCreatures;
    public Goods[]  equipments = new Goods[7];
    public Container bag;
    public int remain_point;
    public int remain_skill_point;
}
