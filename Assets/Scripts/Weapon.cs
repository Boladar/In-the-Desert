using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatHits;

	public Transform BulletTrailPrefab;

	float timeToFire = 0;
	float timeToEffect = 0;
	Transform firePoint;


	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild("FirePoint");
		if (firePoint == null)
			Debug.LogError ("FirePoint doesn't exist");
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

	void Shoot(){
		Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition - firePointPosition, 100, whatHits );

		Vector2 direction = mousePosition - firePointPosition;



		Effect();

		Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);

		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);
			Debug.Log ("HIT:" + hit.collider.name + ", dealt:" + Damage + "damage");
		}
	}

	void Effect(){
		Instantiate (BulletTrailPrefab, firePoint.position , firePoint.rotation);
	}

}
