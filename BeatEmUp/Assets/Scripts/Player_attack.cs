using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
public class Player_attack : MonoBehaviour {

	GameObject Enemy;
	public bool Muerto;

	public GameObject Jugador;
	Player scriptJugador;

    public bool EnemyDamaged;
	// Use this for initialization
	void Start () {
		scriptJugador = Jugador.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D (Collider2D Enemigo) { 
		Enemy = Enemigo.gameObject;
		Enemy_1 script = Enemy.GetComponent<Enemy_1>(); 
		if (script != null){
			if (script.healthEnemy <= 0) {
				scriptJugador.animaciones.SetBool ("Atacado", false);
				Muerto = true;
			}
		}
	}
}
