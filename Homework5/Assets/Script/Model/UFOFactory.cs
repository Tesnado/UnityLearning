using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFactory { //开始时继承了MonoBehaviour后来发现不继承也可以
    public GameObject ufoPrefab;
    public static UFOFactory DF = new UFOFactory();

    private Dictionary<int, UFO> used = new Dictionary<int, UFO>();//used是用来保存正在使用的飞碟 
    private List<UFO> ufoPool = new List<UFO>();//free是用来保存未激活的飞碟 

    private UFOFactory()
    {
        ufoPrefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UFO"));//获取预制的游戏对象
        ufoPrefab.AddComponent<UFO>();
        ufoPrefab.SetActive(false);
    }

    public void FreeUFO()
    {
        foreach (UFO x in used.Values)
        {
            if (!x.gameObject.activeSelf)
            {
                ufoPool.Add(x);
                used.Remove(x.GetInstanceID());
                return;
            }
        }
    }

    public UFO GetUFO(int round)  
    {
        FreeUFO();
        GameObject newUFO = null;
        UFO ufodata;
        if (ufoPool.Count > 0)
        {
            //从之前生产的UFO中拿出可用的
            newUFO = ufoPool[0].gameObject;
            ufoPool.Remove(ufoPool[0]);
        }
        else
        {
            //克隆预制对象，生产新UFO
            newUFO = GameObject.Instantiate<GameObject>(ufoPrefab, Vector3.zero, Quaternion.identity);
        }
        newUFO.SetActive(true);
        ufodata = newUFO.GetComponent<UFO>();

        int swith;

        /** 
         * 根据回合数来生成相应的飞碟,难度逐渐增加。
         */
        float s;
        if (round == 1)
        {
            swith = Random.Range(0, 3);
            s = Random.Range(30, 40);
        }
        else if (round == 2)
        {
            swith = Random.Range(0, 4);
            s = Random.Range(40, 50);
        }
        else {
            swith = Random.Range(0, 6);
            s = Random.Range(50, 60);
        } 
        
        switch (swith)  
        {  
             
            case 0:  
                {  
                    ufodata.color = Color.yellow;  
                    ufodata.speed = s;  
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;  
                    ufodata.Direction = new Vector3(RanX, 1, 0);
                    ufodata.StartPoint = new Vector3(Random.Range(-130, -110), Random.Range(30,90), Random.Range(110,140));
                    break;  
                }  
            case 1:  
                {  
                    ufodata.color = Color.red;  
                    ufodata.speed = s + 10;  
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;  
                    ufodata.Direction = new Vector3(RanX, 1, 0);
                    ufodata.StartPoint = new Vector3(Random.Range(-130, -110), Random.Range(30, 80), Random.Range(110, 130));
                    break;  
                }  
            case 2:  
                {  
                    ufodata.color = Color.black;  
                    ufodata.speed = s + 15;  
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;  
                    ufodata.Direction = new Vector3(RanX, 1, 0);
                    ufodata.StartPoint = new Vector3(Random.Range(-130,-110), Random.Range(30, 70), Random.Range(90, 120));
                    break;  
                }
            case 3:
                {
                    ufodata.color = Color.yellow;
                    ufodata.speed = -s;
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
                    ufodata.Direction = new Vector3(RanX, 1, 0);
                    ufodata.StartPoint = new Vector3(Random.Range(130, 110), Random.Range(30, 90), Random.Range(110, 140));
                    break;
                }
            case 4:
                {
                    ufodata.color = Color.red;
                    ufodata.speed = -s - 10;
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
                    ufodata.Direction = new Vector3(RanX, 1, 0);
                    ufodata.StartPoint = new Vector3(Random.Range(130, 110), Random.Range(30, 80), Random.Range(110, 130));
                    break;
                }
            case 5:
                {
                    ufodata.color = Color.black;
                    ufodata.speed = -s - 15;
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
                    ufodata.Direction = new Vector3(RanX, 1, 0);
                    ufodata.StartPoint = new Vector3(Random.Range(130, 110), Random.Range(30, 70), Random.Range(90, 120));
                    break;
                }
        }
        used.Add(ufodata.GetInstanceID(), ufodata); //添加到使用中
        ufodata.name = ufodata.GetInstanceID().ToString();
        return ufodata;  
    }
}
