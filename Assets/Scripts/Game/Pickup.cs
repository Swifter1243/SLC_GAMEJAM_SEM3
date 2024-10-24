using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : PickupZone, IResettable
{
	private bool _startEnabled = true;

	private void Start()
	{
		_startEnabled = gameObject.activeSelf;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == Constants.LAYER_PLAYER)
		{
			gameObject.SetActive(!Pickup(collision.gameObject));
		}
	}

	public void Reset()
	{
		gameObject.SetActive(_startEnabled);
	}
}
