using UnityEngine;
using System.Collections;

public class TaskWindowUIManager : MonoBehaviour {

    private static TaskWindowUIManager instance;

    public static TaskWindowUIManager Instance {
        get {
            if (instance == null) {
                instance = new TaskWindowUIManager();
            }
            return instance;
        }
    }

    UILabel taskText;

    public void OnTaskAccept() {
        if (NpcTalkUIManager.Instance.curNpcId < 4)
        {
            TaskManager.Instance.EndTask(NpcTalkUIManager.Instance.curNpcId);
        }
        else if (NpcTalkUIManager.Instance.curNpcId > 3)
        {
            TaskManager.Instance.SubTask_NpcTalk(NpcTalkUIManager.Instance.curNpcId);
        }
        gameObject.SetActive(false);

    }

    public void ShowTaskDetailMsg() {
        if (taskText == null) {
            return;
        }
        Task task = TaskManager.Instance.getCurTaskData(Property_Controller.Instance.GetCurrentTask() + 1);

        if (task.type != TaskType.Talk)
        {
            taskText.text = "[ffff00]" + task.title + "\n\n" +
                "[-]" + task.missionmessage + "\n\n" +
                "[00ff00]" + task.moneyReward + " x Gold\n\n" +
                "[00ff00]" + task.ExpReward + " EXP\n";
        }
    }

    public void ShowSubTaskDetail(int npcId)
    {
        if (taskText == null)
        {
            return;
        }
        Task task = TaskManager.Instance.SubTask_getCurSubTask(npcId);

        taskText.text = "[ffff00]" + task.title + "\n\n" +
                "[-]" + task.missionmessage + "\n\n" +
                "[00ff00]" + task.moneyReward + " x Gold\n\n" +
                "[00ff00]" + task.ExpReward + " EXP\n";
    }

    public void OnTaskCancel() {
        gameObject.SetActive(false);
        NpcTalkUIManager.Instance.gameObject.SetActive(true);
        TaskManager.Instance.SubTask_RemoveTask(NpcTalkUIManager.Instance.curNpcId);
    }

	// Use this for initialization
	void Start () {
        instance = this;
        gameObject.SetActive(false);
        taskText = transform.Find("TaskMsg").GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
