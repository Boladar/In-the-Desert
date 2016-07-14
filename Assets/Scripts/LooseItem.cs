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

	void Update(){

		float distanceToplayer = Vector2.Distance (this.transform.position, player.transform.position);

		if (distanceToplayer <= detectDistance) {
			player.GetComponent<LooseItemTooltip> ().AddNearbyItem(item, this.gameObject);
		} else {
			player.GetComponent<LooseItemTooltip> ().RemoveNearbyItem (item, this.gameObject);
		}
	}

		
}
