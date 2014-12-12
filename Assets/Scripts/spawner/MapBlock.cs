using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapBlock {
	// convention for grid definition	
	// 0 - nothing
	// 1 - obstacle
	// 2 - powerup
	private int[,] _grid;
	public List<Vector2> waysIn;
	public List<Vector2> waysOut;
	public float spawnChance;

	public MapBlock() {
		_grid = new int[4, 1];
		waysIn  = new List<Vector2>();
		waysOut = new List<Vector2>();
		spawnChance = .5f;
	}

	public int[,] grid {
		get {
			return _grid;
		}
		set {
			_grid = value;
			setWays();
		}
	}

	void setWays ()
	{
		if(_grid.GetLength(1) > 1){
			for(int r = 0; r < _grid.GetLength(0); r++){
				if(_grid[r,0] == 0)
					waysIn.Add(new Vector2(r,0));
				if(_grid[r,_grid.GetLength(1) - 1] == 0)
					waysOut.Add(new Vector2(r,_grid.GetLength(1) - 1));
			}
		}
		else{
			for(int r = 0; r < _grid.GetLength(0); r++){
				if(_grid[r,0] == 0)
					waysIn.Add(new Vector2(r,0));
			}
			waysOut = waysIn;
		}
	}
}
