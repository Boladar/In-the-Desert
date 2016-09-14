using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IDropHandler {

	public GameObject EquipmentItemPrefab;
	public GameObject itemObj;

	private ItemData currentItemData;

	public void OnDrop (PointerEventData eventData){
		//check if it's a proper type



		currentItemData = eventData.pointerDrag.GetComponent<ItemData>();
		GameObject itemObj = Instantiate (EquipmentItemPrefab);

		this.itemObj = itemObj;

		itemObj.GetComponent<EquipmentItem> ().data = currentItemData;
		itemObj.transform.GetChild (0).GetComponent<Text> ().text = currentItemData.Amount.ToString ();

		itemObj.transform.SetParent (this.transform);
		itemObj.transform.localPosition = Vector2.zero;

		itemObj.GetComponent<Image> ().sprite = currentItemData.Item.Sprite;
		itemObj.name = currentItemData.Item.Title;
	}
}
