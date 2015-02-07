using UnityEngine;
using System.Collections;

public class DungEffect : Effect {

	public DungEffect(Player target, float duration) : base("DungEffect"){
		this.target = target;
		this.duration = duration;
	}

	#region implemented abstract members of Effect

	public override void onAddEffect ()
	{
		target.laneChangeSpeed = 2;
	}

	public override void onRemoveEffect ()
	{
		target.laneChangeSpeed = 4;
	}

	#endregion
}
