using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {
    GameObject man;
    Vector3 height;
	// Use this for initialization
	void Start () {
        man = GameObject.Find("Swordman");
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(man.transform.position.x,130,man.transform.position.z);
	}
}
