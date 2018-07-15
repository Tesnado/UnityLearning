using UnityEngine;
using System.Collections;

public class LV_Change : MonoBehaviour {

	// Use this for initialization
	void Start () {
        lv();
    }
	
	// Update is called once per frame
	void Update () {
		/*if (Property_Controller.my_Property == null)
			return;
		UILabel label = GetComponent<UILabel> ();
		if (Property_Controller.my_Property.career == Career.Swordman) {
            if(label.text == "Lv" + Skill_Register.GetSkill("Sword" + name).skill_level)
            return;
            label.text = "Lv" + Skill_Register.GetSkill("Sword" + name).skill_level;
        }		
		else if (Property_Controller.my_Property.career == Career.Magician) {
            if (label.text == "Lv" + Skill_Register.GetSkill("Magic" + name).skill_level)
            return;
            label.text = "Lv" + Skill_Register.GetSkill("Magic" + name).skill_level;
        }*/
			
	}
    public void lv () {
        if (Property_Controller.my_Property == null)
            return;
        UILabel label = GetComponent<UILabel>();
        if (Property_Controller.my_Property.career == Career.Swordman)
            label.text = "Lv" + Skill_Register.GetSkill("Sword" + name).skill_level;
        else if (Property_Controller.my_Property.career == Career.Magician) {
            Debug.Log(Skill_Register.GetSkill("Magic" + name));
            label.text = "Lv" + Skill_Register.GetSkill("Magic" + name).skill_level;
        }
            
    }
}
