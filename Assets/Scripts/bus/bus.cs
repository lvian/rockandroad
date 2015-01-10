using UnityEngine;
using System.Collections;

public class bus : MonoBehaviour {

	protected Vector2 speed;
	protected GameControl gc;
	// Use this for initialization

	private Animator anim;

	void Start () {
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		gc.stateChanged += gameStateChanged;

		speed = new Vector2(gc.GameSpeed, gc.GameSpeed);
		anim = gameObject.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if(gc.currentGameState == GameControl.GameState.Play || gc.currentGameState == GameControl.GameState.MainMenu )
		{
			anim.speed = 1;
			Vector3 movement = new Vector3(
				speed.x * 2 ,
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
