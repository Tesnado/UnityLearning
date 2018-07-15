using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : SSActionManager, ISSActionCallback
{
    Vector3[] posset = new Vector3[] { new Vector3(1, 0.25f, 1), new Vector3(1, 0.25f, -1), new Vector3(-1, 0.25f, -1), new Vector3(-1, 0.25f, 1) };
    Vector3[] Random = new Vector3[] { new Vector3(3, 0, 0), new Vector3(0, 0, 3), new Vector3(-3, 0, 0), new Vector3(0, 0, -3) };
    int rollnum = 0;
    private static GameController instance;
    public bool isOver = false;

    public GameObject player, guard;
    private GameObject _player, scene;
    private List<GameObject> GuardSet;

    // Use this for initialization
    new void Start()
    {
        _player = Instantiate(player);
        _player.name = "player";
        instance = this;
        initGuard();
    }


    public static GameController getInstance()
    {
        return instance;
    }
    void initGuard()
    {
        GuardSet = new List<GameObject>(4);
        for (int i = 0; i < 4; i++)
        {
            GameObject newone = Instantiate(guard);
            newone.name = "guard" + i;
            newone.transform.position = posset[i];
        }

    }
    // Update is called once per frame

    public int getPlayerArea()
    {
        return _player.GetComponent<playerStatus>().areaIndex;
    }
    public void goCatch(GameObject guard)
    {
        addSingleMoving(guard, _player.transform.position, 0.05f, false);
    }
    public void goBack(GameObject guard, bool aaa)
    {

        string name = guard.gameObject.name;
        char cindex = name[name.Length - 1];
        int index = cindex - '0';
        addSingleMoving(guard, posset[index], 0.05f, false);
    }
    public void goarround(GameObject guard, int dir)
    {
        string name = guard.gameObject.name;
        char cindex = name[name.Length - 1];
        int index = cindex - '0';
        Vector3 oriTarget = Random[dir];
        oriTarget += guard.transform.position;

        addSingleMoving(guard, oriTarget, 0.05f, false);

    }
    void addSingleMoving(GameObject sourceObj, Vector3 target, float speed, bool isCatching)
    {
        this.runAction(sourceObj, CCMoveToAction.CreateSSAction(target, speed, isCatching), this);
    }
    public bool nearby(Vector3 a, Vector3 b)
    {
        float x1, x2, z1, z2;
        x1 = a.x;
        x2 = b.x;
        z1 = a.z;
        z2 = b.z;


        if (Math.Abs(x1 - x2) < 0.9f && Math.Abs(z1 - z2) < 0.9f)
        {
            //Debug.Log("pos:" + Math.Abs(x1 - x2) + "," + Math.Abs(z1 - z2));
            return true;
        }
        else return false;
    }
    public void patrolHitHeroAndGameover()
    {
        isOver = true;
    }
    public void SSActionEvent(SSAction source, SSActionEventType eventType = SSActionEventType.Completed, SSActionTargetType intParam = SSActionTargetType.Normal, string strParam = null, object objParam = null)
    {
        //throw new NotImplementedException();
    }
}
