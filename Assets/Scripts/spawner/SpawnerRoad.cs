 using UnityEngine;
using System.Collections;

public class SpawnerRoad : Spawner {

	public float min_interval;     
	public float max_interval;
	private float nextInterval;     
	private float timeSinceLast;
	private GameObject parent;
	private int objectLenght;
	// Use this for initialization
	void Start () {
		timeSinceLast = 0;
		parent = GameObject.Find("Obstacles");
		nextInterval = Random.Range(min_interval, max_interval);
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(gc.currentGameState == GameControl.GameState.Play)
		{
			timeSinceLast += Time.deltaTime;
			if(timeSinceLast >= nextInterval)
			{	
				timeSinceLast = 0;
				objectLenght = obstacles.Length;
				int rand = Random.Range( 0 , objectLenght );
				GameObject obs = (GameObject) Instantiate(obstacles[rand] , this.transform.position , this.transform.rotation); 
				obs.transform.parent = parent.transform;
				
				
					
			}
		}
	
	}
}
