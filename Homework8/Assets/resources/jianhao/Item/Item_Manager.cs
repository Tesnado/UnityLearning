using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Item_Manager {
	private static Item_Attribution[] items = new Item_Attribution[100];

	/*private static Item_Manager instance;
	public static Item_Manager Instance {
		get {
			if(instance == null) {
				instance = new Item_Manager();
			}
			return Item_Manager.instance;
		}
	}*/

	public static void AddItem (int _item_id,Item_Attribution _item) {//注册物品
		if (items [_item_id] == null)
			items [_item_id] = _item;
	}

	public static void CreateItem (int _item_id,int _num) {//创造物品 _num为创造数量
		if (items [_item_id] != null)
			items [_item_id].num += _num;
	}

	public static bool UseItem (int _item_id) {//使用物品 没有则返回false
		if (items [_item_id] != null) {
			if(items [_item_id].num>=1){
				if(items [_item_id].type==Item_Attribution.Item_Type.One_Time
				   ||items [_item_id].type==Item_Attribution.Item_Type.Task
				   ) {
					items [_item_id].num --;
					//Propertity_Controller_Change_CurrentHP(items[_item_id].add_hp);
					//Propertity_Controller_Change_CurrentMP(items[_item_id].add_mp);
				}
					
				else //if(items [_item_id].type==Item_Attribution.Item_Type.Right_Hand||
				     //   items [_item_id].type==Item_Attribution.Item_Type.Armor||)
				{	
					items [_item_id].armed=true;
					//Propertity_Controller_Change_AD(items[_item_id].add_ad);
					//Propertity_Controller_Change_AP(items[_item_id].add_ap);
				}
					
				return true;
			} else 
				return false;
		} 
		return false;
	}


}
