using UnityEngine;
using System.Collections;

public class Level1 : Difficulty {

	// Use this for initialization
	void Start () {
		this.blocks = new Level1DB().blocks;
	}
}
