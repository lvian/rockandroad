﻿using UnityEngine;
using System.Collections;

public class coneObestacle : obstacle {

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			movement();
		}
	}
}
