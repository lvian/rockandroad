using UnityEngine;
using System.Collections;

public class HealthBoost : Obstacle {
	
	// Use this for initialization
	public int points = 15;
	public float sp = 1.0f;
	public int healthAmount;
	
	// Update is called once per frame
	void Update () {
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			movement(points , sp);
		}
	}


	public int HealthAmount {
		get {
			return healthAmount;
		}
	}

	public int Points {
		get {
			return points;
		}
	}
}
