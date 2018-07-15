using UnityEngine;
using System.Collections;

public class CurTaskUIManager : MonoBehaviour {

    private static CurTaskUIManager instance;

    public static CurTaskUIManager Instance {
        get {
            if (instance == null) {
                instance = new CurTaskUIManager();
            }
            return instance;
        }
    }

    UILabel curTask;


	// Use this for initialization
	void Start () {

        instance = this;
        curTask = transform.Find("Label").GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetTaskText(string text) {
        curTask.text = text;
    }
}
