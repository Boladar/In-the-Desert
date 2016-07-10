using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryTooltip : MonoBehaviour {

	private Item item;
	private string data;
	private GameObject tooltip;

	void Start(){
		tooltip = GameObject.Find("InventoryTooltip");
		tooltip.SetActive(false);
	}

	void Update(){
		if (tooltip.activeSelf) {
			tooltip.transform.position = Input.mousePosition;
		}
	}

	public void Activate(Item item){
		this.item = item;
		ConstructDataString ();
		tooltip.SetActive (true);
	}

	public void Deactivate(){
		tooltip.SetActive (false);
	}

	public void ConstructDataString(){
		data = "<color=#000000>" + item.Title + "</color> \n\n" + item.Description + "\n" + item.Value;
		tooltip.transform.GetChild (0).GetComponent<Text> ().text = data;
	}
}
