using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlocksDB : MonoBehaviour {
	public List<MapBlock> blocks;

	// Use this for initialization
	void Awake () {
		blocks = new List<MapBlock>();

		MapBlock b1 = new MapBlock();
		b1.grid = new int[4,8]
		{
			{ 0, 1, 0, 0, 1, 0, 0, 1 },
			{ 0, 0, 1, 0, 0, 1, 0, 0 },
			{ 1, 0, 1, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 1, 0 }
		};
		blocks.Add(b1);

		MapBlock b2 = new MapBlock();
		b2.grid = new int[4,8]
		{
			{ 0, 1, 0, 0, 1, 0, 1, 1 },
			{ 0, 1, 0, 0, 0, 0, 0, 0 },
			{ 1, 0, 1, 0, 0, 1, 0, 0 },
			{ 0, 0, 0, 0, 1, 0, 1, 0 }
		};
		blocks.Add(b2);


		MapBlock b3 = new MapBlock();
		b2.grid = new int[4,8]
		{
			{ 0, 1, 0, 0, 1, 0, 1, 1 },
			{ 0, 1, 0, 2, 0, 0, 0, 0 },
			{ 1, 0, 1, 0, 0, 1, 0, 0 },
			{ 0, 0, 0, 0, 1, 0, 1, 0 }
		};
		blocks.Add(b3);
	}
}
