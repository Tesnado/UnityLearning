using UnityEngine;
using System.Collections;

public class testUI : MonoBehaviour {

    GameObject bag;
    GameObject store;
    GameObject status;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Store()
    {
        if (store == null)
        {
            store = Instantiate(Resources.Load("hufan/Shop")) as GameObject;
            store.transform.parent = GameObject.Find("UI Root").transform;
            store.transform.localScale = Vector3.one;
            //store.GetComponent<ShopManager>().Init(Property_Controller.Instance.Get_Bag());
            store.transform.localPosition = Vector3.zero;
        }
    }

    public void Bag()
    {
        if (bag == null)
        {
            bag = Instantiate(Resources.Load("hufan/Bag")) as GameObject;
            bag.transform.parent = GameObject.Find("UI Root").transform;
            
            bag.transform.localScale = Vector3.one;
            bag.GetComponent<BagManager>().Init(Property_Controller.Instance.Get_Bag());
            bag.transform.localPosition = Vector3.zero;
        }
    }

    public void Status()
    {
        if (status == null)
        {
            status = Instantiate(Resources.Load("Feiz/StatusUI")) as GameObject;
            status.transform.parent = GameObject.Find("UI Root").transform;
            status.transform.localScale = Vector3.one;
            status.transform.localPosition = Vector3.zero;
        }
        else
        {
            if (status.active)
            {
                status.SetActive(false);
            }
            else
            {
                status.SetActive(true);
            }
        }
    }
}
