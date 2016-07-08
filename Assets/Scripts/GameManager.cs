using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private BoardManager boardScript;


	// Use this for initialization
	void Start () {
		boardScript = GetComponent<BoardManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
