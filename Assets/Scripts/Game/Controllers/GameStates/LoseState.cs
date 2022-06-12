using UnityEngine;

public class LoseState : State
{
	[SerializeField] private LoseGameVM _loseGameVm;
	
	public LoseState(GameStateController gameStateController) : base(gameStateController)
	{
	}

	public override void OnEnter()
	{
		base.OnEnter();
		_loseGameVm.TryActivate();
		Debug.Log("LoseState");
	}
	
	public override void OnExit()
	{
		base.OnExit();
		_loseGameVm.TryDeactivate();
	}
}