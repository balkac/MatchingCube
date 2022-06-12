using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : State
{
	[SerializeField] private MainMenuVM _mainMenuVm;
	
	public MainMenuState(GameStateController gameStateController) : base(gameStateController)
	{
	}
	 
	public override void OnEnter()
	{	
		base.OnEnter();
		_mainMenuVm.TryActivate();
		Debug.Log("main menu state");
	}

	public override void OnExit()
	{
		base.OnExit();
		_mainMenuVm.TryDeactivate();
	}
}