using UnityEngine;
using System.Collections;

public class DungObstacle : Obstacle {

	// Use this for initialization
	public int points = 5;
	public float sp = 1.0f;
	private Animator anim;
	public GameObject[] smell;
	private float lastSmoke;

	public override void Start()
	{
		base.Start ();
		anim = GetComponent<Animator>();
		lastSmoke = 0.1f;
	}

	void Update () {
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			anim.speed = 1;
			movement(points, sp);
			if(lastSmoke >= 0.50f)
			{
				GameObject smk = (GameObject) Instantiate(smell[0], transform.position , transform.rotation); 
				smk.transform.parent = transform;
				lastSmoke = 0;
				
			}else 
			{
				lastSmoke +=  Time.deltaTime;
			}
		}else if (gc.currentGameState == GameControl.GameState.Pause)
		{
			anim.speed = 0;
		}
	}

	#region implemented abstract members of SpawnableObject

	public override void onCollide (Playermovement p)
	{
		p.popEnergyText("-" + 5, Color.red);
		//Changed because it already slows, so the damage can be lower
		p.Energy -= 5;
		p.addEffect(new DungEffect(p, 2f));
		NGUITools.PlaySound(hitSound, 0.5f);
		p.popEffectText("SLOW", Color.magenta);
	}

	#endregion
}
