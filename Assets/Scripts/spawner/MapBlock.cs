using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapBlock {
	private const int GRID_HSIZE = 8;

	// convention for grid definition	
	// 0 - nothing
	// 1 - obstacle
	// 2 - powerup
	public int[,] grid;
	public List<Vector2> waysIn;
	public List<Vector2> waysOut;

	public MapBlock() {
		grid = new int[4, GRID_HSIZE];
		waysIn  = new List<Vector2>();
		waysOut = new List<Vector2>();
	}

	public static int Size {
		get {
			return GRID_HSIZE -1;
		}
	}
	
}
