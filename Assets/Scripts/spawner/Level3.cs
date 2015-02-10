using UnityEngine;
using System.Collections;

public class Level3 : Difficulty {
	
	// Use this for initialization
	void Start () {
		this.blocks = new Level3DB().blocks;
	}
}