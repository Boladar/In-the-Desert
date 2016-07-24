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

	public Player player;

	void Start(){
		database = this.transform.GetComponent<ItemDatabase>();

		if (database == null)
			Debug.LogError ("ItemDatabase does not exist! - Inventory line 22");


		inventoryPanel = GameObject.Find ("InventoryPanel");
		slotPanel = inventoryPanel.transform.FindChild ("SlotPanel").gameObject;
		player = GameObject.Find ("Character").GetComponent<Player>();

		for (int i = 0; i < slotAmount; i++) {
			items.Add (new Item());
			slots.Add (Instantiate(inventorySlot));
			slots [i].GetComponent<InventorySlot> ().slotID = i;
			slots [i].transform.SetParent (slotPanel.transform);
		}


		AddItem (99, 3);
		AddItem (98, 4);
		AddItem (100, 1);
		AddItem (101, 1);

	}

	public void EquipWeapon(int slotID, int WeaponID){
		if (player.currentWeapon != null)
			DeEquipWeapon (player.currentWeapon.ID);

		RemoveItem (slotID);
		player.currentWeapon = database.GetWeaponByID (WeaponID);

		Debug.Log ("player.currentWeapon.id" + player.currentWeapon.ID);
	}

	public void DeEquipWeapon(int WeaponID)
	{
		ReserveItemSpace(WeaponID);
		player.currentWeapon = null;
	}

	public void RemoveItem(int slotID){

		items.RemoveAt (slotID);
		items.Insert (slotID, new Item ());

	}

	public void ReserveItemSpace(int id){
		
		Item item = database.GetItemByID(id);

		for (int i = 0; i < items.Count; i++) {
			if (items [i].ID == -1) {

				GameObject itemObj = Instantiate (inventoryItem);

				if (id >= 100) {
					Weapon weapon = database.GetWeaponByID (id);
					items [i] = weapon;
					itemObj.GetComponent<ItemData> ().weapon = weapon;
					itemObj.GetComponent<Image> ().sprite = weapon.Sprite;
				} else {
					items [i] = item;
					itemObj.GetComponent<ItemData> ().Item = item;
					itemObj.GetComponent<Image> ().sprite = item.Sprite;
					itemObj.name = item.Title;
				}
					
				itemObj.transform.SetParent (slots [i].transform);
				itemObj.transform.localPosition = Vector2.zero;
				itemObj.GetComponent<ItemData> ().slotID = i;
				itemObj.GetComponent<ItemData> ().Amount = 1;
				return;

			}
		}
	}


	public void AddItem(int id, int quantity){

		Item item = database.GetItemByID(id);

		if (GetNotFullItemDataFromID (id) == null)
			ReserveItemSpace (id);
		else
			quantity += 1;

		ItemData data = GetNotFullItemDataFromID (id);
		if (item.MaxStackSize > 1) {
			for (int j = 1; j < quantity; j++) {

				if (data.Amount < item.MaxStackSize) {
					data.Amount += 1;
				} else {
					ReserveItemSpace (id);
					data = GetNotFullItemDataFromID (id);
					if (data == null) {
						Debug.LogError ("item data is equal to null");
						Debug.Log ("data amount: " + data.Amount);
					}
				}
			}
		}else {
			for (int i = 1; i < quantity; i++) {
				ReserveItemSpace (id);
			}
		}
	}

	public ItemData GetNotFullItemDataFromID(int id){
		
		for (int i = 0; i < items.Count; i++) {
			if (items[i].ID == id) {
				ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
				if (data.Amount < items [i].MaxStackSize)
					return data;
			}
		}
		return null;
	}

	bool CheckForItemInInventory(Item item){
		for (int i = 0; i < items.Count; i++) {
			if (items[i].ID == item.ID)
				return true;
		}
		return false;
	}
}
