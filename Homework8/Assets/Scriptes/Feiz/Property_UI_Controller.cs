using UnityEngine;
using System.Collections;

public class Property_UI_Controller : MonoBehaviour {
    public static GameObject ui;
    UILabel hp_label, mp_label, exp_label;
    GameObject attribute;
    UISprite head, hp, mp, exp;
    bool init = false;
	// Use this for initialization
	void Start () {
        attribute = transform.Find("Attribute").gameObject;
        ui = this.gameObject;
        ui.SetActive(false);
        head = attribute.transform.Find("head").GetComponent<UISprite>();
        hp = attribute.transform.Find("hp").transform.GetChild(1).GetComponent<UISprite>();
        mp = attribute.transform.Find("mp").transform.GetChild(1).GetComponent<UISprite>();
        exp = attribute.transform.Find("exp").transform.GetChild(1).GetComponent<UISprite>();
        hp_label = attribute.transform.Find("hp").transform.GetChild(2).GetComponent<UILabel>();
        mp_label = attribute.transform.Find("mp").transform.GetChild(2).GetComponent<UILabel>();
        exp_label = attribute.transform.Find("exp").transform.GetChild(2).GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Property_Controller.my_Property != null)
        {
            if(!init){
                Init();
            }
            hp.fillAmount = Property_Controller.my_Property.Character_Current_HP * 1.0f / Property_Controller.my_Property.Character_MAX_HP;
            hp_label.text = Property_Controller.my_Property.Character_Current_HP.ToString() + "/" + Property_Controller.my_Property.Character_MAX_HP.ToString();
            mp.fillAmount = Property_Controller.my_Property.Character_Current_MP*1.0f / Property_Controller.my_Property.Character_MAX_MP;
            mp_label.text = Property_Controller.my_Property.Character_Current_MP.ToString() + "/" + Property_Controller.my_Property.Character_MAX_MP.ToString();
            exp.fillAmount = Property_Controller.my_Property.Character_Current_EXP*1.0f / Property_Controller.my_Property.Character_MAX_EXP;
            exp_label.text = Property_Controller.my_Property.Character_Current_EXP.ToString() + "/" + Property_Controller.my_Property.Character_MAX_EXP.ToString();
            //Property_Controller.Instance.Property_Controller_Change_CurrentEXP(1);

        }
        //else if(ui.active)
        //{
        //        //ui.SetActive(false);
        //}
	}

    void Init()
    {
        attribute.transform.Find("Name").transform.Find("Label").GetComponent<UILabel>().text = Property_Controller.Instance.GetName();

        if (Property_Controller.my_Property.career == Career.Magician)
        {
            head.spriteName = "magician";
        }
        else
        {
            head.spriteName = "swordmans";
        }
        init = true;
    }

    public void ClickBag()
    {
        UI_manager.Instance.ClickBag();
    }

    public void ClickStatus()
    {
        UI_manager.Instance.ClickStatus();
    }

    public void ClickAttack()
    {
        Character_Controller.Instance.ClickAttack();
    }

    public void ClickSkill()
    {
        UI_manager.Instance.ClickSkill();
    }

    public void ClickEquipments()
    {
        UI_manager.Instance.ClickEquipments();
    }

    public void ClickSetting()
    {
        UI_manager.Instance.ClickSetting();
    }
}
