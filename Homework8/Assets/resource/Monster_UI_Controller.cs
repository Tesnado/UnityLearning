using UnityEngine;
using System.Collections;

public class Monster_UI_Controller : MonoBehaviour {
    public GameObject head,hp;
	public static GameObject m_Boss, m_Norm, m_Baby;
	public static GameObject monster;
    //获取单实例
    private static Monster_UI_Controller instance;
    public static Monster_UI_Controller Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Monster_UI_Controller();
            }
            return instance;
        }
    }
	UILabel m_hp, m_name;
	UISprite wolfhp;
	// Use this for initialization
	void Start () {
        head = transform.Find("Mon_Headpic").gameObject;
        hp = transform.Find("Mon_Hp").gameObject;
		m_Boss = head.transform.Find("Mon_Bosshead_Sprite").gameObject;
        m_Norm = head.transform.Find("Mon_Normhead_Sprite").gameObject;
        m_Baby = head.transform.Find("Mon_Babyhead_Sprite").gameObject;
        m_hp = hp.transform.Find("Mon_Hp_Label").GetComponent<UILabel>();
        m_name = head.transform.Find("Mon_Name_Label").GetComponent<UILabel>();
        wolfhp = hp.transform.Find("Mon_Hp_Sprite").GetComponent<UISprite>();
        head.SetActive(false);
        hp.SetActive(false);
	}
	public static void setMonsterCurrentProperty (GameObject tmp) {
        //Debug.Log("set");
		   monster = tmp;
	}

	// Update is called once per frame
	void Update () {
       
		//setMonsterCurrentProperty();
        if (!Character_Controller.fight || monster == null)
        {
            //Debug.Log("hahah");
            //UI.SetActive(false);
            head.SetActive(false);
            hp.SetActive(false);
            return;
        }
		if (monster.GetComponent<Monster_Manage> ().monster_id == 1 && Character_Controller.fight) {
			//UI.SetActive(true);
            head.SetActive(true);
            hp.SetActive(true);
            m_Baby.SetActive(true);
			m_Boss.SetActive(false);
			m_Norm.SetActive(false);
			m_name.text = "WolfBaby";
			wolfhp.fillAmount = monster.GetComponent<Monster_Manage> ().Monster_Manage_Get_CurrentHp() * 1.0f / 
				                monster.GetComponent<Monster_Manage> ().Monster_Manage_Get_MaxHP();
			m_hp.text = monster.GetComponent<Monster_Manage> ().Monster_Manage_Get_CurrentHp().ToString() + "/" + 
				        monster.GetComponent<Monster_Manage> ().Monster_Manage_Get_MaxHP().ToString();
		}

		if (monster.GetComponent<Monster_Manage> ().monster_id == 2 && Character_Controller.fight != false) {
			head.SetActive(true);
            hp.SetActive(true);
            m_Norm.SetActive(true);
			m_Boss.SetActive(false);
			m_Baby.SetActive(false);
			m_name.text = "WolfNormal";
			wolfhp.fillAmount = monster.GetComponent<Monster_Manage> ().Monster_Manage_Get_CurrentHp() * 1.0f / 
				                monster.GetComponent<Monster_Manage> ().Monster_Manage_Get_MaxHP();
			m_hp.text = monster.GetComponent<Monster_Manage> ().Monster_Manage_Get_CurrentHp().ToString() + "/" + 
				        monster.GetComponent<Monster_Manage> ().Monster_Manage_Get_MaxHP().ToString();
		}

		if (monster.GetComponent<Monster_Manage> ().monster_id == 3 && Character_Controller.fight == true) {
			//UI.SetActive(true);
            head.SetActive(true);
            hp.SetActive(true);
            m_Boss.SetActive(true);
			m_Baby.SetActive(false);
			m_Norm.SetActive(false);
			m_name.text = "WolfBoss";
			wolfhp.fillAmount = monster.GetComponent<Monster_Manage> ().Monster_Manage_Get_CurrentHp() * 1.0f / 
				                monster.GetComponent<Monster_Manage> ().Monster_Manage_Get_MaxHP();
			m_hp.text = monster.GetComponent<Monster_Manage> ().Monster_Manage_Get_CurrentHp().ToString() + "/" + 
				        monster.GetComponent<Monster_Manage> ().Monster_Manage_Get_MaxHP().ToString();
		}
		

	}
}
