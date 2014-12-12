using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlocksDB : MonoBehaviour {
	public List<MapBlock> blocks;

	// Use this for initialization
	void Awake () {
		blocks = new List<MapBlock>();

		MapBlock b1 = new MapBlock();
		b1.grid = new int[,]
		{
			{ 0, 1, 0, 0, 1, 0, 0, 1 },
			{ 0, 0, 1, 0, 0, 1, 0, 0 },
			{ 1, 0, 1, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 1, 0 }
		};
		blocks.Add(b1);

		b1 = new MapBlock();
		b1.grid = new int[,]
		{
			{ 0, 1, 0, 0, 1, 0, 1, 1 },
			{ 0, 1, 0, 0, 0, 0, 0, 0 },
			{ 1, 0, 1, 0, 0, 1, 0, 0 },
			{ 0, 0, 0, 0, 1, 0, 1, 0 }
		};
		blocks.Add(b1);


		b1 = new MapBlock();
		b1.grid = new int[,]
		{
			{ 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
			{ 0, 1, 0, 0, 0, 0, 0, 0, 1, 2, 0, 0 },
			{ 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
			{ 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 }
		};
		blocks.Add(b1);

		b1 = new MapBlock();
		b1.grid = new int[,]
		{
			{ 1, 1, 0, 1, 0 },
			{ 1, 1, 0, 0, 1 },
			{ 0, 0, 0, 0, 0 },
			{ 0, 1, 0, 0, 1 }
		};
		blocks.Add(b1);

		b1 = new MapBlock();
		b1.spawnChance = .1f;
		b1.grid = new int[,]
		{
			{ 0, 0, 1 },
			{ 1, 0, 1 },
			{ 0, 2, 0 },
			{ 1, 0, 0 }
		};
		blocks.Add(b1);

		b1 = new MapBlock();
		b1.spawnChance = .3f;
		b1.grid = new int[,]
		{
			{ 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0 },
			{ 0, 0, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 1 },
			{ 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 }
		};
		blocks.Add(b1);

		b1 = new MapBlock();
		b1.spawnChance = .07f;
		b1.grid = new int[,]
		{
			{ 0, 1, 0, 2, 1, 0, 1, 1 },
			{ 0, 1, 1, 0, 2, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 1, 0, 0 },
			{ 0, 1, 0, 0, 2, 0, 1, 0 }
		};
		blocks.Add(b1);
		//blocks.Add(b1);
	}
}
