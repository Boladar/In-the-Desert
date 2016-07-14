using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotKeySlot : MonoBehaviour, IDropHandler{

	public int HotKeyID { get; set;}
	public GameObject HotkeyItemPrefab;
	public GameObject itemObj;

	private Inventory inventory;
	private ItemData currentItemData;


	void Start(){
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();
	}


	public void OnDrop (PointerEventData eventData)	{
		currentItemData = eventData.pointerDrag.GetComponent<ItemData>();
		GameObject itemObj = Instantiate (HotkeyItemPrefab);

		this.itemObj = itemObj;

		itemObj.GetComponent<HotKeyItem> ().data = currentItemData;
		itemObj.transform.GetChild (0).GetComponent<Text> ().text = currentItemData.Amount.ToString ();

		itemObj.transform.SetParent (this.transform);
		itemObj.transform.localPosition = Vector2.zero;

		itemObj.GetComponent<Image> ().sprite = currentItemData.item.Sprite;
		itemObj.name = currentItemData.item.Title;

	}

}
