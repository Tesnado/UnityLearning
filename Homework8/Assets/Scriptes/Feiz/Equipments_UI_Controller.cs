using UnityEngine;
using System.Collections;

public class Equipments_UI_Controller : MonoBehaviour {
    UISprite head, armor, right, left, shoe, accessory;
	// Use this for initialization
	void Start () {
        head = transform.Find("Head").transform.Find("Icon").GetComponent<UISprite>();
        armor = transform.Find("Armor").transform.Find("Icon").GetComponent<UISprite>();
        right = transform.Find("Right").transform.Find("Icon").GetComponent<UISprite>();
        left = transform.Find("Left").transform.Find("Icon").GetComponent<UISprite>();
        shoe = transform.Find("Shoe").transform.Find("Icon").GetComponent<UISprite>();
        accessory = transform.Find("Accessory").transform.Find("Icon").GetComponent<UISprite>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Property_Controller.my_Property.equipments[1] != null)
        {
            head.spriteName = Property_Controller.my_Property.equipments[1].spname;
        }
        if (Property_Controller.my_Property.equipments[2] != null)
        {
            armor.spriteName = Property_Controller.my_Property.equipments[2].spname;
        }
        if (Property_Controller.my_Property.equipments[3] != null)
        {
            right.spriteName = Property_Controller.my_Property.equipments[3].spname;
        }
        if (Property_Controller.my_Property.equipments[4] != null)
        {
            left.spriteName = Property_Controller.my_Property.equipments[4].spname;
        }
        if (Property_Controller.my_Property.equipments[5] != null)
        {
            shoe.spriteName = Property_Controller.my_Property.equipments[5].spname;
        }
        if (Property_Controller.my_Property.equipments[6] != null)
        {
            accessory.spriteName = Property_Controller.my_Property.equipments[6].spname;
        }
	}

    public void exit()
    {
        this.gameObject.SetActive(false);
    }
}
