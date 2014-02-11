using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public GameObject[] disks;
	private Playermovement player;
	private int playerMaxHealth;

	// Use this for initialization
	void Start () {
	
		//Subscribing to receive event healthchanged from PlayerMovement, if so, calls healthChanged	
		player = GameObject.Find("Player").GetComponent<Playermovement>();
		player.healthChanged += healthChanged;
		playerMaxHealth = player.Health;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Review this logic later
	void healthChanged (int sp)
	{
		Debug.Log("health");
		Debug.Log(sp);
		for(int x = 0; x < playerMaxHealth; x++)
		{	Debug.Log(x);
			Debug.Log("health 1");
			if(x > sp - 1)
			{
				Debug.Log("health 2");
				disks[x].transform.renderer.enabled = false;
			} 
			else
			{
			}		
		}
	}
	
	
}
