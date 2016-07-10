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

			inv.items [droppedItem.slot] = new Item ();
			inv.items [slotID] = droppedItem.item;
			droppedItem.slot = slotID;

		}else if (droppedItem.slot != slotID) {

			Transform item = this.transform.GetChild (0);

			if (inv.items [slotID].ID == droppedItem.item.ID 
				&& droppedItem.item.MaxStackSize > 1
				&& item.GetComponent<ItemData> ().amount + droppedItem.amount <= droppedItem.item.MaxStackSize ) {

				Debug.Log ("item.amount: " + item.GetComponent<ItemData> ().amount + "droppeditem.amount" + droppedItem.amount);

				item.GetComponent<ItemData> ().amount += droppedItem.amount;

				Debug.Log ("po: " + item.GetComponent<ItemData> ().amount);

				item.GetChild(0).GetComponent<Text>().text = item.GetComponent<ItemData>().amount + "";

				inv.items [droppedItem.slot] = new Item ();
				inv.items [slotID] = droppedItem.item;
				droppedItem.slot = slotID;

				foreach (Transform child in droppedItem.transform) {
					GameObject.Destroy (child.gameObject);
				}
				Destroy (droppedItem.gameObject);

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
