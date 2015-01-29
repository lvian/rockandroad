using UnityEngine;
using System.Collections;

public class bus : MonoBehaviour {

	protected Vector2 speed;
	protected GameControl gc;
	protected float speedCompensate;
	// Use this for initialization

	private Animator anim;

	void Start () {
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		gc.stateChanged += gameStateChanged;

		speed = new Vector2(5f, 5f);
		anim = gameObject.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if( gc.currentGameState == GameControl.GameState.MainMenu)
		{
			speedCompensate = speed.x * 2;
		}
		if( gc.currentGameState == GameControl.GameState.Play)
		{
			speedCompensate = speed.x * 1.25f;
		}
		if(gc.currentGameState == GameControl.GameState.Play || gc.currentGameState == GameControl.GameState.MainMenu )
		{
			anim.speed = 1;

			Vector3 movement = new Vector3(
				speedCompensate ,
				0 ,
				0);
			
			movement *= Time.deltaTime;
			transform.Translate(movement);
			
			
			if(transform.position.x >  10)
			{
				Destroy(gameObject);
			}	
		}else if (gc.currentGameState == GameControl.GameState.Pause)
		{
			anim.speed = 0;
		}



	}

	void gameStateChanged(float gs)
	{	
		speed = new Vector2(gs, 0);
	}
}
