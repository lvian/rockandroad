using UnityEngine;
using System.Collections;

public class TrestleObstacle : Obstacle {

	// Use this for initialization
	public int points = 25;
	public float sp = 1.0f;

	// Update is called once per frame
	void Update () {
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			movement(points , sp);
			if(isHit)
			{
				transform.GetChild(0).position = Vector3.MoveTowards(transform.GetChild(0).position ,new Vector3(transform.position.x + adjustPosition.x,
				                                                                                                 adjustPosition.y,
				                                                                                                 adjustPosition.z),
				                                                                                                 4 *  Time.deltaTime);
				transform.GetComponentInChildren<TweenRotation>().PlayForward();
			}
		}
	}

	#region implemented abstract members of SpawnableObject

	public override void onCollide (Player p)
	{
		p.Energy -= 10;
		p.popEnergyText("-" + 10, Color.red);
		NGUITools.PlaySound(hitSound, 0.5f);
		isHit = true;
		adjustPosition = new Vector3( 0.5f , transform.GetChild(0).position.y + 0.3f, transform.GetChild(0).position.z + transform.position.z );

	}

	#endregion
}
