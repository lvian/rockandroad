using UnityEngine;
using System.Collections;

public class InvunarabilityPowerUp : PowerUp {

	// Use this for initialization
	public int points = 50;
	public float sp = 1.0f;
	public float duration = 3f;
	
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
		p.givePoints(points);
		NGUITools.PlaySound(hitSound, 0.75f);
		p.popEffectText("INVULNERABLE", Color.green);
		p.addEffect(new InvulnerabilityEffect(p, duration));
	}
	
	#endregion
}
