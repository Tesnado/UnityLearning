using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardStatus : MonoBehaviour {
    Vector3[] posset = new Vector3[] { new Vector3(1, 0.25f, 1), new Vector3(1, 0.25f, -1), new Vector3(-1, 0.25f, -1), new Vector3(-1, 0.25f, 1) };
    Vector3[] Random = new Vector3[] { new Vector3(3, 0, 0), new Vector3(0, 0, 3), new Vector3(-3, 0, 0), new Vector3(0, 0, -3) };
    Vector3 prepos;

    private int ownIndex = -1;
    public bool isCatching;
    public bool isIdle;
    int dir = 0;
    public GameController _controller;
    
    // Use this for initialization
    void Start () {
        ownIndex = getOwnIndex();
        dir = ownIndex;
        if (dir == 3) dir = 1;
        else if (dir == 1) dir = 3;
        isCatching = false;
        isIdle = true;
        _controller = GameController.getInstance() as GameController;
    }
	
	// Update is called once per frame
	void Update () {
        checkPlayerCome();
        
    }
    int getOwnIndex()
    {
        string name = this.gameObject.name;
        char cindex = name[name.Length - 1];
        int result = cindex - '0';
        return result;
    }
    void checkPlayerCome()
    {
        if (_controller.getPlayerArea() == ownIndex)
        {    //只有当走进自己的区域
            if (!isCatching)
            {
                isCatching = true;
                isIdle = false;
            }
            _controller.goCatch(this.gameObject);
        }
        else
        {
            if (isCatching)
            {    //刚才为捕捉状态，但此时hero已经走出所属区域
                //gameStatusOp.heroEscapeAndScore();
                isCatching = false;
                resetdir();
                _controller.goBack(this.gameObject, isIdle);
            }
            else
            {
                //gameStatusOp.heroEscapeAndScore();
            }
            if(!isIdle)
            {
                checkIsIdle();
            }
            else
            {
                if(this.transform.position == prepos + Random[dir])
                {
                    turn();
                    prepos = transform.position;
                    _controller.goarround(this.gameObject, dir);
                }
                    
            }
        }
    }
    void OnCollisionStay(Collision e)
    {
        
        //撞击hero，游戏结束
        if (e.gameObject.name.Contains("player"))
        {
            _controller.patrolHitHeroAndGameover();
            Debug.Log("Game Over!");
        }
    }
    void resetdir()
    {
        dir = ownIndex;
        if (dir == 3) dir = 1;
        else if (dir == 1) dir = 3;

    }
    void checkIsIdle()
    {
        Vector3 a = this.transform.position;
        Vector3 b = posset[ownIndex];
        if (a == b)
        {
            isIdle = true;
            prepos = posset[ownIndex];
            _controller.goarround(this.gameObject, dir);
        }
        
    }
    public void turn()
    {
        dir++;
        if (dir > 3) dir = 0;
        Debug.Log(dir + "turn");
    }
}
