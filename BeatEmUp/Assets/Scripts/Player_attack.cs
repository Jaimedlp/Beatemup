using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
public class Player_attack : MonoBehaviour {

	GameObject Enemy;
	public bool Muerto;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay (Collider Enemigo) { 
		Enemy = Enemigo.gameObject;
		Enemy_1 script = Enemy.GetComponent<Enemy_1>(); 

		/*if (script != null){
			Debug.Log ("Bien");
		}else{
			Debug.Log ("Mal");
		}*/

		if (script != null && Player.Attack) {
			script.Damaged = true;
		}
		if (script != null && script.healthEnemy <= 0) {
			Debug.Log("DetectadaMuerte");
			Muerto = true;
		}
	}
}
