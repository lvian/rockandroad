using UnityEngine;
using System.Collections;

public class HealthBoost : PowerUp {
	
	// Use this for initialization
	public int points = 15;
	public float sp = 1.0f;
	public int healthAmount;
	
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
		if(p.Energy + healthAmount > 100)
			p.Energy = 100;
		else
			p.Energy += healthAmount;
		p.givePoints(points);
		p.popEnergyText("+" + healthAmount, Color.cyan);
		NGUITools.PlaySound(hitSound, 0.5f);
	}

	#endregion
}
