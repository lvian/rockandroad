
using UnityEngine;
using System.Collections;

public class VaseObstacle : Obstacle {

	// Use this for initialization
	public int points = 15;
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
		p.Energy -= points;
		p.popEnergyText("-" + points, Color.red);
		NGUITools.PlaySound(hitSound, 0.75f);

		GameObject go = (GameObject) GameObject.Instantiate (objectsSmoke, transform.position, transform.rotation);
		go.GetComponentInChildren<SpriteRenderer> ().color = new Color( 0.8f, 0.3f, 0.1f, 1f);
	}

	#endregion
}
