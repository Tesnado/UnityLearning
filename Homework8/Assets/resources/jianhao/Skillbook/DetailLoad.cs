using UnityEngine;
using System.Collections;
using System.Xml;

public class DetailLoad : MonoBehaviour
{
    public static string skill_name;
    private XmlNode root;
    private static WWW www;

    public static DetailLoad Instance;

    void Awake()
    {
        Instance = this;
        StartCoroutine(GetXML());
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Property_Controller.my_Property == null)
            return;
        ReadDamage();
    }

    IEnumerator GetXML()
    {
        string localPath;
        if (Application.platform == RuntimePlatform.Android)
        {
            localPath = Application.streamingAssetsPath + "/SkillInfo.xml";
            //Debug.Log(localPath);
        }
        else
        {
            localPath = "file://" + Application.streamingAssetsPath + "/SkillInfo.xml";
            //Debug.Log(localPath);
        }
        www = new WWW(localPath);
        while (!www.isDone)
        {
            Debug.Log("Getting GetXML");
            yield return www;
        }
    }

    void ReadDescription()
    {
        XmlElement n = (XmlElement)root.SelectSingleNode("data[@name='description']");
        //XmlElement m = (XmlElement)root.SelectSingleNode("data[@name='damage_des']");
        GameObject temp = transform.Find("Description").gameObject;
        UILabel temp_label = temp.GetComponent<UILabel>();
        if (n != null
            //&& m != null
            )
        {
            temp_label.text = n.InnerText;
        }
    }

    void ReadMp()
    {
        XmlElement n = (XmlElement)root.SelectSingleNode("data[@name='mp']");
        GameObject temp = transform.Find("MP").gameObject;
        UILabel temp_label = temp.GetComponent<UILabel>();
        if (n != null)
        {
            temp_label.text = n.InnerText + " mp";
        }
    }

    void ReadCD()
    {
        XmlElement n = (XmlElement)root.SelectSingleNode("data[@name='cd']");
        GameObject temp = transform.Find("CD").gameObject;
        UILabel temp_label = temp.GetComponent<UILabel>();
        if (n != null)
        {
            temp_label.text = n.InnerText + " seconds";
        }
    }

    void ReadDamage()
    {
        XmlElement n = (XmlElement)root.SelectSingleNode("data[@name='damage']");
        XmlElement m = (XmlElement)root.SelectSingleNode("data[@name='Rname']");
        GameObject temp = transform.Find("Damage").gameObject;
        UILabel temp_label = temp.GetComponent<UILabel>();
        if (n != null && m != null)
        {
            temp_label.text = n.InnerText + " [FFFFFF]: [FF0000]" + Skill_Damage.Instance.GetDamage(m.InnerText);
        }
    }

    public void ReadSkill()
    {
        if (Property_Controller.my_Property == null)
            return;
        StartCoroutine(GetXML());
        UISprite face = transform.Find("Face").gameObject.GetComponent<UISprite>();
        if (Property_Controller.my_Property.career == Career.Swordman)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(www.text);
            root = xmlDoc.SelectSingleNode("Skill/Sword" + gameObject.name + (DescriptionShow.current_click + 1));
            face.spriteName = "Sword";
            skill_name = "SwordSkill" + (DescriptionShow.current_click + 1);
        }
        else if (Property_Controller.my_Property.career == Career.Magician)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(www.text);
            root = xmlDoc.SelectSingleNode("Skill/Magic" + gameObject.name + (DescriptionShow.current_click + 1));
            face.spriteName = "Magic";
            skill_name = "MagicSkill" + (DescriptionShow.current_click + 1);
        }

        ReadDescription();
        ReadMp();
        ReadCD();
        ReadDamage();
    }
}