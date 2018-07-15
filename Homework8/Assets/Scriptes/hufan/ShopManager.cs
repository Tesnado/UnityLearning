using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;	

public class ShopManager : MonoBehaviour {

	// Use this for initialization
	GameObject[] objlist = new GameObject[12];
	List<Goods> goodslist;
	UILabel pageobj,label2,label3,label4;
	int choose;
	int page;
	int count;
	int price;
	int money;

	void Start () {
		
        //Container shop = new Container ();
        //shop.addGoods ("potion1", 1);
        //shop.addGoods ("potion2", 2);
        //shop.addGoods ("potion3", 3);
        //shop.addGoods ("potion1", 4);
        //shop.addGoods ("potion2", 5);
        //shop.addGoods ("potion3", 6);
        //shop.addGoods ("potion1", 7);
        //shop.addGoods ("potion2", 8);
        //shop.addGoods ("potion3", 9);
        //shop.addGoods ("potion1", 10);
        //shop.addGoods ("potion2", 11);
        //shop.addGoods ("potion3", 12);
        //shop.addGoods ("potion1", 13);
        //shop.addGoods ("potion2", 14);
        //shop.addGoods ("potion3", 15);
        //shop.addGoods ("potion1", 16);
        //shop.addGoods ("potion2", 17);
        //shop.addGoods ("potion3", 18);
        //shop.addGoods ("po123n1", 19);
        //Init (shop);
	}


	void onclick(GameObject btnn) {
//Debug.Log(btnn);
		string name = btnn.name;
		//Debug.Log (name);
		if (name.Contains ("Shop_Button_")) {
			if (name.Contains ("10"))
				return;
			if ((page - 1) * 9 + name [12] - '0' - 1 >= goodslist.Count)
				return;
			if (choose != name [12] - '0')
				count = 0;
			choose = name [12] - '0';
		} else if (name == "Shop_Left") {
			if (page > 1) {
				page--;
				count = 0;
				choose = 0;
			}
		} else if (name == "Shop_Right") {
			if (page * 9 < goodslist.Count) {
				page++;
				count = 0;
				choose = 0;
			}
		} else if (name == "Shop_Up") {
			if (choose == 0) return;
			count++;
		} else if (name == "Shop_Down") {
			if (choose == 0) return;
			if (count > 0)
				count--;
		} else if (name == "Shop_Buy") {
			if (choose == 0) return;
            if (price * count > Property_Controller.Instance.GetMoney())
                return;
            Property_Controller.Instance.ChangeMoney(-price * count);
            for (int i = 1; i <= count; i++)
            {
                Property_Controller.Instance.Get_Bag().addGoods(goodslist[(page - 1) * 9 + choose - 1].name, 0);
//                TaskManager.Instance.CollectCreatures(goodslist[(page - 1) * 9 + choose - 1].name);
            }
            price = 0;
            count = 0;
			choose = 0;
            if (UI_manager.m_BagUI != null)
                UI_manager.m_BagUI.GetComponent<BagManager>().refresh();
		} else if (name == "Shop_Exit") {
            this.gameObject.SetActive(false);
        }
        refresh();
	}

    public  void refresh()
    {
        money = Property_Controller.Instance.GetMoney();
        if ((page - 1) * 9 >= goodslist.Count && page > 1)
        {
            page--;
            choose = 0;
        }
        pageobj.text = "" + page;
        for (int i = (page - 1) * 9, j = 1; i < page * 9; i++, j++)
        {
            //Debug.Log (i + " " +  j);
            if (i < goodslist.Count)
            {
                objlist[j].GetComponent<UIButton>().normalSprite = goodslist[i].spname;
                objlist[j].transform.GetChild(0).GetComponent<UISprite>().spriteName = goodslist[i].spname;
                objlist[j].transform.GetChild(1).GetComponent<UILabel>().text = "";
                //btn[j] = goodslist[i].spname;
                if (goodslist[i].price != 0)
                    objlist[j].transform.GetChild(1).GetComponent<UILabel>().text += goodslist[i].price;
            }
            else
            {
                objlist[j].GetComponent<UIButton>().normalSprite = "icon-null";
                objlist[j].transform.GetChild(0).GetComponent<UISprite>().spriteName = "icon-null";
                objlist[j].transform.GetChild(1).GetComponent<UILabel>().text = "";
                //btn[j] = "icon-null";
            }
        }
        if (choose != 0)
        {
            objlist[10].GetComponent<UIButton>().normalSprite =
                objlist[choose].transform.GetChild(0).GetComponent<UISprite>().spriteName;
            objlist[10].transform.GetChild(0).GetComponent<UISprite>().spriteName =
                objlist[choose].transform.GetChild(0).GetComponent<UISprite>().spriteName;
            objlist[10].transform.GetChild(1).GetComponent<UILabel>().text =
                objlist[choose].transform.GetChild(1).GetComponent<UILabel>().text;
            label2.text = "" + count;
            price = goodslist[(page - 1) * 9 + choose - 1].price;
            label3.text = "" + count * price;
            label4.text = "" + money;
        }
        else
        {
            objlist[10].GetComponent<UIButton>().normalSprite = "icon-null";
            objlist[10].transform.GetChild(0).GetComponent<UISprite>().spriteName = "icon-null";
            objlist[10].transform.GetChild(1).GetComponent<UILabel>().text = "";
            price = 0;
            label2.text = "";
            label3.text = "";
            label4.text = "" + money;
        }
    }

	// Update is called once per frame
	void Update () {
	}

	protected static ShopManager instance_ = null;
	public static ShopManager instance {
		get {
			if (instance_ == null) {
				instance_ = new ShopManager();
			}
			return instance_;
		}
	}

	public void Init(Container container) {
		if (container == null)
			return;
		goodslist = container.goodslist;
		page = 1;
		choose = 0;
		count = 0;
		price = 0;
		money = 100;
        List<string> btnName = new List<string>();
        btnName.Clear();
        for (int i = 1; i <= 10; i++)
            btnName.Add("Shop_Button_" + i);
        btnName.Add("Shop_Left");
        btnName.Add("Shop_Right");
        btnName.Add("Shop_Up");
        btnName.Add("Shop_Down");
        btnName.Add("Shop_Buy");
        btnName.Add("Shop_Exit");
        foreach (string btnname in btnName)
        {
            GameObject btnobj = GameObject.Find(btnname);
            if (btnobj != null)
            {
                btnobj.AddComponent<UIEventListener>();
                UIEventListener.Get(btnobj).onClick = onclick;
            }
        }
        for (int i = 1; i <= 10; i++)
            objlist[i] = GameObject.Find("Shop_Button_" + i);
        pageobj = GameObject.Find("Shop_Label_1").GetComponent<UILabel>();
        label2 = GameObject.Find("Shop_Label_2").GetComponent<UILabel>();
        label3 = GameObject.Find("Shop_Label_3").GetComponent<UILabel>();
        label4 = GameObject.Find("Shop_Label_4").GetComponent<UILabel>();
        refresh();
		/*
		//UIAtlas at = (UIAtlas)Resources.Load ("icon");
		for (int i = 0; i < 9; i++)
			if (i >= goodslist.Count)
				break;
			else {
				//objlist[i + 1].transform.GetChild(0).GetComponent<UISprite>().atlas = at;
				objlist[i + 1].transform.GetChild(0).GetComponent<UISprite>().spriteName = goodslist[i].spname;
				objlist[i + 1].transform.GetChild(1).GetComponent<UILabel>().text = "" + goodslist[i].price;
		}*/
	}

}


public class Goods{
	public Goods(string name_, int price_) {
		name = name_;
		price = price_;
		if (name == "HpPotion") {
			spname = ("icon-potion1");
		} else if (name == "potion2") {
			spname = ("icon-potion2");
			type = 0;
		} else if (name == "MpPotion") {
			spname = ("icon-potion3");
			type = 0;
		} else if (name == "RustySword") {
			spname = ("sword0-icon");
			type = 3;
		} else if (name == "Long Sword") {
			spname = ("sword1-icon");
			type = 3;
		} else if (name == "Sword Of Knight") {
			spname = ("sword2-icon");
			type = 3;
		} else if (name == "Rod Of Learner") {
			spname = ("rod-icon");
			type = 3;
		} else if (name == "Wood Rod") {
			spname = ("rod-icon02");
			type = 3;
		} else if (name == "Rod Of Magic") {
			spname = ("rod-icon03");
			type = 3;
		} else if (name == "Rusty Armor") {
			spname = ("armor0-icon");
			type = 2;
		} else if (name == "Iron Armor") {
			spname = ("armor1-icon");
			type = 2;
		} else if (name == "Mantle Of Learner") {
			spname = ("armor2-icon");
			type = 2;
		} else if (name == "Mantle Of Intelligence") {
			spname = ("armor3-icon");
			type = 2;
		} else if (name == "Wool Boot") {
			spname = "icon-boot0";
			type = 5;
		} else if (name == "Red Boot") {
			spname = "icon-boot0-01";
			type = 5;
		} else if (name == "etcFang") {
			spname = "icon-etcFang";
			type = -1;
		} else if (name == "etcFur") {
			spname = "icon-etcFur";
			type = 6;
		} else if (name == "White Helm") {
			spname = "icon-helm";
			type = 1;
		} else if (name == "Black Helm") {
			spname = "icon-helm-01";
			type = 1;
		} else if (name == "Magic Hat") {
			spname = "icon-helm-02";
			type = 1;
		} else if (name == "Higher Hat") {
			spname = "icon-helm-03";
			type = 1;
		} else if (name == "Yellow Ring") {
			spname = "icon-ring";
			type = 6;
		} else if (name == "Green Ring") {
			spname = "icon-ring-01";
			type = 6;
		} else if (name == "Blue Shield") {
			spname = "icon-shield";
			type = 4;
		} else if (name == "High Blue Shield") {
			spname = "icon-shield1";
			type = 4;
		} else if (name == "Tailman") {
			spname = "icon-tailman";
			type = -1;
		} else if (name == "Sange and Yasha") {
			spname = "sword0-icon00";
			type = 3;
		} else if (name == "null") {
			spname = "icon-null";
			type = -1;
		} else {
			spname = ("coin-icon");
			type = 0;
		}
	}
	public string name;
	public int price;
	public string spname;
	public int type;

	public void Use(int k = 1) {
		if (type == -1)
			return;
		if (Property_Controller.dead)
			return;
		if (type != 0 && k != -1) {
			Goods g1 = Property_Controller.my_Property.equipments[type];
			if (g1 != null) {
				Goods g2 = new Goods(g1.name, 0);
				g2.Use(-1);
				Property_Controller.Instance.Get_Bag().addGoods(g1);
			}
			Property_Controller.my_Property.equipments[type] = new Goods(name, 0);
		}
		Property_Controller.Instance.Get_Bag().delGoods(this);
		if (name == "HpPotion")
			Property_Controller.Instance.Change_CurrentHP (20 * k);
		else if (name == "potion2")
			Property_Controller.Instance.Change_CurrentHP (50 * k);
		else if (name == "MpPotion")
			Property_Controller.Instance.Change_CurrentMP (20 * k);
		else if (name == "RustySword") 
			Property_Controller.Instance.Change_AD (3 * k);
		else if (name == "Long Sword") 
			Property_Controller.Instance.Change_AD (8 * k);
		else if (name == "Sword Of Knight") 
			Property_Controller.Instance.Change_AD (15 * k);
		else if (name == "Rod Of Learner") {
			Property_Controller.Instance.Change_AD (1 * k);
			Property_Controller.Instance.Change_AP (3 * k);
		} else if (name == "Wood Rod") {
			Property_Controller.Instance.Change_AD (3 * k);
			Property_Controller.Instance.Change_AP (5 * k);
		} else if (name == "Rod Of Magic") {
			Property_Controller.Instance.Change_AD (5 * k);
			Property_Controller.Instance.Change_AP (12 * k);
		} else if (name == "Rusty Armor") {
			Property_Controller.Instance.Change_Def (5 * k);
			Property_Controller.Instance.Change_Max_HP (10 * k);
		} else if (name == "Iron Armor") {
			Property_Controller.Instance.Change_Def (10 * k);
			Property_Controller.Instance.Change_Max_HP (20 * k);
		} else if (name == "Mantle Of Learner") {
			Property_Controller.Instance.Change_Def (3 * k);
			Property_Controller.Instance.Change_Max_MP (20 * k);
		} else if (name == "Mantle Of Intelligence") {
			Property_Controller.Instance.Change_Def (5 * k);
			Property_Controller.Instance.Change_Max_MP (50 * k);
		} else if (name == "Wool Boot") {
			Property_Controller.Instance.Change_Def (2 * k);
			Property_Controller.Instance.Change_Speed (10 * k);
		} else if (name == "Red Boot") {
			Property_Controller.Instance.Change_Def (2 * k);
			Property_Controller.Instance.Change_Speed (20 * k);
		} else if (name == "etcFur") {
			Property_Controller.Instance.Change_Max_HP (1000 * k);
			Property_Controller.Instance.Change_Max_MP (1000 * k);
			Property_Controller.Instance.Change_AD (1000 * k);
			Property_Controller.Instance.Change_AP (1000 * k);
			Property_Controller.Instance.Change_Def (1000 * k);
			Property_Controller.Instance.Change_Speed (100 * k);
		} else if (name == "White Helm") {
			Property_Controller.Instance.Change_Def (2 * k);
		} else if (name == "Black Helm") {
			Property_Controller.Instance.Change_Def (5 * k);
		} else if (name == "Magic Hat") {
			Property_Controller.Instance.Change_AP (2 * k);
		} else if (name == "Higher Hat") {
			Property_Controller.Instance.Change_AP (5 * k);
		} else if (name == "Yellow Ring") {
			Property_Controller.Instance.Change_Max_HP (20 * k);
			Property_Controller.Instance.Change_Max_MP (40 * k);
		} else if (name == "Green Ring") {
			Property_Controller.Instance.Change_AD (5 * k);
			Property_Controller.Instance.Change_AP (10 * k);
		} else if (name == "Blue Shield") {
			Property_Controller.Instance.Change_Max_HP (40 * k);
			Property_Controller.Instance.Change_Def (5 * k);
		} else if (name == "High Blue Shield") {
			Property_Controller.Instance.Change_Max_MP (80 * k);
			Property_Controller.Instance.Change_Def (8 * k);
		} else if (name == "Sange and Yasha") {
			Property_Controller.Instance.Change_AD (20 * k);
			Property_Controller.Instance.Change_Speed (60 * k);
		}else
			Property_Controller.Instance.ChangeMoney (10);
		if (UI_manager.m_BagUI != null)
			UI_manager.m_BagUI.GetComponent<BagManager>().refresh();
	}
}

public class Container {
	public Container() {
		goodslist = new List<Goods> ();
		goodslist.Clear ();
	}

	public bool Use(string spriteName) {
		foreach (Goods g in goodslist) {
			if (g.spname.Equals (spriteName)) {
				g.Use ();
				break;
			}
		}
		foreach (Goods g in goodslist) {
			if (g.spname.Equals (spriteName)) {
				return true;
			}
		}
		return false;
	}

	public void addGoods(Goods g) {
		goodslist.Add (g);
	}
	public void addGoods(string name_, int price_) {
		goodslist.Add (new Goods (name_, price_));
	}
	public void delGoods(Goods g) {
		if (goodslist.Contains (g))
			goodslist.Remove (g);
	}
	public void delGoods(string s) {
		foreach (Goods g in goodslist) {
			if (g.name == s) {
				goodslist.Remove(g);
				return;
			}
		}
	}
	public void delGoods(int i) {
        goodslist.Remove(goodslist[i]);
	}
	public string getName(int i) {
		return goodslist [i].name;
	}

	public int getPrice(int i) {
		return goodslist [i].price;
	}


	public List<Goods> goodslist;


}