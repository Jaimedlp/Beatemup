using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_1 : MonoBehaviour {

	public Renderer rend;
	public Animator Enemigo1_Anim;
	SpriteRenderer SpriteEnemy;

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

	public int scoreValue = 100; //Los puntos son: 5 por cada golpe y 15 al matar a un enemigo.

	public bool haAtacado = false;
	public bool YaAtacado;
	public float TiempoVolverAtacar;
	public float VolverAtacar;
	public float velocidadVolverAtacar;

	public AudioClip collisionSound, deathSound, levelClearSong;
	private AudioSource audioS;
	private MusicController music;

	Enemigo_Attack scriptAtaqueEnemigo;
	public GameObject Trigger_AtaqueEnemigo;

	Player Ninja;
	public GameObject Player_Ninja;

    [SerializeField]
    float animacionMuerte = 1.6f;
	// Use this for initialization
	void Start () {
		SpriteEnemy = GetComponent <SpriteRenderer>();
		Enemigo1_Anim = GetComponent <Animator>();
		scriptAtaqueEnemigo = Trigger_AtaqueEnemigo.GetComponent<Enemigo_Attack>();
		Ninja = Player_Ninja.GetComponent<Player>();

		audioS = GetComponent<AudioSource> ();
		music = FindObjectOfType<MusicController> ();
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
		Enemigo1_Anim.SetBool ("Anda", true);
		transform.position = Vector3.MoveTowards(transform.position, PJ.position, speed * Time.deltaTime);
		//Rotacion
		if (PJ.position.x <= transform.position.x)
        {
			transform.localScale = new Vector3 (-1.558122f,1.558122f,1.558122f);
        }
        else {
			transform.localScale = new Vector3 (1.558122f,1.558122f,1.558122f);
        }
	}

	void Vida(){
		if (healthEnemy <= 0){
            Enemigo1_Anim.SetBool("Muerto", true);
			Dead = true;
			animacionMuerte -= 2 * Time.unscaledDeltaTime;
			//PlaySong (deathSound); //cuando un enemigo muera -> tiene que reproducir la voz de muerte.

            if (animacionMuerte <= 0) {
                Destroy (gameObject);
				UIManager.score += 50; //Cuando la animación muerte desaparezca -> puntos extra
				FindObjectOfType<Player>().health += 30; // Cuando se coja la cosa roja -> se recupera la vida
				//PlaySong(deathSound);
				SceneManager.LoadScene (1); // Para cargar la escena de victoria.
				//Invoke("LoadScene", 3f);

            }
        }

		/*if (healthEnemy <= 0)
		{
			PlaySong (deathSound);
		}*/

		if (Damaged == true){
			healthEnemy -= Ninja.damage;

			PlaySong (collisionSound);
			UIManager.score += scoreValue; //Cada hostia que se dé a un enemigo -> puntos

            Ninja.ActivateStopTime = true;
            //rend.material.color = Color.yellow;
            Damaged = false;
		}
	}

	void Ataca(){
		if (scriptAtaqueEnemigo.EstaPlayer == true){
		Debug.Log ("EnemigoPreparado");
				if (haAtacado == false){
					Enemigo1_Anim.SetTrigger ("Ataca");
					Ninja.animaciones.SetTrigger ("Atacado");
					Ninja.health -= DMG;

					PlaySong (collisionSound);

					Debug.Log ("Ataca");
					haAtacado = true;
					YaAtacado = false;
					//rend.material.color = Color.blue;
				}
			}
				if(haAtacado == true){
					YaAtacado = true;
					VolverAtacar -= velocidadVolverAtacar * Time.deltaTime;
					Debug.Log ("Recargando ataque");
					if (VolverAtacar <= 0){
						Ninja.animaciones.SetBool ("Atacado", false);
						//rend.material.color = Color.red;
						haAtacado = false;
						VolverAtacar = TiempoVolverAtacar;
						Debug.Log ("Ataque Cargado");
					}
				}
	}

	public void PlaySong(AudioClip clip)
	{
		audioS.clip = clip;
		audioS.Play ();
	}

	public void EnemySound(AudioClip clip)
	{
		if (healthEnemy <= 0){
			PlaySong (deathSound);
		}
	}

	/*void LoadScene()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}*/
}
