using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-50)]
public class Level : MonoBehaviour, IResettable
{
	public TaskGroup[] taskGroups;

	public MonoBehaviour[] resetArray;
	private List<IResettable> _resetInterfaces;
	public UnityEvent onLevelComplete;
	public Transform spawnPoint;
	public Player playerPrefab;
	public int bullets;

	private Player _player;

	private int _taskGroupsLeft;

	private void Awake()
	{
		//Kinda cursed???
		_resetInterfaces = resetArray.OfType<IResettable>().ToList();

		foreach (TaskGroup group in taskGroups)
		{
			group.SetGroupManager(this);
		}

		ResetTaskGroups();
	}

	public void StartGameplay()
	{
		_player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
		_player.Initialize(this);
		RefillGun();
		_resetInterfaces.Add(_player);
	}

	private void RefillGun()
	{
		_player.gun.bulletsLeft = bullets;
	}

	public float? GetBulletsLeft()
	{
		if (_player)
		{
			return _player.gun.bulletsLeft;
		}
		else
		{
			return null;
		}
	}

	public void Reset()
	{
		RefillGun();
		ResetTaskGroups();
		foreach (IResettable resettable in _resetInterfaces) resettable.Reset();
	}

	private void ResetTaskGroups()
	{
		_taskGroupsLeft = taskGroups.Length;

	}

	public void TaskGroupCompleted(TaskGroup group)
	{
		_taskGroupsLeft--;

		CheckCompletion();
	}

	private void CheckCompletion()
	{
		if (_taskGroupsLeft == 0) LevelCompleted();
	}

	private void LevelCompleted()
	{
		onLevelComplete.Invoke();
		_player.Destroy();
	}
}
