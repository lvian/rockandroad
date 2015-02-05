using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {

	protected GameControl gc;

	private Animator anim;
	private TweenAlpha tweenAlpha;
	private TweenPosition tweenPosition;
	private UILabel uiLabel;


	public float speedCompensate;
	public string text;
	public Color color;

	// Use this for initialization
	void Start () {
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		
		tweenAlpha = gameObject.GetComponentInChildren<TweenAlpha>();
		tweenPosition = gameObject.GetComponentInChildren<TweenPosition>();

		uiLabel = gameObject.GetComponentInChildren<UILabel>();
		uiLabel.color = color;
		uiLabel.text = text;

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!enabled)
			return;
		if(gc.currentGameState == GameControl.GameState.Play )
		{
			tweenAlpha.enabled = true;
			tweenPosition.enabled = true;
			
		}else if (gc.currentGameState == GameControl.GameState.Pause)
		{
			tweenAlpha.enabled = false;
			tweenPosition.enabled = false;
		}
		
	}
	
	public void Destroy()
	{
		Destroy (gameObject);
	}
}
