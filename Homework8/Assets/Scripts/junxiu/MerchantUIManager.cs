using UnityEngine;
using System.Collections;

public class MerchantUIManager : MonoBehaviour {

    private static MerchantUIManager instance;

    public static MerchantUIManager Instance
    {
        get {
            if (instance == null) {
                instance = new MerchantUIManager();
            }
            return instance;
        }
    }

    const int WEAPON_MERCHANT_ID = 4;
    const int HEALING_MERCHANT_ID = 5;
    GameObject WeaponStore;
    GameObject MedicineStore;
    GameObject buyBtn;

    public void ShowItemsUI(int npcid) {
        gameObject.SetActive(true);
        if (npcid == WEAPON_MERCHANT_ID) {
            WeaponStore.SetActive(true);
            MedicineStore.SetActive(false);
        }
        else if (npcid == HEALING_MERCHANT_ID) {
            MedicineStore.SetActive(true);
            WeaponStore.SetActive(false);
        }
    }

    /*点击“购买”的时候处理事件*/
    public void BuyItems() {
        MerchantManager.Instance.HandleBuyItems(ItemType.Medicine, 4, 4);
    }

    /*点击“离开”退出商店*/
    public void OnLeave() {
        gameObject.SetActive(false);
        WeaponStore.SetActive(false);
        MedicineStore.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        instance = this;
        gameObject.SetActive(false);
        WeaponStore = transform.Find("WeaponStoreWindow").gameObject;
        MedicineStore = transform.Find("MedicineStoreWindow").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
