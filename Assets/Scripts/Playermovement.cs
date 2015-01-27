using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Playermovement : MonoBehaviour {

	static int UP_ONE_LANE = 1;
	static int DOWN_ONE_LANE = -1;

	private int score = 0;
	private int multiplier = 1;
	private float energy = 100;

	public AudioClip[] playerHitSound;
	public float invulTime;
	public float laneChangeSpeed;
	private bool isInvul;
	public GameObject startingPlace;
	private GameObject scoreValue;
	public int firstLane;
	public GameObject[] lanes;
	public float speed;
	public GameObject healthbar;
	public Dictionary<string,Effect> effects;
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
		PlayerPrefs.SetFloat("defaultLaneChangeSpeed",laneChangeSpeed );
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

		effects = new Dictionary<string,Effect>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(gameControl.currentGameState == GameControl.GameState.Play)
		{
			anim.speed = 1;
			checkInput();
			moveTo();
			updateEffects();
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
			}
		}
		if(isInvul){
			animateAlpha();
		}
	}

	public void FixedUpdate() {
		
		anim.SetInteger("Speed", gameControl.gameSpeed);
		anim.SetInteger("GameState", (int)gameControl.currentGameState);
	}

	private void moveTo()
	{
		if(isMoving)
		{
			transform.position = Vector3.MoveTowards(gameObject.transform.position,lanes[currentLane].transform.position, 0.03f * laneChangeSpeed);
		}
	
	}

	private void checkInput(){
		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			move(UP_ONE_LANE);
		}
		
		if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			move (DOWN_ONE_LANE);
		}		
	}

	private void animateAlpha (){
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
				StartCoroutine(hitByObstacle(other.GetComponent<SpawnableObject>()));
			}
		}
		
		//Player hit by powerup
		if(other.gameObject.tag == "PowerUp")
		{
			SpawnableObject obj = other.gameObject.GetComponent<SpawnableObject>();
			other.collider2D.enabled = false;
			Destroy(other.gameObject);
			obj.onCollide(this);
		}
	}
	
	IEnumerator hitByObstacle(SpawnableObject obj) {
		if(gameControl.currentGameState != GameControl.GameState.Play) // ??? Reacess!
			yield break;
		if(!isBeingHit)
		{	

			Multiplier = 1;
			isBeingHit = true;
			if(obj)
			{
				transform.audio.PlayOneShot(playerHitSound[0]);
				obj.onCollide(this);
			}
			//play damage animation here
			if(energy <= 0)
			{
				//change timer to something like 'death animation time lenght'
				yield return new WaitForSeconds(1f);
				StartCoroutine(hitByObstacle(null));
			}
			else
			{
				isInvul = true;
				yield return new WaitForSeconds(invulTime);
				isInvul = false;
				Color c = new Color(
					renderer.material.color.r, 
					renderer.material.color.g, 
					renderer.material.color.b, 1f
				);
				renderer.material.color = c;
				StartCoroutine(hitByObstacle(null));
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

	private void adjustEnergy() {
		pg.value = energy / 100;

	}

	public void adjustMultiplier() {
		UILabel mp = GameObject.Find("scoremultiplier_value").GetComponent<UILabel>();
		mp.text = "x " + Multiplier.ToString();
	}
	
	public void givePoints(int pts) {
		if(energy > 0)
		{
			Score += pts * multiplier;
		}
	}

	IEnumerator energyDecay ()
	{
		energyTimer += Time.deltaTime;
		if(energyTimer >= 1)
		{
			Energy--;
			if(Energy <= 0)
			{
				yield return new WaitForSeconds(1f);
				StartCoroutine(hitByObstacle(null));
			}
			energyTimer = 0;
		}
	}

	public void addEffect(Effect e){
		if(effects.ContainsKey(e.UniqueName)){
			effects[e.UniqueName].duration = e.duration;
		}
		else{
			effects.Add(e.UniqueName,e);
			e.onAddEffect();
		}
	}

	private void removeEffect(string uName){
		effects[uName].onRemoveEffect();
		effects.Remove(uName);
	}

	private void updateEffects(){
		List<string> temp = new List<string>(effects.Keys);
		foreach(string e in temp){
			if(effects[e].duration <= 0){
				removeEffect(effects[e].UniqueName);
			}
			else{
				effects[e].duration -= Time.deltaTime;
			}
		}
	}

	public int Score {
		get {
			return score;
		}
		set {
			score = value;
			UILabel _score = GameObject.Find("score_value").GetComponent<UILabel>();
			_score.text = score.ToString();
		}
	}
	
	public int Multiplier {
		get {
			return multiplier;
		}
		set {
			multiplier = value;
			adjustMultiplier();
			
		}
	}
	
	public float Energy {
		get {
			return energy;
		}
		set {
			energy = value;
			adjustEnergy();
		}
	}
}
