using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubTaskUIManager : MonoBehaviour {

    private static SubTaskUIManager instance;

    public static SubTaskUIManager Instance {
        get {
            if (instance == null) {
                instance = new SubTaskUIManager();
            }

            return instance;
        }
    }

    UILabel[] LabelLists;
    Dictionary<int, UILabel> pair;
    int useLabelNum;

	// Use this for initialization
	void Start () {
        instance = this;
        LabelLists = new UILabel[3];
        pair = new Dictionary<int, UILabel>();
        useLabelNum = 0;
        LabelLists[0] = transform.Find("Label").GetComponent<UILabel>();
        LabelLists[1] = transform.Find("Label1").GetComponent<UILabel>();
        LabelLists[2] = transform.Find("Label2").GetComponent<UILabel>();
	}

    public void setSubTaskText(string[] texts, int count) {
        for (int i = 0; i < LabelLists.Length; i++) {
            if (LabelLists[i] == null) {
                return;
            }

            LabelLists[i].text = "";
        }

        for (int i = 0; i < count; i++) {
            LabelLists[i].text = texts[i];
        }

    }

    // Update is called once per frame
    void Update()
    {
	
	}
}
