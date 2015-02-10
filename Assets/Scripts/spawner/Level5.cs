using UnityEngine;
using System.Collections;

public class Level5 : Difficulty {
	
	// Use this for initialization
	void Start () {
		this.blocks = new Level5DB().blocks;
	}
}