using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapBlock {
	// convention for grid definition	
	// 0 - nothing
	// 1 - obstacle
	// 2 - powerup
	public int[,] grid;

	public MapBlock() {
		grid = new int[4, 1];
	}
}
