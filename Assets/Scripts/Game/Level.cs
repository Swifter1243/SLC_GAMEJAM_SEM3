using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-100)]
public class Level : MonoBehaviour, IResettable
{
	public TaskGroup[] taskGroups;

	public MonoBehaviour[] resetArray;
	private List<IResettable> _resetInterfaces;
	public UnityEvent onLevelComplete;
	public UnityEvent onGameplayStarted;
	public Transform spawnPoint;
	public Player playerPrefab;
	public Door door;

	public LevelInfo info;


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
		UISingleton.maxBullets = info.bulletCount;
		_player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
		_player.Initialize(this);
		door.Initialize();
		_resetInterfaces.Add(_player);
		onGameplayStarted.Invoke();
	}

	public void Reset()
	{
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
		UISingleton.OnPlayerDeath.Invoke();
		UISingleton.OnLevelFinished.Invoke();
		_player.Destroy();
	}
}
