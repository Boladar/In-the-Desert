using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LooseItemTooltip : MonoBehaviour {

	private Item item;
	private string data;
	private GameObject tooltip;

	void Start(){
		tooltip = GameObject.Find ("LooseItemTooltip");
		tooltip.SetActive (false);
	}

	void Update(){
		if (tooltip.activeSelf) {
			tooltip.transform.position = this.transform.position;
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
