﻿using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

	public float speed;
	public LayerMask blockingLayer;

	public float horizontalCheck;
	public float verticalCheck;

	public bool isFacingLeft = false;

	SpriteRenderer[] renderers;

	void Awake(){
		renderers = GetComponentsInChildren<SpriteRenderer>();
	}

	void FixedUpdate () {
		//up
		if (Input.GetKey (KeyCode.W)) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.up, verticalCheck * speed * Time.deltaTime, blockingLayer);
			if (hit.collider == null) {
				transform.position += Vector3.up * speed * Time.deltaTime;
			}
		}
		//right
		if (Input.GetKey (KeyCode.D)) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.right, horizontalCheck * speed * Time.deltaTime, blockingLayer);
			if (hit.collider == null) {
				transform.position += Vector3.right * speed * Time.deltaTime;
			}
		}
		//left
		if (Input.GetKey (KeyCode.A)) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.left, horizontalCheck * speed * Time.deltaTime, blockingLayer);
			if (hit.collider == null) {
				transform.position += Vector3.left * speed * Time.deltaTime;
			}
		}
		//down
		if (Input.GetKey (KeyCode.S)) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, verticalCheck * speed * Time.deltaTime, blockingLayer);
			if (hit.collider == null) {
				transform.position += Vector3.down * speed * Time.deltaTime;
			}
		}
	}

	Vector3 mousePos;
	public GameObject gun;
	public Transform firePoint;
	Vector3 objectPos;
	float angle;

	void Update(){
		
		mousePos = Input.mousePosition;

		objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;

		if ((angle >= 90 || angle <= -90)) {
			foreach (SpriteRenderer sr in renderers)
				sr.flipX = true;
		
			isFacingLeft = true;

			Vector3 scale = gun.transform.localScale;
			scale.x = -1;
			scale.y = -1;
			gun.transform.localScale = scale;

		} else {
			foreach (SpriteRenderer sr in renderers)
				sr.flipX = false;

			isFacingLeft = false;
			Vector3 scale = gun.transform.localScale;
			scale.x = 1;
			scale.y = 1;
			gun.transform.localScale = scale;
		}

		angle = Mathf.Atan2 (mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		gun.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
		//Debug.Log("angle: " + angle );
	}
}
