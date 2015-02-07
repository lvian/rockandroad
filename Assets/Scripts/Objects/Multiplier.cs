using UnityEngine;
using System.Collections;

public class Multiplier : PowerUp {
	
	// Use this for initialization
	public int points = 50;
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
		p.Multiplier++;
		NGUITools.PlaySound(hitSound, 0.5f);
		p.givePoints(points);
		p.popMultiplierText("x"+p.Multiplier, Color.green);
	}

	#endregion
}
