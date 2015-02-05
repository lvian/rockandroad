using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockSpawner {
	private float totalInfluence;
	private static BlockSpawner instance;

	public BlockSpawner()
	{
		//totalInfluence = calcInfluences
	}
	
	public static BlockSpawner Instance
	{
		get
		{
			if (instance==null)
			{
				instance = new BlockSpawner();
			}
			return instance;
		}
	}
	
	private float calcInfluences(SpawnableObject[] items){
		float sum = 0;
		foreach(SpawnableObject i in items){
			sum += i.spawnChance;
		}

		return sum;
	}
	
	public int randomInfluencedIndex(SpawnableObject[] items){
		totalInfluence = calcInfluences(items);
		float rand = Random.Range(0f, totalInfluence);
		float tempSum = 0;
		for(int i = 0; i < items.Length; i++){
			tempSum += items[i].spawnChance;
			if(rand <= tempSum){
				return i;
			}
		}
		return items.Length-1;
	}

	private float calcInfluences(List<MapBlock> items){
		float sum = 0;
		foreach(MapBlock i in items){
			sum += i.spawnChance;
		}
		return sum;
	}
	
	public int randomInfluencedIndex(List<MapBlock> items){
		totalInfluence = calcInfluences(items);
		float rand = Random.Range(0f, totalInfluence);
		float tempSum = 0;
		for(int i = 0; i < items.Count; i++){
			tempSum += items[i].spawnChance;
			if(rand <= tempSum){
				return i;
			}
		}
		return items.Count-1;
	}
}
