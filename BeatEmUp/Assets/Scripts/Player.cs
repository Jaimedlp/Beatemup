using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	//Enemy_1 Enemigo;
	//public GameObject Enemigo_1;

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

	public AudioClip collisionSound, polloItem, trailSound, deathSound, fireSound;
	private AudioSource audioS;
	private MusicController music;

	public Transform firePoint;
	public GameObject fireBall;

	public UIManager Life;

	private float currentSpeed;
	private Rigidbody rb; //Lo vamos a usar para el movimiento del personaje
	//private Animator anim; //Falsearemos la transición de las animaciones
	private Transform groundCheck; 
	private bool onGround;
	private bool isDead = false; //Verificar si el player está muerto
	private bool facingRight = true;
	//private bool jump = false;
	public bool Attack;

	public GameObject trigger_Ataque;
	Player_attack scriptAtaque;
	//public Renderer rend;

	public float countdownAttack = 1;
    public float momentHit = 0.1f;
    public float countdownAttackVuelta = 1;

    public Transform CentroCaja;
    public float CajaX;
    public float CajaY;

    public bool ActivateStopTime = false;
	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody> ();
		animaciones = GetComponent<Animator> ();
		groundCheck = gameObject.transform.Find ("GroundCheck");
		currentSpeed = maxSpeed;
		//rend = GetComponent<Renderer>();
		scriptAtaque = trigger_Ataque.GetComponent<Player_attack>();
		//Enemigo = Enemigo_1.GetComponent<Enemy_1>();

		audioS = GetComponent<AudioSource> ();
		music = FindObjectOfType<MusicController> ();

		Life = GameObject.FindObjectOfType (typeof(UIManager)) as UIManager;
	}

    public bool triedHit = false;

    // Update is called once per frame
    void Update()
    {
        Weapon_system();
        Health_Bar();
        StopTime();

        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        //anim.SetBool ("OnGround", onGround);
        //anim.SetBool ("Dead", isDead);
        if (!isDead)
        {

            if (Input.GetButtonDown("Fire1") && !Attack)
            {
                Attack = true;
                animaciones.SetBool("Ataca", true);
                triedHit = false;
                // Hacer daño solo en 1 frame
                Collider2D[] ArrayEnemigos = Physics2D.OverlapBoxAll(CentroCaja.position, new Vector2(CajaX, CajaY),0);
                Debug.DrawLine(CentroCaja.position - new Vector3(CajaX, CajaY, 0) / 2, CentroCaja.position + new Vector3(CajaX, CajaY, 0) / 2, Color.red, 1);
                Debug.DrawLine(CentroCaja.position - new Vector3(CajaX, -CajaY, 0) / 2, CentroCaja.position + new Vector3(CajaX, -CajaY,0) / 2, Color.red, 1);
                Debug.Log(ArrayEnemigos.Length);
                foreach (Collider2D i in ArrayEnemigos)
                {
                    i.gameObject.GetComponent<Enemy_1>().Damaged = true;
                    ActivateStopTime = true;
                    if (i.gameObject.GetComponent<Enemy_1>().Dead) {
                        health += health_Increase;
                        Debug.Log("RecuperaVida");
                    }
                }
            }
            else if (countdownAttack <= countdownAttackVuelta && countdownAttack > 0  && Attack)
            {
                countdownAttack -= Time.deltaTime * 1;
                if (countdownAttack <= momentHit && !triedHit)
                {
                    triedHit = true;
                    hitStopCountdown = hitStopDuration;
                }
            }
            else if (countdownAttack <= 0 && Attack)
            {
                Attack = false;
                animaciones.SetBool("Ataca", false);
                countdownAttack = countdownAttackVuelta;
            }

			if (Input.GetKeyDown (KeyCode.Space) && !Attack)
			{
				Instantiate (fireBall, firePoint.position, firePoint.rotation);
				//PlaySong (fireSound); // LO PONGO -> NO ATACA, LO QUITO -> SÍ ATACA
			}

			/*if (Input.GetButtonDown ("Fire1") && !Attack)
			{
				PlaySong (trailSound);
			}*/
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
				animaciones.SetBool ("Moviendo", true); // movimiento
			}else if(z > 0 ||z < 0){
				animaciones.SetBool ("Moviendo", true); // movimiento
			}else{
				animaciones.SetBool ("Moviendo", false);
			}

			float minWidth = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 10)).x; //se obtendrá el valor mínimo de X en función de la posición de la cámara
			float maxWidth = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, 0, 10)).x;
			rb.position = new Vector3 (Mathf.Clamp (rb.position.x, minWidth + 1, maxWidth - 1),
				rb.position.y,
				Mathf.Clamp (rb.position.z, minHeight, maxHeight));
		}
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
			//PlaySong (trailSound);
			//health_Decrease += Trigger_Arma.DMG_ADD;
		}
	}

	public void Health_Bar () {
		health_bar.value = health;
		health -= health_Decrease * Time.deltaTime;
			if (health <= 0){
				//isDead = true;
				health += 100;
			FindObjectOfType<UIManager> ().UpdateLives();
			//Life.UpdateLives();
			/*if (facingRight) {
				rb.AddForce (new Vector3 (-3, 5, 0), ForceMode.Impulse);
			} else {
				rb.AddForce (new Vector3 (3, 5, 0), ForceMode.Impulse);
			}*/


				//Debug.Log("HemosMuerto");
				//health = 0;
				//Destroy(gameObject);
			}
		if (health > 100) {
			health = 100;
		}
	}

    public float hitStopDuration = .1f;
    float hitStopCountdown;
    public void StopTime() {
        if (ActivateStopTime) {
            if (hitStopCountdown > 0)
            {
                Time.timeScale = 0;
                hitStopCountdown -= 1 * Time.unscaledDeltaTime;
            }
            else
            {
                Time.timeScale = 1.0f;
                ActivateStopTime = false;
            }
        }
        
    }

	public void PlaySong(AudioClip clip) //sonidos y música
	{
		audioS.clip = clip;
		audioS.Play ();
	}

	private void OnTriggerStay(Collider other) // coger el pollo con el clic derecho
	{
		if (other.CompareTag ("Pollo")) //el tag del pollo
		{
			if (Input.GetButtonDown ("Fire2"))
			{
				Destroy (other.gameObject);
				health += 50; // Vida que se recupera al coger el pollo
				PlaySong(polloItem);
				Debug.Log("Cogido");
			}
		}
	}

	void ThrowFireBall()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Instantiate(fireBall, transform.position, transform.rotation);
		}
	}

}
