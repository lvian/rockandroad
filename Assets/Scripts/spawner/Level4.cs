using UnityEngine;
using System.Collections;

public class Level4 : Difficulty {
	
	// Use this for initialization
	void Start () {
		this.blocks = new Level4DB().blocks;
	}
}