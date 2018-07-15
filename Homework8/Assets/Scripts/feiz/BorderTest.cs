using UnityEngine;
using System.Collections;

public class BorderTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter( Collider other){
            Application.LoadLevelAdditive(this.gameObject.name);
            GameObject.Destroy(this.gameObject);
    }
}
