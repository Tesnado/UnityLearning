using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;	

public class FastUI : MonoBehaviour {


	public string atmouse;
	List<string> btnName1 = new List<string> ();
	List<string> btnName2 = new List<string> ();
	string[] btn2 = new string[6];
	UISprite[] sp2 = new UISprite[6];
	int[] tp = new int[6];
	float[] cd = new float[6];
	float[] cdt = new float[6];
	UISprite[] cdsp = new UISprite[6];
	string used;
	float cdtt;

	// Use this for initialization
	void Start () {
		btnName1.Clear ();
		btnName1.Add ("Blink");
		btnName1.Add ("Strike");
		btnName1.Add ("Ultimate");
		for (int i = 1; i <= 9; i++)
			btnName1.Add ("Bag_Button_" + i);
		for (int i = 1; i <= 5; i++)
			btnName1.Add ("Icon" + i);
		btnName2.Clear ();
		for (int i = 1; i <= 5; i++)
			btnName2.Add ("FastUI_Button_" + i);
        for (int i = 1; i <= 5; i++)
            cdsp[i] = GameObject.Find("FastUI_Button_" + i).transform.GetChild(1).GetComponent<UISprite>();

	}

    // Update is called once per frame
    void Update()
    {
		/*
		foreach(string btnname in btnName1) {
			GameObject btnobj = GameObject.Find(btnname);
			if (btnobj != null) {
                if (btnobj.GetComponent<UIEventListener>() == null)
                {
                    btnobj.AddComponent<UIEventListener>();
                    UIEventListener.Get(btnobj).onDrag = ondrag;
                }
			}
		}*/
		foreach(string btnname in btnName2) {
			GameObject btnobj = GameObject.Find(btnname);
			if (btnobj != null) {
				if (btnobj.GetComponent<UIEventListener>() == null)
					btnobj.AddComponent<UIEventListener>();
                if (sp2[btnname[14] - '0'] == null)
                {
                    sp2[btnname[14] - '0'] = btnobj.transform.GetChild(0).GetComponent<UISprite>();
                    UIEventListener.Get(btnobj).onDrop = ondrop;
                    UIEventListener.Get(btnobj).onClick = onclick;
                }
			}
		}
		if (Skill.Instance != null && Skill.Instance.UsedSkill ()) {
			for (int i = 1; i <= 5; i++)
				if (btn2[i] == used) {
					cd[i] = 1;
					cdt[i] = cdtt;
				}
		}
		for (int i = 1; i <= 5; i++)
			sp2 [i].spriteName = btn2 [i];
		for (int i = 1; i <= 5; i++) {
            //Debug.Log(i + " "+ cd[i] +", " + cdt[i]);
            if (cd[i] > 0 && cdt[i] != 0)
            {
                cd[i] -= Time.deltaTime / cdt[i];
            }
			//if (cdsp[i] != null)
			cdsp[i].fillAmount = cd[i];
		}
	}

	void ondrag(GameObject g, Vector2 delta) {
			
	}

	void ondrop(GameObject to, GameObject fo) {
		//Debug.Log (fo.name + "->" + to.name);
		if (!btnName1.Contains (fo.name))
			return;
		if (!btnName2.Contains (to.name))
			return;
		string s = fo.transform.GetChild (0).GetComponent<UISprite> ().spriteName;
		tp [to.name [14] - '0'] = 1;
		if (fo.name.Contains ("Bag")) {
			s = GameObject.Find ("Bag(Clone)").GetComponent<BagManager> ().btn [fo.name [11] - '0'];
			tp [to.name [14] - '0'] = 2;
		}
		//Debug.Log (s);
		btn2 [to.name [14] - '0'] = s;
	}

	public void refresh(string s, bool t) {
		for (int i = 1; i <= 5; i++)
			if (btn2 [i] != null)
				if (btn2 [i] == s) {
					if (t)
						btn2 [i] = "icon-null";
					cdt[i] = 0.8f;
					cd[i] = 1;
				}
	}

	void onclick(GameObject g) {
        if (btn2[g.name[14] - '0'] == null) return;
		if (btn2 [g.name [14] - '0'] ==  ("icon-null"))
			return;
        //Debug.Log("use " + (g.name[14] - '0') + " " + btn2[g.name[14] - '0']);
		if (tp [g.name [14] - '0'] == 2) {
            if (cd[g.name[14] - '0'] > 0) return;
			if (!Property_Controller.Instance.Get_Bag ().Use (btn2 [g.name [14] - '0'])) {
				for (int i = 1; i <= 5; i++)
					if (i != g.name [14] - '0')
						if (btn2[i] != null)
							if (btn2[i] == btn2 [g.name [14] - '0'])
								btn2[i] = "icon-null";
				btn2 [g.name [14] - '0'] = "icon-null";
			}
			for (int i = 1; i <= 5; i++)
				if (btn2[i] != null && btn2 [g.name [14] - '0'] != "icon-null")
					if (btn2[i] == btn2 [g.name [14] - '0']) {
            			cdt[i] = 0.8f;
            			cd[i] = 1;
					}
            if (UI_manager.m_BagUI != null)
				UI_manager.m_BagUI.GetComponent<BagManager>().refresh();
		} else {
			Debug.Log(cd[g.name [14] - '0']);
			if (cd[g.name [14] - '0'] > 0) return;
            //Debug.Log(123);
            /*
            if (btn2[g.name[14] - '0'].Equals("skill-01"))
            {
                cd[g.name[14] - '0'] = Skill.blink_cd;
                cdt[g.name[14] - '0'] = Skill.blink_cd;
            }
            if (btn2[g.name[14] - '0'].Equals("skill-02"))
            {
                cd[g.name[14] - '0'] = Skill.strike_cd;
                cdt[g.name[14] - '0'] = Skill.strike_cd;
            }
            if (btn2[g.name[14] - '0'].Equals("skill-03"))
            {
                cd[g.name[14] - '0'] = Skill.ultimate_cd;
                cdt[g.name[14] - '0'] = Skill.ultimate_cd;
                Debug.Log(cdt[g.name[14] - '0']);
            }
            */
			cdtt = Skill_Register.GetSkill(btn2[g.name[14] - '0']).cd;
            //cd[g.name[14] - '0'] = 1;
			Skill_Register.DoSkill(btn2[g.name [14] - '0']);
			used = btn2[g.name [14] - '0'];
           // Debug.Log(42324242342);
		}
	}
}
