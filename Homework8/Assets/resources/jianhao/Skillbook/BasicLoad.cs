using UnityEngine;
using System.Collections;
using System.Xml;

public class BasicLoad : MonoBehaviour {
	XmlNode root;
    WWW www;
    void Awake () {
        StartCoroutine(GetXML());
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator GetXML() {
        string localPath;
        if (Application.platform == RuntimePlatform.Android) {
            localPath = Application.streamingAssetsPath + "/SkillInfo.xml";
            //Debug.Log(localPath);
        }
        else {
            localPath = "file://" + Application.streamingAssetsPath + "/SkillInfo.xml";
            //Debug.Log(localPath);
        }
        www = new WWW(localPath);
        while (!www.isDone) {
            Debug.Log("Getting GetXML");
            yield return www;
        }
    }

    void ReadName () {
		XmlElement n = (XmlElement)root.SelectSingleNode("data[@name='name']");
		GameObject temp = transform.Find("Name").gameObject;
		UILabel temp_label = temp.GetComponent<UILabel> ();			
		if (n != null) {
			temp_label.text = n.InnerText;
		}
	}

	void ReadType () {
		XmlElement n = (XmlElement)root.SelectSingleNode("data[@name='type']");
		GameObject temp = transform.Find("Type").gameObject;
		UILabel temp_label = temp.GetComponent<UILabel> ();			
		if (n != null) {
			temp_label.text = n.InnerText;
		}
	}

	void ReadLevel () {
		XmlElement n = (XmlElement)root.SelectSingleNode("data[@name='level']");
		GameObject temp = transform.Find("Level").gameObject;
		UILabel temp_label = temp.GetComponent<UILabel> ();			
		if (n != null) {
			temp_label.text = n.InnerText;
		}
	}

	public void ReadBasic () {
		if (Property_Controller.my_Property == null)
			return;
		UIButton but = gameObject.GetComponentInChildren<UIButton> ();
		UISprite icon = but.gameObject.GetComponentInChildren<UISprite> ();
		if (Property_Controller.my_Property.career == Career.Swordman) {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(www.text);
            root = xmlDoc.SelectSingleNode ("Skill/Sword" + gameObject.name);
			icon.spriteName = "Sword"+icon.name;
			but.normalSprite = "Sword"+icon.name;
		} else if (Property_Controller.my_Property.career == Career.Magician) {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(www.text);
            root = xmlDoc.SelectSingleNode("Skill/Magic"+gameObject.name);
			icon.spriteName = "Magic"+icon.name;
			but.normalSprite = "Magic"+icon.name;
		}

		ReadName ();
		ReadType ();
		//ReadLevel ();
	}

}
