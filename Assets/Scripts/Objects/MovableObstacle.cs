using UnityEngine;
using System.Collections;

public class MovableObstacle : Obstacle {

	public bool laneChanger;
	private bool earlyChange = false,lateChange = false, alreadyChanged = false;
	protected float newLaneY = 0;

	protected override void movement(int pts, float sp)
	{
		// Movement function changed to allow lane changing
		// speed = 1 is stationary(same speed as background)
		Vector3 movement = new Vector3(
			speed.x * direction.x * sp,
			speed.y * newLaneY * sp ,
			0);

		movement *= Time.deltaTime;
		transform.Translate(movement);
		newLaneY -= movement.y;
		if(laneChanger){
			if(transform.position.x < 4 && earlyChange == false)
				{
					earlyChange = true;
					int rd =  Random.Range(1,5);
					if(rd != 4)
					{
						changeLane();
						alreadyChanged = true;
					}
				}

				if(transform.position.x < 0 && lateChange == false && alreadyChanged == false)
				{
					lateChange = true;
					int rd =  Random.Range(1,5);
					if(rd < 3)
					{
						changeLane();
					}
				}
		}
			
			if(transform.position.x < -5 && pointsAwarded == false && gc.currentGameState == GameControl.GameState.Play )
			{
				pm.givePoints(pts);
				pointsAwarded = true;
			}
			
			if(transform.position.x < -8)
			{
				Destroy(gameObject);
			}	
			
			
	}

	private void changeLane()
	{
		float v = lanes [spawnLane].transform.position.y;
		int l = Random.Range (0, 4);
		Debug.Log ("l "+ l);
		while ( l == spawnLane  || Mathf.Abs(l - spawnLane ) > 1)
		{
			Debug.Log ("spawn "+spawnLane);
			l = Random.Range(0,4);
		}
		spawnLane = l;
		newLaneY = lanes [spawnLane].transform.position.y - v ;
	}

}

