using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : Resettable
{
	[SerializeField] private Vector2 direction;
	private Rigidbody2D rbody;

	private Vector2 initialDirection;
	private Vector3 initialPosition;

	private void Start()
	{
		initialDirection = direction;
		initialPosition = transform.position;

		rbody = GetComponent<Rigidbody2D>();
		rbody.velocity = direction;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//TODO: Filter players
		if (gameObject.layer == Constants.LAYER_PLAYER)
		{
			rbody.velocity = direction;
			return;
		}

        //Get the total impulse (but no tangential forces)
        Vector2 impulse = Vector2.zero;
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint2D contactPoint = collision.GetContact(i);
            Vector2 contactImpulse = contactPoint.normal * contactPoint.normalImpulse;
            impulse += contactImpulse;
        }

        if (Vector2.Dot(direction, impulse) < 0) //Filter by direction
		{
            direction = Vector2.Reflect(direction, impulse.normalized);
			rbody.velocity = direction;
		}
	}

	public override void Reset()
	{
		rbody.velocity = direction = initialDirection;
		transform.position = initialPosition;

		return;
	}
}
