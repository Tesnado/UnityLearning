using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemType { 
    Weapon,
    Medicine
}

public class MerchantManager : MonoBehaviour {

    const int WEAPON_MERCHANT_ID = 4;
    const int HEALING_MERCHANT_ID = 5;
    Dictionary<int, Task> SubStreamTasks = new Dictionary<int, Task>();  // 次要任务列表


    private static MerchantManager instance;

    public static MerchantManager Instance
    {
        get {
            if (instance == null) {
                instance = new MerchantManager();
            }
            return instance;
        }
    }

    public void NpcTalk(int npcId, string defaultTalk) {
        if (npcId == WEAPON_MERCHANT_ID) {
            NpcTalkUIManager.Instance.ShowText(defaultTalk, npcId, true, true);
        } else if (npcId == HEALING_MERCHANT_ID) {
            NpcTalkUIManager.Instance.ShowText(defaultTalk, npcId, true, true);
        }
    }

    /*处理购买物品的事件
     *param: 商品类型，id，数量
     **/
    public void HandleBuyItems(ItemType type, int ItemId, int num) {
        Debug.Log("You have bought a item");
    }

    //用于更新次要任务列表,接口
    public void UpdateSubStreamTask(int npcId)
    {
        if (!SubStreamTasks.ContainsKey(npcId))
        {
            Task tmp = new Task();
            tmp.type = TaskType.KillMonster;
            tmp.npcid = npcId;
            tmp.moneyReward = 500;
            tmp.ExpReward = 500;
            tmp.beginmessage = "You help me a Lot!";
            tmp.endmessage = 500 + " money, " + 500 + " EXP";
            tmp.missionmessage = "Kill Normal Wolf";
            tmp.killid = 2;
            tmp.killcount = 10;
            tmp.taskid = npcId;
            SubStreamTasks.Add(tmp.taskid, tmp);
        }
        else
        { 
            
        }
    }


    public Task getCurSubTask(int taskId)
    {
        if (!SubStreamTasks.ContainsKey(taskId))
        {
            return null;
        }
        return SubStreamTasks[taskId];
    }

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
