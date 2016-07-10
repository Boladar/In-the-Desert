using UnityEngine;
using System.Collections;

public class ToggleInventory : MonoBehaviour {
	CanvasGroup cg;
	// Use this for initialization
	void Start () {
		cg = GetComponent<CanvasGroup> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.I)){
			if (cg.alpha == 1)
				cg.alpha = 0;
			else
				cg.alpha = 1;

			if (cg.interactable == true)
				cg.interactable = false;
			else
				cg.interactable = true;

			if (cg.blocksRaycasts == true)
				cg.blocksRaycasts = false;
			else
				cg.blocksRaycasts = true;
		}
	}
}
