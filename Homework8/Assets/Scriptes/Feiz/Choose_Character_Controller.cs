using UnityEngine;
using System.Collections;

public class Choose_Character_Controller : MonoBehaviour {
    private static Choose_Character_Controller instance;
    public static Choose_Character_Controller Instance{
        get{
            if(instance == null){
                instance = new Choose_Character_Controller();
            }
            return instance;
        }
    }
    private int current_id;
    GameObject sowrdman, magicman,controller;
    // Use this for initialization
	void Start () {
        current_id = 0;
        sowrdman = GameObject.Find("SwordmanModel");
        magicman = GameObject.Find("MagicianModel");
        controller = GameObject.Find("Controller");
        controller.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
        if (current_id == 0)
        {
            sowrdman.SetActive(true);
            magicman.SetActive(false);
        }
        else
        {
            sowrdman.SetActive(false);
            magicman.SetActive(true);
        }
	}

    public void Next()
    {
        current_id = (current_id + 1) % 2;
    }

    public void OK()
    {
        string Characrer_name = transform.Find("name").transform.Find("Label").GetComponent<UILabel>().text;
        if (Characrer_name.Equals(""))
        {
            return;
        }
        controller.SetActive(true);
        Property_Controller.Instance.ChooseCareer(current_id);
        Property_Controller.Instance.SetName(Characrer_name);
        GameObject.Destroy(sowrdman);
        GameObject.Destroy(magicman);
        this.gameObject.SetActive(false);
    }
}
