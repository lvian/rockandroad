using UnityEngine;
using System.Collections;

public class TireObestacle : Obstacle {

	// Use this for initialization
	public int points = 15;
	public float sp = 1.3f;

	private Animator anim;
	// Update is called once per frame

	void Start()
	{
		base.Start ();
		anim = GetComponent<Animator>();
	}

	void Update () {
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			anim.speed = 1;
			movement(points, sp);
		}else if (gc.currentGameState == GameControl.GameState.Pause)
		{
			anim.speed = 0;
		}
	}
}
