using UnityEngine;
using System.Collections;

public class MusicControl : MonoBehaviour {

	public AudioClip[] gameMusics;
	public AudioClip mainMenuMusic, gameStartSound;
	public float fadeTime;
	public float musicVolume;
	private bool restarting;
	private AudioSource audios;
	private int currentSong;
	// Use this for initialization
	void Start () {
		audios = transform.GetComponent<AudioSource>();
		currentSong = 0;
	}
	
	// Update is called once per frame
	void Update () {


	}
	public void startMusic(){

		audios.clip = mainMenuMusic;
		audios.Play ();
		FadeIn (fadeTime);
	}

	public IEnumerator fadeOutInMusic(float fade = 3f)
	{
		yield return StartCoroutine(DoFadeOut (fade));
		audios.Stop();
		audios.Play();
		yield return StartCoroutine(DoFadeIn (fade));
	}

	public void restartMusic ()
	{
		audios.Stop();
		audios.clip = mainMenuMusic;
		audios.Play();
	}

	public IEnumerator gameStart ()
	{
		audios.Stop ();
		audios.PlayOneShot (gameStartSound);
		yield return new WaitForSeconds(gameStartSound.length);
		audios.clip = gameMusics[currentSong];
		if (currentSong < 2) {
			currentSong ++;
		} else {
			currentSong = 0;
		}
		audios.Play();
	}

	public void FadeIn(float fade)
	{
		StartCoroutine(DoFadeIn(fade));
	}
	
	public void FadeOut(float fade)
	{
		StartCoroutine(DoFadeOut(fade));
	}
	
	private IEnumerator DoFadeIn(float fade)
	{
		float t = 0;
		while(audios.volume < musicVolume) {
			audios.volume = Mathf.Lerp(0.0f, musicVolume, t / fade);
			t+=Time.deltaTime;
			yield return null;
		}     

	}
	
	private IEnumerator DoFadeOut(float fade)
	{
		float t = 0;
		while( audios.volume > 0) {
			audios.volume = Mathf.Lerp(musicVolume, 0.0f, t / fade);
			t+=Time.deltaTime;
			yield return null;
		}     
	}
}
