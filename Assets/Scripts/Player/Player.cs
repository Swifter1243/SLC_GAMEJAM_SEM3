using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody),typeof(Collider))]
public class Player : MonoBehaviour, IResettable
{
	public Rigidbody2D rb;
	public Animator animator;
	public Gun gun;
	public List<Bullet> bullets = new();
	public float respawnTime = 0.2f;

	public float speed;
	public float shootForce;

	public AudioClip clipBounce;
	public AudioClip clipNonBounce;
	public AudioClip clipShoot;
	public AudioClip clipReset;

	private Level _level;

	private const float GUY_ANIM_MOVE_SCALE = 0.2f;

	private const string GUY_ANIM_AIM_NAME = "AimDir";
	private const string GUY_ANIM_MOVE_NAME = "MoveDir";
	private const string GUY_ANIM_RESET_NAME = "Reset";
	private const int GUY_ANIM_MOVE_LAYER = 1;

	private readonly int GUY_ANIM_AIM_INDEX = Animator.StringToHash(GUY_ANIM_AIM_NAME);
	private readonly int GUY_ANIM_MOVE_INDEX = Animator.StringToHash(GUY_ANIM_MOVE_NAME);
	private readonly int GUY_ANIM_RESET_INDEX = Animator.StringToHash(GUY_ANIM_RESET_NAME);

	private int animMoveParameter;

	const float AUDIO_PHYS_MIN_VELOCITY = 2f;

	private void Start()
	{
		gun.onFire.AddListener(OnFire);
	}

	public void Initialize(Level level)
	{
		_level = level;
		gun.bulletsLeft = _level.info.bulletCount;
		UISingleton.Bullets = gun.bulletsLeft; //Kinda want to move this into Gun
		UpdateUIScreenPosition();
		UISingleton.OnPlayerSpawned.Invoke();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			_level.Reset();
		}

		UpdateUIScreenPosition();

		if (animator)
		{
			float angle = gun.faceCursor.GetAngle() / (-360); //hacky
			animator.SetFloat(GUY_ANIM_AIM_INDEX, angle);
		}
	}



	private void UpdateUIScreenPosition()
	{
		UISingleton.playerWorldPosition = transform.position;
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


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.relativeVelocity.sqrMagnitude > AUDIO_PHYS_MIN_VELOCITY)
		{
			switch (collision.gameObject.layer)
			{
				case Constants.LAYER_WORLD_BOUNCY:
					{
						_level.audioSource.PlayOneShot(clipBounce);
						break;
					}
				case Constants.LAYER_WORLD_NONBOUNCE:
					{
						_level.audioSource.PlayOneShot(clipNonBounce);
						break;
					}
				default:
					{
						break;
					}

			}
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
		_level.audioSource.PlayOneShot(clipShoot);
	}

	public void Die()
	{
		rb.simulated = false;
		gun.Disable();
		UISingleton.OnPlayerDeath.Invoke();
		StartCoroutine(DeathCoroutine());
	}

	private IEnumerator DeathCoroutine()
	{
		yield return new WaitForSeconds(respawnTime);

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
		StopAllCoroutines();
		rb.simulated = true;
		gun.Enable();
		transform.position = _level.spawnPoint.position;
		UpdateUIScreenPosition();
		UISingleton.maxBullets = _level.info.bulletCount;
		UISingleton.OnPlayerSpawned.Invoke();
		rb.velocity = Vector2.zero;
		ClearBullets();

		gun.bulletsLeft = _level.info.bulletCount;
		UISingleton.Bullets = gun.bulletsLeft;

		if (animator) animator.SetTrigger(GUY_ANIM_RESET_INDEX);
		_level.audioSource.PlayOneShot(clipReset);
	}
}
