using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public GameObject LooseItemPrefab;

	private Item item;
	public Item Item {
		get {
			if (item == null)
				return weapon as Item;
			else
				return item;
		}
		set{ item = value;}
	}	
	public Weapon weapon;

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

	private Inventory inventory;
	private InventoryTooltip tooltip;
	private Vector2 offset;

	void Start(){
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory>();
		tooltip = inventory.GetComponent<InventoryTooltip> ();
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		if (item != null || weapon != null) {

			offset = eventData.position - new Vector2 (this.transform.position.x,this.transform.position.y);
			this.transform.SetParent (this.transform.parent.parent.parent.parent);
			this.transform.position = eventData.position;
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}
	}

	public void OnDrag (PointerEventData eventData)
	{
		if (item != null || weapon!= null) {
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

		if(item != null)
			looseItem.item = new Item(item.ID,item.Title,item.Value,item.Description,item.MaxStackSize,item.Slug);
		if (weapon != null)
			looseItem.weapon = new Weapon (weapon.ID, weapon.Title, weapon.Value, weapon.Description, weapon.MaxStackSize, weapon.Slug, weapon.AmmoID, weapon.Range, weapon.Damage);
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
		if (item != null)
			tooltip.Activate (item);
		if(weapon != null)
			tooltip.Activate (weapon);
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		tooltip.Deactivate ();
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right && weapon != null) {
			inventory.EquipWeapon (this.slotID, this.weapon.ID);
			DestroyItemDataObject (this);
			Destroy (this.transform);
		}
	}

	public void UpdateCounter(){
		this.transform.GetChild (0).GetComponent<Text> ().text = amount.ToString ();
	}
}
