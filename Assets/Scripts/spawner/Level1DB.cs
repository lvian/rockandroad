using UnityEngine;
using System.Collections;

public class Level1DB : BlocksDB {

	public Level1DB(){
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
			{ 1, 1, 0, 0, 0, 1, 1, 1 },
			{ 0, 1, 0, 0, 1, 0, 1, 0 },
			{ 1, 0, 1, 0, 1, 1, 0, 1 },
			{ 0, 1, 0, 0, 0, 0, 1, 1 }
		};
		blocks.Add(b1);
		
		b1 = new MapBlock();
		b1.grid = new int[,]
		{
			{ 1, 0, 0, 0, 0, 0, 1, 0 },
			{ 0, 0, 0, 1, 0, 0, 0, 1 },
			{ 0, 0, 0, 0, 0, 0, 0, 1 },
			{ 0, 0, 1, 0, 0, 0, 1, 0 }
		};
		blocks.Add(b1);
		
		b1 = new MapBlock();
		b1.grid = new int[,]
		{
			{ 0, 0, 0, 0, 1, 1, 1, 1 },
			{ 0, 1, 0, 0, 0, 0, 0, 0 },
			{ 1, 0, 1, 0, 0, 0, 1, 0 },
			{ 0, 1, 0, 0, 1, 1, 1, 1 }
		};
		blocks.Add(b1);
		
		b1 = new MapBlock();
		b1.grid = new int[,]
		{
			{ 0, 0, 0, 0, 0, 1, 1, 1 },
			{ 0, 0, 1, 0, 0, 1, 1, 1 },
			{ 0, 0, 1, 0, 0, 0, 1, 1 },
			{ 0, 0, 1, 1, 0, 0, 0, 0 }
		};
		blocks.Add(b1);
		
		b1 = new MapBlock();
		b1.grid = new int[,]
		{
			{ 1, 1, 0, 0, 0, 0, 0, 0 },
			{ 1, 0, 1, 0, 0, 0, 0, 1 },
			{ 0, 0, 0, 0, 0, 1, 1, 1 },
			{ 1, 0, 0, 1, 0, 1, 0, 0 }
		};
		blocks.Add(b1);
		
		b1 = new MapBlock();
		b1.grid = new int[,]
		{
			{ 0, 1, 0, 1, 1, 0, 1, 1 },
			{ 0, 1, 1, 0, 1, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 1, 0, 0 },
			{ 0, 1, 0, 0, 1, 0, 1, 0 }
		};
		blocks.Add(b1);

		b1 = new MapBlock();
		b1.grid = new int[,]
		{
			{ 1, 1, 0, 1, 1, 0, 1, 1 },
			{ 1, 0, 0, 0, 0, 1, 0, 0 },
			{ 0, 0, 1, 1, 0, 0, 0, 1 },
			{ 0, 1, 0, 1, 1, 0, 1, 0 }
		};
		blocks.Add(b1);
	}

}
