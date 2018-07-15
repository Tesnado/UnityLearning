using UnityEngine;
using System.Collections;

public class ShopConnect : MonoBehaviour {
    public Container potionContainer;
    public Container weaponContainer;
    public GameObject store;

	// Use this for initialization
	void Start () {
        weaponContainer = new Container();
        potionContainer = new Container();
        potionContainer.addGoods("HpPotion", 20);
        potionContainer.addGoods("potion2", 40);
        potionContainer.addGoods("MpPotion", 30);
        weaponContainer.addGoods("RustySword", 100);
        weaponContainer.addGoods("Long Sword", 100);
        weaponContainer.addGoods("Sword Of Knight", 200);
        weaponContainer.addGoods("Rod Of Learner", 100);
        weaponContainer.addGoods("Wood Rod", 50);
        weaponContainer.addGoods("Rod Of Magic", 150);
        weaponContainer.addGoods("Rusty Armor", 100);
        weaponContainer.addGoods("Iron Armor", 120);
        weaponContainer.addGoods("Mantle Of Learner", 100);
        weaponContainer.addGoods("Mantle Of Intelligence", 150);
        weaponContainer.addGoods("Wool Boot", 120);
        weaponContainer.addGoods("Red Boot", 100);
        //weaponContainer.addGoods("etcFang", 4);
        //weaponContainer.addGoods("etcFur", 4);
        weaponContainer.addGoods("White Helm", 100);
        weaponContainer.addGoods("Black Helm", 100);
        weaponContainer.addGoods("Magic Hat", 100);
        weaponContainer.addGoods("Higher Hat", 150);
        weaponContainer.addGoods("Yellow Ring", 200);
        weaponContainer.addGoods("Green Ring", 200);
        weaponContainer.addGoods("Blue Shield", 200);
        weaponContainer.addGoods("High Blue Shield", 250);
        weaponContainer.addGoods("Tailman", 300);
        weaponContainer.addGoods("Sange and Yasha", 500);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnStoreConnect() {
        UI_manager.Instance.ShowMyUI("Shop");
            if (NpcTalkUIManager.Instance.curNpcId == 4) {
                UI_manager.m_ShopUI.GetComponent<ShopManager>().Init(weaponContainer);
            }
            else if (NpcTalkUIManager.Instance.curNpcId == 5)
            {
                UI_manager.m_ShopUI.GetComponent<ShopManager>().Init(potionContainer);
            }
            NpcTalkUIManager.Instance.gameObject.SetActive(false);

    }
}
