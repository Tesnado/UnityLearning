using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Skill_Register {
	static Dictionary<string,Skill_Attribution> skill_dic = new Dictionary<string,Skill_Attribution> ();
	//static Dictionary<string,Action<string>> SkillDic_string =new Dictionary<string,Action<string>> ();
	public static void AddSkill (string _skill_name,Skill_Attribution _skill) {
		if (!skill_dic.ContainsKey (_skill_name))
			skill_dic.Add (_skill_name, _skill);
	}
	
	public static void DoSkill (string _skill_name) {
		if (skill_dic.ContainsKey (_skill_name))
			skill_dic [_skill_name].effect();
	}

	public static Skill_Attribution GetSkill (string _skill_name) {
		if (skill_dic.ContainsKey (_skill_name))
			return skill_dic [_skill_name];
		else
			return null;
	}

	public static void UpgradeSkill (string _skill_name) {
		if (skill_dic.ContainsKey (_skill_name))
			skill_dic [_skill_name].skill_level++;
	}
}
