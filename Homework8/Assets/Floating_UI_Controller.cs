using UnityEngine;
using System.Collections;
using System.Collections.Generic;
class information{
    public string name;
    public string discription;
    public information(string name_, string discription_)
    {
        this.name = name_;
        this.discription = discription_;
    }
}




public class Floating_UI_Controller : MonoBehaviour {
    public static GameObject current_Button;
    string current_Item;
    UILabel m_name, m_discription;
    Dictionary<string, information> dict;
	// Use this for initialization
	void Start () {
        current_Item = "";
        dict = new Dictionary<string,information> ();
        m_name = transform.Find("Name").GetComponent<UILabel>();
        m_discription = transform.Find("Discription").GetComponent<UILabel>();
        dict.Add("icon-potion1", new information("HpPotion", "HP + 20"));
        dict.Add("icon-potion2", new information("potion2", "HP + 50"));
        dict.Add("icon-potion3", new information("MpPotion", "MP + 20"));
        dict.Add("sword0-icon", new information("RustySword", "AD + 3"));
        dict.Add("sword1-icon", new information("Long Sword", "AD + 8"));
        dict.Add("sword2-icon", new information("Sword Of Knight", "AD + 15"));
        dict.Add("rod-icon", new information("Rod Of Learner", "AD + 1\nAP + 3"));
        dict.Add("rod-icon02", new information("Wood Rod", "AD + 3\nAP + 5"));
        dict.Add("rod-icon03", new information("Rod Of Magic", "AD + 5\nAP + 12"));
        dict.Add("armor0-icon", new information("Rusty Armor", "DEF + 5\nMax_HP + 10"));
        dict.Add("armor1-icon", new information("Iron Armor", "DEF + 10\nMax_HP + 20"));
        dict.Add("armor2-icon", new information("Mantle Of Learner", "DEF + 3\nMax_MP + 20"));
        dict.Add("armor3-icon", new information("Mantle Of Intelligence", "DEF + 5\nMax_MP + 50"));
        dict.Add("icon-boot0", new information("Wool Boot", "DEF + 2\nSpeed + 10"));
        dict.Add("icon-boot0-01", new information("Red Boot", "DEF + 2\nSpeed + 20"));
        dict.Add("icon-etcFur", new information("etcFur", "Max_HP + 1000\nMax_MP + 1000\nAD + 1000\nAP + 1000\nDEF + 1000\nSpeed + 1000"));
        dict.Add("icon-helm", new information("White Helm", "DEF + 2"));
        dict.Add("icon-helm-01", new information("Black Helm", "DEF + 5"));
        dict.Add("icon-helm-02", new information("Magic Hat", "AP + 2"));
        dict.Add("icon-helm-03", new information("Higher Hat", "AP + 5"));
        dict.Add("icon-ring", new information("Yellow Ring", "Max_HP + 20\nMax_MP + 40"));
        dict.Add("icon-ring-01", new information("Green Ring", "AD + 5\nAP + 10"));
        dict.Add("icon-shield", new information("Blue Shield", "Max_HP + 40\nDEF + 5"));
        dict.Add("icon-shield1", new information("High Blue Shield", "Max_MP + 80\nDEF + 8"));
        dict.Add("sword0-icon00", new information("Sange and Yasha", "AD + 20\nSpeed + 60"));

	}
	
	// Update is called once per frame
	void Update () {
        if (current_Button == null)
            return;
        if (current_Item != current_Button.transform.GetChild(0).GetComponent<UISprite>().spriteName&&
            dict.ContainsKey(current_Button.transform.GetChild(0).GetComponent<UISprite>().spriteName))
        {
            transform.localPosition = current_Button.transform.localPosition;
            m_name.text = dict[current_Button.transform.GetChild(0).GetComponent<UISprite>().spriteName].name;
            m_discription.text = dict[current_Button.transform.GetChild(0).GetComponent<UISprite>().spriteName].discription;
            current_Item = current_Button.transform.GetChild(0).GetComponent<UISprite>().spriteName;
        }

	}

    public void exit()
    {
        this.gameObject.SetActive(false);
    }
}
