using UnityEngine;
using System.Collections;

public class BusSmoke : MonoBehaviour {

	protected GameControl gc;
	private Animator anim;
	private TweenAlpha tweenAlpha;
	private TweenScale tweenScale;
	private TweenPosition tweenPosition;
	protected float speedCompensate;
	// Use this for initialization
	void Start () {
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();

		tweenAlpha = gameObject.GetComponentInChildren<TweenAlpha>();
		tweenScale = gameObject.GetComponentInChildren<TweenScale>();
		tweenPosition = gameObject.GetComponentInChildren<TweenPosition>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( gc.currentGameState == GameControl.GameState.MainMenu)
		{
			speedCompensate = 0;
		}
		if( gc.currentGameState == GameControl.GameState.Play)
		{
			speedCompensate = 5;
		}
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			Vector3 movement = new Vector3(
				- speedCompensate,
				0.5f ,
				0);
			
			movement *= Time.deltaTime;

			transform.Translate(movement);
			tweenAlpha.enabled = true;
			tweenScale.enabled = true;
				
		}else if (gc.currentGameState == GameControl.GameState.Pause)
		{
			tweenAlpha.enabled = false;
			tweenScale.enabled = false;
		}
			
	}

	public void DestroySmoke()
	{
		Destroy (gameObject);
	}

}
