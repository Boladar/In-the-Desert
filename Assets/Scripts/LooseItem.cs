using UnityEngine;
using System.Collections;

public class LooseItem : MonoBehaviour {

	public Item item;
	public int amount;
	public float detectDistance;
	public LayerMask playerMask;

	private GameObject player;
	private Inventory inventory;

	void Start(){
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory>();
		player = GameObject.Find ("Character");
	}

	/*void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player" && Input.GetKey (KeyCode.E)) {
			Debug.Log ("pickup, ID: " + item.ID + ", amount: " + amount);
			inv.AddItem (item.ID, amount);	
			Destroy (this.gameObject);
		}
	}*/

	void Update(){
		RaycastHit2D hit = Physics2D.Raycast (transform.position, player.transform.position - this.transform.position, detectDistance, playerMask);
		if (hit != null) {
			player.GetComponent<LooseItemTooltip> ().Activate (item);	

			if (Input.GetKey (KeyCode.E)) {
				Debug.Log ("pickup, ID: " + item.ID + ", amount: " + amount);
				inventory.AddItem (item.ID, amount);	
				Destroy (this.gameObject);
			}
		}
	}

		
}
