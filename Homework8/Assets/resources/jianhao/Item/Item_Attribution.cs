using UnityEngine;
using System.Collections;
using System;

public class Item_Attribution {
	public enum Item_Type
	{
		One_Time,
		Task,

		Headgear,
		Armor,
		Right_Hand,
		left_Hand,
		Shoe,
		Accessory
	}

	//public Character_Type char_type;
	public Item_Type type;
	public int id;
	public int num;
	public int price;

	public int add_hp;
	public int add_mp;
	public int add_ad;
	public int add_ap;
	public int add_df;

	public bool armed;//是否被穿
	public string name;
	public string description;
	public Action effect;
}
