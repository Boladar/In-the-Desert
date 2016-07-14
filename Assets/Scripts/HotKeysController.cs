using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HotKeysController : MonoBehaviour {

	public GameObject hotKeyPrefab;
	public int hotKeyAmount;
	public List<GameObject> hotKeys = new List<GameObject>();

	private Inventory inventory;

	void Start(){
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory>();
	

		for (int i = 0; i < hotKeyAmount; i++) {
			hotKeys.Add (Instantiate (hotKeyPrefab));
			hotKeys [i].transform.SetParent (this.transform);
			hotKeys [i].GetComponent<HotKey> ().HotKeyID = i;
		}
		
	}
}
