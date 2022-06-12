using UnityEngine;

public class WinState : State
{
	[SerializeField] private WinGameVM _winGameVm;
	
	public WinState(GameStateController gameStateController) : base(gameStateController)
	{
	}

	public override void OnEnter()
	{
		base.OnEnter();
		UserSaveManager.Instance.SaveCurLevel(UserSaveManager.Instance.GetLastCompletedCount() + 1);
		_winGameVm.TryActivate();
		Debug.Log("WinState");
	}
	
	public override void OnExit()
	{
		base.OnExit();
		_winGameVm.TryDeactivate();
	}
}