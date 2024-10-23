using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour
{
	[SerializeField] private Vector2 direction;
	private Rigidbody2D rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
		rbody.velocity = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
	{
		//TODO: Filter players

		if (Vector2.Dot(direction, collision.relativeVelocity) < 0) // Only execute if
		{
			direction = Vector2.Reflect(direction, collision.relativeVelocity.normalized);
			rbody.velocity = direction;
		}
	}
}
