using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {

	public GameObject LooseItemPrefab;
	public Item item;

	private int amount;
	public int Amount { 
		get{
			return this.amount;
		}
		set{
			this.amount = value; 
			UpdateCounter ();

			if (this.amount == 0)
				DestroyItemDataObject (this);
		}
	}
	
	public int slotID;

	private Inventory inv;
	private InventoryTooltip tooltip;
	private Vector2 offset;

	void Start(){
		inv = GameObject.Find ("Inventory").GetComponent<Inventory>();
		tooltip = inv.GetComponent<InventoryTooltip> ();
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		if (item != null) {

			offset = eventData.position - new Vector2 (this.transform.position.x,this.transform.position.y);
			this.transform.SetParent (this.transform.parent.parent.parent.parent);
			this.transform.position = eventData.position;
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}
	}

	public void OnDrag (PointerEventData eventData)
	{
		if (item != null) {
			this.transform.position = eventData.position - offset;
		}
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		if (EventSystem.current.IsPointerOverGameObject()) {
			this.transform.SetParent (inv.slots [slotID].transform);
			this.transform.position = inv.slots [slotID].transform.position;
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
		} else {
			DropItem (eventData);
		}
	}

	void DropItem(PointerEventData eventData){
		Debug.Log ("drop");
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
		inv.RemoveItem (slotID);

		GameObject player = GameObject.Find ("Character");
		float height = player.GetComponent<BoxCollider2D> ().size.y;

		DestroyItemDataObject (droppedItem);

		GameObject looseGameObject = (GameObject) Instantiate (LooseItemPrefab, new Vector3 (player.transform.position.x, player.transform.position.y - height/2), Quaternion.identity);
		LooseItem looseItem = looseGameObject.GetComponent<LooseItem>();
		looseItem.item = new Item(item.ID,item.Title,item.Value,item.Description,item.MaxStackSize,item.Slug);
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
		tooltip.Activate (item);
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		tooltip.Deactivate ();
	}

	public void UpdateCounter(){
		this.transform.GetChild (0).GetComponent<Text> ().text = amount.ToString ();
	}
}
