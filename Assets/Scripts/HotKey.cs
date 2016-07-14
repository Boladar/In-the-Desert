using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotKey : MonoBehaviour, IDropHandler, IBeginDragHandler {

	public int HotKeyID { get; set;}
	public GameObject InventoryItemPrefab;

	private Inventory inventory;
	private ItemData currentItemData;


	void Start(){
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();
	}
	//TODO create different class for hotkeyes objects to prevent bugs!
	public void OnDrop (PointerEventData eventData)	{
		currentItemData = eventData.pointerDrag.GetComponent<ItemData>();
		GameObject itemObj = Instantiate (InventoryItemPrefab);

		itemObj.GetComponent<ItemData> ().item = currentItemData.item;
		itemObj.GetComponent<ItemData> ().amount = currentItemData.amount;
		itemObj.transform.GetChild (0).GetComponent<Text> ().text = currentItemData.amount.ToString ();

		itemObj.transform.SetParent (this.transform);
		itemObj.transform.localPosition = Vector2.zero;

		itemObj.GetComponent<Image> ().sprite = currentItemData.item.Sprite;
		itemObj.name = currentItemData.item.Title;

	}

}
