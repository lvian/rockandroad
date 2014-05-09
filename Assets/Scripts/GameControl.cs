using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public delegate void gameControl(float sp);
	public event gameControl stateChanged;
	
	public int gameSpeed;
	private GameState gameState;
	private Playermovement player;
	
	// Use this for initialization
	void Start () {
		//Subscribing to receive event healthchanged from PlayerMovement, if so, calls healthChanged	
		player = GameObject.Find("Player").GetComponent<Playermovement>();
		player.healthChanged += healthChanged;
	
		gameState = GameState.MainMenu;
	}
	
	// Update is called once per frame
	void Update ()
	{
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
			//show victory panel, with total points and other related infos
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
