using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public delegate void gameControl(float sp);
	public event gameControl stateChanged;
	
	public int gameSpeed;
	private GameState gameState;
	private Playermovement player;
	private GridSpawner spawner;

	public GameObject gameplayPanel, menuPanel, controlsPanel, creditsPanel, mainCamera, tutorialPanel1, tutorialPanel2, defeatPanel, exitPanel, readyMessage, goMessage, pauseMessage, recordMessage,busSpawn, bus, muteButton1, muteButton2, pauseButton, scorePanel, scorePanelGrid, scoredGridItenBase;
	private MusicControl musicControl;

	void Awake(){
		musicControl = GameObject.Find("MusicControl").GetComponent<MusicControl>();
	}

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Playermovement>();
		spawner = GameObject.Find("Spawners").GetComponent<GridSpawner>();
		gameState = GameState.MainMenu;
		musicControl.startMusic ();
		UIEventListener.Get(muteButton1).onClick += toggleSound;
		UIEventListener.Get(muteButton2).onClick += toggleSound;
	}
	
	// Update is called once per frame
	void Update ()
	{

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
					Debug.Log (tg.enabled);
					tg.enabled = false;
					Debug.Log (tg.enabled);
					checkReadyGo();
					StartCoroutine(playDelay(1f));
				}
		}
		
	}

	private void SpawnBus()
	{
		Instantiate(bus , busSpawn.gameObject.transform.position , busSpawn.gameObject.transform.rotation); 
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
		StartCoroutine( musicControl.gameStart ());
		//Uncomment to test tutorialpanels
		//PlayerPrefs.DeleteAll (); 
		StartCoroutine(playDelay (1.5f));
		NGUITools.SetActive( menuPanel,false);
		controlsPanel.GetComponent<TweenPosition> ().PlayReverse ();
		creditsPanel.GetComponent<TweenPosition> ().PlayReverse ();
		NGUITools.SetActive( gameplayPanel,true);
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
		//PlayerPrefs.SetString("MyString", "MyValue");
		if (PlayerPrefs.GetInt ("skipTutorial") == 1)
		{
			GameStart ();
		} else
		{
			NGUITools.SetActive (menuPanel, false);
			//NGUITools.SetActive (controlsPanel, false);
			//NGUITools.SetActive (creditsPanel, false);
			NGUITools.SetActive (tutorialPanel1, true);
		}
	} 

	public void swapTutorials()
	{
		NGUITools.SetActive( tutorialPanel1,false);
		NGUITools.SetActive( tutorialPanel2,true);
	} 



	public void Defeat()
	{
		currentGameState = GameState.Defeat;
		UILabel score = GameObject.Find("score_value").GetComponent<UILabel>();
		score.text = "0";
		NGUITools.SetActive( gameplayPanel,false);

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
		gameState = GameState.GameRestart;
		player.resetPLayerPosition ();

		spawner.tileCounter = 0;

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
}
