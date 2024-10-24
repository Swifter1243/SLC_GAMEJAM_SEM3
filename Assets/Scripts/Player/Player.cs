using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IResettable
{
	public Rigidbody2D rb;
	public Gun gun;
	public List<Bullet> bullets = new();

	public float speed;
	public float shootForce;

	private Level _level;

	private void Start()
	{
		gun.onFire.AddListener(OnFire);
	}

	public void Initialize(Level level)
	{
		_level = level;
		gun.bulletsLeft = _level.info.bulletCount;
		UISingleton.Bullets = gun.bulletsLeft; //Kinda want to move this into Gun
		UISingleton.OnPlayerSpawned.Invoke();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			_level.Reset();
		}

		UpdateUIScreenPosition();
	}

	private void UpdateUIScreenPosition()
	{
		UISingleton.playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
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
		rb.simulated = false;
		UISingleton.OnPlayerDeath.Invoke();
		StartCoroutine(DeathCoroutine());
	}

	private IEnumerator DeathCoroutine()
	{
		yield return new WaitForSeconds(1f);

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
		rb.simulated = true;
		UISingleton.OnPlayerSpawned.Invoke();
		UpdateUIScreenPosition();
		transform.position = _level.spawnPoint.position;
		rb.velocity = Vector2.zero;
		ClearBullets();

		gun.bulletsLeft = _level.info.bulletCount;
		UISingleton.Bullets = gun.bulletsLeft;
	}
}
