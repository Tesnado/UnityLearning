using UnityEngine;
using System.Collections;

public class SmallMap : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.transform.position.x,
		                                 10,
		                                 player.transform.position.z);
		transform.LookAt (player.transform.position);
	}
}
