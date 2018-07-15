using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class Item : MonoBehaviour {
	static Item instance;
	public static Item Instance {
		get {
			if(instance==null){
				instance=new Item();
			}
			return instance;
		}
	}

	void Awake () {
		#if UNITY_EDITOR
		string filepath = Application.dataPath +"/StreamingAssets/";
		
		#elif UNITY_ANDROID
		string filepath = "jar:file://" + Application.dataPath + "!/assets/";
		
		#endif
		
		ReadItemInfo(filepath,"ItemInfo.txt");
	}

	void ReadItemInfo(string path,string name) {
		StreamReader sr = null;
		try {
			sr = File.OpenText(path+"//"+ name);
		} catch (Exception e) {
			return;
		}
		string line;
		while ((line = sr.ReadLine()) != null) {
			string[] attr = line.Split(',');
			Item_Attribution temp = new Item_Attribution();
			temp.id = int.Parse(attr[0]);
			temp.price = int.Parse(attr[1]);
			temp.name = attr[2];
			switch (attr[3]) {
			case "onetime":
				temp.type = Item_Attribution.Item_Type.One_Time;
				break;
			case "righthand":
				temp.type = Item_Attribution.Item_Type.Right_Hand;
				break;
			case "lefthand":
				temp.type = Item_Attribution.Item_Type.left_Hand;
				break;
			case "armor":
				temp.type = Item_Attribution.Item_Type.Armor;
				break;
			case "headgear":
				temp.type = Item_Attribution.Item_Type.Headgear;
				break;
			case "shoe":
				temp.type = Item_Attribution.Item_Type.Shoe;
				break;
			case "accessory":
				temp.type = Item_Attribution.Item_Type.Accessory;
				break;
			}
			temp.num = 0;
			temp.armed = false;
			if (temp.type == Item_Attribution.Item_Type.One_Time) {
				temp.add_hp = int.Parse(attr[4]);
				temp.add_mp = int.Parse(attr[5]);
			} else {
				temp.add_ad = int.Parse(attr[4]);
				temp.add_ap = int.Parse(attr[5]);
				temp.add_df = int.Parse(attr[6]);
			}
			Item_Manager.AddItem (temp.id, temp);
		}
		sr.Close ();
		sr.Dispose ();
	}  

}
