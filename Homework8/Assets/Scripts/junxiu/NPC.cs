using UnityEngine;
using System.Collections;


public enum NPCType { 
    Task,
    Merchant
}

public class NPC : MonoBehaviour {

    const float DISTANCE = 4;  // 点击NPC回应的距离。

    public NPCType type;
    public int npcId;
    public string defaultTalk;

    //SwordmanControl player;
    Property_Controller player;

	// Use this for initialization
    void Start()
    {
        //player = GameObject.Find("Swordman_No_Animation").GetComponent<SwordmanControl>();
        player = Property_Controller.Instance;

    }
    void OnMouseClick() {
        if (Character_Controller.character == null)
        {
            return;
        }
        Ray ray = Character_Controller.character.transform.Find("Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            GameObject go = hit.collider.gameObject;

            if (Input.GetMouseButtonDown(0) && go.name == this.gameObject.name)
            {
                if (Vector3.Distance(go.transform.position, Character_Controller.character.transform.position) < DISTANCE)
                {
                    transform.LookAt(Character_Controller.character.transform);
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

                    if (type == NPCType.Task)
                    {
                        TaskManager.Instance.NpcTalk(npcId, defaultTalk);
                    }
                    else if (type == NPCType.Merchant) {
                        MerchantManager.Instance.NpcTalk(npcId, defaultTalk);
                    }
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        OnMouseClick();
	}
}
