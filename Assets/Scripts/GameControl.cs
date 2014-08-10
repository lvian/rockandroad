using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public delegate void gameControl(float sp);
	public event gameControl stateChanged;
	
	public int gameSpeed;
	private GameState gameState;
	private Playermovement player;
	public GameObject gameplayPanel,menuPanel, controlsPanel, creditsPanel, mainCamera, tutorialPanel1,tutorialPanel2;

	// Use this for initialization
	void Start () {
		gameState = GameState.MainMenu;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			pause();
		}
		if(Input.GetKeyDown(KeyCode.R))
		{
			GameSpeed ++;
		}
		
		if(Input.GetKeyDown(KeyCode.F))
		{
			GameSpeed --;
		}
		if(gameState == GameState.MainMenu)
		{
		
		}
		
		if(gameState == GameState.GameMenu)
		{
			
		}
		
		if(gameState == GameState.Play)
		{
			
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
		bool myState = UIToggle.current.value;
		Debug.Log (myState);

		if(currentGameState == GameState.Pause)
		{
			currentGameState = GameState.Play;
		}
		else if(currentGameState == GameState.Play)
		{
			currentGameState = GameState.Pause;
		}		
			
	}

	public void togglePause()
	{
		if(UIToggle.current.value == true)
		{
			currentGameState = GameState.Pause;
		}
		else
		{
			currentGameState = GameState.Play;
		}
		
	}

	public void backToMenu()
	{
		NGUITools.SetActive( controlsPanel,false);
		NGUITools.SetActive( creditsPanel,false);
		//NGUITools.SetActive( menuPanel,true);
		
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

		NGUITools.SetActive( menuPanel,false);
		NGUITools.SetActive( controlsPanel,false);
		NGUITools.SetActive( creditsPanel,false);
		NGUITools.SetActive( gameplayPanel,true);
		NGUITools.SetActive( tutorialPanel1,false);
		NGUITools.SetActive( tutorialPanel2,false);
		currentGameState = GameState.Play;
		//mainCamera.GetComponent<AudioSource>().Play();
		
	} 

	public void Tutorial()
	{

		NGUITools.SetActive( menuPanel,false);
		NGUITools.SetActive( controlsPanel,false);
		NGUITools.SetActive( creditsPanel,false);
		NGUITools.SetActive( tutorialPanel1,true);
	} 

	public void swapTutorials()
	{
		NGUITools.SetActive( tutorialPanel1,false);
		NGUITools.SetActive( tutorialPanel2,true);
	} 

	public void Control()
	{
		
		NGUITools.SetActive( creditsPanel,false);
		NGUITools.SetActive( controlsPanel,true);
		//mainCamera.GetComponent<AudioSource>().Play();
		
	}


	public void Credits()
	{
		
		NGUITools.SetActive( controlsPanel,false);
		NGUITools.SetActive( creditsPanel,true);
		//mainCamera.GetComponent<AudioSource>().Play();
		
	}
	
	public enum GameState{
		MainMenu,
		GameMenu,
		Play,
		Pause,
		Victory,
		Defeat
	}
}
