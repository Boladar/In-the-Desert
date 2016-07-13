using UnityEngine;
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
			
			inv.items [droppedItem.slotID] = new Item ();
			inv.items [slotID] = droppedItem.item;
			droppedItem.slotID = slotID;

		}else if (droppedItem.slotID != slotID) {

			Transform item = this.transform.GetChild (0);

			if (inv.items [slotID].ID == droppedItem.item.ID 
				&& droppedItem.item.MaxStackSize > 1
				&& item.GetComponent<ItemData> ().amount + droppedItem.amount <= droppedItem.item.MaxStackSize ) {

				item.GetComponent<ItemData> ().amount += droppedItem.amount;

				item.GetChild(0).GetComponent<Text>().text = item.GetComponent<ItemData>().amount + "";

				inv.items [droppedItem.slotID] = new Item ();
				inv.items [slotID] = droppedItem.item;
				droppedItem.slotID = slotID;

				foreach (Transform child in droppedItem.transform) {
					GameObject.Destroy (child.gameObject);
				}
				Destroy (droppedItem.gameObject);

			} else {

				item.GetComponent<ItemData> ().slotID = droppedItem.slotID;
				item.transform.SetParent (inv.slots [droppedItem.slotID].transform);
				item.transform.position = inv.slots [droppedItem.slotID].transform.position;

				inv.items [droppedItem.slotID] = item.GetComponent<ItemData> ().item;

				droppedItem.slotID = slotID;
				droppedItem.transform.SetParent (this.transform);
				droppedItem.transform.position = this.transform.position;

				inv.items [slotID] = droppedItem.item;
			}
		}
	}
}
