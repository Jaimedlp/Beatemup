using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Attack : MonoBehaviour {

	public static bool EstaPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay (Collider col){
		if (col.tag == "Player"){
			EstaPlayer = true;
			Debug.Log ("HaEntrado");
		}else{
			EstaPlayer = false;
		}
	}
}
