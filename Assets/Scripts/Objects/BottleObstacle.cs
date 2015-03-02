using UnityEngine;
using System.Collections;

public class BottleObstacle : MovableObstacle {

	// Use this for initialization
	public int points = 25;
	public float sp = 1.5f;



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
		}else if (gc.currentGameState == GameControl.GameState.Pause)
		{
			anim.speed = 0;
		}
	}

	#region implemented abstract members of SpawnableObject

	public override void onCollide (Player p)
	{
		p.addEffect(new DungEffect(p, 2f));
		p.Energy -= 15;
		p.popEnergyText("-" + 15, Color.red);
		NGUITools.PlaySound(hitSound, 0.75f);
		p.popEffectText("SLOW", Color.magenta);
		GetComponentInChildren<Animator> ().enabled = false;
		GetComponentInChildren<SpriteRenderer> ().enabled = false;
		transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = false;
	}

	#endregion
}
