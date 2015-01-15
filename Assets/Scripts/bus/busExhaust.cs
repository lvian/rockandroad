 using UnityEngine;
using System.Collections;

public class busExhaust : MonoBehaviour {

	public GameObject[] smoke;
	private float lastSmoke;
	protected GameControl gc;
	// Use this for initialization
	void Start () {
		lastSmoke = 0.1f;
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();

	
	}
	
	// Update is called once per frame
	void Update () {

		if(gc.currentGameState == GameControl.GameState.Play || gc.currentGameState == GameControl.GameState.MainMenu )
		{
			if(lastSmoke >= 0.20f)
			{
				GameObject smk = (GameObject) Instantiate(smoke[0], gameObject.transform.position , gameObject.transform.rotation); 
				//smk.transform.parent = gameObject.transform;
				lastSmoke = 0;

			}else 
			{
				lastSmoke +=  Time.deltaTime;
			}

		} 
	
	}

}
