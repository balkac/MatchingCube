using UnityEngine;

public class RunState : State
{
	[SerializeField] private InGameVM _inGameVM;
	public RunState(GameStateController gameStateController) : base(gameStateController)
	{
	}

	public override void OnEnter()
	{
		base.OnEnter();
		_inGameVM.TryActivate();
		Debug.Log("OnRunState");
	}
	
	public override void OnExit()
	{
		base.OnExit();
		_inGameVM.TryDeactivate();
	}
}