using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
	private State _currentState;
	private LevelManager _levelManager;
	private List<State> _states;
	public static Action OnLevelStarted;
	public Action OnLevelWon;
	public Action OnLevelLosed;

	private void Awake()
	{
		_levelManager = LevelManager.Instance;
		_states = GetComponentsInChildren<State>().ToList();
		MainMenuVM.OnPlayButtonClicked += OnPlayButtonClicked;
		ObstacleHandler.OnCharacterFailed += OnCharacterFailed;
		Finishline.OnFinishLineTriggered += OnFinishLineTriggered;
	}

	private void Start()
	{
		SetState(EState.MainMenu);
	}

	private void OnDestroy()
	{
		MainMenuVM.OnPlayButtonClicked -= OnPlayButtonClicked;
		ObstacleHandler.OnCharacterFailed -= OnCharacterFailed;
		Finishline.OnFinishLineTriggered -= OnFinishLineTriggered;
	}

	private void OnFinishLineTriggered()
	{
		SetState(EState.Win);
		
		OnLevelWon?.Invoke();
		
		WinGameVM.OnNextLevelButtonClicked += OnNextLevelButtonClicked;

	}

	private void OnNextLevelButtonClicked()
	{
		WinGameVM.OnNextLevelButtonClicked -= OnNextLevelButtonClicked;
		_levelManager.LoadScene(UserSaveManager.Instance.GetCurLevelID());
	}

	private void OnCharacterFailed()
	{
		ObstacleHandler.OnCharacterFailed -= OnCharacterFailed;
		SetState(EState.Lose);
		
		LoseGameVM.OnReplayButtonClicked += OnReplayButtonClicked;
		OnLevelLosed?.Invoke();
	}

	private void OnReplayButtonClicked()
	{
		LoseGameVM.OnReplayButtonClicked -= OnReplayButtonClicked;
		_levelManager.LoadScene(UserSaveManager.Instance.GetCurLevelID());
	}

	private void OnPlayButtonClicked()
	{
		SetState(EState.Run);
		OnLevelStarted?.Invoke();
	}

	private void SetState(EState stateType)
	{
		if (_currentState != null)
		{
			if (_currentState.StateType == stateType)
			{
				return;
			}
		}
		
		foreach (var state in _states)
		{
			if (state.StateType == stateType)
			{
				if (_currentState != null)
				{
					_currentState.OnExit();
				}

				_currentState = state;
				state.OnEnter();
			}
		}
	}
}