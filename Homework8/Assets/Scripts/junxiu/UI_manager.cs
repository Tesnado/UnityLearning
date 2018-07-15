using UnityEngine;
using System.Collections;

public class UI_manager : MonoBehaviour
{
    private static UI_manager instance;
    public static UI_manager Instance
    {
        get
        {
            if (instance == null)
                instance = new UI_manager();
            return instance;
        }
    }

    public GameObject m_MainUI;
    public static GameObject m_FastUI;
    public static GameObject m_MonsterHpUI;
    public static GameObject m_Root;

    //public GameObject m_CurrentUI;
    public static GameObject m_BagUI;

    public static GameObject m_ShopUI;
    public static GameObject m_TaskUI;
    public static GameObject m_StoreUI;
    public static GameObject m_ChooseingCharacterUI;
    public static GameObject m_PropertyUI;
    public static GameObject m_SkillBookUI;
    public static GameObject m_StatusUI;
    public static GameObject m_EquipmentsUI;
    public static GameObject m_SettingUI;
    public static GameObject m_FloatingUI;
    /*public string Bag = "Bag";
	public string MainUI = "MainUI";
	public string Shop = "Shop";
	public string TaskUI = "TaskUI";
	public string StoreUI = "StoreUI";
	public string Property = "Property";
	public string ChooseingCharacter = "ChooseingCharacter";*/

    // Use this for initialization
    void Start()
    {
        m_Root = this.gameObject;
        ShowMyUI("TaskUI");
        ShowMyUI("ChooseingCharacter");
        ShowMyUI("Property");
        ShowMyUI("MonsterHpUI");
        m_FastUI = m_PropertyUI.transform.Find("FastUI").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ClickStatus()
    {
        if (m_StatusUI == null)
        {
            ShowMyUI("Status");
        }
        else
        {
            if (m_StatusUI.active)
            {
                CloseMyUI("Status");
            }
            else
            {
                ShowMyUI("Status");
            }
        }
    }

    public void ClickSkill()
    {
        if (m_SkillBookUI == null)
        {
            ShowMyUI("SkillBook");
            //Debug.Log(m_SkillBookUI);
            m_SkillBookUI.BroadcastMessage("ReadBasic");
            m_SkillBookUI.BroadcastMessage("sp");
        }
        else
        {
            if (m_SkillBookUI.active)
            {
                CloseMyUI("SkillBook");
            }
            else
            {
                ShowMyUI("SkillBook");
                m_SkillBookUI.BroadcastMessage("ReadBasic");
                m_SkillBookUI.BroadcastMessage("sp");
            }
        }
    }

    public void ClickEquipments()
    {
        if (m_EquipmentsUI == null)
        {
            ShowMyUI("Equipments");
        }
        else
        {
            if (m_EquipmentsUI.active)
            {
                CloseMyUI("Equipments");
            }
            else
            {
                ShowMyUI("Equipments");
            }
        }
    }

    public void ClickSetting()
    {
        if (m_SettingUI == null)
        {
            ShowMyUI("Setting");
        }
        else
        {
            if (m_SettingUI.active)
            {
                CloseMyUI("Setting");
                // Debug.Log("123");
            }
            else
            {
                ShowMyUI("Setting");
            }
        }
    }

    public void ClickBag()
    {
        if (m_BagUI == null)
        {
            ShowMyUI("Bag");
        }
        else
        {
            if (m_BagUI.active)
            {
                CloseMyUI("Bag");
                // Debug.Log("123");
            }
            else
            {
                ShowMyUI("Bag");
            }
        }
    }

    public void ShowMyUI(string ShowUI)
    {
        //加载主
        if (ShowUI == "MainUI")
        {
            if (m_MainUI == null)
            {
                m_MainUI = Instantiate(Resources.Load("MainUI")) as GameObject;
                m_MainUI.transform.parent = m_Root.transform;
                m_MainUI.transform.localPosition = Vector3.zero;
                m_MainUI.transform.localScale = Vector3.one;
            }
            else
            {
                m_MainUI.SetActive(true);
            }
        }
        //加载背包UI
        else if (ShowUI == "Bag")
        {
            if (m_BagUI == null)
            {
                m_BagUI = Instantiate(Resources.Load("hufan/Bag")) as GameObject;
                m_BagUI.transform.parent = m_Root.transform;
                m_BagUI.GetComponent<BagManager>().Init(Property_Controller.Instance.Get_Bag());
                m_BagUI.transform.localPosition = Vector3.zero;
                m_BagUI.transform.localScale = Vector3.one;
            }
            else
            {
                m_BagUI.SetActive(true);
            }
        }
        //加载shopUI
        else if (ShowUI == "Status")
        {
            if (m_StatusUI == null)
            {
                m_StatusUI = Instantiate(Resources.Load("Feiz/StatusUI")) as GameObject;
                m_StatusUI.transform.parent = m_Root.transform;
                m_StatusUI.transform.localPosition = Vector3.zero;
                m_StatusUI.transform.localScale = Vector3.one;
            }
            else
            {
                m_StatusUI.SetActive(true);
            }
        }
        else if (ShowUI == "Shop")
        {
            if (m_ShopUI == null)
            {
                m_ShopUI = Instantiate(Resources.Load("hufan/Shop")) as GameObject;
                m_ShopUI.transform.parent = m_Root.transform;
                m_ShopUI.transform.localPosition = Vector3.zero;
                m_ShopUI.transform.localScale = Vector3.one;
            }
            else
            {
                m_ShopUI.SetActive(true);
            }
        }
        //加载任务UI
        else if (ShowUI == "TaskUI")
        {
            if (m_TaskUI == null)
            {
                m_TaskUI = Instantiate(Resources.Load("Feiz/TaskUI")) as GameObject;
            }
            else
            {
                m_TaskUI.SetActive(true);
            }
            m_TaskUI.transform.parent = m_Root.transform;
            m_TaskUI.transform.localPosition = Vector3.zero;
            m_TaskUI.transform.localScale = Vector3.one;
        }
        //加载StoreUI
        else if (ShowUI == "StoreUI")
        {
            if (m_StoreUI == null)
            {
                m_StoreUI = Instantiate(Resources.Load("hufan/Shop")) as GameObject;
                m_StoreUI.transform.parent = m_Root.transform;
                m_StoreUI.transform.localPosition = Vector3.zero;
                m_StoreUI.transform.localScale = Vector3.one;
            }
            else
            {
                m_StoreUI.SetActive(true);
            }
        }
        //加载选择角色UI
        else if (ShowUI == "ChooseingCharacter")
        {
            if (m_ChooseingCharacterUI == null)
            {
                m_ChooseingCharacterUI = Instantiate(Resources.Load("Feiz/ChoosingCharacter")) as GameObject;
                m_ChooseingCharacterUI.transform.parent = m_Root.transform;
                m_ChooseingCharacterUI.transform.localPosition = Vector3.zero;
                m_ChooseingCharacterUI.transform.localScale = Vector3.one;
            }
            else
            {
                m_ChooseingCharacterUI.SetActive(true);
            }
        }
        //加载人物属性UI
        else if (ShowUI == "Property")
        {
            if (m_PropertyUI == null)
            {
                m_PropertyUI = Instantiate(Resources.Load("Feiz/Property")) as GameObject;
                m_PropertyUI.transform.parent = m_Root.transform;
                m_PropertyUI.transform.localPosition = Vector3.zero;
                m_PropertyUI.transform.localScale = Vector3.one;
            }
            else
            {
                m_PropertyUI.SetActive(true);
            }
        }
        //加载Swordman技能UI
        else if (ShowUI == "SkillBook")
        {
            if (m_SkillBookUI == null)
            {
                m_SkillBookUI = Instantiate(Resources.Load("jianhao/Skillbook/SkillBook")) as GameObject;
                m_SkillBookUI.transform.parent = m_Root.transform;
                m_SkillBookUI.transform.localPosition = Vector3.zero;
                m_SkillBookUI.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
                m_SkillBookUI.transform.Find("Details").gameObject.SetActive(false);
            }
            else
            {
                m_SkillBookUI.SetActive(true);
            }
        }
        //加载装备UI
        else if (ShowUI == "Equipments")
        {
            if (m_EquipmentsUI == null)
            {
                m_EquipmentsUI = Instantiate(Resources.Load("Feiz/Equipments")) as GameObject;
                m_EquipmentsUI.transform.parent = m_Root.transform;
                m_EquipmentsUI.transform.localPosition = Vector3.zero;
                m_EquipmentsUI.transform.localScale = Vector3.one;
            }
            else
            {
                m_EquipmentsUI.SetActive(true);
            }
        }
        else if (ShowUI == "Setting")
        {
            if (m_SettingUI == null)
            {
                m_SettingUI = Instantiate(Resources.Load("Feiz/Setting")) as GameObject;
                m_SettingUI.transform.parent = m_Root.transform;
                m_SettingUI.transform.localPosition = Vector3.zero;
                m_SettingUI.transform.localScale = Vector3.one;
            }
            else
            {
                m_SettingUI.SetActive(true);
            }
        }
        else if (ShowUI == "MonsterHpUI")
        {
            if (m_MonsterHpUI == null)
            {
                m_MonsterHpUI = Instantiate(Resources.Load("Feiz/MonsterHpUI")) as GameObject;
                m_MonsterHpUI.transform.parent = m_Root.transform;
                m_MonsterHpUI.transform.localPosition = Vector3.zero;
                m_MonsterHpUI.transform.localScale = Vector3.one;
            }
            else
            {
                m_MonsterHpUI.SetActive(true);
            }
        }
        else if (ShowUI == "FloatingUI")
        {
            if (m_FloatingUI == null)
            {
                m_FloatingUI = Instantiate(Resources.Load("Feiz/FloatingWindow")) as GameObject;
                m_FloatingUI.transform.parent = m_Root.transform;
                //m_FloatingUI.transform.localPosition = Vector3.zero;
                m_FloatingUI.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
            else
            {
                m_FloatingUI.SetActive(true);
            }
        }
    }

    //关闭UI
    public void CloseMyUI(string CloseUI)
    {
        if (CloseUI == "MainUI" && m_MainUI != null)
        {
            m_MainUI.SetActive(false);
            //m_MainUI = null;
        }
        else if (CloseUI == "Status" && m_StatusUI != null)
        {
            m_StatusUI.SetActive(false);
            //m_BagUI = null;
        }
        else if (CloseUI == "Bag" && m_BagUI != null)
        {
            m_BagUI.SetActive(false);
            Debug.Log("456");
            //m_BagUI = null;
        }
        else if (CloseUI == "Shop" && m_ShopUI != null)
        {
            m_ShopUI.SetActive(false);
            //m_ShopUI = null;
        }
        else if (CloseUI == "TaskUI" && m_TaskUI != null)
        {
            m_TaskUI.SetActive(false);
            //m_TaskUI = null;
        }
        else if (CloseUI == "StoreUI" && m_StoreUI != null)
        {
            m_StoreUI.SetActive(false);
            //m_StoreUI = null;
        }
        else if (CloseUI == "ChooseingCharacter" && m_ChooseingCharacterUI != null)
        {
            m_ChooseingCharacterUI.SetActive(false);
            //m_ChooseingCharacterUI = null;
        }
        else if (CloseUI == "Property" && m_PropertyUI != null)
        {
            m_PropertyUI.SetActive(false);
            //m_PropertyUI = null;
        }
        else if (CloseUI == "SkillBook" && m_SkillBookUI != null)
        {
            m_SkillBookUI.SetActive(false);
            //m_SkillBookUI = null;
        }
        else if (CloseUI == "Equipments" && m_EquipmentsUI != null)
        {
            m_EquipmentsUI.SetActive(false);
            //m_SkillBookUI = null;
        }
        else if (CloseUI == "Setting" && m_SettingUI != null)
        {
            m_SettingUI.SetActive(false);
            //m_SkillBookUI = null;
        }
        else if (CloseUI == "FloatingUI" && m_FloatingUI != null)
        {
            m_FloatingUI.SetActive(false);
            //m_SkillBookUI = null;
        }
    }
}