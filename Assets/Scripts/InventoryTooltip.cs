using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryTooltip : MonoBehaviour {

	private Item item;
	private Weapon weapon;
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
		ConstructItemDataString ();
		tooltip.SetActive (true);
	}

	public void Activate(Weapon weapon){
		this.weapon = weapon;
		ConstructWeaponDataString ();
		tooltip.SetActive (true);
	}

	public void Deactivate(){
		tooltip.SetActive (false);
	}

	public void ConstructItemDataString(){
		data = "<color=#000000>" + item.Title + "</color> \n\n" + item.Description + "\n" + item.Value + '\n';
		tooltip.transform.GetChild (0).GetComponent<Text> ().text = data;
	}

	public void ConstructWeaponDataString(){
		data = "<color=#000000>" + weapon.Title + "</color> \n\n" + weapon.Description + "\n" + weapon.Value + '\n' + "kupa";
		tooltip.transform.GetChild (0).GetComponent<Text> ().text = data;
	}
}
