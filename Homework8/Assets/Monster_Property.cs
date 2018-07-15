using UnityEngine;
using System.Collections;

public class Monster_Property : MonoBehaviour {
	public int Monster_Max_Hp, Monster_Current_Hp, Damage;       //怪物最大血量,怪物当前血量以及怪物的伤害
	public int Monster_DropExp;                                  //怪物掉落经验
	public double DropRate;                                       //物品掉率
	public string MonsterName;                                   //怪物名
}
