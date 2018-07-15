using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;	


public class BagManager : MonoBehaviour {

	// Use this for initialization
	GameObject[] objlist = new GameObject[12];
	public string[] btn = new string[15];
	List<Goods> goodslist;
	UILabel pageobj,label2,label3,label4;
	int choose;
	int page;
	int count;
	int price;
	int money;
	
	void Start () {
		
		/*Container shop = new Container ();
		shop.addGoods ("potion1", 0);
		shop.addGoods ("potion2", 0);
		shop.addGoods ("potion3", 0);
		shop.addGoods ("potion1", 0);
		shop.addGoods ("potion2", 0);
		shop.addGoods ("potion3", 0);
		shop.addGoods ("potion1", 0);
		shop.addGoods ("potion2", 0);
		shop.addGoods ("potion3", 0);
		shop.addGoods ("potion1", 0);
		shop.addGoods ("potion2", 0);
		shop.addGoods ("potion3", 0);
		shop.addGoods ("potion1", 0);
		shop.addGoods ("potion2", 0);
		shop.addGoods ("potion3", 0);
		shop.addGoods ("potion1", 0);
		shop.addGoods ("potion2", 0);
		shop.addGoods ("potion3", 0);
		shop.addGoods ("po123n1", 0);
		//Init (shop);*/
	}
	
	
	void onclick(GameObject btnn) {
		string name = btnn.name;
		//Debug.Log (name);
		if (name.Contains ("Bag_Button_")) {
			if (name.Contains ("10"))
				return;
			if ((page - 1) * 9 + name [11] - '0' - 1 >= goodslist.Count)
				return;
			if (choose != name [11] - '0')
				count = 0;
			choose = name [11] - '0';
		} else if (name == "Bag_Left") {
			if (page > 1) {
				page--;
				count = 0;
				choose = 0;
			}
		} else if (name == "Bag_Right") {
			if (page * 9 < goodslist.Count) {
				page++;
				count = 0;
				choose = 0;
			}
		} else if (name == "Bag_Up") {
			if (choose == 0) return;
			count++;
		} else if (name == "Bag_Down") {
			if (choose == 0) return;
			if (count > 0)
				count--;
		} else if (name == "Bag_Use") {
			if (choose == 0) return;
			string s = goodslist [(page - 1) * 9 + choose - 1].spname;
            if (goodslist[(page - 1) * 9 + choose - 1].name ==
               TaskManager.Instance.getCurTaskData(Property_Controller.Instance.GetCurrentTask()).creatureid)
            {
                Property_Controller.Instance.CurrentCreaturesDecrease();
                TaskManager.Instance.UpdateTaskText();
            }

			goodslist [(page - 1) * 9 + choose - 1].Use();
			bool yes = true;
			foreach (Goods g in goodslist) {
				if (g.spname == s) {
					yes = false;
					break;
				}
			}
			if (UI_manager.m_FastUI != null)
				UI_manager.m_FastUI.GetComponent<FastUI>().refresh(s, yes);
            choose = 0;
		} else if (name == "Bag_Exit") {
            this.gameObject.SetActive(false);
		}
        refresh();
	}
	

    public void refresh(){
    money = Property_Controller.Instance.GetMoney();
    if ((page - 1) * 9 >= goodslist.Count && page > 1)
    {
        page--;
        choose = 0;
    }
		pageobj.text = "" + page;
		for (int i = (page - 1) * 9, j = 1; i < page * 9; i++, j++) {
			//Debug.Log (i + " " +  j);
			if (i < goodslist.Count) {
				objlist[j].GetComponent<UIButton>().normalSprite = goodslist[i].spname;
				objlist[j].transform.GetChild(0).GetComponent<UISprite>().spriteName = goodslist[i].spname;
				objlist[j].transform.GetChild(1).GetComponent<UILabel>().text = "";
				btn[j] = goodslist[i].spname;
				if (goodslist[i].price != 0)
					objlist[j].transform.GetChild(1).GetComponent<UILabel>().text += goodslist[i].price;
			} else {
				objlist[j].GetComponent<UIButton>().normalSprite = "icon-null";
				objlist[j].transform.GetChild(0).GetComponent<UISprite>().spriteName = "icon-null";
				objlist[j].transform.GetChild(1).GetComponent<UILabel>().text = "";
				btn[j] = "icon-null";
			}
		}
        //Debug.Log(choose);
		if (choose != 0) {
			objlist [10].GetComponent<UIButton>().normalSprite =
				objlist [choose].transform.GetChild (0).GetComponent<UISprite> ().spriteName;
			objlist [10].transform.GetChild (0).GetComponent<UISprite> ().spriteName =
				objlist [choose].transform.GetChild (0).GetComponent<UISprite> ().spriteName;
			objlist [10].transform.GetChild (1).GetComponent<UILabel> ().text =
				objlist [choose].transform.GetChild (1).GetComponent<UILabel> ().text;
			//label2.text = "" + count;
            //if ((page - 1) * 9 + choose - 1 >= 0)
            //    price = goodslist[(page - 1) * 9 + choose - 1].price;
            //else
            //    price = 0;
			//label3.text = "" + count * price;
			label4.text = "" + money;
		} else {
			objlist [10].GetComponent<UIButton>().normalSprite = "icon-null";
			objlist [10].transform.GetChild (0).GetComponent<UISprite> ().spriteName = "icon-null";
			objlist [10].transform.GetChild (1).GetComponent<UILabel> ().text = "";
			price = 0;
			label4.text = "" + money;
		}
    }
	// Update is called once per frame
	void Update () {
	}
	
    //protected static ShopManager instance_ = null;
    //public static ShopManager instance {
    //    get {
    //        if (instance_ == null) {
    //            instance_ = new ShopManager();
    //        }
    //        return instance_;
    //    }
    //}
	
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
            btnName.Add("Bag_Button_" + i);
        btnName.Add("Bag_Left");
        btnName.Add("Bag_Right");
        //btnName.Add ("Bag_Up");
        //btnName.Add ("Bag_Down");
        btnName.Add("Bag_Use");
        btnName.Add("Bag_Exit");
        foreach (string btnname in btnName)
        {
            GameObject btnobj = GameObject.Find(btnname);
            btnobj.AddComponent<UIEventListener>();
            UIEventListener.Get(btnobj).onClick = onclick;
        }
        for (int i = 1; i <= 10; i++)
            objlist[i] = GameObject.Find("Bag_Button_" + i);
        pageobj = GameObject.Find("Bag_Label_1").GetComponent<UILabel>();
        //label2 = GameObject.Find ("Bag_Label_2").GetComponent<UILabel> ();
        //label3 = GameObject.Find ("Bag_Label_3").GetComponent<UILabel> ();
        label4 = GameObject.Find("Bag_Label_4").GetComponent<UILabel>();
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

/*
public class Goods{
	public Goods(string name_, int price_) {
		name = name_;
		price = price_;
		if (name == "potion1")
			spname = ("icon-potion1");
		else if (name == "potion2")
			spname = ("icon-potion2");
		else if (name == "potion3")
			spname = ("icon-potion3");
		else 
			spname = ("coin-icon");
	}
	public string name;
	public int price;
	public string spname;
}

public class Shop {
	public Shop() {
		goodslist = new List<Goods> ();
		goodslist.Clear ();
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
	
	public string getName(int i) {
		return goodslist [i].name;
	}
	
	public int getPrice(int i) {
		return goodslist [i].price;
	}
	
	
	public List<Goods> goodslist;
	
	
}
*/
