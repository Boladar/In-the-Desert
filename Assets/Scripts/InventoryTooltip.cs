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

		switch (item.Type) {
		case ItemType.WEAPON:
			ConstructWeaponDataString ();
			break;
		default:
			ConstructItemDataString ();
			break;
		}

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
		Weapon weapon = item as Weapon;
		data = "<color=#000000>" + weapon.Title + "</color> \n\n" + weapon.Description + "\n" + weapon.Value + '\n' + "weapon ammo id" + weapon.AmmoID;
		tooltip.transform.GetChild (0).GetComponent<Text> ().text = data;
	}
}
