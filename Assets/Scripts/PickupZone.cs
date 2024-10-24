using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupZone : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == Constants.LAYER_PLAYER)
		{
			Pickup(collision.gameObject);
		}
	}

	protected bool Pickup(GameObject playerObject)
	{
		Player player = playerObject.GetComponent<Player>();
		if (player)
		{
			player.AddBullets();
			return true;
		}
		return false;
	}
}
