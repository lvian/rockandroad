using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlocksDB : MonoBehaviour {
	public List<MapBlock> blocks;

	// Use this for initialization
	void Start () {
		blocks = new List<MapBlock>();

		MapBlock b1 = new MapBlock();
		b1.grid = new int[4,8]
		{
			{ 0, 1, 0, 0, 1, 0, 0, 1 },
			{ 0, 0, 1, 0, 0, 1, 0, 0 },
			{ 1, 0, 1, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 1, 0 }
		};
		b1.waysIn.Add(new Vector2(0,0));
		b1.waysIn.Add(new Vector2(0,1));
		b1.waysIn.Add(new Vector2(0,3));
		b1.waysOut.Add(new Vector2(7,1));
		b1.waysOut.Add(new Vector2(7,2));
		b1.waysOut.Add(new Vector2(7,3));
		blocks.Add(b1);

		MapBlock b2 = new MapBlock();
		b2.grid = new int[4,8]
		{
			{ 0, 1, 0, 0, 1, 0, 1, 1 },
			{ 0, 1, 0, 0, 0, 0, 0, 0 },
			{ 1, 0, 1, 0, 0, 1, 0, 0 },
			{ 0, 0, 0, 0, 1, 0, 1, 0 }
		};
		b2.waysIn.Add(new Vector2(0,0));
		b2.waysIn.Add(new Vector2(0,1));
		b2.waysIn.Add(new Vector2(0,3));
		b2.waysOut.Add(new Vector2(7,1));
		b2.waysOut.Add(new Vector2(7,2));
		b2.waysOut.Add(new Vector2(7,3));
		blocks.Add(b2);
	}
}
