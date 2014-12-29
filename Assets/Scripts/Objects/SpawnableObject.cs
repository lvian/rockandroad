﻿using UnityEngine;
using System.Collections;

public abstract class SpawnableObject : MonoBehaviour {

	protected Vector2 speed;
	public Vector2 direction = new Vector2(-1, 0);
	protected GameControl gc;
	protected Playermovement pm;
	protected bool pointsAwarded = false;
	public float spawnChance;
	public GameObject[] lanes;
	public int spawnLane;
	
	// Use this for initialization
	public virtual void Start () {

		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		gc.stateChanged += gameStateChanged;
		
		pm = GameObject.Find("Player").GetComponent<Playermovement>();
		//Makes sure the obstacle starts with current game speed
		speed = new Vector2(gc.GameSpeed, gc.GameSpeed);
		spawnChance = .5f;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected virtual void movement(int pts, float sp)
	{
		// Movement
		// speed = 1 is stationary(same speed as background)
		Vector3 movement = new Vector3(
			speed.x * direction.x * sp,
			speed.y * direction.y * sp,
			0);
		
		movement *= Time.deltaTime;
		transform.Translate(movement);


		if(transform.position.x < -5 && pointsAwarded == false && gc.currentGameState == GameControl.GameState.Play )
		{
			pm.givePoints(pts);
			pointsAwarded = true;
		}

		if(transform.position.x < -8)
		{
			Destroy(gameObject);
		}
	
	}

	
	void gameStateChanged(float gs)
	{
		speed = new Vector2(gs, 0);
	}
}
