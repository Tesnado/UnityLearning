using UnityEngine;
using System.Collections;

public class SP_Change : MonoBehaviour {

	// Use this for initialization
	void Start () {
        sp();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void sp() {
        if (Property_Controller.my_Property == null)
            return;
        UILabel label = GetComponent<UILabel>();
        label.text = "" + Property_Controller.Instance.Get_Skill_Point();
    }
}
