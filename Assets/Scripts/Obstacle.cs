using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour
{
	[SerializeField] private Vector2 direction;
	private Rigidbody2D rbody;

	const float PHYS_IMPULSE_SNAP_DEGREES = 45.0f;

	private void Start()
	{
		rbody = GetComponent<Rigidbody2D>();
		rbody.velocity = direction;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        //TODO: Filter players

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
}
