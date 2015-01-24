using UnityEngine;
using System.Collections;

public class Level3DB : BlocksDB {

	public Level3DB(){
		MapBlock b3 = new MapBlock();
		b3.grid = new int[,]
		{
			{ 0, 2, 0, 0, 2, 0, 0, 2 },
			{ 0, 0, 2, 0, 0, 2, 0, 0 },
			{ 2, 0, 2, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 2, 0 }
		};
		blocks.Add(b3);
		
		b3 = new MapBlock();
		b3.grid = new int[,]
		{
			{ 0, 2, 0, 0, 2, 0, 2, 2 },
			{ 0, 2, 0, 0, 0, 0, 0, 0 },
			{ 2, 0, 2, 0, 0, 2, 0, 0 },
			{ 0, 0, 0, 0, 2, 0, 2, 0 }
		};
		blocks.Add(b3);
		
		b3 = new MapBlock();
		b3.grid = new int[,]
		{
			{ 2, 2, 0, 0, 0, 2, 2, 2 },
			{ 0, 2, 0, 0, 2, 0, 2, 0 },
			{ 2, 0, 2, 0, 2, 2, 0, 2 },
			{ 0, 2, 0, 0, 0, 0, 2, 2 }
		};
		blocks.Add(b3);
		
		b3 = new MapBlock();
		b3.grid = new int[,]
		{
			{ 2, 0, 0, 0, 0, 0, 2, 0 },
			{ 0, 0, 0, 2, 0, 0, 0, 2 },
			{ 0, 0, 0, 0, 0, 0, 0, 2 },
			{ 0, 0, 2, 0, 0, 0, 2, 0 }
		};
		blocks.Add(b3);
		
		b3 = new MapBlock();
		b3.grid = new int[,]
		{
			{ 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2 },
			{ 0, 2, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0 },
			{ 2, 0, 2, 0, 0, 0, 2, 0, 0, 0, 0, 0 },
			{ 0, 2, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2 }
		};
		blocks.Add(b3);
		
		b3 = new MapBlock();
		b3.spawnChance = 5000f;
		b3.grid = new int[,]
		{
			{ 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
			{ 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
			{ 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
			{ 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }
		};
		blocks.Add(b3);
		
		b3 = new MapBlock();
		b3.spawnChance = .3f;
		b3.grid = new int[,]
		{
			{ 2, 2, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0 },
			{ 2, 0, 2, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 2, 0, 2, 2 },
			{ 2, 0, 0, 2, 0, 2, 0, 0, 0, 2, 0, 2, 2, 2 }
		};
		blocks.Add(b3);
		
		b3 = new MapBlock();
		b3.spawnChance = .2f;
		b3.grid = new int[,]
		{
			{ 2, 2, 0, 2, 2, 0, 2, 2 },
			{ 2, 2, 2, 0, 2, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 2, 0, 0 },
			{ 0, 2, 0, 0, 2, 0, 2, 0 }
		};
		blocks.Add(b3);
		//blocks.Add(b2);
	}
}
