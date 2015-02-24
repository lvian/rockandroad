using UnityEngine;
using System.Collections;

public class BoxObstacle : Obstacle {

	// Use this for initialization
	public int points = 15;
	public float sp = 1.0f;

	// Update is called once per frame
	void Update () {
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			movement(points , sp);
			if(isHit)
			{
				transform.GetChild(0).position = Vector3.MoveTowards(transform.GetChild(0).position ,transform.position + adjustPosition ,  5 *Time.deltaTime);
				transform.GetComponentInChildren<TweenRotation>().PlayForward();
			}
		}
	}

	#region implemented abstract members of SpawnableObject

	public override void onCollide (Player p)
	{
		p.Energy -= points;
		p.popEnergyText("-" + points, Color.red);
		NGUITools.PlaySound(hitSound, 0.5f);
		isHit = true;
		adjustPosition = new Vector3( 0.1f , 0.15f, transform.position.z );
	}

	#endregion
}
