using UnityEngine;
using System.Collections;

public class Level2 : Difficulty {
	
	// Use this for initialization
	void Start () {
		this.blocks = new Level2DB().blocks;
	}
}