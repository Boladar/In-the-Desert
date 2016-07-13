using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class LooseItemTooltip : MonoBehaviour {

	private List<Item> nearbyItems = new List<Item>();
	private List<GameObject> nearbyGameObjects = new List<GameObject>();
	private Item currentItem;
	private GameObject currentGameObject;
	private string data;
	private GameObject tooltip;
	public float detectDistance;
	public SortingLayer itemLayer;
	private Inventory inventory;

	void Start(){
		tooltip = GameObject.Find ("LooseItemTooltip");
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory>();
		tooltip.SetActive (false);
	}

	void Update(){
		if (nearbyItems.Count > 0) {
			Activate ();
			ConstructDataString ();
		}
		else
			Deactivate();

		if (Input.GetKeyDown(KeyCode.Q)) {
			ChangeCurrentItem ();
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			LooseItem li = currentGameObject.GetComponent<LooseItem> ();

			Debug.Log ("pickup, ID: " + currentItem.ID);
			inventory.AddItem (currentItem.ID, li.amount);
			RemoveNearbyItem (currentItem,currentGameObject);
			Destroy (currentGameObject);

			ChangeCurrentItem ();
		}

	}

	public void ChangeCurrentItem(){
		int index = nearbyItems.IndexOf (currentItem);

		if (nearbyItems.Count != 0) {
			currentItem = nearbyItems.ElementAt ((index + 1) % nearbyItems.Count);
			currentGameObject = nearbyGameObjects.ElementAt ((index + 1) % nearbyItems.Count);
		} 
	}

	public void AddNearbyItem(Item item, GameObject itemGameObject){
		if (!nearbyItems.Contains (item)) {
			nearbyItems.Add (item);
			currentItem = item;
		}

		if (!nearbyGameObjects.Contains (itemGameObject)) {
			nearbyGameObjects.Add (itemGameObject);
			currentGameObject = itemGameObject;
		}
	}

	public void RemoveNearbyItem(Item Item, GameObject itemGameObject){
		nearbyItems.Remove (Item);
		nearbyGameObjects.Remove (itemGameObject);
	}

	public void Activate(){
		tooltip.SetActive (true);
	}

	public void Deactivate(){
		tooltip.SetActive (false);
	}

	public void ConstructDataString(){
		data = "<color=#000000>" + currentItem.Title + "</color> \n\n" + currentItem.Description + "\n" + currentItem.Value;
		tooltip.transform.GetChild (0).GetComponent<Text> ().text = data;
	}
}
