using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour {

	public Renderer rend;

	//public Transform posicion; 
	public Transform PJ;
	public float speed;

	public GameObject Jugador;
	public Vector3 targetPoint;
	Quaternion targetRotation;

	public int healthEnemy = 10;
	public bool Dead;
	public int DMG = 2;
	public bool Damaged;

	public bool haAtacado;
	public float TiempoVolverAtacar;
	public float VolverAtacar;
	public float velocidadVolverAtacar;

	Enemigo_Attack scriptAtaqueEnemigo;
	public GameObject Trigger_AtaqueEnemigo;
	// Use this for initialization
	void Start () {
		scriptAtaqueEnemigo = Trigger_AtaqueEnemigo.GetComponent<Enemigo_Attack>();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vida();

	}
	void FixedUpdate(){
		Ataca();
		Movimiento();
	}

	void Movimiento(){
		transform.position = Vector3.MoveTowards(transform.position, PJ.position, speed * Time.deltaTime);
		//Rotacion
		targetPoint = new Vector3 (Jugador.transform.position.x, transform.position.y, Jugador.transform.position.z) - transform.position;
		targetRotation = Quaternion.LookRotation (-targetPoint, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,2 * speed * Time.deltaTime);
	}

	void Vida(){
		if (healthEnemy <= 0){
			Dead = true;
			Destroy (gameObject);
		}
		if (Damaged == true){
			healthEnemy -= Player.damage;
			rend.material.color = Color.red;
			Damaged = false;
		}
	}

	void Ataca(){
		if (scriptAtaqueEnemigo.EstaPlayer == true){
		Debug.Log ("EnemigoPreparado");
			if (haAtacado == false){
				Player.health -= DMG;
				Debug.Log ("Ataca");
				haAtacado = true;
				rend.material.color = Color.blue;
			}else{
				rend.material.color = Color.yellow;
				VolverAtacar -= velocidadVolverAtacar * Time.deltaTime;
				Debug.Log ("Recargando ataque");
				if (VolverAtacar <= 0){
					haAtacado = false;
					VolverAtacar = TiempoVolverAtacar;
					Debug.Log ("Ataque Cargado");
				}
			}
		}
	}
}
