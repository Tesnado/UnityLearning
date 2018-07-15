using UnityEngine;
using System.Collections;

public class Skill_Damage {
	int ad {
		get {
			return Property_Controller.Instance.Get_AD ();
		}
	}

	int ap {
		get {
			return Property_Controller.Instance.Get_AP ();
		}
	}

	static Skill_Damage instance;
	public static Skill_Damage Instance {
		get {
			if(instance==null){
				instance=new Skill_Damage();
			}
			return instance;
		}
	}
	
	public int GetDamage (string skill_name) {
		switch (skill_name) {
		case "SwordSkill1":
			return Sword1 ();
		case "SwordSkill2":
			return Sword2 ();
		case "SwordSkill3":
			return Sword3 ();
		case "SwordSkill4":
			return Sword4 ();
		case "MagicSkill1":
			return Magic1 ();
		case "MagicSkill2":
			return Magic2 ();
		case "MagicSkill3":
			return Magic3 ();
		case "MagicSkill4":
			return Magic4 ();
		}
		return 0;
	}
	
	int Sword1 () {
		int lv = Skill_Register.GetSkill("SwordSkill1").skill_level;
        return 5 + 3 * lv + 2 * ad;
	}

	int Sword2 () {
		int lv = Skill_Register.GetSkill("SwordSkill2").skill_level;
        return 20 + 8 * lv + 3 * ad;
	}

	int Sword3 () {
		int lv = Skill_Register.GetSkill("SwordSkill3").skill_level;
        return 12 + 5 * lv + 2 * ad;
	}

	int Sword4 () {
		int lv = Skill_Register.GetSkill("SwordSkill4").skill_level;
        return 8 + 3 * lv + (int)(0.1 * ad);
	}

	int Magic1 () {
		int lv = Skill_Register.GetSkill("MagicSkill1").skill_level;
        return 3 * lv + 2 * ap;
	}

	int Magic2 () {
		int lv = Skill_Register.GetSkill("MagicSkill2").skill_level;
        return 10 + 3 * lv + 3 * ap;
	}

	int Magic3 () {
		int lv = Skill_Register.GetSkill("MagicSkill3").skill_level;
        return 5 + 3 * lv + 1 * ap;
	}

	int Magic4 () {
        int lv = Skill_Register.GetSkill("MagicSkill4").skill_level;
        return 25 + 5 * lv + 3 * ap;
    }
}
