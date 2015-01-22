using UnityEngine;
using System.Collections;

public class MusicControl : MonoBehaviour {

	public AudioClip music1,music2,music3, gameStartSound;
	public float fadeTime;
	private bool restarting;
	private AudioSource audios;
	// Use this for initialization
	void Start () {
		audios = transform.GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {


	}
	public void startMusic(){

		audios.clip = music1;
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
		audios.Play();
	}

	public IEnumerator gameStart ()
	{
		audios.Stop ();
		audios.PlayOneShot (gameStartSound);
		yield return new WaitForSeconds(gameStartSound.length);
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
		while(audios.volume < 0.5) {
			audios.volume = Mathf.Lerp(0.0f, 0.5f, t / fade);
			t+=Time.deltaTime;
			yield return null;
		}     

	}
	
	private IEnumerator DoFadeOut(float fade)
	{
		float t = 0;
		while( audios.volume > 0) {
			audios.volume = Mathf.Lerp(0.5f, 0.0f, t / fade);
			t+=Time.deltaTime;
			yield return null;
		}     
	}
}
