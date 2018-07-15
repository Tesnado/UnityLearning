using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
	GameObject character;
	// Use this for initialization
	void Start () {
		character = Character_Controller.character;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = character.transform.position;
	}
}
