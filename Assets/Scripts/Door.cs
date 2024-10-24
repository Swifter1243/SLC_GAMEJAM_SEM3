using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : Task, IResettable
{
	private void Start()
	{
		gameObject.SetActive(false);
	}

	public void Open()
	{
		gameObject.SetActive(true); //TODO: visuals
	}

	protected override void CompleteTask()
	{
		base.CompleteTask();

		//TODO: implement door visuals???
	}


	public override void Reset()
	{
		base.Reset();
		gameObject.SetActive(false); //TODO: visuals
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.layer == Constants.LAYER_PLAYER) CompleteTask();
	}


}
