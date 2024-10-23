using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
