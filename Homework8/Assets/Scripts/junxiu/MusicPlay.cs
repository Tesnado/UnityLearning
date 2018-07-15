using UnityEngine;
using System.Collections;

public class MusicPlay : MonoBehaviour {
	UIToggle musicToggle;
	GameObject bgMusic;

	void Awake() {
		bgMusic = GameObject.Find ("BgMusic");
		musicToggle = GetComponent<UIToggle> ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MusicControl() {
		//Debug.Log (GetComponent<UIToggle> ().value);
		if (musicToggle.value) {
			bgMusic.GetComponent<AudioSource>().Play();
		} else {
			bgMusic.GetComponent<AudioSource>().Stop ();
		}
	}
}
