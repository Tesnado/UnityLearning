using UnityEngine;
using System.Collections;

public class Skill_Upgrade : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Upgrade () {
		if (Property_Controller.Instance.Get_Skill_Point() > 0) {
			Skill_Register.GetSkill(DetailLoad.skill_name).skill_level++;
			Property_Controller.Instance.Change_Skill_Point(-1);
		}
	}
}
