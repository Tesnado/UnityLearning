using UnityEngine;
using System.Collections;

public class Skill_Range : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Collider[] cols=Physics.OverlapSphere(transform.position, 3f);
		for (int i=0; i<cols.Length; i++) {
			if(cols[i].tag=="monster"){
				//Health health = cols[i].GetComponent<Health>();
				//		health.SetHp(-20);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
