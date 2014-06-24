using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Playermovement : MonoBehaviour {

	static int UP_ONE_LANE = 1;
	static int DOWN_ONE_LANE = -1;
	
	//TODO Player states
	public int health;
	public float invulTime;
	public GameObject startingPlace;
	public int firstLane;
	public GameObject[] lanes;
	public float speed;
	//private int previousLane;
	private int currentLane;
	private bool isMoving;
	private bool isBeingHit;
	private GameControl gameControl;
	private Animator anim;
	
	
	
			
	// Use this for initialization
	void Start () {
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
	
		// Get the Animator component from your gameObject
		anim = GetComponent<Animator>();
	
		//starting position
		firstLane--;
		Vector3 pos = new Vector3(lanes[firstLane].transform.position.x,lanes[firstLane].transform.position.y, gameObject.gameObject.transform.position.z);
		gameObject.transform.position = pos;
		
		
		currentLane = firstLane;
		isMoving = false;
		isBeingHit = false;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log(gameControl.currentGameState);
		if(gameControl.currentGameState == GameControl.GameState.Play)
		{
			checkInput();

			moveTo();
		}
	}

	
	private void moveTo()
	{
		if(isMoving)
		{
			transform.position = Vector3.MoveTowards(gameObject.transform.position,lanes[currentLane].transform.position, 0.03f * gameControl.GameSpeed);
		}
	
	}
	
	
	
	private void checkInput()
	{
		if(Input.GetKeyDown(KeyCode.W))
		{
			move(UP_ONE_LANE);
		}
		
		if(Input.GetKeyDown(KeyCode.S))
		{
			move(DOWN_ONE_LANE);
		}
		
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
			StartCoroutine("gotAHealthBoost");
		}
	}
	
	IEnumerator hitByObstacle() {
		if(!isBeingHit)
		{	
			isBeingHit = true;
			health --;
			//play damage animation here
			yield return new WaitForSeconds(invulTime);
			StartCoroutine(hitByObstacle());
		} else
		{
			isBeingHit = false;
			//Test is being made here, so we can play an animation before showing de defeat screen
			if(health == 0)
			{
				gameControl.currentGameState = GameControl.GameState.Defeat;
			}
		}
	}

	public int Health {
		get {
			return health;
		}
		set {
			health = value;
		}
	}
	
	
	public void FixedUpdate() {
		
		anim.SetInteger("Speed", gameControl.gameSpeed);
		anim.SetInteger("GameState", (int)gameControl.currentGameState);
	}
	
}
