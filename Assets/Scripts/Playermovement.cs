using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Playermovement : MonoBehaviour {

	static int UP_ONE_LANE = 1;
	static int DOWN_ONE_LANE = -1;
	
	//TODO Player states
	public int points = 0;
	public int multiplier = 1;
	//public int health;
	public float energy;
	public float invulTime;
	private bool isInvul;
	public GameObject startingPlace;
	private GameObject scoreValue;
	public int firstLane;
	public GameObject[] lanes;
	public float speed;
	public GameObject healthbar;
	//public GameObject[] healthDisks;
	//private int previousLane;
	private float energyTimer;
	private int currentLane;
	private bool isMoving;
	private bool isBeingHit;
	private GameControl gameControl;
	private Animator anim;
	private BoxCollider2D movementDelimiter;
	private UIProgressBar pg;

	
	
			
	// Use this for initialization
	void Start () {
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();

		PlayerPrefs.SetFloat("defaultEnergy",energy );
		anim = GetComponent<Animator>();
	
		//starting position
		firstLane--;
		Vector3 pos = new Vector3(lanes[firstLane].transform.position.x,lanes[firstLane].transform.position.y, gameObject.gameObject.transform.position.z);
		gameObject.transform.position = pos;

		energyTimer = Time.time;
		anim = GetComponent<Animator>();
		pg  = healthbar.GetComponent<UIProgressBar>();
		currentLane = firstLane;
		isMoving = false;
		isBeingHit = false;
		isInvul = false;

		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(gameControl.currentGameState == GameControl.GameState.Play)
		{
			anim.speed = 1;
			checkInput();

			moveTo();
			StartCoroutine(energyDecay());
		} else if (gameControl.currentGameState == GameControl.GameState.Pause)
		{
			anim.speed = 0;
		}
		else if (gameControl.currentGameState == GameControl.GameState.GameRestart)
		{
			gameControl.gameSpeed = 2;
			anim.speed = 1;
			moveTo();
			if(Vector3.Distance(lanes[currentLane].transform.position, transform.position) == 0)
			{
				isMoving = false;
				gameControl.currentGameState = GameControl.GameState.MainMenu;
				gameControl.gameSpeed = 5;
				Debug.Log (gameControl.currentGameState);
			}
		}
		if(isInvul){
			animateAlpha();
		}
	}

	
	private void moveTo()
	{
		if(isMoving)
		{
			transform.position = Vector3.MoveTowards(gameObject.transform.position,lanes[currentLane].transform.position, 0.02f * gameControl.GameSpeed);
		}
	
	}
	
	
	
	private void checkInput(){

		if(Input.GetKeyDown(KeyCode.W))
		{
			move(UP_ONE_LANE);
		}
		
		if(Input.GetKeyDown(KeyCode.S))
		{
			move (DOWN_ONE_LANE);
		}

		
	}

	void animateAlpha (){
		float duration = .1f;
		float lerp = Mathf.PingPong (Time.time, duration) / duration;

		float alpha = Mathf.Lerp(0.0f, 1.0f, lerp) ;
		Color c = new Color(
			renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, alpha);
		renderer.material.color = c;
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

	public void resetPLayerPosition()
	{
		currentLane  = firstLane;
		isMoving = true;
		
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
		
		//Player got an energy increase
		if(other.gameObject.tag == "healthBoost")
		{
			//We could have disabled the collider and avoided this whole 'check for hit thing'
			HealthBoost hb = other.gameObject.GetComponent<HealthBoost>();
			other.collider2D.enabled = false;
			Destroy(other.gameObject);
			energy += hb.HealthAmount;
			if(energy > 100)
			{
				energy = 100;
				this.adjustEnergy();
			}
			givePoints(hb.Points);
			//coroutine not needed atm
			//StartCoroutine("gotAHealthBoost");
		}
		if(other.gameObject.tag == "multiplier")
		{
			//We could have disabled the collider and avoided this whole 'check for hit thing'
			Multiplier mp = other.gameObject.GetComponent<Multiplier>();
			other.collider2D.enabled = false;
			Destroy(other.gameObject);
			multiplier ++;
			givePoints(mp.Points);
			adjustMultiplier();
			//coroutine not needed atm
			//StartCoroutine("gotAHealthBoost");
		}
	}
	
	IEnumerator hitByObstacle() {
		if(gameControl.currentGameState != GameControl.GameState.Play) // ??? Reacess!
			yield break;
		if(!isBeingHit)
		{	
			multiplier = 1;
			adjustMultiplier();
			isBeingHit = true;
			energy -= 10;
			this.adjustEnergy();
			//play damage animation here
			if(energy <= 0)
			{
				//change timer to something like 'death animation time lenght'
				yield return new WaitForSeconds(1f);
				StartCoroutine(hitByObstacle());
			}
			else
			{
				isInvul = true;
				yield return new WaitForSeconds(invulTime);
				isInvul = false;
				Color c = new Color(
					renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
				renderer.material.color = c;
				StartCoroutine(hitByObstacle());
			}
		}
		else
		{
			isBeingHit = false;
			//Test is being made here, so we can play an animation before showing de defeat screen
			if(energy <= 0)
			{
				gameControl.Defeat();
			}
		}
	}


	public float Energy {
		get {
			return energy;
		}
		set {
			energy = value;
		}
	}

	public void adjustEnergy() {
		pg.value = this.energy / 100;
		//Debug.Log (pg.value);

	}

	public void adjustMultiplier() {
		UILabel mp = GameObject.Find("scoremultiplier_value").GetComponent<UILabel>();
		mp.text = "x " +multiplier.ToString();
		
	}
	
	public void givePoints(int pts) {
		if(energy > 0)
		{
			points += pts * multiplier;
			UILabel score = GameObject.Find("score_value").GetComponent<UILabel>();
			score.text = points.ToString();
			}
	}



	IEnumerator energyDecay ()
	{
		energyTimer += Time.deltaTime;
		if(energyTimer >= 1)
		{
			this.energy --;
			if(energy <= 0)
			{
				yield return new WaitForSeconds(1f);
				StartCoroutine(hitByObstacle());
			}
			energyTimer = 0;
			adjustEnergy();
		}
	}

	public void FixedUpdate() {
		
		anim.SetInteger("Speed", gameControl.gameSpeed);
		anim.SetInteger("GameState", (int)gameControl.currentGameState);
	}
	
}
