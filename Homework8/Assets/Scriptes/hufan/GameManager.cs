using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected static GameManager instance_ = null;
	public static GameManager instance {
		get {
			if (instance_ == null) {
				instance_ = new GameManager();
			}
			return instance_;
		}
	}

}


public class atmouse{
	string name;

}