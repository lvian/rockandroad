﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Playermovement : MonoBehaviour {

	static int UP_ONE_LANE = 1;
	static int DOWN_ONE_LANE = -1;
	
	//TODO Player states
	public int points = 0;
	public int health;
	public float invulTime;
	public GameObject startingPlace;
	private GameObject scoreValue;
	public int firstLane;
	public GameObject[] lanes;
	public float speed;
	public GameObject[] healthDisks;
	//private int previousLane;
	private int currentLane;
	private bool isMoving;
	private bool isBeingHit;
	private GameControl gameControl;
	private Animator anim;
	private BoxCollider2D movementDelimiter;
	
	
			
	// Use this for initialization
	void Start () {
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
		movementDelimiter = GameObject.Find("MovementDelimiter").GetComponent<BoxCollider2D>();

		PlayerPrefs.SetInt("defaultHealth",health );
		anim = GetComponent<Animator>();
	
		//starting position
		firstLane--;
		Vector3 pos = new Vector3(lanes[firstLane].transform.position.x,lanes[firstLane].transform.position.y, gameObject.gameObject.transform.position.z);
		gameObject.transform.position = pos;

		anim = GetComponent<Animator>();
		
		currentLane = firstLane;
		isMoving = false;
		isBeingHit = false;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(gameControl.currentGameState == GameControl.GameState.Play)
		{
			anim.speed = 1;
			checkInput();

			moveTo();
		} else if (gameControl.currentGameState == GameControl.GameState.Pause)
		{
			anim.speed = 0;
		}
	}

	
	private void moveTo()
	{
		if(isMoving)
		{
			transform.position = Vector3.MoveTowards(gameObject.transform.position,lanes[currentLane].transform.position, 0.02f * gameControl.GameSpeed);
		}
	
	}
	
	
	
	private void checkInput()
	{
		float x = gameObject.transform.position.x ;
		float newx = x;
		float y = gameObject.transform.position.y;
		float newy = y;

		if(Input.GetButton("W"))
		{
			newy += 0.05f;
		}
		
		if(Input.GetButton("S"))
		{
			newy -= 0.05f;
		}
		if(Input.GetButton("D"))
		{
			newx += 0.05f;
		}
		
		if(Input.GetButton("A"))
		{
			newx -= 0.1f;
		}

		Vector2 pos = new Vector2 (newx,y);
		//Test x and y separetly to allow movement of a single axis when on the edge
		if(movementDelimiter.bounds.Contains(pos)){
			//transform.Translate(0, 0, z);
			x =  newx;
		}
		pos = new Vector2 (x ,newy);
		if(movementDelimiter.bounds.Contains(pos)){
			//transform.Translate(0, 0, z);
			y = newy;
		}

		transform.position = new Vector2(x,y);

		
	}

	private void move(int nextLane)
	{
		if((nextLane == UP_ONE_LANE && currentLane < 3) || ( nextLane == DOWN_ONE_LANE && currentLane > 0))
		{
			//previousLane = currentLane;
			currentLane  += nextLane;
			isMoving = true;
		}
						
	}
	
	
	void OnTriggerEnter2D(Collider2D other) {
		//Player hit an obstacle
		if(other.gameObject.tag == "Obstacles")
		{
			if(!isBeingHit)
			{
				StartCoroutine("hitByObstacle");
			}
		}
		
		//Player got an health increase
		if(other.gameObject.tag == "healthBoost")
		{
			//We could have disabled the collider and avoided this whole 'check for hit thing'
			other.collider2D.enabled = false;
			Destroy(other.gameObject);
			if(health <= 3)
			{
				health ++;
				this.adjustHealthIcons();
			}
			givePoints(100);
			//coroutine not needed atm
			//StartCoroutine("gotAHealthBoost");
		}
	}
	
	IEnumerator hitByObstacle() {
		if(!isBeingHit)
		{	
			isBeingHit = true;
			health --;
			this.adjustHealthIcons();
			//play damage animation here
			if(health == 0)
			{
				//change timer to something like 'death animation time lenght'
				yield return new WaitForSeconds(1f);
				StartCoroutine(hitByObstacle());
			} else
			{
				yield return new WaitForSeconds(invulTime);
				StartCoroutine(hitByObstacle());
			}
		} else
		{
			isBeingHit = false;
			//Test is being made here, so we can play an animation before showing de defeat screen
			if(health == 0)
			{
				gameControl.Defeat();
			}
		}
	}

	IEnumerator gotAHealthBoost() {

		health ++;
		this.adjustHealthIcons();
		//play healing animation here
		yield return new WaitForSeconds(invulTime);
		StartCoroutine(hitByObstacle());
	}

	public int Health {
		get {
			return health;
		}
		set {
			health = value;
		}
	}

	public void adjustHealthIcons() {
		switch(Health){
			case 0:
				NGUITools.SetActive(healthDisks[0],false);
				NGUITools.SetActive(healthDisks[1],false);
				NGUITools.SetActive(healthDisks[2],false);
				NGUITools.SetActive(healthDisks[3],false);
				break;
			case 1:
				NGUITools.SetActive(healthDisks[0],true);
				NGUITools.SetActive(healthDisks[1],false);
				NGUITools.SetActive(healthDisks[2],false);
				NGUITools.SetActive(healthDisks[3],false);
				break;
			case 2:
				NGUITools.SetActive(healthDisks[0],true);
				NGUITools.SetActive(healthDisks[1],true);
				NGUITools.SetActive(healthDisks[2],false);
				NGUITools.SetActive(healthDisks[3],false);
				break;
			case 3:
				NGUITools.SetActive(healthDisks[0],true);
				NGUITools.SetActive(healthDisks[1],true);
				NGUITools.SetActive(healthDisks[2],true);
				NGUITools.SetActive(healthDisks[3],false);
				break;
			case 4:
				NGUITools.SetActive(healthDisks[0],true);
				NGUITools.SetActive(healthDisks[1],true);
				NGUITools.SetActive(healthDisks[2],true);
				NGUITools.SetActive(healthDisks[3],true);
				break;


		}
	}
	
	public void givePoints(int pts) {
		if(health > 0)
		{
			points += pts;
			UILabel score = GameObject.Find("score_value").GetComponent<UILabel>();
			score.text = points.ToString();
			}
	}
	public void FixedUpdate() {
		
		anim.SetInteger("Speed", gameControl.gameSpeed);
		anim.SetInteger("GameState", (int)gameControl.currentGameState);
	}
	
}
