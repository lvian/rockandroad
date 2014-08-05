using UnityEngine;
using System.Collections;

public class trashObstacle : obstacle {

	// Use this for initialization
	public int points = 10;
	
	// Update is called once per frame
	void Update () {
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			movement(points);
		}
	}
}
