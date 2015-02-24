using UnityEngine;
using System.Collections;

public class TrashObstacle : SpawnableObject {

	// Use this for initialization
	public int points = 10;
	public float sp = 1.0f;
	// Update is called once per frame
	void Update () {
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			movement(points, sp);
		}
	}

	#region implemented abstract members of SpawnableObject

	public override void onCollide (Player p)
	{
		p.Energy -= points;
		p.popEnergyText("-" + points, Color.red);
		NGUITools.PlaySound(hitSound, 0.5f);

		GameObject go = (GameObject) GameObject.Instantiate (objectsSmoke, transform.position, transform.rotation);
		go.GetComponentInChildren<SpriteRenderer> ().color = new Color( 0.6f, 0.6f, 0.6f, 1f);

	}

	#endregion
}
