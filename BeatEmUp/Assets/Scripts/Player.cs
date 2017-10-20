using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	Enemy_1 Enemigo;
	public GameObject Enemigo_1;

	public float maxSpeed = 4;
	public float jumpForce = 400;
	public float minHeight, maxHeight;
	public int damage = 2;
	public float health = 100;
	[SerializeField]
	float health_Decrease;
	public float health_Increase;
	public Slider health_bar;

	public Animator animaciones;

	private float currentSpeed;
	private Rigidbody rb; //Lo vamos a usar para el movimiento del personaje
	//private Animator anim; //Falsearemos la transición de las animaciones
	private Transform groundCheck; 
	private bool onGround;
	private bool isDead = false; //Verificar si el player está muerto
	private bool facingRight = true;
	//private bool jump = false;
	public static bool Attack;

	public GameObject trigger_Ataque;
	Player_attack scriptAtaque;
	//public Renderer rend;

	public float countdownAttack = 1;
	public float countdownAttackVuelta = 1;
	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody> ();
		animaciones = GetComponent<Animator> ();
		groundCheck = gameObject.transform.Find ("GroundCheck");
		currentSpeed = maxSpeed;
		//rend = GetComponent<Renderer>();
		scriptAtaque = trigger_Ataque.GetComponent<Player_attack>();
		Enemigo = Enemigo_1.GetComponent<Enemy_1>();
	}
	
	// Update is called once per frame
	void Update () {
	    Weapon_system();
		Health_Bar ();

		onGround = Physics.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		//anim.SetBool ("OnGround", onGround);
		//anim.SetBool ("Dead", isDead);

		if (Input.GetButtonDown ("Fire1") || (countdownAttack < countdownAttackVuelta && countdownAttack > 0))
		{
			countdownAttack -= Time.deltaTime * 1;
			//rend.material.color = Color.red;
			Attack = true;
			animaciones.SetBool ("Ataca", true);
			//anim.SetTrigger ("Attack");
		}else if (countdownAttack <= 0){
			Attack = false;
			animaciones.SetBool ("Ataca", false);
			countdownAttack = countdownAttackVuelta;
		}
	}

	private void FixedUpdate()
	{
		if (!isDead && !Attack)
		{
			float h = Input.GetAxis ("Horizontal");
			float z = Input.GetAxis ("Vertical");

			if (!onGround)
				z = 0;

			rb.velocity = new Vector3 (h * currentSpeed, rb.velocity.y, z * currentSpeed);

			if (onGround)
				

			if (h > 0 && !facingRight)
			{
				Flip ();
			}
			else if (h < 0 && facingRight)
			{
				Flip ();
			}
			if (h > 0 || h < 0){
				animaciones.SetBool ("Moviendo", true);
			}else if(z > 0 ||z < 0){
				animaciones.SetBool ("Moviendo", true);
			}else{
				animaciones.SetBool ("Moviendo", false);
			}

			float minWidth = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 10)).x; //se obtendrá el valor mínimo de X en función de la posición de la cámara
			float maxWidth = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, 0, 10)).x;
			rb.position = new Vector3 (Mathf.Clamp (rb.position.x, minWidth + 1, maxWidth - 1),
				rb.position.y,
				Mathf.Clamp (rb.position.z, minHeight, maxHeight));
		}
		//rend.material.color = Color.black;
	}

	void Flip()
	{
		facingRight = !facingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void ZeroSpeed()
	{
		currentSpeed = 0;
	}

	void ResetSpeed()
	{
		currentSpeed = maxSpeed;
	}

	void Weapon_system () {
		if (Trigger_Arma.Arma_1 == true){
			//rend.material.color = Color.green;
			damage +=10;
			//health_Decrease += Trigger_Arma.DMG_ADD;
		}
	}

	void Health_Bar () {
		health_bar.value = health;
		health -= health_Decrease * Time.deltaTime;
			if (scriptAtaque.Muerto == true){
				animaciones.SetBool ("Atacado", false);
				Debug.Log ("RecuperaVida");
				health += health_Increase;
				scriptAtaque.Muerto = false;
			}
			if (health <= 0.0f){
				isDead = true;
				Debug.Log("Ha muerto");
				health = 0;
				Destroy(gameObject);
			}
			if (Enemigo.haAtacado == true) {
				animaciones.SetBool ("Atacado", true);
			}else if (Enemigo.YaAtacado == true){
				animaciones.SetBool ("Atacado", false);
			}
				
			
	}

}
