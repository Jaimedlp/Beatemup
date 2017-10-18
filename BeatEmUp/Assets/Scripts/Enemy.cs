
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float maxSpeed;
	public float minHeight, maxHeight;
	public float damageTime = 0.5f; //Durante este tiempo el enemigo no se moverá y estará quieto hasta que pueda atacar -> cuando el jugador entre en su rango de ataque
	public int enemyMaxHealth;
	public float attackRate = 1;

	private int enemyCurrentHealth;
	private float currentSpeed;
	private Rigidbody rb;
	private Animator anim;
	private Transform groundCheck;
	private bool onGround;
	private bool facingRight = false;
	private Transform target;
	private bool isDead = false;
	private float zForce;
	private float walkTimer; //Para que el enemigo ande, cada X segundos, N pasos
	private bool damaged = false;
	private float damageTimer;
	private float nextAttack;


	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		groundCheck = transform.Find ("GroundCheck");
		target = FindObjectOfType<Player>().transform;
		enemyCurrentHealth = enemyMaxHealth;

	}
	
	// Update is called once per frame
	void Update () {

		onGround = Physics.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		anim.SetBool ("Grounded", onGround);
		anim.SetBool ("Dead", isDead);

		// Que el enemigo rote 180º

		facingRight = (target.position.x < transform.position.x) ? false : true;

		if (facingRight)
		{
			transform.eulerAngles = new Vector3 (0, 180, 0);
		}
		else
		{
			transform.eulerAngles = new Vector3 (0, 0, 0);
		}

		if (damaged && !isDead)
		{
			damageTimer += Time.deltaTime;
			if (damageTime >= damageTime)
			{
				damaged = false;
				damageTimer = 0;
			}
		}

		walkTimer += Time.deltaTime;

	}

	private void FixedUpdate()
	{
		if (!isDead)
		{
			Vector3 targetDistance = target.position - transform.position;
			float hForce = targetDistance.x / Mathf.Abs (targetDistance.x);

			if (walkTimer >= Random.Range (1f, 2f))
			{
				zForce = Random.Range (-1, 2);
				walkTimer = 0;
			}

			if (Mathf.Abs (targetDistance.x) < 1.5f)
			{
				hForce = 0;
			}

			if(!damaged)

			rb.velocity = new Vector3 (hForce * currentSpeed, 0, zForce * currentSpeed);

			anim.SetFloat ("Speed", Mathf.Abs (currentSpeed));

			if (Mathf.Abs (targetDistance.x) < 1.5f && Mathf.Abs (targetDistance.z) < 1.5f && Time.time > nextAttack)
			{
				anim.SetTrigger ("Attack");
				currentSpeed = 0;
				nextAttack = Time.time + attackRate;
			}
		}

		rb.position = new Vector3(rb.position.x, rb.position.y, Mathf.Clamp(rb.position.z, minHeight, maxHeight));
	}

	public void TookDamage(int damage)
	{
		if (!isDead)
		{
			damaged = true;
			enemyCurrentHealth -= damage;
			anim.SetTrigger ("HitDamage");
			if (enemyCurrentHealth <= 0)
			{
				isDead = true;
			}
		}
	}

	public void DisableEnemy()
	{
		gameObject.SetActive (false);
	}

	void ResetSpeed()
	{
		currentSpeed = maxSpeed;
	}
}
