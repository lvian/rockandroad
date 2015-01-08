using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public delegate void gameControl(float sp);
	public event gameControl stateChanged;
	
	public int gameSpeed;
	private GameState gameState;
	private Playermovement player;
	public GameObject gameplayPanel, menuPanel, controlsPanel, creditsPanel, mainCamera, tutorialPanel1, tutorialPanel2, defeatPanel, exitPanel, readyMessage, goMessage, pauseMessage;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Playermovement>();

		gameState = GameState.MainMenu;
		mainCamera.GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			//GameSpeed ++;
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
		
		if(gameState == GameState.Pause)
		{
			
		}
		
		if(gameState == GameState.Defeat)
		{
			//show defeat panel, with total points and info about how to restart the level or exit
		}
		
		if(gameState == GameState.Victory)
		{
			//show victory panel, with total points and other related stuff
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
	//	NGUITools.SetActive( exitPanel,false);
	//	NGUITools.SetActive( gameplayPanel,true);
		if(currentGameState == GameState.Pause)
		{
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
		
	}

	public void togglePause()
	{
		if (currentGameState == GameState.MainMenu) {
			currentGameState = GameState.Pause;
		}else{
			if(UIToggle.current.value == true)
			{
				currentGameState = GameState.Pause;
				NGUITools.SetActive(pauseMessage, true);
			}
			else 
			{
				NGUITools.SetActive(pauseMessage, false);
				checkReadyGo();
				StartCoroutine(playDelay(1f));
			}
		}
		
	}



	public void checkCreditsControl()
	{
		if (UIButton.current.name == "controls_button") {
			creditsPanel.GetComponent<TweenPosition> ().PlayReverse ();
			controlsPanel.GetComponent<TweenPosition> ().Play ();
		}
		if(UIButton.current.name == "credits_button")
		{
			controlsPanel.GetComponent<TweenPosition> ().PlayReverse ();
			creditsPanel.GetComponent<TweenPosition> ().Play ();
		}

	}

	public void checkReadyGo()
	{
		readyMessage.GetComponent<TweenScale> ().ResetToBeginning();
		readyMessage.GetComponent<TweenScale> ().PlayForward();
	}

	public void toggleSound()
	{
		if(UIToggle.current.value == true)
		{
			AudioListener.volume = 0;
		}
		else
		{
			AudioListener.volume = 1;
		}
		
	}
		
	public void GameStart()
	{
		//PlayerPrefs.DeleteAll ();
		StartCoroutine(playDelay (0.5f));
		NGUITools.SetActive( menuPanel,false);
		controlsPanel.GetComponent<TweenPosition> ().PlayReverse ();
		//NGUITools.SetActive( controlsPanel,false);
		creditsPanel.GetComponent<TweenPosition> ().PlayReverse ();
		//NGUITools.SetActive( creditsPanel,false);
		NGUITools.SetActive( gameplayPanel,true);
		NGUITools.SetActive( tutorialPanel1,false);
		NGUITools.SetActive( tutorialPanel2,false);
		NGUITools.SetActive( defeatPanel,false);
		//currentGameState = GameState.Play;

		//Makes sure the score label is set to zero after a restart
		UILabel score = GameObject.Find("score_value").GetComponent<UILabel>();
		score.text = "0";
		
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
		//mainCamera.GetComponent<AudioSource>().Play();
		
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

		if(player.points > PlayerPrefs.GetInt("topScore"))
		{
			PlayerPrefs.SetInt("topScore",player.points );
		}
		NGUITools.SetActive( defeatPanel,true);
		UILabel defeatScore = GameObject.Find("defeat_score_value").GetComponent<UILabel>();
		UILabel topScore = GameObject.Find("top_score_value").GetComponent<UILabel>();
		defeatScore.text = player.points.ToString();
		topScore.text = PlayerPrefs.GetInt("topScore").ToString();

	}


	public void exitGameplay ()
	{
		pause ();

		NGUITools.SetActive( gameplayPanel,false);
		NGUITools.SetActive( exitPanel,true);
	}

	public void gameReset()
	{
		gameState = GameState.GameRestart;
		player.resetPLayerPosition ();
		player.points = 0;
		player.multiplier = 1;

		player.energy = PlayerPrefs.GetFloat("defaultEnergy");
		player.adjustEnergy ();

		GameObject[] multiplier = GameObject.FindGameObjectsWithTag ("multiplier");
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("Obstacles");
		GameObject[] healthBoost = GameObject.FindGameObjectsWithTag ("healthBoost");
		foreach( GameObject ob in obstacles)
		{
			Destroy (ob);
		}
		foreach( GameObject hb in healthBoost)
		{
			Destroy (hb);
		}
		foreach( GameObject mp in multiplier)
		{
			Destroy (mp);
		}
		if (UIButton.current.name.Equals ("main_button") || UIButton.current.name.Equals ("yes_button")) 
		{
			MainMenu();
		} else if (UIButton.current.name.Equals ("restart_button"))
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
