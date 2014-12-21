using UnityEngine;
using System.Collections;

public class Multiplier : Obstacle {
	
	// Use this for initialization
	public int points = 50;
	public float sp = 1.0f;
	
	// Update is called once per frame
	void Update () {
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			movement(points , sp);
		}
	}


	public int Points {
		get {
			return points;
		}
	}
}
