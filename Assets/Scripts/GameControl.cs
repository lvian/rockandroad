using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public delegate void gameControl(float sp);
	public event gameControl stateChanged;
	
	public int gameSpeed;
	private GameState gameState;
	private Playermovement player;
	public GameObject gameplayPanel,menuPanel, mainCamera;
	
	// Use this for initialization
	void Start () {
		NGUITools.SetActive( gameplayPanel ,false);
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
		if(currentGameState == GameState.Pause)
		{
			currentGameState = GameState.Play;
		}
		else if(currentGameState == GameState.Play)
		{
			currentGameState = GameState.Pause;
		}		
			
	}
	
	public void toggleSound()
	{
		if(mainCamera.GetComponent<AudioListener>().enabled == true)
		{
			mainCamera.GetComponent<AudioListener>().enabled = false;
		}
		else
		{
			mainCamera.GetComponent<AudioListener>().enabled = true;
		}
		
	}
		
	public void GameStart()
	{
		currentGameState = GameState.Play;
		NGUITools.SetActive( menuPanel,false);
		//gameplayPanel.GetComponent<UIPanel>().enabled = true;
		NGUITools.SetActive( gameplayPanel,true);
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
