using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	//Poner un AudioSource en el jefe -> quitar Play On Awake

	public AudioClip levelSong, bossSong, levelClearSong;

	private AudioSource audioS;

	// Use this for initialization
	void Start () {

		audioS = GetComponent<AudioSource> ();
		PlaySong (levelSong);

	}



	public void PlaySong(AudioClip clip)
	{
		audioS.clip = clip;
		audioS.Play ();
	}
}
