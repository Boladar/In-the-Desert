using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

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

			inv.items [droppedItem.slot] = new Item ();
			inv.items [slotID] = droppedItem.item;
			droppedItem.slot = slotID;

		}else if (droppedItem.slot != slotID) {

			Transform item = this.transform.GetChild (0);

			if (inv.items [slotID].ID == droppedItem.item.ID 
				&& droppedItem.item.MaxStackSize > 1
				&& item.GetComponent<ItemData> ().amount < droppedItem.item.MaxStackSize 
				&& droppedItem.amount < droppedItem.item.MaxStackSize) {

				Debug.Log ("dodawanie");
			} else {

				item.GetComponent<ItemData> ().slot = droppedItem.slot;
				item.transform.SetParent (inv.slots [droppedItem.slot].transform);
				item.transform.position = inv.slots [droppedItem.slot].transform.position;

				droppedItem.slot = slotID;
				droppedItem.transform.SetParent (this.transform);
				droppedItem.transform.position = this.transform.position;

				inv.items [droppedItem.slot] = item.GetComponent<ItemData> ().item;
				inv.items [slotID] = droppedItem.item;
			}
		}
	}
}
