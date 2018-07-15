using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TaskType {
    Talk,
    KillMonster,
    CollectCreature
}

public class Task {
    public TaskType type;
    public int npcid;
    public string beginmessage;
    public string endmessage;
    public int killcount;
    public int killid;
    public int creatureCount;
    public string creatureid;
    public string missionmessage;
    public int moneyReward;
    public int taskid;
    public int ExpReward;
    public string title;
}

public class TaskManager {
    List<Task> MainStreamTasks = new List<Task>();  // 主流任务列表
    Dictionary<int, Task> SubStreamTasks = new Dictionary<int, Task>();  // 次要任务列表

    //SwordmanControl play;
    Property_Controller play;

    static TaskManager instance;

    public static TaskManager Instance {
        get {
            if (instance == null) {
                instance = new TaskManager();
                instance.Init();
            }
            return instance;
        }
    }

    //主流任务流程安排
    public void Init() {
        //play = GameObject.Find("Swordman_No_Animation").GetComponent<SwordmanControl>();
       play = Property_Controller.Instance;


       Task a = new Task();
       a.title = "Visit the Eldest";
       a.type = TaskType.Talk;
       a.npcid = 1;
       a.beginmessage = "Welcome, my warrior!";
       a.endmessage = "Kill 2 wolves";
       a.missionmessage = "Go talk to the old man";
       MainStreamTasks.Add(a);

       Task b = new Task();
       b.title = "The First Hunting";
       b.type = TaskType.KillMonster;
       b.npcid = 1;
       b.beginmessage = "You're so brave! Thank you!";
       b.endmessage = "The pretty lady needs your help! Go and have a look!";
       b.missionmessage = "Kill 2 wolves";
       b.killid = 2;
       b.killcount = 2;
       b.moneyReward = 200;
       b.ExpReward = 200;
       MainStreamTasks.Add(b);

       Task c = new Task();
       c.title = "Visit the potion seller";
       c.type = TaskType.Talk;
       c.npcid = 2;
       c.beginmessage = "Hello, my brave man!";
       c.endmessage = "Boss wolf is nausty, help me punish him";
       c.missionmessage = "Talk to the beauty";
       MainStreamTasks.Add(c);

       Task d = new Task();
       d.title = "Save the village";
       d.type = TaskType.KillMonster;
       d.npcid = 2;
       d.beginmessage = "It won't come here now! Thank you!";
       d.endmessage = "The weapon merchant has somethings to tell you, GO!";
       d.missionmessage = "Kill Boss wolf";
       d.killid = 3;
       d.killcount = 1;
       d.moneyReward = 500;
       d.ExpReward = 500;
       MainStreamTasks.Add(d);


       Task f = new Task();
       f.title = "Visit the weapon seller";
       f.type = TaskType.Talk;
       f.npcid = 3;
       f.beginmessage = "I have been waiting for you soooo long!";
       f.endmessage = "I have missed my magic bottle, please help me find them.";
       f.missionmessage = "Talk to the merchant";
       MainStreamTasks.Add(f);

       Task g = new Task();
       g.title = "Collect Items";
       g.type = TaskType.CollectCreature;
       g.npcid = 3;
       g.beginmessage = "You have given me a big favor, here you go!";
       g.endmessage = "No more tasks already, go and have a adventure!";
       g.missionmessage = "Collect Magic Bottle";
       g.creatureid = "MpPotion";
       g.creatureCount = 5;
       g.moneyReward = 200;
       g.ExpReward = 200;
       MainStreamTasks.Add(g);
        //MainStreamTasks.Add(g);
    }

    public Task getCurTaskData(int index)
    {
        if (index < 0 || index >= MainStreamTasks.Count)
            return null;
        return MainStreamTasks[index];
    }

    public void NpcTalk(int npcId, string defaultTalk) {
        Task task = TaskManager.Instance.getCurTaskData(play.GetCurrentTask());
        //Task subTask = TaskManager.Instance.getCurSubTask(play.taskId);

        if (task != null && task.npcid == npcId)
        {
            if (task.type == TaskType.Talk)
            {
                NpcTalkUIManager.Instance.ShowText(task.beginmessage, npcId, true, false);
            }
            else if (task.type == TaskType.KillMonster && play.GetKillMonster() >= task.killcount)
            {
                NpcTalkUIManager.Instance.ShowText(task.beginmessage, npcId, true, false);
                Property_Controller.Instance.GetEXP(task.ExpReward);
                Property_Controller.Instance.ChangeMoney(task.moneyReward);
            }
            else if (task.type == TaskType.CollectCreature && play.GetCurrentCreatures() >= task.creatureCount)
            {
                NpcTalkUIManager.Instance.ShowText(task.beginmessage, npcId, true, false);
                Property_Controller.Instance.GetEXP(task.ExpReward);
                Property_Controller.Instance.ChangeMoney(task.moneyReward);
            }
            else
           {
               NpcTalkUIManager.Instance.ShowText(defaultTalk, npcId, false, false);
           }
        }
        else
        {
            NpcTalkUIManager.Instance.ShowText(defaultTalk, npcId, false, false);
        }
    }

    public void EndTask(int npcid) {
        Task task = getCurTaskData(play.GetCurrentTask());

        if (task != null && task.npcid == npcid) {
            play.SetKillMonster(0);
            play.SetCurrentCreatures(0);
            play.CurrentTaskIncrease();
            Task nextTask = getCurTaskData(play.GetCurrentTask());
            if (nextTask != null && nextTask.type == TaskType.CollectCreature)
            {
                Container bag = Property_Controller.Instance.Get_Bag();
                foreach(Goods a in bag.goodslist)
                {
                    if (a.name == nextTask.creatureid)
                    {
                        play.CurrentCreaturesIncrease();
                    }
                }
            }
            if (play.GetCurrentTask() > 5)
                play.SetCurrentTask(0);
            NpcTalkUIManager.Instance.ShowText(task.endmessage, npcid, false, false);
            TaskManager.Instance.UpdateTaskText();
        }
    }

    public void UpdateTaskText()
    {

        Task task = getCurTaskData(play.GetCurrentTask());
        if (task != null)
        {
            if (task.type == TaskType.Talk)
                CurTaskUIManager.Instance.SetTaskText("Main Task:\n" + task.missionmessage);
            else if (task.type == TaskType.KillMonster)
                CurTaskUIManager.Instance.SetTaskText("Main Task:\n" + task.missionmessage + "(" + play.GetKillMonster() + "/" + task.killcount + ")");
            else if (task.type == TaskType.CollectCreature)
                CurTaskUIManager.Instance.SetTaskText("Main Task:\n" + task.missionmessage + "(" + play.GetCurrentCreatures() + "/" + task.creatureCount + ")");
        }
        else
        {

            CurTaskUIManager.Instance.SetTaskText("");

        }
    }

    public void MonsterDie(int monsterId) {
        Task task = getCurTaskData(play.GetCurrentTask());
        if (task == null)
        {
            return;
        }

        SubTask_MonsterDie(monsterId);

        if (monsterId == task.killid)
            play.KillMonsterIncrease();
        TaskManager.Instance.UpdateTaskText();
    }

    public void CollectCreatures(string creatureId)
    {
        Task task = getCurTaskData(play.GetCurrentTask());
        if (task.type == TaskType.CollectCreature && creatureId == task.creatureid)
        {
            play.CurrentCreaturesIncrease();
            TaskManager.Instance.UpdateTaskText();
        }
    }

    public int getMoneyReward(int npcId) {
        Task task = getCurTaskData(play.GetCurrentTask());

        if (task != null && task.npcid == npcId)
        {
            if (task.type == TaskType.Talk)
                return task.moneyReward;
            else if (task.type == TaskType.KillMonster && play.GetKillMonster() >= task.killcount)
                return task.moneyReward;
            else if (task.type == TaskType.CollectCreature && play.GetCurrentCreatures() >= task.creatureCount)
                return task.moneyReward;
            else
                return 0;
        }
        return 0;
    }

    public int getExpReward(int npcId)
    {
        Task task = getCurTaskData(play.GetCurrentTask());

        if (task != null && task.npcid == npcId)
        {
            if (task.type == TaskType.Talk)
                return task.ExpReward;
            else if (task.type == TaskType.KillMonster && play.GetKillMonster() >= task.killcount)
                return task.ExpReward;
            else if (task.type == TaskType.CollectCreature && play.GetCurrentCreatures() >= task.creatureCount)
                return task.ExpReward;
            else
                return 0;
        }
        return 0;
    }

    //用于更新次要任务列表,接口
    public void SubTask_init(int npcId)
    {
        if (!SubStreamTasks.ContainsKey(npcId))
        {
            if (npcId == 4)
            {
                Task tmp = new Task();
                tmp.title = "Weapon Mission";
                tmp.type = TaskType.KillMonster;
                tmp.npcid = npcId;
                tmp.moneyReward = 500;
                tmp.ExpReward = 500;
                tmp.beginmessage = "I need wolf bones to make weapon, please help me.";
                tmp.endmessage = "Oh! Thanks for you help!";
                tmp.missionmessage = "Kill Normal Wolf For Bones";
                tmp.killid = 2;
                tmp.killcount = 10;
                tmp.taskid = npcId;
                SubStreamTasks.Add(tmp.taskid, tmp);
            }
            else if (npcId == 5)
            {
                Task tmp = new Task();
                tmp.title = "Potion Mission";
                tmp.type = TaskType.KillMonster;
                tmp.npcid = npcId;
                tmp.moneyReward = 500;
                tmp.ExpReward = 500;
                tmp.beginmessage = "Wolf blood is a great medicine, bring me the blood.";
                tmp.endmessage = "You help me a lot, here you are!";
                tmp.missionmessage = "Kill Boss for Blood";
                tmp.killid = 3;
                tmp.killcount = 1;
                tmp.taskid = npcId;
                SubStreamTasks.Add(tmp.taskid, tmp);
            }
        }
    }

    public void SubTask_NpcTalk(int npcId)
    {

        Task task = TaskManager.Instance.SubTask_getCurSubTask(npcId);

        if (task.type == TaskType.KillMonster && Property_Controller.my_Property.SubTask_Monster[task.killid -1] >= task.killcount)
        {
            NpcTalkUIManager.Instance.ShowText(task.endmessage, npcId, false, false);
            Property_Controller.Instance.GetEXP(task.ExpReward);
            Property_Controller.Instance.ChangeMoney(task.moneyReward);
            SubTask_EndTask(npcId);
            return;
        }
        else if (task.type == TaskType.CollectCreature && Property_Controller.my_Property.SubTask_Monster[task.killid - 1] >= task.creatureCount)
        {
            NpcTalkUIManager.Instance.ShowText(task.endmessage, npcId, false, false);
            return;
        }
        else
        {
            NpcTalkUIManager.Instance.ShowText(task.beginmessage, npcId, false, false);
            SubTask_UpdateTaskText();
        }
    }


    public Task SubTask_getCurSubTask(int taskId)
    {
        if (!SubStreamTasks.ContainsKey(taskId))
        {
            return null;
        }
        return SubStreamTasks[taskId];
    }

    public void SubTask_RemoveTask(int taskId)
    {
        if (!SubStreamTasks.ContainsKey(taskId))
        {
            return;
        }
        SubStreamTasks.Remove(taskId);
    }

    public bool SubTask_isInit(int taskId)
    {
        return SubStreamTasks.ContainsKey(taskId);
    }

    public void SubTask_UpdateTaskText()
    {
        int num = 0;
        string[] taskMsgs = new string[SubStreamTasks.Count];
        foreach (var task in SubStreamTasks)
        {
            if (task.Value.type == TaskType.KillMonster)
            {
                taskMsgs[num] = "Sub Task:\n" + task.Value.missionmessage + "(" + Property_Controller.my_Property.SubTask_Monster[task.Value.killid - 1] + "/" + task.Value.killcount + ")";
                num++;
            }
        }
        SubTaskUIManager.Instance.setSubTaskText(taskMsgs, SubStreamTasks.Count);
    }

    public void SubTask_MonsterDie(int monsterId){
        if (SubStreamTasks.Count == 0)
        {
            return;
        }

        Property_Controller.my_Property.SubTask_Monster[monsterId - 1] += 1;
        SubTask_UpdateTaskText();
    }

    public void SubTask_EndTask(int npcId)
    {
        Task task = SubTask_getCurSubTask(npcId);

        if (task != null)
        {
            Property_Controller.my_Property.SubTask_Monster[task.killid - 1] = 0;
        }

        SubTask_RemoveTask(npcId);
        SubTask_UpdateTaskText();
    }
}


public class TaskUIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
