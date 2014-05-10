using UnityEngine;
using System.Collections;

public abstract class obstacle : MonoBehaviour {

	private Vector2 speed;
	public Vector2 direction = new Vector2(-1, 0);
	protected GameControl gc;
	
	// Use this for initialization
	public virtual void Start () {
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		gc.stateChanged += gameStateChanged;
		
		//Makes sure the obstacle starts with current game speed
		speed = new Vector2(gc.GameSpeed, 0);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected virtual void movement()
	{
		// Movement
		Vector3 movement = new Vector3(
			speed.x * direction.x,
			speed.y * direction.y,
			0);
		
		movement *= Time.deltaTime;
		transform.Translate(movement);
		
		if(transform.position.x < -8)
		{
			Destroy(gameObject);
		}
	
	}
	
	void gameStateChanged(float gs)
	{
		speed = new Vector2(gs, 0);
	}
}
