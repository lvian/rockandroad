using UnityEngine;
using System.Collections;

public abstract class Effect {
	private string uniqueName;
	public float duration;
	public Player target;

	public Effect(string name){
		this.uniqueName = name;
	}

	public string UniqueName {
		get {
			return uniqueName;
		}
	}

	public abstract void onAddEffect();
	public abstract void onRemoveEffect();
	
}
