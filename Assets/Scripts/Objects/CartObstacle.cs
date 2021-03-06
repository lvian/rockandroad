using UnityEngine;
using System.Collections;

public class CartObstacle : MovableObstacle {

	// Use this for initialization
	public int points = 15;
	public float sp = 1.2f;


	private Animator anim;
	// Update is called once per frame

	public override void Start()
	{
		base.Start ();
		anim = GetComponentInChildren<Animator>();
	}

	void Update () {
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			anim.speed = 1;
			movement(points, sp);
		}
		else {
			anim.speed = 0;
		}
	}

	#region implemented abstract members of SpawnableObject

	public override void onCollide (Player p)
	{
		p.Energy -= points;
		p.popEnergyText("-" + points, Color.red);
		NGUITools.PlaySound(hitSound, 0.75f);
		changeLane ();
	}

	#endregion
}
