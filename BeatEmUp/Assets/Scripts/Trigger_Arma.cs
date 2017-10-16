using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Arma : MonoBehaviour {

 public static bool Arma_1 = false;
 public static float DMG_ADD = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Arma_1 == true) {
			Destroy (gameObject);
		}
	}
	void OnTriggerStay (Collider PJ){
		if (Input.GetKeyDown (KeyCode.E)) {
			print ("Arma recogida");
			Arma_1 = true;
		}
	}
}
