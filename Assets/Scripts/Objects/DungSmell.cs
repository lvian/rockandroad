using UnityEngine;
using System.Collections;

public class DungSmell : MonoBehaviour {

	protected GameControl gc;
	private TweenAlpha tweenAlpha;
	private TweenScale tweenScale;
	// Use this for initialization
	void Start () {
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();

		tweenAlpha = gameObject.GetComponentInChildren<TweenAlpha>();
		tweenScale = gameObject.GetComponentInChildren<TweenScale>();
	}
	
	// Update is called once per frame
	void Update ()
	{

		if(gc.currentGameState == GameControl.GameState.Play)
		{
			Vector3 movement = new Vector3(
				0,
				0.25f ,
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

	public void DestroySmell()
	{
		Destroy (gameObject);
	}

}
