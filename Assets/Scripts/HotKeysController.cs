using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HotKeysController : MonoBehaviour {

	public GameObject HotKeySlotPrefab;
	public int hotKeyAmount;
	public List<GameObject> hotKeys = new List<GameObject>();

	private Inventory inventory;

	void Start(){
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory>();
	

		for (int i = 0; i < hotKeyAmount; i++) {
			hotKeys.Add (Instantiate (HotKeySlotPrefab));
			hotKeys [i].transform.SetParent (this.transform);
			hotKeys [i].GetComponent<HotKeySlot> ().HotKeyID = i;
		}
		
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			UseItem (0);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			UseItem (1);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			UseItem (2);
		}
	}

	void UseItem(int id){
		if (hotKeys [id].GetComponent<HotKeySlot> ().itemObj != null) {
			hotKeys [id].GetComponent<HotKeySlot> ().itemObj.GetComponent<HotKeyItem> ().UseItem();
		}
	}

}
