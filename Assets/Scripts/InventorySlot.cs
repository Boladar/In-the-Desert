﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler {

	public int slotID;
	private Inventory inv;


	void Start(){
		inv = GameObject.Find ("Inventory").GetComponent<Inventory>();
	}

	public void OnDrop (PointerEventData eventData)
	{
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

		if (inv.items [slotID].ID == -1) {
			//moving to free slot
			inv.items [droppedItem.slotID] = new Item ();
			inv.items [slotID] = droppedItem.Item;
			droppedItem.slotID = slotID;

		}else if (droppedItem.slotID != slotID) {
			// Adding items
			Transform item = this.transform.GetChild (0);

			if (inv.items [slotID].ID == droppedItem.Item.ID 
				&& droppedItem.Item.MaxStackSize > 1
				&& item.GetComponent<ItemData> ().Amount + droppedItem.Amount <= droppedItem.Item.MaxStackSize ) {

				item.GetComponent<ItemData> ().Amount += droppedItem.Amount;


				inv.items [droppedItem.slotID] = new Item ();
				inv.items [slotID] = droppedItem.Item;
				droppedItem.slotID = slotID;

				foreach (Transform child in droppedItem.transform) {
					GameObject.Destroy (child.gameObject);
				}
				Destroy (droppedItem.gameObject);

			} else {
				//switching items
				item.GetComponent<ItemData> ().slotID = droppedItem.slotID;
				item.transform.SetParent (inv.slots [droppedItem.slotID].transform);
				item.transform.position = inv.slots [droppedItem.slotID].transform.position;

				inv.items [droppedItem.slotID] = item.GetComponent<ItemData> ().Item;

				droppedItem.slotID = slotID;
				droppedItem.transform.SetParent (this.transform);
				droppedItem.transform.position = this.transform.position;

				inv.items [slotID] = droppedItem.Item;
			}
		}
	}
}
