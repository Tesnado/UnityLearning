using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStatus : MonoBehaviour {
    public int areaIndex = -1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //定位玩家
        float posX = this.gameObject.transform.position.x;
        float posZ = this.gameObject.transform.position.z;
        if(posX>0)
        {
            if (posZ > 0) areaIndex = 0;
            else areaIndex = 1;
        }
        else
        {
            if (posZ > 0) areaIndex = 3;
            else areaIndex = 2;
        }
    }
}
