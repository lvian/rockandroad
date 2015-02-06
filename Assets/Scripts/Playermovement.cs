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
	public GameObject floatingText;
	public GameObject energyTextSpawner;
	public GameObject scoreTextSpawner;
	public GameObject multiplierTextSpawner;
	public GameObject floatingTextParent;
	public Camera GUICamera, sceneCamera;
	private float energyTimer;
	private int currentLane;
	private bool isMoving;
	private bool isBeingHit;
	private GameControl gameControl;
	private Animator anim;
	private BoxCollider2D movementDelimiter;
	private UIProgressBar pg;
	private bool brokeRecord;
	private Vector3 scoreTextPosition, multiplierTextPosition, energyTextPosition, effectTextPosition; 
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

		//Generate GUI position for score and multiplier
		scoreTextPosition = sceneCamera.WorldToViewportPoint(scoreTextSpawner.transform.position);
		scoreTextPosition = GUICamera.ViewportToWorldPoint (scoreTextPosition);
		energyTextPosition = sceneCamera.WorldToViewportPoint(energyTextSpawner.transform.position);
		energyTextPosition = GUICamera.ViewportToWorldPoint (energyTextPosition);
		multiplierTextPosition = sceneCamera.WorldToViewportPoint(multiplierTextSpawner.transform.position);
		multiplierTextPosition = GUICamera.ViewportToWorldPoint (multiplierTextPosition);


		energyTimer = Time.time;
		anim = GetComponent<Animator>();
		pg  = healthbar.GetComponent<UIProgressBar>();
		currentLane = firstLane;
		isMoving = false;
		isBeingHit = false;
		isInvul = false;
		brokeRecord = false;
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
		if(gameControl.currentGameState != GameControl.GameState.Play)
			yield break;
		if(!isBeingHit)
		{	

			Multiplier = 1;
			isBeingHit = true;
			if(obj)
			{
				NGUITools.PlaySound(playerHitSound[0], 0.5f);
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
			popScoreText("+" + pts * multiplier, new Color(1,1, 1));
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

	void checkRecord ()
	{
		int sc =  PlayerPrefs.GetInt("topScore");
		if(sc > 0 && !brokeRecord)
		{
			if( Score > sc )
			{
				StartCoroutine(gameControl.NewRecord());
				brokeRecord = true;
			}
		}
	}

	public void popEffectText(string text, Color color){
		//GameObject go = (GameObject) GameObject.Instantiate(floatingText, energyTextPosition, Quaternion.identity );
		GameObject go = NGUITools.AddChild (floatingTextParent, floatingText); 
		effectTextPosition = sceneCamera.WorldToViewportPoint(transform.position);
		effectTextPosition = GUICamera.ViewportToWorldPoint (effectTextPosition);
		go.transform.localPosition = go.transform.parent.InverseTransformPoint(effectTextPosition);
		FloatingText fText = go.GetComponentInChildren<FloatingText>();
		fText.text = text;
		fText.color = color;
		go.SetActive(true);
	}

	public void popEnergyText(string text, Color color){
		//GameObject go = (GameObject) GameObject.Instantiate(floatingText, energyTextPosition, Quaternion.identity );
		GameObject go = NGUITools.AddChild (floatingTextParent, floatingText); 
		go.transform.localPosition = go.transform.parent.InverseTransformPoint(energyTextPosition);
		FloatingText fText = go.GetComponentInChildren<FloatingText>();
		fText.text = text;
		fText.color = color;
		go.SetActive(true);
	}

	public void popScoreText(string text, Color color){
		//GameObject go = (GameObject) GameObject.Instantiate(floatingText, scoreTextPosition, Quaternion.identity );
		GameObject go = NGUITools.AddChild (floatingTextParent, floatingText); 
		go.transform.localPosition = go.transform.parent.InverseTransformPoint(scoreTextPosition);
		FloatingText fText = go.GetComponentInChildren<FloatingText>();
		fText.text = text;
		fText.color = color;
		go.SetActive(true);
	}

	public void popMultiplierText(string text, Color color){
		//GameObject go = (GameObject) GameObject.Instantiate(floatingText, multiplierTextPosition, Quaternion.identity );
		GameObject go = NGUITools.AddChild (floatingTextParent, floatingText);
		go.transform.localPosition = go.transform.parent.InverseTransformPoint(multiplierTextPosition);
		FloatingText fText = go.GetComponentInChildren<FloatingText>();
		fText.text = text;
		fText.color = color;
		go.SetActive(true);
	}

	public int Score {
		get {
			return score;
		}
		set {
			score = value;
			UILabel _score = GameObject.Find("score_value").GetComponent<UILabel>();
			_score.text = score.ToString();
			checkRecord();
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
