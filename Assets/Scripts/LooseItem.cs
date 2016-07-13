using UnityEngine;
using System.Collections;

public class LooseItem : MonoBehaviour {

	public Item item;
	public int amount;

	private Inventory inv;

	void Start(){
		inv = GameObject.Find ("Inventory").GetComponent<Inventory>();
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player" && Input.GetKey (KeyCode.E)) {
			Debug.Log ("pickup, ID: " + item.ID + ", amount: " + amount);
			inv.AddItem (item.ID, amount);	
			Destroy (this.gameObject);
		}
	}
		
}
