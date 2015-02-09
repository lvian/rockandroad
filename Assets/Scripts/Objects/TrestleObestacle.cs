using UnityEngine;
using System.Collections;

public class TrestleObestacle : Obstacle {

	// Use this for initialization
	public int points = 25;
	public float sp = 1.0f;

	// Update is called once per frame
	void Update () {
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			movement(points , sp);
		}
	}

	#region implemented abstract members of SpawnableObject

	public override void onCollide (Player p)
	{
		Debug.Log ("teste");
		p.Energy -= 10;
		p.popEnergyText("-" + 10, Color.red);
		NGUITools.PlaySound(hitSound, 0.5f);
	}

	#endregion
}
