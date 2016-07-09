using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

	public float speed;
	public LayerMask blockingLayer;

	public float horizontalCheck;
	public float verticalCheck;

	public bool isFacingRight;

	void FixedUpdate () {
		//up
		if (Input.GetKey (KeyCode.W)) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.up, verticalCheck*speed * Time.deltaTime, blockingLayer);
			if (hit.collider == null) {
				transform.position += Vector3.up * speed * Time.deltaTime;
			}
		}
		//right
		if (Input.GetKey (KeyCode.D)) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.right, horizontalCheck*speed * Time.deltaTime, blockingLayer);
			if (hit.collider == null) {
				transform.position += Vector3.right * speed * Time.deltaTime;
			}
		}
		//left
		if (Input.GetKey (KeyCode.A)) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.left, horizontalCheck*speed * Time.deltaTime, blockingLayer);
			if (hit.collider == null) {
				transform.position += Vector3.left * speed * Time.deltaTime;
			}
		}
		//down
		if (Input.GetKey (KeyCode.S)) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, verticalCheck*speed * Time.deltaTime, blockingLayer);
			if (hit.collider == null) {
				transform.position += Vector3.down * speed * Time.deltaTime;
			}
		}
	}
}
