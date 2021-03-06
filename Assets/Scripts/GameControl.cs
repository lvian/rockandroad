using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public delegate void gameControl(float sp);
	public event gameControl stateChanged;
	
	public int gameSpeed;
	private GameState gameState;
	private Player player;
	private GridSpawner spawner;

	public GameObject gameplayPanel, menuPanel, controlsPanel, creditsPanel, mainCamera, tutorialPanel1, tutorialPanel2, defeatPanel, exitPanel, readyMessage, goMessage, pauseMessage, recordMessage,busSpawn, bus,bussWing,bussKiss, muteButton1, muteButton2, pauseButton, scorePanel, scorePanelGrid, scoredGridItenBase, blockPanel, bandPanel, healthbar,defeatSymbolKiss,defeatSymbolWing,defeatSymbolSkull;
	private MusicControl musicControl;
	private bool pauseLock;
	private Band band;

	void Awake(){
		musicControl = GameObject.Find("MusicControl").GetComponent<MusicControl>();
	}

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Player>();
		spawner = GameObject.Find("Spawners").GetComponent<GridSpawner>();
		gameState = GameState.MainMenu;
		musicControl.startMusic ();
		UIEventListener.Get(muteButton1).onClick += toggleSound;
		UIEventListener.Get(muteButton2).onClick += toggleSound;
		pauseLock = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log ("mute 2 "+muteButton2.GetComponentInChildren<UISprite>());
		if(Input.GetKeyDown(KeyCode.R))
		{
			//GameSpeed ++;
			//highScore();
		}
		if(Input.GetKeyDown(KeyCode.F))
		{
			//GameSpeed --;
		}
		if(gameState == GameState.MainMenu)
		{
		
		}
		if(gameState == GameState.GameMenu)
		{
					
		}
		if(gameState == GameState.Play)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				exitGameplay();
			}
			if(Input.GetKeyDown(KeyCode.Space) && !pauseLock)
			{
				UIToggle uiToogle = pauseButton.GetComponent<UIToggle>();
				uiToogle.SendMessage("OnClick");
				pauseLock = true;
			}
			if(Input.GetKeyUp(KeyCode.Space))
			{
				pauseLock = false;
			}
		}
		if(gameState == GameState.Pause){
			if(Input.GetKeyUp(KeyCode.Space) && !pauseLock)
			{
				UIToggle uiToogle = pauseButton.GetComponent<UIToggle>();
				uiToogle.SendMessage("OnClick");
				pauseLock = true;
			}
			if(Input.GetKeyUp(KeyCode.Space))
			{
				pauseLock = false;
			}
		}
	}

	public int GameSpeed {
		get {
			return gameSpeed;
		}
		set {
			stateChanged (value);
			gameSpeed = value;
		}
	}	
	
	void healthChanged (int sp)
	{
		if(sp == 0)
		{
			gameState = GameState.Defeat;
		}
	}

	public GameState currentGameState {
		get {
			return gameState;
		}
		set {
			gameState = value;
		}
	}

	public Band currentBand {
		get {
			return band;
		}
	}
	
	public void pause()
	{
		if(currentGameState == GameState.Pause)
		{
			UIToggle tog = pauseButton.GetComponent<UIToggle> ();
			tog.value = false;
			NGUITools.SetActive(pauseMessage, false);
			NGUITools.SetActive( gameplayPanel,true);
			NGUITools.SetActive( exitPanel,false);
			checkReadyGo();
			StartCoroutine(playDelay(1f));

		}
		else if(currentGameState == GameState.Play)
		{
			currentGameState = GameState.Pause;
		}		
			
	}

	IEnumerator playDelay(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		currentGameState = GameState.Play;
		UIToggle tg = pauseButton.GetComponent<UIToggle>();
		tg.enabled = true;
		NGUITools.SetActive( blockPanel,false);
		
		
	}

	public void togglePause()
	{
		if (currentGameState != GameState.MainMenu) 
		{
			if(UIToggle.current.value == true)
			{
				currentGameState = GameState.Pause;
				NGUITools.SetActive(pauseMessage, true);
			}
			else 
			{
				NGUITools.SetActive(pauseMessage, false);
				NGUITools.SetActive(exitPanel, false);
				UIToggle tg = pauseButton.GetComponent<UIToggle>();
				tg.enabled = false;
				checkReadyGo();
				StartCoroutine(playDelay(1f));
			}
		}
		
	}

	private void SpawnBus()
	{
		if(band == Band.Skull)
		{
			Instantiate(bus , busSpawn.gameObject.transform.position , busSpawn.gameObject.transform.rotation); 
		}
		if(band == Band.Wing)
		{
			Instantiate(bussWing , busSpawn.gameObject.transform.position , busSpawn.gameObject.transform.rotation); 
		}
		if(band == Band.Kiss)
		{
			Instantiate(bussKiss , busSpawn.gameObject.transform.position , busSpawn.gameObject.transform.rotation); 
		}

	}

	public void checkCreditsControl()
	{
		if (UIButton.current.name == "controls_button") {
			creditsPanel.GetComponent<TweenPosition> ().PlayReverse ();
			scorePanel.GetComponent<TweenPosition> ().PlayReverse ();
			controlsPanel.GetComponent<TweenPosition> ().PlayForward ();
		}
		if(UIButton.current.name == "credits_button")
		{
			controlsPanel.GetComponent<TweenPosition> ().PlayReverse ();
			scorePanel.GetComponent<TweenPosition> ().PlayReverse ();
			creditsPanel.GetComponent<TweenPosition> ().PlayForward ();
		}
		if(UIButton.current.name == "scoreboardButton")
		{

			controlsPanel.GetComponent<TweenPosition> ().PlayReverse ();
			creditsPanel.GetComponent<TweenPosition> ().PlayReverse ();
			highScore();
		}

	}

	public void checkReadyGo()
	{

		readyMessage.GetComponent<TweenScale> ().ResetToBeginning();
		readyMessage.GetComponent<TweenScale> ().PlayForward();

	}

	public void toggleSound(GameObject ob)
	{
		UIToggle tog = ob.GetComponent<UIToggle> ();
		if(tog.value == true)
		{
			mainCamera.audio.volume = 0;
			musicControl.audio.volume = 0;
			muteButton1.GetComponent<UIToggle>().value = true;
			muteButton2.GetComponent<UIToggle>().value = true;
		}
		else
		{
			mainCamera.audio.volume = 0.5f;
			musicControl.audio.volume = musicControl.musicVolume;
			muteButton1.GetComponent<UIToggle>().value = false;
			muteButton2.GetComponent<UIToggle>().value = false;
		}
	}
		
	public void GameStart()
	{
		if (UIButton.current.name.Equals ("skullBandButton")) 
		{
			band = Band.Skull;
			healthbar.GetComponent<UISprite>().color = new Color(
				0.5f, 
				0.4f, 
				0.95f,
				1f
				);
		}
		if (UIButton.current.name.Equals ("wingBandButton")) 
		{
			band = Band.Wing;
			healthbar.GetComponent<UISprite>().color = new Color(
				0.4f, 
				0.6f, 
				0.9f,
				1f
				);
		}
		if (UIButton.current.name.Equals ("kissBandButton")) 
		{
			band = Band.Kiss;

			healthbar.GetComponent<UISprite>().color = new Color(
				0.75f, 
				0.75f, 
				0.3f,
				1f
				);
		}
		GA.API.Design.NewEvent ("Band Selected", (float)band);
		spawner.Band = band;
		StartCoroutine( musicControl.gameStart ());
		//Uncomment to test tutorialpanels
		//PlayerPrefs.DeleteAll (); 
		StartCoroutine(playDelay (1.5f));
		NGUITools.SetActive( menuPanel,false);
		controlsPanel.GetComponent<TweenPosition> ().PlayReverse ();
		creditsPanel.GetComponent<TweenPosition> ().PlayReverse ();
		NGUITools.SetActive( bandPanel,false);
		NGUITools.SetActive( tutorialPanel1,false);
		NGUITools.SetActive( tutorialPanel2,false);
		NGUITools.SetActive( defeatPanel,false);
		//currentGameState = GameState.Play;

		//Makes sure the score label is set to zero after a restart
		player.Score = 0;
		player.Multiplier = 1;

		readyMessage.GetComponent<TweenScale> ().PlayForward ();
		SpawnBus ();

		
	} 

	public void SkipTutorial()
	{
			PlayerPrefs.SetInt("skipTutorial", 1);
	} 

	public void MainMenu()
	{
		NGUITools.SetActive( menuPanel,true);
		NGUITools.SetActive( gameplayPanel,false);
		NGUITools.SetActive( defeatPanel,false);
		NGUITools.SetActive( exitPanel,false);
		
	}

	public void Tutorial()
	{
		GA.API.Design.NewEvent ("Pressed Play Button");
		NGUITools.SetActive( blockPanel,true);
		NGUITools.SetActive( gameplayPanel,true);
		//PlayerPrefs.SetString("MyString", "MyValue");
		creditsPanel.GetComponent<TweenPosition> ().PlayReverse ();
		controlsPanel.GetComponent<TweenPosition> ().PlayReverse ();
		scorePanel.GetComponent<TweenPosition> ().PlayReverse ();
		if (PlayerPrefs.GetInt ("skipTutorial") == 1)
		{
			selectBand();
			//GameStart ();
		} else
		{
			//NGUITools.SetActive (menuPanel, false);
			//NGUITools.SetActive (controlsPanel, false);
			//NGUITools.SetActive (creditsPanel, false);
			NGUITools.SetActive (tutorialPanel1, true);
		}
	} 

	public void SwapTutorials()
	{
		NGUITools.SetActive( tutorialPanel1,false);
		NGUITools.SetActive( tutorialPanel2,true);
	} 


	public void selectBand()
	{
		NGUITools.SetActive( bandPanel,true);
		NGUITools.SetActive( tutorialPanel1,false);
		NGUITools.SetActive( tutorialPanel2,false);
	}



	public void Defeat()
	{
		currentGameState = GameState.Defeat;
		UILabel score = GameObject.Find("score_value").GetComponent<UILabel>();
		score.text = "0";

		if (band == Band.Skull) {
			defeatSymbolKiss.SetActive(false);
			defeatSymbolSkull.SetActive(true);
			defeatSymbolWing.SetActive(false);
		}
		if (band == Band.Kiss) {
			defeatSymbolKiss.SetActive(true);
			defeatSymbolSkull.SetActive(false);
			defeatSymbolWing.SetActive(false);
		}
		if (band == Band.Wing) {
			defeatSymbolKiss.SetActive(false);
			defeatSymbolSkull.SetActive(false);
			defeatSymbolWing.SetActive(true);
		
			
		}

		

		//NGUITools.SetActive( gameplayPanel,false);
		NGUITools.SetActive( blockPanel,true);

		if(player.Score > PlayerPrefs.GetInt("topScore"))
		{
			PlayerPrefs.SetInt("topScore",player.Score );
		}
		NGUITools.SetActive( defeatPanel,true);
		UILabel defeatScore = GameObject.Find("defeat_score_value").GetComponent<UILabel>();
		UILabel topScore = GameObject.Find("top_score_value").GetComponent<UILabel>();
		UILabel distance = GameObject.Find("distance_value").GetComponent<UILabel>();
		defeatScore.text = player.Score.ToString();
		topScore.text = PlayerPrefs.GetInt("topScore").ToString();

		GridSpawner gs = GameObject.Find ("Spawners").GetComponent<GridSpawner> ();
		distance.text = gs.TileCounter.ToString() + " m";
		GA.API.Design.NewEvent ("Distance when defeat" , gs.TileCounter);

	}

	public IEnumerator NewRecord (){
		NGUITools.SetActive (recordMessage, true);
		yield return new WaitForSeconds (3f);
		NGUITools.SetActive (recordMessage, false);

	}
	public void exitGameplay ()
	{
		if(gameState == GameState.Play)
		{
			pause ();
		}
		UIToggle tog = pauseButton.GetComponent<UIToggle> ();
		tog.value = true;
		//NGUITools.SetActive( gameplayPanel,false);
		NGUITools.SetActive( exitPanel,true);
	}
	public void highScore()
	{
		//Disabled until a real scoreboard is implemented
		/*
		string[] scores = loadScore ();

		char[] delimiter = {';'};
		int n = 1;
	
		while (scorePanelGrid.transform.childCount > 0)
		{
			NGUITools.Destroy(scorePanelGrid.transform.GetChild(0).gameObject);
		}
		foreach( string score in scores)
		{
			GameObject sc = NGUITools.AddChild(scorePanelGrid , scoredGridItenBase);
			string[] s = score.Split(delimiter);
			sc.transform.FindChild("ItenNumber").GetComponent<UILabel>().text = n.ToString();
			sc.transform.FindChild("ItenName").GetComponent<UILabel>().text = s[1];
			sc.transform.FindChild("ItenScore").GetComponent<UILabel>().text = s[2];
			sc.transform.FindChild("ItenDistance").GetComponent<UILabel>().text = s[3];
			sc.SetActive(true);
			n++;
		}

		scorePanelGrid.GetComponent<UIGrid> ().enabled = true;
		*/
		scorePanel.GetComponent<TweenPosition> ().PlayForward ();
	}


	protected string[] loadScore()
	{
		string[] scoreLines = System.IO.File.ReadAllLines ("assets/scoreboard.txt");

		return scoreLines;
	}



	public void gameReset()
	{
		NGUITools.SetActive( blockPanel,false);
		gameState = GameState.GameRestart;
		player.resetPLayerPosition ();

		spawner.reset();

		NGUITools.SetActive(pauseMessage, false);

		player.Energy = PlayerPrefs.GetFloat("defaultEnergy");
		player.laneChangeSpeed = PlayerPrefs.GetFloat("defaultLaneChangeSpeed");

		GameObject[] powerup = GameObject.FindGameObjectsWithTag ("PowerUp");
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("Obstacles");
		foreach( GameObject pu in powerup)
		{
			Destroy (pu);
		}
		foreach( GameObject ob in obstacles)
		{
			Destroy (ob);
		}

		if (UIButton.current.name.Equals ("main_button") || UIButton.current.name.Equals ("yes_button")) 
		{
			healthbar.GetComponent<UISprite>().color = new Color(
				1f, 
				1f, 
				1f,
				1f
				);
			musicControl.restartMusic();
			MainMenu();
		}
		else if (UIButton.current.name.Equals ("restart_button"))
		{
			GameStart();
		}
		
	}

	public enum GameState{
		MainMenu,
		GameMenu,
		Play,
		Pause,
		Victory,
		Defeat,
		GameRestart
	}

	public enum Band{
		Skull,
		Kiss,
		Wing
	}

}
