using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotKeyItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

	public ItemData data;

	private Inventory inventory;

	void Start(){
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();
	}

	void Update(){
		UpdateCounter ();

		if (data.Amount == 0)
			DestroyHotKeyItemObject (this);	
	}


	void DestroyHotKeyItemObject(HotKeyItem ItemToDelete){
		foreach (Transform child in ItemToDelete.transform) {
			GameObject.Destroy (child.gameObject);
		}
		Destroy (ItemToDelete.gameObject);
	}

	public void OnBeginDrag (PointerEventData eventData){
		Destroy (this.gameObject);
	}
		

	public void UseItem(){
		if (data != null)
			data.Amount -= 1;
	}

	public void OnDrag (PointerEventData eventData)
	{
		throw new System.NotImplementedException ();
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		throw new System.NotImplementedException ();
	}

	public void UpdateCounter(){
		this.transform.GetChild (0).GetComponent<Text> ().text = data.Amount.ToString ();
	}
}
