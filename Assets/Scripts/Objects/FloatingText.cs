using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {

	protected GameControl gc;
	private Animator anim;
	private TweenAlpha tweenAlpha;
	private TweenScale tweenScale;
	private TextMesh textMesh;
	private MeshRenderer textObject;

	public float speedCompensate;
	public string text;
	public Color color;
	// Use this for initialization
	void Start () {
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		
		tweenAlpha = gameObject.GetComponentInChildren<TweenAlpha>();
		tweenScale = gameObject.GetComponentInChildren<TweenScale>();
		Debug.Log(text);
		textMesh = gameObject.GetComponentInChildren<TextMesh>();
		textMesh.color = color;
		textMesh.text = text;

		textObject = gameObject.GetComponentInChildren<MeshRenderer>();
		textObject.sortingLayerID = 6;
		textObject.sortingOrder = 6;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!enabled)
			return;
		if( gc.currentGameState == GameControl.GameState.MainMenu)
		{
			speedCompensate = 0;
		}
		if( gc.currentGameState == GameControl.GameState.Play)
		{
			speedCompensate = 5;
		}
		if(gc.currentGameState == GameControl.GameState.Play || gc.currentGameState == GameControl.GameState.MainMenu)
		{
			Vector3 movement = new Vector3(
				- speedCompensate,
				0.5f ,
				0);
			
			movement *= Time.deltaTime;
			
			transform.Translate(movement);
			tweenAlpha.enabled = true;
			tweenScale.enabled = true;
			
		}else if (gc.currentGameState == GameControl.GameState.Pause)
		{
			tweenAlpha.enabled = false;
			tweenScale.enabled = false;
		}
		
	}
	
	public void Destroy()
	{
		Destroy (gameObject);
	}
}
