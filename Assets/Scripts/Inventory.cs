using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	GameObject inventoryPanel;
	GameObject slotPanel;
	ItemDatabase database;
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	public int slotAmount;

	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();

	void Start(){
		database = this.transform.GetComponent<ItemDatabase>();

		if (database == null)
			Debug.LogError ("ItemDatabase does not exist! - Inventory line 22");


		inventoryPanel = GameObject.Find ("InventoryPanel");
		slotPanel = inventoryPanel.transform.FindChild ("SlotPanel").gameObject;

		for (int i = 0; i < slotAmount; i++) {
			items.Add (new Item());
			slots.Add (Instantiate(inventorySlot));
			slots [i].GetComponent<InventorySlot> ().slotID = i;
			slots [i].transform.SetParent (slotPanel.transform);
		}

		AddItem (0,10);
		AddItem (1, 3);

	}

	public void AddItem(int id, int quantity){
		Item item = database.GetItemByID(id);
		for(int q = 0; q < quantity ; q++){
			if (item.MaxStackSize > 1 && CheckForItemInInventory(item)) {
				for (int i = 0; i < items.Count; i++) {
					if (items [i].ID == item.ID) {

						ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();

						//Debug.Log ("data.amount: " + data.amount);

						if (data.amount < item.MaxStackSize) {
							data.amount++;
							data.transform.GetChild (0).GetComponent<Text> ().text = data.amount.ToString ();
							continue;
						}
					}
						
				}
			}
				for (int i = 0; i < items.Count; i++) {
					if (items [i].ID == -1) {

						items [i] = item;
						GameObject itemObj = Instantiate (inventoryItem);

						itemObj.GetComponent<ItemData> ().item = item;
						itemObj.GetComponent<ItemData> ().slot = i;

						itemObj.transform.SetParent (slots [i].transform);
						itemObj.transform.position = Vector2.zero;

						itemObj.GetComponent<Image> ().sprite = item.Sprite;
						itemObj.name = item.Title;
						break;
				}
			}
		}
	}

	bool CheckForItemInInventory(Item item){
		for (int i = 0; i < items.Count; i++) {
			if (items[i].ID == item.ID)
				return true;
		}
		return false;
	}
}
