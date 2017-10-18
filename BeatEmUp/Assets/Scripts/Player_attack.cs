using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_attack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay (Collider Enemigo) {
		if (Player.Attack) {
			Enemy_1.healthEnemy -= Player.damage;
			Enemy_1.Damaged = true;
		}
	}
}
