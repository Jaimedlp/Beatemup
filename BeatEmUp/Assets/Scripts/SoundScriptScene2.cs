using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEngine.UI.Text;

public class SoundScriptScene2 : MonoBehaviour {

	public AudioClip winSound;

	private AudioSource audioS;

	public UIManager finalScore;
	public Text text;

	// Use this for initialization
	void Start () {

		audioS = GetComponent<AudioSource> ();
		PlaySong (winSound);

		finalScore = GameObject.FindObjectOfType (typeof(UIManager)) as UIManager;
		text = GetComponent<Text> ();
		//finalScore.score = PlayerPrefs.GetInt ("Score");

	}
	
	// Update is called once per frame
	void Update () {
		//text = finalScore.score.ToString;
		//Text.text = "Score " + finalScore.score;
		//PlayerPrefs.SetInt("Score", finalScore.score);
	}

	//public void FinalScore()
	//{
		//Text.finalScoreText = "Final Score " + finalScore;
	//}

	public void PlaySong(AudioClip clip)
	{
		audioS.clip = clip;
		audioS.Play ();
	}
}