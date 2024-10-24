using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IResettable
{
	public Rigidbody2D rb;
	public Animator animator;
	public Gun gun;
	public List<Bullet> bullets = new();

	public float speed;
	public float shootForce;

	private Level _level;

	private const float GUY_ANIM_MOVE_SCALE = 0.2f;

	private const string GUY_ANIM_AIM_NAME = "AimDir";
	private const string GUY_ANIM_MOVE_NAME = "MoveDir";
	private const int GUY_ANIM_MOVE_LAYER = 1;

	private readonly int GUY_ANIM_AIM_INDEX = Animator.StringToHash(GUY_ANIM_AIM_NAME);
	private readonly int GUY_ANIM_MOVE_INDEX = Animator.StringToHash(GUY_ANIM_MOVE_NAME);

	private int animMoveParameter;

	private void Start()
	{
		gun.onFire.AddListener(OnFire);

		if (animator)
		{
			
		}

	}
	public void Initialize(Level level)
	{
		_level = level;
		gun.bulletsLeft = _level.info.bulletCount;
		UISingleton.Bullets = gun.bulletsLeft; //Kinda want to move this into Gun
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			_level.Reset();
		}

		if (animator)
		{
			float angle = gun.faceCursor.GetAngle() / (-360); //hacky
			animator.SetFloat(GUY_ANIM_AIM_INDEX, angle);
		}

	}

	private void FixedUpdate()
	{
		if(animator)
		{
			float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) / (MathF.PI * -2);
			animator.SetFloat(GUY_ANIM_MOVE_INDEX, angle);
			animator.SetLayerWeight(GUY_ANIM_MOVE_LAYER,
				Mathf.Clamp01(
					(1 + Mathf.Log(rb.velocity.magnitude)) * GUY_ANIM_MOVE_SCALE)
				);
		}
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == Constants.LAYER_PICKUP)
		{
			throw new NotImplementedException();
		}
		if (other.gameObject.layer == Constants.LAYER_HAZARD)
		{
			Die();
		}
	}

	public void AddBullets(int ammount = 1)
	{
		gun.bulletsLeft += ammount;
		UISingleton.Bullets = gun.bulletsLeft;
	}
	private void OnFire()
	{
		Vector2 toCursor = gun.faceCursor.GetVectorToCursor().normalized;
		rb.velocity -= toCursor * shootForce;
	}

	public void Die()
	{
		_level.Reset();
	}

	private void ClearBullets()
	{
		foreach (Bullet bullet in bullets)
		{
			Destroy(bullet.gameObject);
		}

		bullets.Clear();
	}

	public void Destroy()
	{
		ClearBullets();
		Destroy(gameObject);
	}

	public void Reset()
	{
		transform.position = _level.spawnPoint.position;
		rb.velocity = Vector2.zero;
		ClearBullets();

		gun.bulletsLeft = _level.info.bulletCount;
		UISingleton.Bullets = gun.bulletsLeft;
	}
}
