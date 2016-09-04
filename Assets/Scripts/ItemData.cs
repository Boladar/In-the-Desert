using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public GameObject LooseItemPrefab;

	public Item Item {get;set;}

	private int amount;
	public int Amount { 
		get{
			return this.amount;
		}
		set{
			this.amount = value; 
			UpdateCounter ();

			if (this.amount == 0) {
				inventory.RemoveItem (this.slotID);
				DestroyItemDataObject (this);
			}
		}
	}
	
	public int slotID;

	private Inventory inventory;
	private InventoryTooltip tooltip;
	private Vector2 offset;

	void Start(){
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory>();
		tooltip = inventory.GetComponent<InventoryTooltip> ();
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		if (Item != null) {

			offset = eventData.position - new Vector2 (this.transform.position.x,this.transform.position.y);
			this.transform.SetParent (this.transform.parent.parent.parent.parent);
			this.transform.position = eventData.position;
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}
	}

	public void OnDrag (PointerEventData eventData)
	{
		if (Item != null) {
			this.transform.position = eventData.position - offset;
		}
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		if (EventSystem.current.IsPointerOverGameObject()) {
			this.transform.SetParent (inventory.slots [slotID].transform);
			this.transform.position = inventory.slots [slotID].transform.position;
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
		} else {
			DropItem (eventData);
		}
	}

	void DropItem(PointerEventData eventData){
		Debug.Log ("drop");
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
		inventory.RemoveItem (slotID);

		GameObject player = GameObject.Find ("Character");
		float height = player.GetComponent<BoxCollider2D> ().size.y;

		DestroyItemDataObject (droppedItem);

		GameObject looseGameObject = (GameObject) Instantiate (LooseItemPrefab, new Vector3 (player.transform.position.x, player.transform.position.y - height/2), Quaternion.identity);
		LooseItem looseItem = looseGameObject.GetComponent<LooseItem>();

		if (Item != null) {
			looseItem.item = Item;
		}
		
		looseItem.amount = this.Amount;
		Destroy (this.transform);
	}

	void DestroyItemDataObject(ItemData ItemToDelete){
		foreach (Transform child in ItemToDelete.transform) {
			GameObject.Destroy (child.gameObject);
		}
		Destroy (ItemToDelete.gameObject);
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		if (Item != null)
			tooltip.Activate (Item);
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		tooltip.Deactivate ();
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right && Item != null) {
			if (Item.Type == ItemType.WEAPON) {
				inventory.EquipWeapon (this.slotID, this.Item.ID);
				DestroyItemDataObject (this);
				Destroy (this.transform);
			}
		}
	}

	public void UpdateCounter(){
		this.transform.GetChild (0).GetComponent<Text> ().text = amount.ToString ();
	}
}
