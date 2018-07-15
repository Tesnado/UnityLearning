using UnityEngine;
using System.Collections;

public class Disapear : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void hide() {
        GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
        GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, 0);
    }

    void show() {
        GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
        GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, 1);
    }
}
