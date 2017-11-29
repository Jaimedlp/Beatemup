using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static int score; //Puntuación

	public Text livesText; //Las vidas extras

	Text text;

	public Player Vida;

	public void Start ()
	{
		Vida = GameObject.FindObjectOfType (typeof(Player)) as Player;
		Vida.Health_Bar ();
		Vida.health_bar.value = Vida.health;

		//UpdateLives ();

		text = GetComponent<Text> ();
		score = 0;
	}
	
	// Update is called once per frame
	public void Update () {
		text.text = "Score " + score;
	}

	public void UpdateLives()
	{
		//livesText.text.ToString = -1;
		//livesText.text = "x " + livesText; //FindObjectOfType<GameManager> ().lives.ToString ();
	}

	/*public void UpdateHealth(int amount)
	{
		Vida.health_bar.value = amount;
	}*/

}
