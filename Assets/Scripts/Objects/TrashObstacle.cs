using UnityEngine;
using System.Collections;

public class TrashObstacle : Obstacle {

	// Use this for initialization
	public int points = 10;
	public float sp = 1.0f;
	// Update is called once per frame
	void Update () {
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			movement(points, sp);
		}
	}
}
