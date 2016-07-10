using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	GameObject inventoryPanel;
	GameObject slotPanel;
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	public int slotAmount;

	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();

	void Start(){
		inventoryPanel = GameObject.Find ("InventoryPanel");
		slotPanel = inventoryPanel.transform.FindChild ("SlotPanel").gameObject;

		for (int i = 0; i < slotAmount; i++) {
			slots.Add (Instantiate(inventorySlot));
			slots [i].transform.SetParent (slotPanel.transform);
		}

	}
}
