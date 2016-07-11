using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {

	public GameObject LooseItemPrefab;
	public Item item;
	public int amount;
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
			this.transform.SetParent (this.transform.parent.parent);
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
		if (EventSystem.current.IsPointerOverGameObject ()) {
			this.transform.SetParent (inv.slots [slotID].transform);
			this.transform.position = inv.slots [slotID].transform.position;
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
		} else {
			Debug.Log ("drop");
			ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
			inv.RemoveItem (slotID);

			foreach (Transform child in droppedItem.transform) {
				GameObject.Destroy (child.gameObject);
			}
			Destroy (droppedItem.gameObject);

			GameObject player = GameObject.Find ("Character");
			float height = player.GetComponent<BoxCollider2D> ().size.y;

			GameObject looseGameObject = (GameObject) Instantiate (LooseItemPrefab, new Vector3 (player.transform.position.x, player.transform.position.y - height/2), Quaternion.identity);
			LooseItem looseItem = looseGameObject.GetComponent<LooseItem>();
			looseItem.item = item;
			looseItem.amount = amount;
			Destroy (this.transform);
		}
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		tooltip.Activate (item);
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		tooltip.Deactivate ();
	}

}
