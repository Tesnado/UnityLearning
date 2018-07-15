using UnityEngine;
using System.Collections;

public class NpcTalkUIManager : MonoBehaviour {

    public static NpcTalkUIManager Instance;

    GameObject TaskOkBtn, StoreOkBtn;
    UILabel talk;
    public int curNpcId;
    UISprite headIcon;

	// Use this for initialization
	void Start () {
        Instance = this;
        gameObject.SetActive(false);
        TaskOkBtn = transform.Find("TaskOk").gameObject;
        StoreOkBtn = transform.Find("StoreOk").gameObject;
        talk = transform.Find("TalkLabel").gameObject.GetComponent<UILabel>();
        headIcon = transform.Find("HeadIcon").gameObject.GetComponent<UISprite>();
	}

    public void ShowText(string text, int npcid, bool showTaskOk, bool showStoreOk)
    {
        if (npcid == 1)
        {
            headIcon.spriteName = "questkeeper";
        }
        else if (npcid == 2)
        {
            headIcon.spriteName = "potionseller";
        }
        else if (npcid == 3)
        {
            headIcon.spriteName = "weaponsmith";
        }
        else if (npcid == 4)
        {
            headIcon.spriteName = "weaponsmith";
        }
        else if (npcid == 5)
        {
            headIcon.spriteName = "potionseller";
        }

        gameObject.SetActive(true);
        talk.text = text;
        TaskOkBtn.SetActive(showTaskOk);
        StoreOkBtn.SetActive(showStoreOk);
        curNpcId = npcid;
    }

    public void OnTaskOk()
    {
        Task task = TaskManager.Instance.getCurTaskData(Property_Controller.Instance.GetCurrentTask());
        //Debug.Log(player.currentTask);
        if (curNpcId < 4)
        {
            if (task.type == TaskType.Talk)
            {
                TaskWindowUIManager.Instance.gameObject.SetActive(true);
                gameObject.SetActive(false);
                TaskWindowUIManager.Instance.ShowTaskDetailMsg();
            }
            else
            {
                TaskManager.Instance.EndTask(curNpcId);
            }
        }
        else if (curNpcId > 3)
        {
            if (!TaskManager.Instance.SubTask_isInit(curNpcId))
            {
                TaskManager.Instance.SubTask_init(curNpcId);
                TaskWindowUIManager.Instance.gameObject.SetActive(true);
                TaskWindowUIManager.Instance.ShowSubTaskDetail(curNpcId);
                gameObject.SetActive(false);
            }
            else
            {
                TaskManager.Instance.SubTask_NpcTalk(curNpcId);
            }
        }
    }

    public void OnStoreOk() {
        MerchantUIManager.Instance.ShowItemsUI(curNpcId);
        gameObject.SetActive(false);
    }

    public void OnLeave() {
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}


}
