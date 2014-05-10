using UnityEngine;
using System.Collections;

public class trashObstacle : obstacle {

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			movement();
		}
	}
}
