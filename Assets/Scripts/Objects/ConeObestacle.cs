using UnityEngine;
using System.Collections;

public class ConeObestacle : Obstacle {

	// Use this for initialization
	public int points = 15;
	public float sp = 1.0f;

	// Update is called once per frame
	void Update () {
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			movement(points , sp);
		}
	}
}
