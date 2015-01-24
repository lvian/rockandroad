using UnityEngine;
using System.Collections;

public class Level2DB : BlocksDB {

	public Level2DB(){
		MapBlock b2 = new MapBlock();
		b2.grid = new int[,]
		{
			{ 0, 1, 0, 0, 1, 0, 0, 1 },
			{ 0, 0, 1, 0, 0, 1, 0, 0 },
			{ 1, 0, 1, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 1, 0 }
		};
		blocks.Add(b2);
		
		b2 = new MapBlock();
		b2.grid = new int[,]
		{
			{ 0, 1, 0, 0, 1, 0, 1, 1 },
			{ 0, 1, 0, 0, 0, 0, 0, 0 },
			{ 1, 0, 1, 0, 0, 1, 0, 0 },
			{ 0, 0, 0, 0, 1, 0, 1, 0 }
		};
		blocks.Add(b2);
		
		b2 = new MapBlock();
		b2.grid = new int[,]
		{
			{ 1, 1, 0, 0, 0, 1, 1, 1 },
			{ 0, 1, 0, 0, 2, 0, 1, 0 },
			{ 1, 0, 1, 0, 2, 1, 0, 2 },
			{ 0, 1, 0, 0, 0, 0, 1, 1 }
		};
		blocks.Add(b2);
		
		b2 = new MapBlock();
		b2.grid = new int[,]
		{
			{ 1, 0, 0, 0, 0, 0, 2, 0 },
			{ 0, 0, 0, 2, 0, 0, 0, 2 },
			{ 0, 0, 0, 0, 0, 0, 0, 2 },
			{ 0, 0, 1, 0, 0, 0, 2, 0 }
		};
		blocks.Add(b2);
		
		b2 = new MapBlock();
		b2.grid = new int[,]
		{
			{ 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
			{ 0, 1, 0, 0, 0, 0, 0, 0, 1, 2, 0, 0 },
			{ 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
			{ 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 }
		};
		blocks.Add(b2);
		
		b2 = new MapBlock();
		b2.spawnChance = 5000f;
		b2.grid = new int[,]
		{
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
		};
		blocks.Add(b2);
		
		b2 = new MapBlock();
		b2.spawnChance = .3f;
		b2.grid = new int[,]
		{
			{ 2, 1, 0, 0, 0, 0, 0, 0, 0, 2, 1, 0, 0, 0 },
			{ 2, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 2, 1, 1, 0, 0, 1, 0, 1, 1 },
			{ 1, 0, 0, 1, 0, 2, 0, 0, 0, 2, 0, 1, 1, 1 }
		};
		blocks.Add(b2);
		
		b2 = new MapBlock();
		b2.spawnChance = .1f;
		b2.grid = new int[,]
		{
			{ 1, 1, 0, 2, 1, 0, 1, 1 },
			{ 1, 1, 1, 0, 2, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 1, 0, 0 },
			{ 0, 1, 0, 0, 2, 0, 1, 0 }
		};
		blocks.Add(b2);
		//blocks.Add(b1);
	}
}
