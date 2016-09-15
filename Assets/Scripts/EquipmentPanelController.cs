using UnityEngine;
using System.Collections.Generic;

public class EquipmentPanelController : MonoBehaviour{

	public GameObject ArmourSlotPrefab;
	public GameObject WeaponSlotPrefab;

	public Dictionary<ArmourSlot,GameObject> ArmourSlots = new Dictionary<ArmourSlot, GameObject>();
	public List<GameObject> WeaponSlots = new List<GameObject> ();

	private Inventory inventory;

	// Use this for initialization
	void Start () {
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();

		ArmourSlots.Add (ArmourSlot.HEAD, Instantiate (ArmourSlotPrefab));
		ArmourSlots [ArmourSlot.HEAD].transform.SetParent (this.transform);

		ArmourSlots.Add (ArmourSlot.BODY, Instantiate (ArmourSlotPrefab));
		ArmourSlots [ArmourSlot.BODY].transform.SetParent (this.transform);

		ArmourSlots.Add (ArmourSlot.FEETS, Instantiate (ArmourSlotPrefab));
		ArmourSlots [ArmourSlot.FEETS].transform.SetParent (this.transform);

		ArmourSlots.Add (ArmourSlot.HANDS, Instantiate (ArmourSlotPrefab));
		ArmourSlots [ArmourSlot.HANDS].transform.SetParent (this.transform);

		WeaponSlots.Add (Instantiate (WeaponSlotPrefab));
		WeaponSlots[0].transform.SetParent(this.transform);
	}

	public bool CheckForArmourType(ItemData data, GameObject armourObject ){
		Armour armour = data.Item as Armour;
		GameObject ArmourSlot = GetArmourSlot( armour.Slot);
		
		if(ArmourSlot.Equals(armourObject))
			return true;
		else
			return false;
	}
	
	public GameObject GetArmourSlot(ArmourSlot slot){
		GameObject value;
		if (ArmourSlots.TryGetValue (slot, out value)) {
			return value;
		}

		Debug.LogError("EquipmentPanelController - Get Armour Slot, no value for armour slot");
		return null;
	}
}
