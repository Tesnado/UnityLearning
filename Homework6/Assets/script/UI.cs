using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public class Diretion
    {
        public const int UP = 0;
        public const int DOWN = 2;
        public const int LEFT = -1;
        public const int RIGHT = 1;
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            heroMove(Diretion.UP);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            heroMove(Diretion.DOWN);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            heroMove(Diretion.LEFT);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            heroMove(Diretion.RIGHT);
        }
    }
    public void heroMove(int dir)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, dir * 90, 0));
        switch (dir)
        {
            case Diretion.UP:
                transform.position += new Vector3(0, 0, 0.1f);
                break;
            case Diretion.DOWN:
                transform.position += new Vector3(0, 0, -0.1f);
                break;
            case Diretion.LEFT:
                transform.position += new Vector3(-0.1f, 0, 0);
                break;
            case Diretion.RIGHT:
                transform.position += new Vector3(0.1f, 0, 0);
                break;
        }
    }
}
