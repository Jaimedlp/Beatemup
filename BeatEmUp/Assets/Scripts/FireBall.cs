using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

	public int direction = 1;

	//public float speed;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody> ();

	}

	/*void Update () {

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed, rigidbody2D.velocity.y);

	}*/

	void FixedUpdate () {

		rb.velocity = new Vector3 (6 * direction, 0, 2 * direction);

	}

	
}
