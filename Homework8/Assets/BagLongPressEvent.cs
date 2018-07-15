using UnityEngine;
using System.Collections;

public class BagLongPressEvent : MonoBehaviour {
    GameObject b1, b2, b3, b4, b5, b6, b7, b8, b9;
    //static GameObject current_Button;
	// Use this for initialization
	void Start () {
        //UIEventListener.Get(this.gameObject).onPress += LongPressMinus;
        b1 = transform.Find("Bag_Sprite1").GetChild(0).gameObject;
        b2 = transform.Find("Bag_Sprite1").GetChild(1).gameObject;
        b3 = transform.Find("Bag_Sprite1").GetChild(2).gameObject;
        b4 = transform.Find("Bag_Sprite1").GetChild(3).gameObject;
        b5 = transform.Find("Bag_Sprite1").GetChild(4).gameObject;
        b6 = transform.Find("Bag_Sprite1").GetChild(5).gameObject;
        b7 = transform.Find("Bag_Sprite1").GetChild(6).gameObject;
        b8 = transform.Find("Bag_Sprite1").GetChild(7).gameObject;
        b9 = transform.Find("Bag_Sprite1").GetChild(8).gameObject;
        UIEventListener.Get(b1).onPress += LongPressPlus;
        UIEventListener.Get(b2).onPress += LongPressPlus;
        UIEventListener.Get(b3).onPress += LongPressPlus;
        UIEventListener.Get(b4).onPress += LongPressPlus;
        UIEventListener.Get(b5).onPress += LongPressPlus;
        UIEventListener.Get(b6).onPress += LongPressPlus;
        UIEventListener.Get(b7).onPress += LongPressPlus;
        UIEventListener.Get(b8).onPress += LongPressPlus;
        UIEventListener.Get(b9).onPress += LongPressPlus;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool pressPlus = false;


    void LongPressPlus(GameObject sender, bool state)
    {
        Floating_UI_Controller.current_Button = sender;
        pressPlus = state;
        if (state)
        {
            StartCoroutine(ExcuteLongPlus());
        }
        else
        {
            CancelInvoke();
        }
    }



    IEnumerator ExcuteLongPlus()
    {
        yield return new WaitForSeconds(0.5f);
        if (pressPlus)
        {
            InvokeRepeating("PlusTimes", 0, 0.1f);
        }
    }

    public void PlusTimes()
    {
        UI_manager.Instance.ShowMyUI("FloatingUI");
    }

}
