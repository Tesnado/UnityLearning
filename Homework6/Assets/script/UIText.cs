using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour {

    GameController _controller;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        _controller = GameController.getInstance();
        if (_controller.isOver) this.gameObject.GetComponent<Text>().text = "Game Over!";
	}
}
