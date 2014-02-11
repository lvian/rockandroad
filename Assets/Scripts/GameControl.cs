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
			
		}
		
		if(gameState == GameState.Victory)
		{
			
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
			Debug.Log("defeat");
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
