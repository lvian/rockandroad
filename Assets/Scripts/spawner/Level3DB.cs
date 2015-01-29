using UnityEngine;
using System.Collections;

public class Level3DB : BlocksDB {
	
	public Level3DB(){
		MapBlock b3 = new MapBlock();
		b3.grid = new int[,]
		{
			{ 0, 1, 0, 1, 1, 2, 0, 1 },
			{ 0, 0, 2, 1, 1, 1, 2, 1 },
			{ 1, 0, 2, 0, 2, 0, 1, 0 },
			{ 0, 0, 1, 0, 2, 0, 1, 0 }
		};
		blocks.Add(b3);
		
		b3 = new MapBlock();
		b3.grid = new int[,]
		{
			{ 0, 2, 0, 0, 1, 0, 1, 1 },
			{ 0, 2, 0, 0, 2, 0, 0, 0 },
			{ 2, 0, 2, 0, 0, 1, 0, 0 },
			{ 0, 0, 0, 0, 1, 0, 1, 0 }
		};
		blocks.Add(b3);
		
		b3 = new MapBlock();
		b3.grid = new int[,]
		{
			{ 1, 1, 0, 1, 0, 2, 1, 2 },
			{ 0, 2, 0, 0, 2, 0, 1, 0 },
			{ 1, 0, 1, 0, 2, 1, 0, 2 },
			{ 0, 1, 0, 2, 1, 0, 2, 1 }
		};
		blocks.Add(b3);
		
		b3 = new MapBlock();
		b3.grid = new int[,]
		{
			{ 1, 0, 0, 2, 1, 0, 2, 1 },
			{ 0, 0, 1, 2, 0, 2, 0, 2 },
			{ 0, 1, 0, 0, 2, 0, 1, 2 },
			{ 0, 1, 1, 0, 2, 1, 2, 0 }
		};
		blocks.Add(b3);
		
		b3 = new MapBlock();
		b3.grid = new int[,]
		{
			{ 0, 0, 0, 2, 1, 1, 1, 1, 2, 1, 2, 1 },
			{ 0, 1, 0, 0, 2, 0, 0, 0, 1, 2, 2, 0 },
			{ 1, 0, 1, 1, 0, 2, 1, 0, 0, 0, 0, 0 },
			{ 0, 1, 0, 0, 1, 2, 2, 1, 2, 1, 1, 1 }
		};
		blocks.Add(b3);
		
		
		b3 = new MapBlock();
		b3.grid = new int[,]
		{
			{ 2, 1, 2, 0, 1, 0, 0, 2, 1, 2, 1, 0, 2, 0 },
			{ 2, 0, 1, 2, 0, 1, 0, 1, 0, 0, 0, 2, 0, 0 },
			{ 0, 0, 0, 2, 1, 2, 1, 0, 1, 0, 1, 0, 1, 1 },
			{ 1, 0, 0, 1, 0, 2, 0, 2, 0, 2, 0, 1, 1, 2 }
		};
		blocks.Add(b3);
		
		b3 = new MapBlock();
		b3.grid = new int[,]
		{
			{ 1, 1, 0, 2, 1, 0, 1, 1 },
			{ 1, 1, 1, 0, 2, 0, 0, 2 },
			{ 0, 0, 0, 1, 0, 1, 2, 0 },
			{ 2, 1, 2, 0, 2, 0, 1, 0 }
		};
		blocks.Add(b3);
	}
}
