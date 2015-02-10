using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Difficulty : MonoBehaviour{
	public List<MapBlock> blocks;
	public SpawnableObject[] staticObjects;
	public SpawnableObject[] doubleObjects;
	public SpawnableObject[] movableObjects;
	public SpawnableObject[] powerUpObjects;
	public SpawnableObject[] multiplierObjects;
}
