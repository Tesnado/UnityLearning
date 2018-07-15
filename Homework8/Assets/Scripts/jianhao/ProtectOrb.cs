using UnityEngine;
using System.Collections;

public class ProtectOrb: MonoBehaviour {
	GameObject particle;
	Collider[] cols;
	GameObject character;
	float timer;
	// Use this for initialization
	void Start () {
        particle = Resources.Load("jianhao/BallParticle") as GameObject;
		//character = GameObject.Find("Character"); 
        character = Character_Controller.character;
        timer = 8f;
		InvokeRepeating ("Rotate_Around", 0, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0)
			Destroy (gameObject);
		/*transform.RotateAround (character.transform.position, Vector3.up, 120*Time.deltaTime);
		transform.LookAt (character.transform.position);
		transform.position = character.transform.position;
		transform.position -= transform.forward*3f;
		Instantiate (particle, transform.position, transform.rotation);*/
	}

	void Rotate_Around () {
		cols=Physics.OverlapSphere(transform.position, 1f);
		for (int i=0; i<cols.Length; i++) {
			if(cols[i] != null && cols[i].tag=="monster"){
				Monster_Manage monster = cols[i].GetComponent<Monster_Manage>();
				monster.Monster_Manage_Change_Hp(-Skill_Damage.Instance.GetDamage("MagicSkill3"));
			}
		}
		transform.RotateAround (character.transform.position, Vector3.up, 20);
		transform.LookAt (character.transform.position);
		transform.position = character.transform.position;
		transform.position -= transform.forward*3f;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        //if (Vector3.Dot(transform.forward, character.transform.GetChild(0).forward) < 0)
        //{
            Instantiate(particle, transform.position, transform.rotation);
        //}
	}
}
