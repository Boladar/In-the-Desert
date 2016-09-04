using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int hp = 100;
	public int Defence { get; set;}
	public Weapon currentWeapon;

	public Armour Head {get;set;}
	public Armour Body { get; set;}
	public Armour Feets { get; set;}
	public Armour Hands {get;set;}

	void Awake(){
		Defence = 0;
	}


}
