using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {

	protected GameControl gc;

	private Animator anim;
	private TweenAlpha tweenAlpha;
	private TweenScale tweenScale;
	private TweenPosition tweenPosition;
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
		tweenPosition = gameObject.GetComponentInChildren<TweenPosition>();
		textMesh = gameObject.GetComponentInChildren<TextMesh>();
		textMesh.color = color;
		textMesh.text = text;

		textObject = gameObject.GetComponentInChildren<MeshRenderer>();
		textObject.sortingLayerID = 8;
		textObject.sortingOrder = 8;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!enabled)
			return;
		if(gc.currentGameState == GameControl.GameState.Play )
		{
			tweenAlpha.enabled = true;
			tweenScale.enabled = true;
			tweenPosition.enabled = true;
			
		}else if (gc.currentGameState == GameControl.GameState.Pause)
		{
			tweenAlpha.enabled = false;
			tweenScale.enabled = false;
			tweenPosition.enabled = false;
		}
		
	}
	
	public void Destroy()
	{
		Destroy (gameObject);
	}
}
