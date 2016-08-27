using UnityEngine;
using System.Collections;

public class WeaponGameObject : MonoBehaviour {
	
	public float fireRate;
	public float Damage;
	public LayerMask whatHits;

	public Transform BulletTrailPrefab;

	float timeToFire = 0;
	Transform firePoint;

	Player player;
	Inventory inventory;


	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild("FirePoint");
		if (firePoint == null)
			Debug.LogError ("FirePoint doesn't exist");

		inventory = GameObject.Find ("Inventory").GetComponent<Inventory>();
		player = GameObject.Find ("Character").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0) {
			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				Shoot ();
			}
		} else {
			if (Input.GetKey(KeyCode.Mouse0) && Time.time > timeToFire) {
				timeToFire = Time.time + 1 / fireRate;
				Shoot ();
			}
		}
			
	}
	//TODO some kind of visual action for changing the weapon like changing the current prefab.
	void ChangeWeapon(int weaponID){
		
	}

	bool CheckForAmmoInInventory(){
		
		if (player.currentWeapon != null) {
			if (inventory.CheckForItemInInventory (player.currentWeapon.AmmoID)) {
				return true;
			}
		}
		return false;
	}

	void Shoot(){
		if (CheckForAmmoInInventory() && player.currentWeapon != null) {

			ItemData data = inventory.GetItemDataFromID (player.currentWeapon.AmmoID);
			if (data != null) {
				data.Amount -= 1;
				if (data.Amount == 0)
					inventory.RemoveItem (data.slotID);
			}

			Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
			Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
			RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition - firePointPosition, 100, whatHits);

			Vector2 direction = mousePosition - firePointPosition;

			Effect ();

			Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);

			if (hit.collider != null) {
				Debug.DrawLine (firePointPosition, hit.point, Color.red);
				Debug.Log ("HIT:" + hit.collider.name + ", dealt:" + Damage + "damage");
			}
		} else
			Debug.Log ("No ammo in inventory!");
	}

	void Effect(){
		Instantiate (BulletTrailPrefab, firePoint.position, firePoint.rotation);
	}

}
