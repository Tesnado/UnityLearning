using UnityEngine;
using System.Collections;

public class Status_UI_Conttoller : MonoBehaviour {
    UILabel lv,hp, mp, ad, ap, def, speed,re_point, skill_point;
    GameObject Upgrade;
	// Use this for initialization
	void Start () {
        Upgrade = transform.Find("Upgrade").gameObject;
        lv = transform.Find("Level").Find("Value").GetComponent<UILabel>();
        hp = transform.Find("HP").Find("Value").GetComponent<UILabel>();
        mp = transform.Find("MP").Find("Value").GetComponent<UILabel>();
        ad = transform.Find("AD").Find("Value").GetComponent<UILabel>();
        ap = transform.Find("AP").Find("Value").GetComponent<UILabel>();
        def = transform.Find("DEF").Find("Value").GetComponent<UILabel>();
        speed = transform.Find("Speed").Find("Value").GetComponent<UILabel>();
        re_point = transform.Find("Point").Find("Value").GetComponent<UILabel>();
        skill_point = transform.Find("Skill_Point").Find("Value").GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Character_Controller.character == null)
            return;
        if (Property_Controller.my_Property.remain_point <= 0)
        {
            Upgrade.SetActive(false);
        }
        else
        {
            Upgrade.SetActive(true);
        }
        lv.text = Property_Controller.my_Property.level.ToString();
        hp.text = Property_Controller.my_Property.Character_MAX_HP.ToString();
        mp.text = Property_Controller.my_Property.Character_MAX_MP.ToString();
        ad.text = Property_Controller.my_Property.AD.ToString();
        ap.text = Property_Controller.my_Property.AP.ToString();
        def.text = Property_Controller.my_Property.defensive.ToString();
        speed.text = Property_Controller.my_Property.speed.ToString();
        re_point.text = Property_Controller.my_Property.remain_point.ToString();
        skill_point.text = Property_Controller.my_Property.remain_skill_point.ToString();
	}

    public void hp_add()
    {
        Property_Controller.Instance.Add_Point();
        Property_Controller.Instance.Change_Max_HP(10);
    }

    public void speed_add()
    {
        Property_Controller.Instance.Add_Point();
        Property_Controller.Instance.Change_Speed(1);
    }


    public void mp_add()
    {
        Property_Controller.Instance.Add_Point();
        Property_Controller.Instance.Change_Max_MP(10);
    }

    public void ad_add()
    {
        Property_Controller.Instance.Add_Point();
        Property_Controller.Instance.Change_AD(1);
    }

    public void ap_add()
    {
        Property_Controller.Instance.Add_Point();
        Property_Controller.Instance.Change_AP(1);
    }

    public void def_add()
    {
        Property_Controller.Instance.Add_Point();
        Property_Controller.Instance.Change_Def(1);
    }

    public void exit()
    {
        this.gameObject.SetActive(false);
    }
}
