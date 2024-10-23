using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : Task
{
	public void Open()
	{
		gameObject.SetActive(false); //TODO: visuals
	}

	protected override void CompleteTask()
	{
		base.CompleteTask();

		//TODO: implement door visuals???
	}


	public override void Reset()
	{
		gameObject.SetActive(true); //TODO: visuals
	}

	private void OnColliderEnter(Collider other)
	{
		if (other.gameObject.layer == Constants.LAYER_PLAYER) CompleteTask();
	}


}
